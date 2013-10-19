/*
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at 
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */
using System;
using java = biz.ritter.javapi;

using org.apache.harmony.luni;

namespace biz.ritter.javapi.security
{
    public class Provider : java.util.Properties
    {
        private String name;
        //The provider preference order number. 
        // Equals -1 for non registered provider.
        [NonSerialized]
        private int providerNumber = -1;

        // Contains "Service.Algorithm" and Provider.Service classes added using
        // putService()
        [NonSerialized]
        private TwoKeyHashMap<String, String, Service> serviceTable;

        // Contains "Service.Alias" and Provider.Service classes added using
        // putService()
        [NonSerialized]
        private TwoKeyHashMap<String, String, Service> aliasTable;

        // Contains "Service.Algorithm" and Provider.Service classes added using
        // put()
        [NonSerialized]
        private TwoKeyHashMap<String, String, Service> propertyServiceTable;

        // Contains "Service.Alias" and Provider.Service classes added using put()
        [NonSerialized]
        private TwoKeyHashMap<String, String, Service> propertyAliasTable;

        // The properties changed via put()
        [NonSerialized]
        private java.util.Properties changedProperties;

        // For getService(String type, String algorithm) optimization:
        // previous result
        [NonSerialized]
        private Provider.Service returnedService;
        // previous parameters
        [NonSerialized]
        private String lastAlgorithm;
        // last name
        [NonSerialized]
        private String lastServiceName;

        // For getServices() optimization:
        [NonSerialized]
        private java.util.Set<Service> lastServicesSet;

        // For getService(String type) optimization:
        [NonSerialized]
        private String lastType;
        // last Service found by type
        [NonSerialized]
        private Provider.Service lastServicesByType;


        /**
         * Returns the service with the specified {@code type} implementing the
         * specified {@code algorithm}, or {@code null} if no such implementation
         * exists.
         * <p/>
         * If two services match the requested type and algorithm, the one added
         * with the {@link #putService(Service)} is returned (as opposed to the one
         * added via {@link #put(Object, Object)}.
         *
         * @param type
         *            the type of the service (for example {@code KeyPairGenerator})
         * @param algorithm
         *            the algorithm name (case insensitive)
         * @return the requested service, or {@code null} if no such implementation
         *         exists
         */
        public Provider.Service getService(String type,
                String algorithm)
        {
            lock (this)
            {
                if (type == null || algorithm == null)
                {
                    throw new java.lang.NullPointerException();
                }

                if (type.equals(lastServiceName)
                        && algorithm.equalsIgnoreCase(lastAlgorithm))
                {
                    return returnedService;
                }

                String alg = algorithm.toUpperCase();
                Object o = null;
                if (serviceTable != null)
                {
                    o = serviceTable.get(type, alg);
                }
                if (o == null && aliasTable != null)
                {
                    o = aliasTable.get(type, alg);
                }
                if (o == null)
                {
                    updatePropertyServiceTable();
                }
                if (o == null && propertyServiceTable != null)
                {
                    o = propertyServiceTable.get(type, alg);
                }
                if (o == null && propertyAliasTable != null)
                {
                    o = propertyAliasTable.get(type, alg);
                }

                if (o != null)
                {
                    lastServiceName = type;
                    lastAlgorithm = algorithm;
                    returnedService = (Provider.Service)o;
                    return returnedService;
                }
                return null;
            }
        }

