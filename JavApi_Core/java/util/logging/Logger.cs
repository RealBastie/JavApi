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
     * Loggers are used to log records to certain outputs, including file, console,
     * etc. They use various handlers to actually do the output-dependent
     * operations.
     * <p>
     * Client applications can get named loggers by calling the {@code getLogger}
     * methods. They can also get anonymous loggers by calling the
     * {@code getAnonymousLogger} methods. Named loggers are organized in a
     * namespace hierarchy managed by a log manager. The naming convention is
     * usually the same as java package's naming convention, that is using
     * dot-separated strings. Anonymous loggers do not belong to any namespace.
     * <p>
     * Loggers "inherit" log level setting from their parent if their own level is
     * set to {@code null}. This is also true for the resource bundle. The logger's
     * resource bundle is used to localize the log messages if no resource bundle
     * name is given when a log method is called. If {@code getUseParentHandlers()}
     * returns {@code true}, loggers also inherit their parent's handlers. In this
     * context, "inherit" only means that "behavior" is inherited. The internal
     * field values will not change, for example, {@code getLevel()} still returns
     * {@code null}.
     * <p>
     * When loading a given resource bundle, the logger first tries to use the
     * context classloader. If that fails, it tries the system classloader. And if
     * that still fails, it searches up the class stack and uses each class's
     * classloader to try to locate the resource bundle.
     * <p>
     * Some log methods accept log requests that do not specify the source class and
     * source method. In these cases, the logging framework will automatically infer
     * the calling class and method, but this is not guaranteed to be accurate.
     * <p>
     * Once a {@code LogRecord} object has been passed into the logging framework,
     * it is owned by the logging framework and the client applications should not
     * use it any longer.
     * <p>
     * All methods of this class are thread-safe.
     *
     * @see LogManager
     */
    public class Logger
    {

        /**
         * When converting the concurrent collection of handlers to an array, we
         * always pass a zero-length array to avoid size miscalculations. Passing
         * properly-sized arrays is non-atomic, and risks a null element in the
         * result.
         *
         * @deprecated Use Logger.getLogger(Logger.GLOBAL_LOGGER_NAME) instead.
         */
        [Obsolete]
        public static readonly Logger global = new Logger(GLOBAL_LOGGER_NAME, default(Logger)); //Basties note: replace "global" with constants
        /**
         * @since 1.6
         */
        public const String GLOBAL_LOGGER_NAME = "global";


        private readonly Handler[] EMPTY_HANDLERS_ARRAY = new Handler[0];
        // the name of this logger
        private volatile String name;

        /** The parent logger of this logger. */
        internal Logger parent;

        /** True to notify the parent's handlers of each log message. */
        private bool notifyParentHandlers = true;

        /** The logging level of this logger, or null if none is set. */
        internal volatile Level levelObjVal;

    /**
     * Child loggers. Should be accessed only while synchronized on {@code
     * LogManager.getLogManager()}.
     */
    internal readonly ArrayList<Logger> children = new ArrayList<Logger>(); //Basties note: is final - so why loose informations...

        /** The filter. */
        private Filter filter;

        /**
         * Indicates whether this logger is named. Only {@link #getAnonymousLogger
         * anonymous loggers} are unnamed.
         */
        private bool isNamed = true;

        /**
         * The effective logging level of this logger. In order of preference this
         * is the first applicable of:
         * <ol>
         * <li>the int value of this logger's {@link #levelObjVal}</li>
         * <li>the logging level of the parent</li>
         * <li>the default level ({@link Level#INFO})</li>
         * </ol>
         */
        internal volatile int levelIntVal = Level.INFO.intValue();

        /**
         * The resource bundle used to localize logging messages. If null, no
         * localization will be performed.
         */
        private volatile String resourceBundleName;

        /** The loaded resource bundle according to the specified name. */
        private volatile ResourceBundle resourceBundle;

    /**
     * The handlers attached to this logger. Eagerly initialized and
     * concurrently modified.
     */
        private readonly java.util.ArrayList<Handler> handlers = new java.util.ArrayList<Handler>();//new java.util.concurrent.CopyOnWriteArrayList<Handler>();

        public Logger(String name, Logger parent)
        {
            this.name = name;
            this.parent = parent;
        }


        /**
         * Constructs a {@code Logger} object with the supplied name and resource
         * bundle name; {@code notifiyParentHandlers} is set to {@code true}.
         * <p>
         * Notice : Loggers use a naming hierarchy. Thus "z.x.y" is a child of "z.x".
         *
         * @param name
         *            the name of this logger, may be {@code null} for anonymous
         *            loggers.
         * @param resourceBundleName
         *            the name of the resource bundle used to localize logging
         *            messages, may be {@code null}.
         * @throws MissingResourceException
         *             if the specified resource bundle can not be loaded.
         */
        protected internal Logger(String name, String resourceBundleName)
        {
            this.name = name;
            initResourceBundle(resourceBundleName);
        }

    /**
     * Initializes this logger's resource bundle.
     *
     * @throws IllegalArgumentException if this logger's resource bundle already
     *      exists and is different from the resource bundle specified.
     */
    private void initResourceBundle(String resourceBundleName) {
        lock (this)
        {
            String current = this.resourceBundleName;

            if (current != null)
            {
                if (current.equals(resourceBundleName))
                {
                    return;
                }
                else
                {
                    // logging.9=The specified resource bundle name "{0}" is
                    // inconsistent with the existing one "{1}".
                    throw new java.lang.IllegalArgumentException("The specified resource bundle name "+resourceBundleName+" is inconsistent with the existing one "+current);
                }
            }

            if (resourceBundleName != null)
            {
                this.resourceBundle = loadResourceBundle(resourceBundleName);
                this.resourceBundleName = resourceBundleName;
            }
        }
    }

        /**
         * This method is for compatibility. Tests written to the reference
         * implementation API imply that the isLoggable() method is not called
         * directly. This behavior is important because subclass may override
         * isLoggable() method, so that affect the result of log methods.
         */
        private bool internalIsLoggable(Level l)
        {
            int effectiveLevel = levelIntVal;
            if (effectiveLevel == Level.OFF.intValue())
            {
                // always return false if the effective level is off
                return false;
            }
            return l.intValue() >= effectiveLevel;
        }

        /**
         * Determines whether this logger will actually log messages of the
         * specified level. The effective level used to do the determination may be
         * inherited from its parent. The default level is {@code Level.INFO}.
         *
         * @param l
         *            the level to check.
         * @return {@code true} if this logger will actually log this level,
         *         otherwise {@code false}.
         */
        public bool isLoggable(Level l)
        {
            return internalIsLoggable(l);
        }

        /**
         * Logs a message of level {@code Level.SEVERE}; the message is transmitted
         * to all subscribed handlers.
         *
         * @param msg
         *            the message to log.
         */
        public void severe(String msg)
        {
            log(Level.SEVERE, msg);
        }

        /**
         * Logs a message of level {@code Level.WARNING}; the message is
         * transmitted to all subscribed handlers.
         *
         * @param msg
         *            the message to log.
         */
        public void warning(String msg)
        {
            log(Level.WARNING, msg);
        }

        /**
         * Logs a message of level {@code Level.INFO}; the message is transmitted
         * to all subscribed handlers.
         *
         * @param msg
         *            the message to log.
         */
        public void info(String msg)
        {
            log(Level.INFO, msg);
        }

        /**
         * Logs a message of level {@code Level.CONFIG}; the message is transmitted
         * to all subscribed handlers.
         *
         * @param msg
         *            the message to log.
         */
        public void config(String msg)
        {
            log(Level.CONFIG, msg);
        }

        /**
         * Logs a message of level {@code Level.FINE}; the message is transmitted
         * to all subscribed handlers.
         *
         * @param msg
         *            the message to log.
         */
        public void fine(String msg)
        {
            log(Level.FINE, msg);
        }

        /**
         * Logs a message of level {@code Level.FINER}; the message is transmitted
         * to all subscribed handlers.
         *
         * @param msg
         *            the message to log.
         */
        public void finer(String msg)
        {
            log(Level.FINER, msg);
        }

        /**
         * Logs a message of level {@code Level.FINEST}; the message is transmitted
         * to all subscribed handlers.
         *
         * @param msg
         *            the message to log.
         */
        public void finest(String msg)
        {
            log(Level.FINEST, msg);
        }

        /**
         * Logs a message of the specified level. The message is transmitted to all
         * subscribed handlers.
         *
         * @param logLevel
         *            the level of the specified message.
         * @param msg
         *            the message to log.
         */
        public void log(Level logLevel, String msg)
        {
            if (!internalIsLoggable(logLevel))
            {
                return;
            }

            LogRecord record = new LogRecord(logLevel, msg);
            record.setLoggerName(this.name);
            setResourceBundle(record);
            log(record);
        }

        /**
         * Sets the resource bundle and its name for a supplied LogRecord object.
         * This method first tries to use this logger's resource bundle if any,
         * otherwise try to inherit from this logger's parent, recursively up the
         * namespace.
         */
        private void setResourceBundle(LogRecord record)
        {
            for (Logger p = this; p != null; p = p.parent)
            {
                String resourceBundleName = p.resourceBundleName;
                if (resourceBundleName != null)
                {
                    record.setResourceBundle(p.resourceBundle);
                    record.setResourceBundleName(resourceBundleName);
                    return;
                }
            }
        }

        /**
         * Logs a given log record. Only records with a logging level that is equal
         * or greater than this logger's level will be submitted to this logger's
         * handlers for logging. If {@code getUseParentHandlers()} returns {@code
         * true}, the log record will also be submitted to the handlers of this
         * logger's parent, potentially recursively up the namespace.
         * <p>
         * Since all other log methods call this method to actually perform the
         * logging action, subclasses of this class can override this method to
         * catch all logging activities.
         * </p>
         *
         * @param record
         *            the log record to be logged.
         */
        public void log(LogRecord record) {
        if (!internalIsLoggable(record.getLevel())) {
            return;
        }

        // apply the filter if any
        Filter f = filter;
        if (f != null && !f.isLoggable(record)) {
            return;
        }

        /*
         * call the handlers of this logger, throw any exception that occurs
         */
        Handler[] allHandlers = getHandlers();
        foreach (Handler element in allHandlers) {
            element.publish(record);
        }
        // call the parent's handlers if set useParentHandlers
        Logger temp = this;
        Logger theParent = temp.parent;
        while (theParent != null && temp.getUseParentHandlers()) {
            Handler[] ha = theParent.getHandlers();
            foreach (Handler element in ha) {
                element.publish(record);
            }
            temp = theParent;
            theParent = temp.parent;
        }
    }
        /**
         * Gets all the handlers associated with this logger.
         *
         * @return an array of all the handlers associated with this logger.
         */
        public Handler[] getHandlers()
        {
            return handlers.toArray(EMPTY_HANDLERS_ARRAY);
        }

        /**
         * Gets the flag which indicates whether to use the handlers of this
         * logger's parent to publish incoming log records, potentially recursively
         * up the namespace.
         *
         * @return {@code true} if set to use parent's handlers, {@code false}
         *         otherwise.
         */
        public bool getUseParentHandlers()
        {
            return this.notifyParentHandlers;
        }


        public static ResourceBundle loadResourceBundle(String name)
        {
            throw new MissingResourceException("loadResourceBundle not yet implemented", typeof(Logger).getClass().getName(), name);
        }

        /**
         * Gets a named logger. The returned logger may already exist or may be
         * newly created. In the latter case, its level will be set to the
         * configured level according to the {@code LogManager}'s properties.
         *
         * @param name
         *            the name of the logger to get, cannot be {@code null}.
         * @return a named logger.
         * @throws MissingResourceException
         *             If the specified resource bundle can not be loaded.
         */
        public static Logger getLogger(String name)
        {
            return LogManager.getLogManager().getOrCreate(name, null);
        }
        /**
         * Sets the logging level for this logger. A {@code null} level indicates
         * that this logger will inherit its parent's level.
         *
         * @param newLevel
         *            the logging level to set.
         * @throws SecurityException
         *             if a security manager determines that the caller does not
         *             have the required permission.
         */
        public void setLevel(Level newLevel)
        {
            // Anonymous loggers can always set the level
            LogManager logManager = LogManager.getLogManager();
            if (this.isNamed)
            {
                logManager.checkAccess();
            }
            logManager.setLevelRecursively(this, newLevel);
        }

        internal void reset() {
        levelObjVal = null;
        levelIntVal = Level.INFO.intValue();

        foreach (Handler handler in handlers) {
            try {
                if (handlers.remove(handler)) {
                    handler.close();
                }
            } catch (Exception ignored) {
            }
        }
    }
        /**
         * Gets the name of this logger, {@code null} for anonymous loggers.
         *
         * @return the name of this logger.
         */
        public String getName()
        {
            return this.name;
        }
    /**
     * Set the logger's manager and initializes its configuration from the
     * manager's properties.
     */
    internal void setManager(LogManager manager) {
        String levelProperty = manager.getProperty(name + ".level");
        if (levelProperty != null) {
            try {
                manager.setLevelRecursively(this, Level.parse(levelProperty));
            } catch (java.lang.IllegalArgumentException invalidLevel) {
                invalidLevel.printStackTrace();
            }
        }

        String handlersPropertyName = "".equals(name) ? "handlers" : name + ".handlers";
        String handlersProperty = manager.getProperty(handlersPropertyName);
        if (handlersProperty != null) {
            foreach (String handlerName in handlersProperty.Split(",|\\s".toCharArray())) {
                if (handlerName.equals("")) {
                    continue;
                }

                Handler handler;
                try {
                    handler = (Handler) LogManager.getInstanceByClass(handlerName);
                } catch (java.lang.Exception invalidHandlerName) {
                    invalidHandlerName.printStackTrace();
                    continue;
                }

                try {
                    String level = manager.getProperty(handlerName + ".level");
                    if (level != null) {
                        handler.setLevel(Level.parse(level));
                    }
                } catch (java.lang.Exception invalidLevel) {
                    invalidLevel.printStackTrace();
                }

                handlers.add(handler);
            }
        }
    }

    /**
     * Gets the nearest parent of this logger in the namespace, a {@code null}
     * value will be returned if called on the root logger.
     *
     * @return the parent of this logger in the namespace.
     */
    public Logger getParent()
    {
        return parent;
    }

    /**
     * Sets the parent of this logger in the namespace. This method should be
     * used by the {@code LogManager} object only.
     *
     * @param parent
     *            the parent logger to set.
     * @throws SecurityException
     *             if a security manager determines that the caller does not
     *             have the required permission.
     */
    public void setParent(Logger parent)
    {
        if (parent == null)
        {
            // logging.B=The 'parent' parameter is null.
            throw new java.lang.NullPointerException("The 'parent' parameter is null."); //$NON-NLS-1$
        }

        // even anonymous loggers are checked
        LogManager logManager = LogManager.getLogManager();
        logManager.checkAccess();
        logManager.setParent(this, parent);
    }

    }
}