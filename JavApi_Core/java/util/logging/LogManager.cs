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

namespace biz.ritter.javapi.util.logging
{
    /**
     * {@code LogManager} is used to maintain configuration properties of the
     * logging framework, and to manage a hierarchical namespace of all named
     * {@code Logger} objects.
     * <p>
     * There is only one global {@code LogManager} instance in the
     * application, which can be get by calling static method
     * {@link #getLogManager()}. This instance is created and
     * initialized during class initialization and cannot be changed.
     * <p>
     * The {@code LogManager} class can be specified by
     * java.util.logging.manager system property, if the property is unavailable or
     * invalid, the default class {@link java.util.logging.LogManager} will
     * be used.
     * <p>
     * On initialization, {@code LogManager} reads its configuration from a
     * properties file, which by default is the "lib/logging.properties" in the JRE
     * directory.
     * <p>
     * However, two optional system properties can be used to customize the initial
     * configuration process of {@code LogManager}.
     * <ul>
     * <li>"java.util.logging.config.class"</li>
     * <li>"java.util.logging.config.file"</li>
     * </ul>
     * <p>
     * These two properties can be set in three ways, by the Preferences API, by the
     * "java" command line property definitions, or by system property definitions
     * passed to JNI_CreateJavaVM.
     * <p>
     * The "java.util.logging.config.class" should specifies a class name. If it is
     * set, this given class will be loaded and instantiated during
     * {@code LogManager} initialization, so that this object's default
     * constructor can read the initial configuration and define properties for
     * {@code LogManager}.
     * <p>
     * If "java.util.logging.config.class" property is not set, or it is invalid, or
     * some exception is thrown during the instantiation, then the
     * "java.util.logging.config.file" system property can be used to specify a
     * properties file. The {@code LogManager} will read initial
     * configuration from this file.
     * <p>
     * If neither of these properties is defined, or some exception is thrown
     * during these two properties using, the {@code LogManager} will read
     * its initial configuration from default properties file, as described above.
     * <p>
     * The global logging properties may include:
     * <ul>
     * <li>"handlers". This property's values should be a list of class names for
     * handler classes separated by whitespace, these classes must be subclasses of
     * {@code Handler} and each must have a default constructor, these
     * classes will be loaded, instantiated and registered as handlers on the root
     * {@code Logger} (the {@code Logger} named ""). These
     * {@code Handler}s maybe initialized lazily.</li>
     * <li>"config". The property defines a list of class names separated by
     * whitespace. Each class must have a default constructor, in which it can
     * update the logging configuration, such as levels, handlers, or filters for
     * some logger, etc. These classes will be loaded and instantiated during
     * {@code LogManager} configuration</li>
     * </ul>
     * <p>
     * This class, together with any handler and configuration classes associated
     * with it, <b>must</b> be loaded from the system classpath when
     * {@code LogManager} configuration occurs.
     * <p>
     * Besides global properties, the properties for loggers and Handlers can be
     * specified in the property files. The names of these properties will start
     * with the complete dot separated names for the handlers or loggers.
     * <p>
     * In the {@code LogManager}'s hierarchical namespace,
     * {@code Loggers} are organized based on their dot separated names. For
     * example, "x.y.z" is child of "x.y".
     * <p>
     * Levels for {@code Loggers} can be defined by properties whose name end
     * with ".level". Thus "alogger.level" defines a level for the logger named as
     * "alogger" and for all its children in the naming hierarchy. Log levels
     * properties are read and applied in the same order as they are specified in
     * the property file. The root logger's level can be defined by the property
     * named as ".level".
     * <p>
     * All methods on this type can be taken as being thread safe.
     */
    public class LogManager
    {
        static LogManager()
        {
            // init LogManager singleton instance
            String className = java.lang.SystemJ.getProperty("java.util.logging.manager"); //$NON-NLS-1$

            if (null != className)
            {
                manager = (LogManager)getInstanceByClass(className);
            }
            if (null == manager)
            {
                manager = new LogManager();
            }

            // read configuration
            try
            {
                manager.readConfiguration();
            }
            catch (Exception)
            {
                // e.printStackTrace();
            }

            // if global logger has been initialized, set root as its parent
            Logger root = new Logger("", default(Logger)); //$NON-NLS-1$
            root.setLevel(Level.INFO);
            Logger.global.setParent(root);//                Logger.global.setParent(root);

            manager.addLogger(root);
            manager.addLogger(Logger.getLogger(Logger.GLOBAL_LOGGER_NAME));//Logger.global);
        }
        // FIXME: use weak reference to avoid heap memory leak
        private Hashtable<String, Logger> loggers;

        /** The configuration properties */
        private Properties props;

        /** the property change listener */
        private java.beans.PropertyChangeSupport listeners;

        /** The singleton instance. */
        private static LogManager manager;
        /**
         * Get the global {@code LogManager} instance.
         *
         * @return the global {@code LogManager} instance
         */
        public static LogManager getLogManager()
        {
            return manager;
        }

