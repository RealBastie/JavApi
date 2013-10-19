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

using org.apache.harmony.security;
using org.apache.harmony.security.fortress;

namespace biz.ritter.javapi.security
{

    /**
     * {@code Security} is the central class in the Java Security API. It manages
     * the list of security {@code Provider} that have been installed into this
     * runtime environment.
     */
    public sealed class Security
    {
        private const String lockJ = "";

        // Security properties
        private static java.util.Properties secprops = new java.util.Properties();

        // static initialization
        // - load security properties files
        // - load statically registered providers
        // - if no provider description file found then load default providers
        static Security()
        {
            bool loaded = false;
            java.io.File f = new java.io.File(java.lang.SystemJ.getProperty("java.home") //$NON-NLS-1$
                    + java.io.File.separator + "lib" + java.io.File.separator //$NON-NLS-1$
                    + "security" + java.io.File.separator + "java.security"); //$NON-NLS-1$ //$NON-NLS-2$
            if (f.exists())
            {
                try
                {
                    java.io.FileInputStream fis = new java.io.FileInputStream(f);
                    java.io.InputStreamReader isJ = new java.io.InputStreamReader(fis);
                    secprops.load(isJ);
                    loaded = true;
                    isJ.close();
                }
                catch (java.io.IOException e)
                {
                    //                        System.err.println("Could not load Security properties file: "
                    //                                        + e);
                }
            }

            if (Util.equalsIgnoreCase("true", secprops.getProperty("security.allowCustomPropertiesFile", "true")))
            { //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
                String securityFile = java.lang.SystemJ.getProperty("java.security.properties"); //$NON-NLS-1$
                if (securityFile != null)
                {
                    if (securityFile.startsWith("="))
                    { // overwrite //$NON-NLS-1$
                        secprops = new java.util.Properties();
                        loaded = false;
                        securityFile = securityFile.substring(1);
                    }
                    try
                    {
                        securityFile = PolicyUtils.expand(securityFile, java.lang.SystemJ.getProperties());
                    }
                    catch (PolicyUtils.ExpansionFailedException e)
                    {
                        //                            System.err.println("Could not load custom Security properties file "
                        //                                    + securityFile +": " + e);
                    }
                    f = new java.io.File(securityFile);
                    java.io.InputStreamReader isj;
                    try
                    {
                        if (f.exists())
                        {
                            java.io.FileInputStream fis = new java.io.FileInputStream(f);
                            isj = new java.io.InputStreamReader(fis);
                        }
                        else
                        {
                            java.net.URL url = new java.net.URL(securityFile);
                            isj = new java.io.InputStreamReader(url.openStream());
                        }
                        secprops.load(isj);
                        loaded = true;
                        isj.close();
                    }
                    catch (java.io.IOException e)
                    {
                        //                           System.err.println("Could not load custom Security properties file "
                        //                                   + securityFile +": " + e);
                    }
                }
            }
            if (!loaded)
            {
                registerDefaultProviders();
            }
            Engine.door = new SecurityDoor();
        }

        /**
         * This class can't be instantiated.
         */
        private Security()
        {
        }

        // Register default providers
        private static void registerDefaultProviders()
        {
            secprops.put("security.provider.1", "org.apache.harmony.security.provider.cert.DRLCertFactory");  //$NON-NLS-1$ //$NON-NLS-2$
            secprops.put("security.provider.2", "org.apache.harmony.security.provider.crypto.CryptoProvider");  //$NON-NLS-1$ //$NON-NLS-2$
            secprops.put("security.provider.3", "org.apache.harmony.xnet.provider.jsse.JSSEProvider");  //$NON-NLS-1$ //$NON-NLS-2$
            secprops.put("security.provider.4", "org.bouncycastle.jce.provider.BouncyCastleProvider");  //$NON-NLS-1$ //$NON-NLS-2$
        }

        /**
         * Returns value for the specified algorithm with the specified name.
         *
         * @param algName
         *            the name of the algorithm.
         * @param propName
         *            the name of the property.
         * @return value of the property.
         * @deprecated Use {@link AlgorithmParameters} and {@link KeyFactory}
         *             instead.
         */
        [Obsolete]
        public static String getAlgorithmProperty(String algName, String propName)
        {
            if (algName == null || propName == null)
            {
                return null;
            }
            String prop = propName + "." + algName; //$NON-NLS-1$
            Provider[] providers = getProviders();
            for (int i = 0; i < providers.Length; i++)
            {
                for (java.util.Enumeration<Object> e = providers[i].propertyNames(); e
                        .hasMoreElements(); )
                {
                    String pname = (String)e.nextElement();
                    if (Util.equalsIgnoreCase(prop, pname))
                    {
                        return providers[i].getProperty(pname);
                    }
                }
            }
            return null;
        }

