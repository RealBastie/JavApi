using System;
using java = biz.ritter.javapi;

using org.apache.harmony.security;

namespace org.apache.harmony.security.fortress
{


    /**
     * 
     * This class implements common functionality for all engine classes
     * @author Boris V. Kuznetsov
     */
    public class Engine
    {

        // Service name
        private String serviceName;

        // for getInstance(String algorithm, Object param) optimization:
        // previous result
        private java.security.Provider.Service returnedService;

        // previous parameter
        private String lastAlgorithm;

        private int refreshNumber;

        /**
         * Provider
         */
        public java.security.Provider provider;

        /**
         * SPI instance
         */
        public Object spi;

        /**
         * Access to package visible api in java.security
         */
        public static SecurityAccess door;

        /**
         * Creates a Engine object
         * 
         * @param service
         */
        public Engine(String service)
        {
            this.serviceName = service;
        }

        /**
         * 
         * Finds the appropriate service implementation and creates instance of the
         * class that implements corresponding Service Provider Interface.
         * 
         * @param algorithm
         * @param service
         * @throws NoSuchAlgorithmException
         */
        public void getInstance(String algorithm, Object param)
        {// throws NoSuchAlgorithmException {
            lock (this)
            {
                java.security.Provider.Service serv;

                if (algorithm == null)
                {
                    throw new java.security.NoSuchAlgorithmException("Null algorithm name"); //$NON-NLS-1$
                }
                Services.refresh();
                if (returnedService != null
                        && Util.equalsIgnoreCase(algorithm, lastAlgorithm)
                        && refreshNumber == Services.refreshNumber)
                {
                    serv = returnedService;
                }
                else
                {
                    if (Services.isEmpty())
                    {
                        throw new java.security.NoSuchAlgorithmException(serviceName + " " + algorithm + " implementation not found");
                    }
                    serv = Services.getService(new java.lang.StringBuilder(128)
                            .append(serviceName).append(".").append( //$NON-NLS-1$
                                    Util.toUpperCase(algorithm)).toString());
                    if (serv == null)
                    {
                        throw new java.security.NoSuchAlgorithmException(serviceName + " " + algorithm + " implementation not found");
                    }
                    returnedService = serv;
                    lastAlgorithm = algorithm;
                    refreshNumber = Services.refreshNumber;
                }
                spi = serv.newInstance(param);
                this.provider = serv.getProvider();
            }
        }

        /**
         * 
         * Finds the appropriate service implementation and creates instance of the
         * class that implements corresponding Service Provider Interface.
         * 
         * @param algorithm
         * @param service
         * @param provider
         * @throws NoSuchAlgorithmException
         */
        public void getInstance(String algorithm, java.security.Provider provider,
                Object param)
        {//throws NoSuchAlgorithmException {
            lock (this)
            {
                java.security.Provider.Service serv = null;
                if (algorithm == null)
                {
                    throw new java.security.NoSuchAlgorithmException(serviceName + " , algorithm is null"); //$NON-NLS-1$
                }
                serv = provider.getService(serviceName, algorithm);
                if (serv == null)
                {
                    throw new java.security.NoSuchAlgorithmException(serviceName + " " + algorithm + " implementation not found");
                }
                spi = serv.newInstance(param);
                this.provider = provider;
            }
        }

    }
}