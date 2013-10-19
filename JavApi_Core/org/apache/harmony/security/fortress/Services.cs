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

namespace org.apache.harmony.security.fortress
{

    /**
     * This class contains information about all registered providers and preferred
     * implementations for all "serviceName.algName".
     * 
     */

    public class Services
    {
        //Basties note: Do not loose innformations from private reference types... (like Map.. xyz = new HashMap..)

        // The HashMap that contains information about preferred implementations for
        // all serviceName.algName in the registered providers
        private static readonly java.util.HashMap<String, java.security.Provider.Service> services = new java.util.HashMap<String, java.security.Provider.Service>(512);

        // Need refresh flag
        private static bool needRefresh; // = false;

        /**
         * Refresh number
         */
        internal static int refreshNumber = 1;

        // Registered providers
        private static readonly java.util.ArrayList<java.security.Provider> providers = new java.util.ArrayList<java.security.Provider>(20);

        // Hash for quick provider access by name
        private static readonly java.util.HashMap<String, java.security.Provider> providersNames = new java.util.HashMap<String, java.security.Provider>(20);

        static Services()
        {
            loadProviders();
        }

        // Load statically registered providers and init Services Info
        private static void loadProviders()
        {
            String providerClassName = null;
            int i = 1;
            java.lang.ClassLoader cl = java.lang.ClassLoader.getSystemClassLoader();
            java.security.Provider p;

            while ((providerClassName = java.security.Security.getProperty("security.provider." //$NON-NLS-1$
                    + i++)) != null)
            {
                try
                {
                    p = (java.security.Provider)java.lang.Class
                            .forName(providerClassName.trim(), true, cl)
                            .newInstance();
                    providers.add(p);
                    providersNames.put(p.getName(), p);
                    initServiceInfo(p);
                }
                catch (java.lang.ClassNotFoundException)
                { // ignore Exceptions
                }
                catch (java.lang.IllegalAccessException)
                {
                }
                catch (java.lang.InstantiationException)
                {
                }
            }
            Engine.door.renumProviders();
        }

        /**
         * Returns registered providers
         * 
         * @return
         */
        public static java.security.Provider[] getProviders()
        {
            return providers.toArray(new java.security.Provider[providers.size()]);
        }

        /**
         * Returns registered providers as List
         * 
         * @return
         */
        public static java.util.List<java.security.Provider> getProvidersList()
        {
            return new java.util.ArrayList<java.security.Provider>(providers);
        }

        /**
         * Returns the provider with the specified name
         * 
         * @param name
         * @return
         */
        public static java.security.Provider getProvider(String name)
        {
            if (name == null)
            {
                return null;
            }
            return providersNames.get(name);
        }

        /**
         * Inserts a provider at a specified position
         * 
         * @param provider
         * @param position
         * @return
         */
        public static int insertProviderAt(java.security.Provider provider, int position)
        {
            int size = providers.size();
            if ((position < 1) || (position > size))
            {
                position = size + 1;
            }
            providers.add(position - 1, provider);
            providersNames.put(provider.getName(), provider);
            setNeedRefresh();
            return position;
        }

        /**
         * Removes the provider
         * 
         * @param providerNumber
         */
        public static void removeProvider(int providerNumber)
        {
            java.security.Provider p = providers.remove(providerNumber - 1);
            providersNames.remove(p.getName());
            setNeedRefresh();
        }

        /**
         * 
         * Adds information about provider services into HashMap.
         * 
         * @param p
         */
        public static void initServiceInfo(java.security.Provider p)
        {
            java.security.Provider.Service serv;
            String key;
            String type;
            String alias;
            java.lang.StringBuilder sb = new java.lang.StringBuilder(128);

            for (java.util.Iterator<java.security.Provider.Service> it1 = p.getServices().iterator(); it1.hasNext(); )
            {
                serv = it1.next();
                type = serv.getType();
                sb.delete(0, sb.length());
                key = sb.append(type).append(".").append( //$NON-NLS-1$
                        Util.toUpperCase(serv.getAlgorithm())).toString();
                if (!services.containsKey(key))
                {
                    services.put(key, serv);
                }
                for (java.util.Iterator<String> it2 = Engine.door.getAliases(serv); it2.hasNext(); )
                {
                    alias = it2.next();
                    sb.delete(0, sb.length());
                    key = sb.append(type).append(".").append(Util.toUpperCase(alias)) //$NON-NLS-1$
                            .toString();
                    if (!services.containsKey(key))
                    {
                        services.put(key, serv);
                    }
                }
            }
        }

        /**
         * 
         * Updates services hashtable for all registered providers
         *  
         */
        public static void updateServiceInfo()
        {
            services.clear();
            for (java.util.Iterator<java.security.Provider> it = providers.iterator(); it.hasNext(); )
            {
                initServiceInfo(it.next());
            }
            needRefresh = false;
        }

        /**
         * Returns true if services contain any provider information  
         * @return
         */
        public static bool isEmpty()
        {
            return services.isEmpty();
        }

        /**
         * 
         * Returns service description.
         * Call refresh() before.
         * 
         * @param key
         * @return
         */
        public static java.security.Provider.Service getService(String key)
        {
            return services.get(key);
        }

        /**
         * Prints Services content  
         */
        // FIXME remove debug function
        public static void printServices()
        {
            refresh();
            java.util.Set<String> s = services.keySet();
            for (java.util.Iterator<String> i = s.iterator(); i.hasNext(); )
            {
                String key = i.next();
                java.lang.SystemJ.outJ.println(key + "=" + services.get(key)); //$NON-NLS-1$
            }
        }

        /**
         * Set flag needRefresh 
         *
         */
        public static void setNeedRefresh()
        {
            needRefresh = true;
        }

        /**
         * Refresh services info
         *
         */
        public static void refresh()
        {
            if (needRefresh)
            {
                refreshNumber++;
                updateServiceInfo();
            }
        }
    }
}