        /**
         * Returns true if this provider implements the given algorithm. Caller
         * must specify the cryptographic service and specify constraints via the
         * attribute name and value.
         * 
         * @param serv
         *            Crypto service.
         * @param alg
         *            Algorithm or type.
         * @param attribute
         *            The attribute name or {@code null}.
         * @param val
         *            The attribute value.
         * @return
         */
        internal bool implementsAlg(String serv, String alg, String attribute, String val)
        {
            String servAlg = serv + "." + alg; //$NON-NLS-1$
            String prop = getPropertyIgnoreCase(servAlg);
            if (prop == null)
            {
                alg = getPropertyIgnoreCase("Alg.Alias." + servAlg); //$NON-NLS-1$
                if (alg != null)
                {
                    servAlg = serv + "." + alg; //$NON-NLS-1$
                    prop = getPropertyIgnoreCase(servAlg);
                }
            }
            if (prop != null)
            {
                if (attribute == null)
                {
                    return true;
                }
                return checkAttribute(servAlg, attribute, val);
            }
            return false;
        }
        // Returns true if this provider has the same value as is given for the
        // given attribute
        private bool checkAttribute(String servAlg, String attribute, String val)
        {

            String attributeValue = getPropertyIgnoreCase(servAlg + ' ' + attribute);
            if (attributeValue != null)
            {
                if (attribute.equalsIgnoreCase("KeySize"))
                { //$NON-NLS-1$
                    if (java.lang.Integer.valueOf(attributeValue).compareTo(
                            java.lang.Integer.valueOf(val)) >= 0)
                    {
                        return true;
                    }
                }
                else
                { // other attributes
                    if (attributeValue.equalsIgnoreCase(val))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        // Searches for the property with the specified key in the provider
        // properties. Key is not case-sensitive.
        // 
        // @param prop
        // @return the property value with the specified key value.
        private String getPropertyIgnoreCase(String key) {
        String res = getProperty(key);
        if (res != null) {
            return res;
        }
        for (java.util.Enumeration<Object> e = propertyNames(); e.hasMoreElements();) {
            String pname = (String) e.nextElement();
            if (key.equalsIgnoreCase(pname)) {
                return getProperty(pname);
            }
        }
        return null;
    }
        /**
         * Get the service of the specified type
         *  
         */
        internal Provider.Service getService(String type)
        {
            lock (this)
            {
                updatePropertyServiceTable();
                if (lastServicesByType != null && type.equals(lastType))
                {
                    return lastServicesByType;
                }
                Provider.Service service;
                for (java.util.Iterator<Service> it = getServices().iterator(); it.hasNext(); )
                {
                    service = it.next();
                    if (type.equals(service.type))
                    {
                        lastType = type;
                        lastServicesByType = service;
                        return service;
                    }
                }
                return null;
            }
        }

        /**
         * Returns an unmodifiable {@code Set} of all services registered by this
         * provider.
         *
         * @return an unmodifiable {@code Set} of all services registered by this
         *         provider
         */
        public java.util.Set<Provider.Service> getServices()
        {
            lock (this)
            {
                updatePropertyServiceTable();
                if (lastServicesSet != null)
                {
                    return lastServicesSet;
                }
                if (serviceTable != null)
                {
                    lastServicesSet = new java.util.HashSet<Service>(serviceTable.values());
                }
                else
                {
                    lastServicesSet = new java.util.HashSet<Service>();
                }
                if (propertyServiceTable != null)
                {
                    lastServicesSet.addAll(propertyServiceTable.values());
                }
                lastServicesSet = java.util.Collections<Object>.unmodifiableSet(lastServicesSet);
                return lastServicesSet;
            }
        }

        /**
         * 
         * Set the provider preference order number.
         * 
         * @param n
         */
        internal void setProviderNumber(int n)
        {
            providerNumber = n;
        }

        /**
         * 
         * Get the provider preference order number.
         * 
         * @return
         */
        internal int getProviderNumber()
        {
            return providerNumber;
        }

        
        // Update provider Services if the properties was changed
        private void updatePropertyServiceTable()
        {
            Object _key;
            Object _value;
            Provider.Service s;
            String serviceName;
            String algorithm;
            if (changedProperties == null || changedProperties.isEmpty())
            {
                return;
            }
            java.util.Iterator<java.util.MapNS.Entry<String, String>> it = changedProperties.entrySet().iterator();
            for (; it.hasNext(); )
            {
                java.util.MapNS.Entry<String, String> entry = it.next();
                _key = entry.getKey();
                _value = entry.getValue();
                if (_key == null || _value == null || !(_key is String)
                        || !(_value is String))
                {
                    continue;
                }
                String key = (String)_key;
                String value = (String)_value;
                if (key.startsWith("Provider"))
                { // Provider service type is reserved //$NON-NLS-1$
                    continue;
                }
                int i;
                if (key.startsWith("Alg.Alias."))
                { // Alg.Alias.<crypto_service>.<aliasName>=<stanbdardName> //$NON-NLS-1$
                    String aliasName;
                    String service_alias = key.substring(10);
                    i = service_alias.indexOf('.');
                    serviceName = service_alias.substring(0, i);
                    aliasName = service_alias.substring(i + 1);
                    algorithm = value;
                    String algUp = algorithm.toUpperCase();
                    Object o = null;
                    if (propertyServiceTable == null)
                    {
                        propertyServiceTable = new TwoKeyHashMap<String, String, Service>(128);
                    }
                    else
                    {
                        o = propertyServiceTable.get(serviceName, algUp);
                    }
                    if (o != null)
                    {
                        s = (Provider.Service)o;
                        s.aliases.add(aliasName);
                        if (propertyAliasTable == null)
                        {
                            propertyAliasTable = new TwoKeyHashMap<String, String, Service>(256);
                        }
                        propertyAliasTable.put(serviceName,
                                aliasName.toUpperCase(), s);
                    }
                    else
                    {
                        String className = (String)changedProperties
                                .get(serviceName + "." + algorithm); //$NON-NLS-1$
                        if (className != null)
                        {
                            java.util.ArrayList<String> l = new java.util.ArrayList<String>();
                            l.add(aliasName);
                            s = new Provider.Service(this, serviceName, algorithm,
                                    className, l, new java.util.HashMap<String, String>());
                            propertyServiceTable.put(serviceName, algUp, s);
                            if (propertyAliasTable == null)
                            {
                                propertyAliasTable = new TwoKeyHashMap<String, String, Service>(256);
                            }
                            propertyAliasTable.put(serviceName, aliasName
                                    .toUpperCase(), s);
                        }
                    }
                    continue;
                }
                int j = key.indexOf('.');
                if (j == -1)
                { // unknown format
                    continue;
                }
                i = key.indexOf(' ');
                if (i == -1)
                { // <crypto_service>.<algorithm_or_type>=<className>
                    serviceName = key.substring(0, j);
                    algorithm = key.substring(j + 1);
                    String alg = algorithm.toUpperCase();
                    Object o = null;
                    if (propertyServiceTable != null)
                    {
                        o = propertyServiceTable.get(serviceName, alg);
                    }
                    if (o != null)
                    {
                        s = (Provider.Service)o;
                        s.className = value;
                    }
                    else
                    {
                        s = new Provider.Service(this, serviceName, algorithm,
                                value, new java.util.ArrayList<String>(), new java.util.HashMap<String, String>());
                        if (propertyServiceTable == null)
                        {
                            propertyServiceTable = new TwoKeyHashMap<String, String, Service>(128);
                        }
                        propertyServiceTable.put(serviceName, alg, s);

                    }
                }
                else
                { // <crypto_service>.<algorithm_or_type>
                    // <attribute_name>=<attrValue>
                    serviceName = key.substring(0, j);
                    algorithm = key.substring(j + 1, i);
                    String attribute = key.substring(i + 1);
                    String alg = algorithm.toUpperCase();
                    Object o = null;
                    if (propertyServiceTable != null)
                    {
                        o = propertyServiceTable.get(serviceName, alg);
                    }
                    if (o != null)
                    {
                        s = (Provider.Service)o;
                        s.attributes.put(attribute, value);
                    }
                    else
                    {
                        String className = (String)changedProperties
                                .get(serviceName + "." + algorithm); //$NON-NLS-1$
                        if (className != null)
                        {
                            java.util.HashMap<String, String> m = new java.util.HashMap<String, String>();
                            m.put(attribute, value);
                            s = new Provider.Service(this, serviceName, algorithm,
                                    className, new java.util.ArrayList<String>(), m);
                            if (propertyServiceTable == null)
                            {
                                propertyServiceTable = new TwoKeyHashMap<String, String, Service>(128);
                            }
                            propertyServiceTable.put(serviceName, alg, s);
                        }
                    }
                }
            }
            servicesChanged();
            changedProperties.clear();
        }
        private void servicesChanged()
        {
            lastServicesByType = null;
            lastServiceName = null;
            lastServicesSet = null;
        }

        /**
         * Returns the name of this provider.
         *
         * @return the name of this provider.
         */
        public String getName()
        {
            return name;
        }

        /**
         * {@code Service} represents a service in the Java Security infrastructure.
         * Each service describes its type, the algorithm it implements, to which
         * provider it belongs and other properties.
         */
        public class Service
        {
            // The provider
            private Provider provider;

            // The type of this service
            internal String type;

            // The algorithm name
            private String algorithm;

            // The class implementing this service
            internal String className;

            // The aliases
            internal java.util.List<String> aliases;

            // The attributes
            internal java.util.Map<String, String> attributes;

            // Service implementation
            private java.lang.Class implementation;

            // For newInstance() optimization
            private String lastClassName;

            /**
             * Constructs a new instance of {@code Service} with the given
             * attributes.
             *
             * @param provider
             *            the provider to which this service belongs.
             * @param type
             *            the type of this service (for example {@code
             *            KeyPairGenerator}).
             * @param algorithm
             *            the algorithm this service implements.
             * @param className
             *            the name of the class implementing this service.
             * @param aliases
             *            {@code List} of aliases for the algorithm name, or {@code
             *            null} if the implemented algorithm has no aliases.
             * @param attributes
             *            {@code Map} of additional attributes, or {@code null} if
             *            this {@code Service} has no attributed.
             * @throws NullPointerException
             *             if {@code provider, type, algorithm} or {@code className}
             *             is {@code null}.
             */
            public Service(Provider provider, String type, String algorithm,
                    String className, java.util.List<String> aliases, java.util.Map<String, String> attributes)
            {
                if (provider == null || type == null || algorithm == null
                        || className == null)
                {
                    throw new java.lang.NullPointerException();
                }
                this.provider = provider;
                this.type = type;
                this.algorithm = algorithm;
                this.className = className;
                this.aliases = aliases;
                this.attributes = attributes;
            }

            /**
             * Returns the type of this {@code Service}. For example {@code
             * KeyPairGenerator}.
             *
             * @return the type of this {@code Service}.
             */
            public String getType()
            {
                return type;
            }

            /**
             * Returns the name of the algorithm implemented by this {@code
             * Service}.
             *
             * @return the name of the algorithm implemented by this {@code
             *         Service}.
             */
            public String getAlgorithm()
            {
                return algorithm;
            }

            /**
             * Returns the {@code Provider} this {@code Service} belongs to.
             *
             * @return the {@code Provider} this {@code Service} belongs to.
             */
            public Provider getProvider()
            {
                return provider;
            }

            /**
             * Returns the name of the class implementing this {@code Service}.
             *
             * @return the name of the class implementing this {@code Service}.
             */
            public String getClassName()
            {
                return className;
            }

            /**
             * Returns the value of the attribute with the specified {@code name}.
             *
             * @param name
             *            the name of the attribute.
             * @return the value of the attribute, or {@code null} if no attribute
             *         with the given name is set.
             * @throws NullPointerException
             *             if {@code name} is {@code null}.
             */
            public String getAttribute(String name)
            {
                if (name == null)
                {
                    throw new java.lang.NullPointerException();
                }
                if (attributes == null)
                {
                    return null;
                }
                return attributes.get(name);
            }

            internal java.util.Iterator<String> getAliases()
            {
                if (aliases == null)
                {
                    aliases = new java.util.ArrayList<String>(0);
                }
                return aliases.iterator();
            }

            /**
             * Creates and returns a new instance of the implementation described by
             * this {@code Service}.
             *
             * @param constructorParameter
             *            the parameter that is used by the constructor, or {@code
             *            null} if the implementation does not declare a constructor
             *            parameter.
             * @return a new instance of the implementation described by this
             *         {@code Service}.
             * @throws NoSuchAlgorithmException
             *             if the instance could not be constructed.
             * @throws InvalidParameterException
             *             if the implementation does not support the specified
             *             {@code constructorParameter}.
             */
            public Object newInstance(Object constructorParameter)
            {//throws NoSuchAlgorithmException {
                if (implementation == null || !className.equals(lastClassName))
                {
                    java.lang.ClassLoader cl = provider.getClass()
                            .getClassLoader();
                    if (cl == null)
                    {
                        cl = java.lang.ClassLoader.getSystemClassLoader();
                    }
                    try
                    {
                        implementation = java.lang.Class.forName(className,
                                true, cl);
                    }
                    catch (Exception e)
                    {
                        return new NoSuchAlgorithmException(
                                type + " " + algorithm + " implementation not found: " + e);
                    }
                    lastClassName = className;
                }

                java.lang.Class[] parameterTypes = new java.lang.Class[1];

                if (constructorParameter != null
                        && !supportsParameter(constructorParameter))
                {
                    throw new InvalidParameterException(type + ": service cannot use the parameter"); //$NON-NLS-1$
                }
                Object[] initargs = { constructorParameter };

                try
                {
                    if (type.equalsIgnoreCase("CertStore"))
                    { //$NON-NLS-1$
                        parameterTypes[0] = java.lang.Class
                                .forName("java.security.cert.CertStoreParameters"); //$NON-NLS-1$
                    }
                    else if (type.equalsIgnoreCase("Configuration"))
                    {
                        parameterTypes[0] = java.lang.Class
                                .forName("javax.security.auth.login.Configuration$Parameters");
                    }

                    if (parameterTypes[0] == null)
                    {
                        if (constructorParameter == null)
                        {
                            return implementation.newInstance();
                        }
                        else
                        {
                            parameterTypes[0] = constructorParameter.getClass();
                        }
                    }
                    return implementation.getConstructor(parameterTypes)
                            .newInstance(initargs);
                }
                catch (java.lang.Exception e)
                {
                    throw new NoSuchAlgorithmException(type + " " + algorithm + " implementation not found: ", e);
                }
            }

            /**
             * Indicates whether this {@code Service} supports the specified
             * constructor parameter.
             *
             * @param parameter
             *            the parameter to test.
             * @return {@code true} if this {@code Service} supports the specified
             *         constructor parameter, {@code false} otherwise.
             */
            public bool supportsParameter(Object parameter)
            {
                return true;
            }

            /**
             * Returns a string containing a concise, human-readable description of
             * this {@code Service}.
             *
             * @return a printable representation for this {@code Service}.
             */
            public override String ToString()
            {
                String result = "Provider " + provider.getName() + " Service " //$NON-NLS-1$ //$NON-NLS-2$
                        + type + "." + algorithm + " " + className; //$NON-NLS-1$ //$NON-NLS-2$
                if (aliases != null)
                {
                    result = result + "\nAliases " + aliases.toString(); //$NON-NLS-1$
                }
                if (attributes != null)
                {
                    result = result + "\nAttributes " + attributes.toString(); //$NON-NLS-1$
                }
                return result;
            }
        }
    }
}