        /**
         * Default constructor. This is not public because there should be only one
         * {@code LogManager} instance, which can be get by
         * {@code LogManager.getLogManager()}. This is protected so that
         * application can subclass the object.
         */
        protected LogManager()
        {
            loggers = new Hashtable<String, Logger>();
            props = new Properties();
            listeners = new java.beans.PropertyChangeSupport(this);
            // add shutdown hook to ensure that the associated resource will be
            // freed when JVM exits
            java.lang.Runtime.getRuntime().addShutdownHook(new IAC_SHUTDOWN_THREAD());
        }
        private sealed class IAC_SHUTDOWN_THREAD : java.lang.Thread
        {
            public override void run()
            {
                LogManager.getLogManager().reset();
            }
        }

        // use SystemClassLoader to load class from system classpath
        internal static Object getInstanceByClass(String className)
        {
            try
            {
                java.lang.Class clazz = java.lang.ClassLoader.getSystemClassLoader().loadClass(className);
                return clazz.newInstance();
            }
            catch (Exception e)
            {
                try
                {
                    java.lang.Class clazz = java.lang.Thread.currentThread().getContextClassLoader().loadClass(className);
                    return clazz.newInstance();
                }
                catch (Exception innerE)
                {
                    // logging.20=Loading class "{0}" failed
                    java.lang.SystemJ.err.println("Loading class " + className + " failed"); //$NON-NLS-1$
                    java.lang.SystemJ.err.println(innerE.ToString());
                    return null;
                }
            }

        }

        /**
         * Re-initialize the properties and configuration. The initialization
         * process is same as the {@code LogManager} instantiation.
         * <p>
         * Notice : No {@code PropertyChangeEvent} are fired.
         * </p>
         *
         * @throws IOException
         *             if any IO related problems happened.
         * @throws SecurityException
         *             if security manager exists and it determines that caller does
         *             not have the required permissions to perform this action.
         */
        public void readConfiguration()
        {//throws IOException {
            // check config class
            String configClassName = java.lang.SystemJ.getProperty("java.util.logging.config.class"); //$NON-NLS-1$
            if (null == configClassName || null == getInstanceByClass(configClassName))
            {
                // if config class failed, check config file
                String configFile = java.lang.SystemJ.getProperty("java.util.logging.config.file"); //$NON-NLS-1$

                if (null == configFile)
                {
                    // if cannot find configFile, use default logging.properties
                    configFile = new java.lang.StringBuilder().append(
                            java.lang.SystemJ.getProperty("java.home")).append(java.io.File.separator) //$NON-NLS-1$
                            .append("lib").append(java.io.File.separator).append( //$NON-NLS-1$
                                    "logging.properties").toString(); //$NON-NLS-1$
                }

                java.io.InputStream input = null;
                try
                {
                    input = new java.io.BufferedInputStream(new java.io.FileInputStream(configFile));
                    readConfiguration(input);
                }
                finally
                {
                    if (input != null)
                    {
                        try
                        {
                            input.close();
                        }
                        catch (Exception e)
                        {// ignore
                        }
                    }
                }
            }
        }


        /**
         * Re-initialize the properties and configuration from the given
         * {@code InputStream}
         * <p>
         * Notice : No {@code PropertyChangeEvent} are fired.
         * </p>
         *
         * @param ins
         *            the input stream
         * @throws IOException
         *             if any IO related problems happened.
         * @throws SecurityException
         *             if security manager exists and it determines that caller does
         *             not have the required permissions to perform this action.
         */
        public void readConfiguration(java.io.InputStream ins)
        {//throws IOException {
            checkAccess();
            throw new java.lang.UnsupportedOperationException("Not yet implemented");
            /*readConfigurationImpl(ins);*/
        }
        /**
         * Check that the caller has {@code LoggingPermission("control")} so
         * that it is trusted to modify the configuration for logging framework. If
         * the check passes, just return, otherwise {@code SecurityException}
         * will be thrown.
         *
         * @throws SecurityException
         *             if there is a security manager in operation and the invoker
         *             of this method does not have the required security permission
         *             {@code LoggingPermission("control")}
         */
        public void checkAccess()
        {
            java.lang.SystemJ.err.println("logManager.checkAccess() not yet implemented");
            /*if (null != System.getSecurityManager())
            {
                System.getSecurityManager().checkPermission(perm);
            }*/
        }

        /**
 * Returns a named logger associated with the supplied resource bundle.
 *
 * @param resourceBundleName the resource bundle to associate, or null for
 *      no associated resource bundle.
 */
        internal Logger getOrCreate(String name, String resourceBundleName)
        {
            lock (this)
            {
                Logger result = getLogger(name);
                if (result == null)
                {
                    result = new Logger(name, resourceBundleName);
                    addLogger(result);
                }
                return result;
            }
        }

        /**
         * Get the logger with the given name.
         *
         * @param name
         *            name of logger
         * @return logger with given name, or {@code null} if nothing is found.
         */
        public Logger getLogger(String name)
        {
            lock (this)
            {
                return loggers.get(name);
            }
        }
        /**
         * Get the value of property with given name.
         *
         * @param name
         *            the name of property
         * @return the value of property
         */
        public String getProperty(String name)
        {
            return props.getProperty(name);
        }