        /**
         * Insert the given {@code Provider} at the specified {@code position}. The
         * positions define the preference order in which providers are searched for
         * requested algorithms.
         * <p/>
         * If a {@code SecurityManager} is installed, code calling this method needs
         * the {@code SecurityPermission} {@code insertProvider.NAME} (where NAME is
         * the provider name) to be granted, otherwise a {@code SecurityException}
         * will be thrown.
         *
         * @param provider
         *            the provider to insert.
         * @param position
         *            the position (starting from 1).
         * @return the actual position or {@code -1} if the given {@code provider}
         *         was already in the list. The actual position may be different
         *         from the desired position.
         * @throws SecurityException
         *             if a {@code SecurityManager} is installed and the caller does
         *             not have permission to invoke this method.
         */
        public static int insertProviderAt(Provider provider,
                int position)
        {
            lock (lockJ)
            {
                // check security access; check that provider is not already
                // installed, else return -1; if (position <1) or (position > max
                // position) position = max position + 1; insert provider, shift up
                // one position for next providers; Note: The position is 1-based
                java.lang.SecurityManager sm = java.lang.SystemJ.getSecurityManager();
                if (sm != null)
                {
                    sm.checkSecurityAccess("insertProvider." + provider.getName()); //$NON-NLS-1$
                }
                if (getProvider(provider.getName()) != null)
                {
                    return -1;
                }
                int result = Services.insertProviderAt(provider, position);
                renumProviders();
                return result;
            }
        }

        /**
         * Adds the given {@code provider} to the collection of providers at the
         * next available position.
         * <p/>
         * If a {@code SecurityManager} is installed, code calling this method needs
         * the {@code SecurityPermission} {@code insertProvider.NAME} (where NAME is
         * the provider name) to be granted, otherwise a {@code SecurityException}
         * will be thrown.
         *
         * @param provider
         *            the provider to be added.
         * @return the actual position or {@code -1} if the given {@code provider}
         *         was already in the list.
         * @throws SecurityException
         *             if a {@code SecurityManager} is installed and the caller does
         *             not have permission to invoke this method.
         */
        public static int addProvider(Provider provider)
        {
            return insertProviderAt(provider, 0);
        }

        /**
         * Removes the {@code Provider} with the specified name form the collection
         * of providers. If the the {@code Provider} with the specified name is
         * removed, all provider at a greater position are shifted down one
         * position.
         * <p/>
         * Returns silently if {@code name} is {@code null} or no provider with the
         * specified name is installed.
         * <p/>
         * If a {@code SecurityManager} is installed, code calling this method needs
         * the {@code SecurityPermission} {@code removeProvider.NAME} (where NAME is
         * the provider name) to be granted, otherwise a {@code SecurityException}
         * will be thrown.
         *
         * @param name
         *            the name of the provider to remove.
         * @throws SecurityException
         *             if a {@code SecurityManager} is installed and the caller does
         *             not have permission to invoke this method.
         */
        public static void removeProvider(String name)
        {
            lock (lockJ)
            {
                // It is not clear from spec.:
                // 1. if name is null, should we checkSecurityAccess or not? 
                //    throw SecurityException or not?
                // 2. as 1 but provider is not installed
                // 3. behavior if name is empty string?

                Provider p;
                if ((name == null) || (name.length() == 0))
                {
                    return;
                }
                p = getProvider(name);
                if (p == null)
                {
                    return;
                }
                java.lang.SecurityManager sm = java.lang.SystemJ.getSecurityManager();
                if (sm != null)
                {
                    sm.checkSecurityAccess("removeProvider." + name); //$NON-NLS-1$
                }
                Services.removeProvider(p.getProviderNumber());
                renumProviders();
                p.setProviderNumber(-1);
            }
        }

        /**
         * Returns an array containing all installed providers. The providers are
         * ordered according their preference order.
         *
         * @return an array containing all installed providers.
         */
        public static Provider[] getProviders()
        {
            lock (lockJ)
            {
                return Services.getProviders();
            }
        }

        /**
         * Returns the {@code Provider} with the specified name. Returns {@code
         * null} if name is {@code null} or no provider with the specified name is
         * installed.
         *
         * @param name
         *            the name of the requested provider.
         * @return the provider with the specified name, maybe {@code null}.
         */
        public static Provider getProvider(String name)
        {
            lock (lockJ)
            {
                return Services.getProvider(name);
            }
        }

