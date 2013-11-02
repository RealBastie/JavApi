/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

// $Id: FactoryFinder.java 670431 2008-06-23 01:40:03Z mrglavas $
using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapix.xml.parsers
{
	/**
 * This class is duplicated for each JAXP subpackage so keep it in
 * sync.  It is package private.
 *
 * This code is designed to implement the JAXP 1.1 spec pluggability
 * feature and is designed to run on JDK version 1.1 and later including
 * JVMs that perform early linking like the Microsoft JVM in IE 5.  Note
 * however that it must be compiled on a JDK version 1.2 or later system
 * since it calls Thread#getContextClassLoader().  The code also runs both
 * as part of an unbundled jar file and when bundled as part of the JDK.
 */
	internal sealed class FactoryFinder
	{
		/**
     * <p>Debug flag to trace loading process.</p>
     */
		private static bool debug = false;
		/**
     * <p>Cache properties for performance.</p>
     */
		private static java.util.Properties cacheProps = new java.util.Properties ();
		/**
     * <p>First time requires initialization overhead.</p>
     */
		private static bool firstTime = true;
		/**
     * Default columns per line.
     */
		private const int DEFAULT_LINE_LENGTH = 80;
		// Define system property "jaxp.debug" to get output
		static FactoryFinder ()
		{
			// Use try/catch block to support applets, which throws
			// SecurityException out of this code.
			try {
				String val = SecuritySupport.getSystemProperty ("jaxp.debug");
				// Allow simply setting the prop to turn on debug
				debug = val != null && (! "false".equals (val));
			} catch (java.lang.SecurityException se) {
				debug = false;
			}
		}

		private FactoryFinder ()
		{
		}

		private static void dPrint (String msg)
		{
			if (debug) {
				java.lang.SystemJ.err.println ("JAXP: " + msg);
			}
		}

		/**
     * Create an instance of a class using the specified ClassLoader and
     * optionally fall back to the current ClassLoader if not found.
     *
     * @param className Name of the concrete class corresponding to the
     * service provider
     *
     * @param cl ClassLoader to use to load the class, null means to use
     * the bootstrap ClassLoader
     *
     * @param doFallback true if the current ClassLoader should be tried as
     * a fallback if the class is not found using cl
     */
		internal static Object newInstance (String className, java.lang.ClassLoader cl,
		                            bool doFallback)
        //throws ConfigurationError
		{
			// assert(className != null);

			try {
				java.lang.Class providerClass;
				if (cl == null) {
					// If classloader is null Use the bootstrap ClassLoader.  
					// Thus Class.forName(String) will use the current
					// ClassLoader which will be the bootstrap ClassLoader.
					providerClass = java.lang.Class.forName (className);
				} else {
					try {
						providerClass = cl.loadClass (className);
					} catch (java.lang.ClassNotFoundException x) {
						if (doFallback) {
							// Fall back to current classloader
							cl = typeof(FactoryFinder).getClass ().getClassLoader ();
							if (cl != null) {
								providerClass = cl.loadClass (className);
							} else {
								providerClass = java.lang.Class.forName (className);
							}
						} else {
							throw x;
						}
					}
				}
                        
				Object instance = providerClass.newInstance ();
				if (debug)
					dPrint ("created new instance of " + providerClass +
					" using ClassLoader: " + cl);
				return instance;
			} catch (java.lang.ClassNotFoundException x) {
				throw new ConfigurationError (
					"Provider " + className + " not found", x);
			} catch (java.lang.Exception x) {
				throw new ConfigurationError (
					"Provider " + className + " could not be instantiated: " + x,
					x);
			}
		}

		/**
     * Finds the implementation Class object in the specified order.  Main
     * entry point.
     * @return Class object of factory, never null
     *
     * @param factoryId             Name of the factory to find, same as
     *                              a property name
     * @param fallbackClassName     Implementation class name, if nothing else
     *                              is found.  Use null to mean no fallback.
     *
     * Package private so this code can be shared.
     */
		internal static Object find (String factoryId, String fallbackClassName)
       // throws ConfigurationError
		{        

			// Figure out which ClassLoader to use for loading the provider
			// class.  If there is a Context ClassLoader then use it.
        
			java.lang.ClassLoader classLoader = SecuritySupport.getContextClassLoader ();
        
			if (classLoader == null) {
				// if we have no Context ClassLoader
				// so use the current ClassLoader
				classLoader = typeof(FactoryFinder).getClass ().getClassLoader ();
			}

			if (debug)
				dPrint ("find factoryId =" + factoryId);
        
			// Use the system property first
			try {
				String systemProp = SecuritySupport.getSystemProperty (factoryId);
				if (systemProp != null && systemProp.length () > 0) {
					if (debug)
						dPrint ("found system property, value=" + systemProp);
					return newInstance (systemProp, classLoader, true);
				}
			} catch (java.lang.SecurityException se) {
				//if first option fails due to any reason we should try next option in the
				//look up algorithm.
			}

			// try to read from $java.home/lib/jaxp.properties
			try {
				String javah = SecuritySupport.getSystemProperty ("java.home");
				String configFile = javah + java.io.File.separator +
				                            "lib" + java.io.File.separator + "jaxp.properties";
				String factoryClassName = null;
				if (firstTime) {
					lock (cacheProps) {
						if (firstTime) {
							java.io.File f = new java.io.File (configFile);
							firstTime = false;
							if (SecuritySupport.doesFileExist (f)) {
								if (debug)
									dPrint ("Read properties file " + f);
								//cacheProps.load( new FileInputStream(f));
								cacheProps.load (SecuritySupport.getFileInputStream (f));
							}
						}
					}
				}
				factoryClassName = cacheProps.getProperty (factoryId);            

				if (factoryClassName != null) {
					if (debug)
						dPrint ("found in $java.home/jaxp.properties, value=" + factoryClassName);
					return newInstance (factoryClassName, classLoader, true);
				}
			} catch (java.lang.Exception ex) {
				if (debug)
					ex.printStackTrace ();
			}

			// Try Jar Service Provider Mechanism
			Object provider = findJarServiceProvider (factoryId);
			if (provider != null) {
				return provider;
			}
			if (fallbackClassName == null) {
				throw new ConfigurationError (
					"Provider for " + factoryId + " cannot be found", null);
			}

			if (debug)
				dPrint ("loaded from fallback value: " + fallbackClassName);
			return newInstance (fallbackClassName, classLoader, true);
		}
		/*
     * Try to find provider using Jar Service Provider Mechanism
     *
     * @return instance of provider class if found or null
     */
		private static Object findJarServiceProvider (String factoryId)
        //throws ConfigurationError
		{

			String serviceId = "META-INF/services/" + factoryId;
			java.io.InputStream isJ = null;

			// First try the Context ClassLoader
			java.lang.ClassLoader cl = SecuritySupport.getContextClassLoader ();
			if (cl != null) {
				isJ = SecuritySupport.getResourceAsStream (cl, serviceId);

				// If no provider found then try the current ClassLoader
				if (isJ == null) {
					cl = typeof(FactoryFinder).getClass ().getClassLoader ();
					isJ = SecuritySupport.getResourceAsStream (cl, serviceId);
				}
			} else {
				// No Context ClassLoader, try the current
				// ClassLoader
				cl = typeof(FactoryFinder).getClass ().getClassLoader ();
				isJ = SecuritySupport.getResourceAsStream (cl, serviceId);
			}

			if (isJ == null) {
				// No provider found
				return null;
			}

			if (debug)
				dPrint ("found jar resource=" + serviceId +
				" using ClassLoader: " + cl);

			// Read the service provider name in UTF-8 as specified in
			// the jar spec.  Unfortunately this fails in Microsoft
			// VJ++, which does not implement the UTF-8
			// encoding. Theoretically, we should simply let it fail in
			// that case, since the JVM is obviously broken if it
			// doesn't support such a basic standard.  But since there
			// are still some users attempting to use VJ++ for
			// development, we have dropped in a fallback which makes a
			// second attempt using the platform's default encoding. In
			// VJ++ this is apparently ASCII, which is a subset of
			// UTF-8... and since the strings we'll be reading here are
			// also primarily limited to the 7-bit ASCII range (at
			// least, in English versions), this should work well
			// enough to keep us on the air until we're ready to
			// officially decommit from VJ++. [Edited comment from
			// jkesselm]
			java.io.BufferedReader rd;
			try {
				rd = new java.io.BufferedReader (new java.io.InputStreamReader (isJ, "UTF-8"), DEFAULT_LINE_LENGTH);
			} catch (java.io.UnsupportedEncodingException e) {
				rd = new java.io.BufferedReader (new java.io.InputStreamReader (isJ), DEFAULT_LINE_LENGTH);
			}
        
			String factoryClassName = null;
			try {
				// XXX Does not handle all possible input as specified by the
				// Jar Service Provider specification
				factoryClassName = rd.readLine ();
			} catch (java.io.IOException x) {
				// No provider found
				return null;
			} finally {
				try { 
					// try to close the reader. 
					rd.close (); 
				} 
            // Ignore the exception. 
            catch (java.io.IOException exc) {
				}
			}

			if (factoryClassName != null &&
			         ! "".equals (factoryClassName)) {
				if (debug)
					dPrint ("found in resource, value="
					+ factoryClassName);

				// Note: here we do not want to fall back to the current
				// ClassLoader because we want to avoid the case where the
				// resource file was found using one ClassLoader and the
				// provider class was instantiated using a different one.
				return newInstance (factoryClassName, cl, false);
			}

			// No provider found
			return null;
		}

		internal class ConfigurationError : java.lang.Error
		{
			private java.lang.Exception exception;

			/**
         * Construct a new instance with the specified detail string and
         * exception.
         */
			internal ConfigurationError (String msg, java.lang.Exception x) :
            base(msg)
			{
				this.exception = x;
			}

			internal java.lang.Exception getException ()
			{
				return exception;
			}
		}
	}
}