        /**
         * Reset configuration.
         * <p>
         * All handlers are closed and removed from any named loggers. All loggers'
         * level is set to null, except the root logger's level is set to
         * {@code Level.INFO}.
         * </p>
         *
         * @throws SecurityException
         *             if security manager exists and it determines that caller does
         *             not have the required permissions to perform this action.
         */
        public void reset()
        {
            lock (this)
            {
                checkAccess();
                props = new Properties();
                Enumeration<String> names = getLoggerNames();
                while (names.hasMoreElements())
                {
                    String name = names.nextElement();
                    Logger logger = getLogger(name);
                    if (logger != null)
                    {
                        logger.reset();
                    }
                }
                Logger root = loggers.get(""); //$NON-NLS-1$
                if (null != root)
                {
                    root.setLevel(Level.INFO);
                }
            }
        }

        /**
         * Get a {@code Enumeration} of all registered logger names.
         *
         * @return enumeration of registered logger names
         */
        public Enumeration<String> getLoggerNames()
        {
            lock (this)
            {
                return loggers.keys();
            }
        }

        /**
         * Sets the level on {@code logger} to {@code newLevel}. Any child loggers
         * currently inheriting their level from {@code logger} will be updated
         * recursively.
         *
         * @param newLevel the new minimum logging threshold. If null, the logger's
         *      parent level will be used; or {@code Level.INFO} for loggers with no
         *      parent.
         */
        internal void setLevelRecursively(Logger logger, Level newLevel)
        {
            lock (this)
            {
                int previous = logger.levelIntVal;
                logger.levelObjVal = newLevel;

                if (newLevel == null)
                {
                    logger.levelIntVal = logger.parent != null
                            ? logger.parent.levelIntVal
                            : Level.INFO.intValue();
                }
                else
                {
                    logger.levelIntVal = newLevel.intValue();
                }

                if (previous != logger.levelIntVal)
                {
                    foreach (Logger child in logger.children)
                    {
                        if (child.levelObjVal == null)
                        {
                            setLevelRecursively(child, null);
                        }
                    }
                }
            }
        }
        /**
         * Add a given logger into the hierarchical namespace. The
         * {@code Logger.addLogger()} factory methods call this method to add newly
         * created Logger. This returns false if a logger with the given name has
         * existed in the namespace
         * <p>
         * Note that the {@code LogManager} may only retain weak references to
         * registered loggers. In order to prevent {@code Logger} objects from being
         * unexpectedly garbage collected it is necessary for <i>applications</i>
         * to maintain references to them.
         * </p>
         *
         * @param logger
         *            the logger to be added.
         * @return true if the given logger is added into the namespace
         *         successfully, false if the given logger exists in the namespace.
         */
        public bool addLogger(Logger logger)
        {
            lock (this)
            {
                String name = logger.getName();
                if (null != loggers.get(name))
                {
                    return false;
                }
                addToFamilyTree(logger, name);
                loggers.put(name, logger);
                logger.setManager(this);
                return true;
            }
        }

        private void addToFamilyTree(Logger logger, String name)
        {
            Logger parent = null;
            // find parent
            int lastSeparator;
            String parentName = name;
            while ((lastSeparator = parentName.lastIndexOf('.')) != -1)
            {
                parentName = parentName.substring(0, lastSeparator);
                parent = loggers.get(parentName);
                if (parent != null)
                {
                    setParent(logger, parent);
                    break;
                }
                else if (getProperty(parentName + ".level") != null || //$NON-NLS-1$
                      getProperty(parentName + ".handlers") != null)
                { //$NON-NLS-1$
                    parent = Logger.getLogger(parentName);
                    setParent(logger, parent);
                    break;
                }
            }
            if (parent == null && null != (parent = loggers.get("")))
            { //$NON-NLS-1$
                setParent(logger, parent);
            }

            // find children
            // TODO: performance can be improved here?
            //Collection<Logger> allLoggers = loggers.values();
            java.util.ArrayList<Logger> allLoggers = new java.util.ArrayList<Logger>(loggers.values());
            foreach (Logger child in allLoggers)
            {
                Logger oldParent = child.getParent();
                if (parent == oldParent
                        && (name.length() == 0 || child.getName().startsWith(
                                name + '.')))
                {
                    Logger thisLogger = logger;
                    child.setParent(thisLogger);
                    if (null != oldParent)
                    {
                        // -- remove from old parent as the parent has been changed
                        oldParent.children.remove(child);
                    }
                }
            }
        }

        /**
         * Sets the parent of this logger in the namespace. Callers must first
         * {@link #checkAccess() check security}.
         *
         * @param newParent
         *            the parent logger to set.
         */
        internal void setParent(Logger logger, Logger newParent)
        {
            lock (this)
            {
                logger.parent = newParent;

                if (logger.levelObjVal == null)
                {
                    setLevelRecursively(logger, null);
                }
                newParent.children.add(logger);
            }
        }


    }
}