        /**
         * Returns the array of providers which meet the user supplied string
         * filter. The specified filter must be supplied in one of two formats:
         * <nl>
         * <li> CRYPTO_SERVICE_NAME.ALGORITHM_OR_TYPE</li>
         * <p/>
         * (for example: "MessageDigest.SHA")
         * <li> CRYPTO_SERVICE_NAME.ALGORITHM_OR_TYPE</li>
         * ATTR_NAME:ATTR_VALUE
         * <p/>
         * (for example: "Signature.MD2withRSA KeySize:512")
         * </nl>
         *
         * @param filter
         *            case-insensitive filter.
         * @return the providers which meet the user supplied string filter {@code
         *         filter}. A {@code null} value signifies that none of the
         *         installed providers meets the filter specification.
         * @throws InvalidParameterException
         *             if an unusable filter is supplied.
         * @throws NullPointerException
         *             if {@code filter} is {@code null}.
         */
        public static Provider[] getProviders(String filter)
        {
            if (filter == null)
            {
                throw new java.lang.NullPointerException("The filter is null"); //$NON-NLS-1$
            }
            if (filter.length() == 0)
            {
                throw new InvalidParameterException("The filter is not in the required format"); //$NON-NLS-1$
            }
            java.util.HashMap<String, String> hm = new java.util.HashMap<String, String>();
            int i = filter.indexOf(':');
            if ((i == filter.length() - 1) || (i == 0))
            {
                throw new InvalidParameterException("The filter is not in the required format"); //$NON-NLS-1$
            }
            if (i < 1)
            {
                hm.put(filter, ""); //$NON-NLS-1$
            }
            else
            {
                hm.put(filter.substring(0, i), filter.substring(i + 1));
            }
            return getProviders(hm);
        }

        /**
         * Returns the array of providers which meet the user supplied set of
         * filters. The filter must be supplied in one of two formats:
         * <nl>
         * <li> CRYPTO_SERVICE_NAME.ALGORITHM_OR_TYPE</li>
         * <p/>
         * for example: "MessageDigest.SHA" The value associated with the key must
         * be an empty string. <li> CRYPTO_SERVICE_NAME.ALGORITHM_OR_TYPE</li>
         * ATTR_NAME:ATTR_VALUE
         * <p/>
         * for example: "Signature.MD2withRSA KeySize:512" where "KeySize:512" is
         * the value of the filter map entry.
         * </nl>
         *
         * @param filter
         *            case-insensitive filter.
         * @return the providers which meet the user supplied string filter {@code
         *         filter}. A {@code null} value signifies that none of the
         *         installed providers meets the filter specification.
         * @throws InvalidParameterException
         *             if an unusable filter is supplied.
         * @throws NullPointerException
         *             if {@code filter} is {@code null}.
         */
        public static Provider[] getProviders(java.util.Map<String, String> filter)
        {
            lock (lockJ)
            {
                if (filter == null)
                {
                    throw new java.lang.NullPointerException("The filter is null"); //$NON-NLS-1$
                }
                if (filter.isEmpty())
                {
                    return null;
                }
                java.util.List<Provider> result = Services.getProvidersList();
                java.util.Set<java.util.MapNS.Entry<String, String>> keys = filter.entrySet();
                java.util.MapNS.Entry<String, String> entry;
                for (java.util.Iterator<java.util.MapNS.Entry<String, String>> it = keys.iterator(); it.hasNext(); )
                {
                    entry = it.next();
                    String key = entry.getKey();
                    String val = entry.getValue();
                    String attribute = null;
                    int i = key.indexOf(' ');
                    int j = key.indexOf('.');
                    if (j == -1)
                    {
                        throw new InvalidParameterException("The filter is not in the required format"); //$NON-NLS-1$
                    }
                    if (i == -1)
                    { // <crypto_service>.<algorithm_or_type>
                        if (val.length() != 0)
                        {
                            throw new InvalidParameterException("The filter is not in the required format"); //$NON-NLS-1$
                        }
                    }
                    else
                    { // <crypto_service>.<algorithm_or_type> <attribute_name>
                        if (val.length() == 0)
                        {
                            throw new InvalidParameterException("The filter is not in the required format"); //$NON-NLS-1$
                        }
                        attribute = key.substring(i + 1);
                        if (attribute.trim().length() == 0)
                        {
                            throw new InvalidParameterException("The filter is not in the required format"); //$NON-NLS-1$
                        }
                        key = key.substring(0, i);
                    }
                    String serv = key.substring(0, j);
                    String alg = key.substring(j + 1);
                    if (serv.length() == 0 || alg.length() == 0)
                    {
                        throw new InvalidParameterException("The filter is not in the required format"); //$NON-NLS-1$
                    }
                    Provider p;
                    for (int k = 0; k < result.size(); k++)
                    {
                        try
                        {
                            p = result.get(k);
                        }
                        catch (java.lang.IndexOutOfBoundsException e)
                        {
                            break;
                        }
                        if (!p.implementsAlg(serv, alg, attribute, val))
                        {
                            result.remove(p);
                            k--;
                        }
                    }
                }
                if (result.size() > 0)
                {
                    return result.toArray(new Provider[result.size()]);
                }
                return null;
            }
        }

        /**
         * Returns the value of the security property named by the argument.
         * <p/>
         * If a {@code SecurityManager} is installed, code calling this method needs
         * the {@code SecurityPermission} {@code getProperty.KEY} (where KEY is the
         * specified {@code key}) to be granted, otherwise a {@code
         * SecurityException} will be thrown.
         *
         * @param key
         *            the name of the requested security property.
         * @return the value of the security property.
         * @throws SecurityException
         *             if a {@code SecurityManager} is installed and the caller does
         *             not have permission to invoke this method.
         */
        public static String getProperty(String key)
        {
            if (key == null)
            {
                throw new java.lang.NullPointerException("The key is null"); //$NON-NLS-1$
            }
            java.lang.SecurityManager sm = java.lang.SystemJ.getSecurityManager();
            if (sm != null)
            {
                sm.checkSecurityAccess("getProperty." + key); //$NON-NLS-1$
            }
            String property = secprops.getProperty(key);
            if (property != null)
            {
                property = property.trim();
            }
            return property;
        }

        /**
         * Sets the value of the specified security property.
         * <p/>
         * If a {@code SecurityManager} is installed, code calling this method needs
         * the {@code SecurityPermission} {@code setProperty.KEY} (where KEY is the
         * specified {@code key}) to be granted, otherwise a {@code
         * SecurityException} will be thrown.
         *
         * @param key
         *            the name of the security property.
         * @param datnum
         *            the value of the security property.
         * @throws SecurityException
         *             if a {@code SecurityManager} is installed and the caller does
         *             not have permission to invoke this method.
         */
        public static void setProperty(String key, String datnum)
        {
            java.lang.SecurityManager sm = java.lang.SystemJ.getSecurityManager();
            if (sm != null)
            {
                sm.checkSecurityAccess("setProperty." + key); //$NON-NLS-1$
            }
            secprops.put(key, datnum);
        }

        /**
         * Returns a {@code Set} of all registered algorithms for the specified
         * cryptographic service. {@code "Signature"}, {@code "Cipher"} and {@code
         * "KeyStore"} are examples for such kind of services.
         *
         * @param serviceName
         *            the case-insensitive name of the service.
         * @return a {@code Set} of all registered algorithms for the specified
         *         cryptographic service, or an empty {@code Set} if {@code
         *         serviceName} is {@code null} or if no registered provider
         *         provides the requested service.
         */
        public static java.util.Set<String> getAlgorithms(String serviceName)
        {
            java.util.HashSet<String> result = new java.util.HashSet<String>();
            Provider[] p = getProviders();
            for (int i = 0; i < p.Length; i++)
            {
                for (java.util.Iterator<Provider.Service> it = p[i].getServices().iterator(); it.hasNext(); )
                {
                    Provider.Service s = (Provider.Service)it.next();
                    if (Util.equalsIgnoreCase(s.getType(), serviceName))
                    {
                        result.add(s.getAlgorithm());
                    }
                }
            }
            return result;
        }

        /**
         * 
         * Update sequence numbers of all providers.
         *  
         */
        private static void renumProviders()
        {
            Provider[] p = Services.getProviders();
            for (int i = 0; i < p.Length; i++)
            {
                p[i].setProviderNumber(i + 1);
            }
        }

        private class SecurityDoor : SecurityAccess
        {
            // Access to Security.renumProviders()
            public void renumProviders()
            {
                Security.renumProviders();
            }

            //  Access to Security.getAliases()
            public java.util.Iterator<String> getAliases(Provider.Service s)
            {
                return s.getAliases();
            }

            // Access to Provider.getService()
            public Provider.Service getService(Provider p, String type)
            {
                return p.getService(type);
            }
        }
    }
}