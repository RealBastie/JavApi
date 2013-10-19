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
 *  
 */
using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.util.logging
{

/**
 * A {@code LogRecord} object represents a logging request. It is passed between
 * the logging framework and individual logging handlers. Client applications
 * should not modify a {@code LogRecord} object that has been passed into the
 * logging framework.
 * <p>
 * The {@code LogRecord} class will infer the source method name and source
 * class name the first time they are accessed if the client application didn't
 * specify them explicitly. This automatic inference is based on the analysis of
 * the call stack and is not guaranteed to be precise. Client applications
 * should force the initialization of these two fields by calling
 * {@code getSourceClassName} or {@code getSourceMethodName} if they expect to
 * use them after passing the {@code LogRecord} object to another thread or
 * transmitting it over RMI.
 */
    [Serializable]
public class LogRecord : java.io.Serializable {

    private static readonly long serialVersionUID = 5372048053134512534L;

    // The major byte used in serialization.
    private const int MAJOR = 1;

    // The minor byte used in serialization.
    private const int MINOR = 4;

    // Store the current value for the sequence number.
    private static long currentSequenceNumber = 0;

    // Store the id for each thread.
    private static java.lang.ThreadLocal<java.lang.Integer> currentThreadId = new java.lang.ThreadLocal<java.lang.Integer>();

    // The base id as the starting point for thread ID allocation.
    private static int initThreadId = 0;

    /**
     * The logging level.
     * 
     * @serial
     */
    private Level level;

    /**
     * The sequence number.
     * 
     * @serial
     */
    private long sequenceNumber;

    /**
     * The name of the class that issued the logging call.
     * 
     * @serial
     */
    private String sourceClassName;

    /**
     * The name of the method that issued the logging call.
     * 
     * @serial
     */
    private String sourceMethodName;

    /**
     * The original message text.
     * 
     * @serial
     */
    private String message;

    /**
     * The ID of the thread that issued the logging call.
     * 
     * @serial
     */
    private int threadID;

    /**
     * The time that the event occurred, in milliseconds since 1970.
     * 
     * @serial
     */
    private long millis;

    /**
     * The associated {@code Throwable} object if any.
     * 
     * @serial
     */
    private java.lang.Throwable thrown;

    /**
     * The name of the source logger.
     * 
     * @serial
     */
    private String loggerName;

    /**
     * The name of the resource bundle used to localize the log message.
     * 
     * @serial
     */
    private String resourceBundleName;

    // The associated resource bundle if any.
        [NonSerialized]
    private ResourceBundle resourceBundle;

    // The parameters.
        [NonSerialized]
    private Object[] parameters;

    // If the source method and source class has been initialized
        [NonSerialized]
    private bool sourceInited;

    /**
     * Constructs a {@code LogRecord} object using the supplied the logging
     * level and message. The millis property is set to the current time. The
     * sequence property is set to a new unique value, allocated in increasing
     * order within the virtual machine. The thread ID is set to a unique value
     * for the current thread. All other properties are set to {@code null}.
     *
     * @param level
     *            the logging level, may not be {@code null}.
     * @param msg
     *            the raw message.
     * @throws NullPointerException
     *             if {@code level} is {@code null}.
     */
    public LogRecord(Level level, String msg) {
        if (null == level) {
            // logging.4=The 'level' parameter is null.
            throw new java.lang.NullPointerException("The 'level' parameter is null."); //$NON-NLS-1$
        }
        this.level = level;
        this.message = msg;
        this.millis = java.lang.SystemJ.currentTimeMillis();

        lock (typeof(LogRecord).getClass()) {
            this.sequenceNumber = currentSequenceNumber++;
            java.lang.Integer id = currentThreadId.get();
            if (null == id) {
                this.threadID = initThreadId;
                currentThreadId.set(java.lang.Integer.valueOf(initThreadId++));
            } else {
                this.threadID = id.intValue();
            }
        }

        this.sourceClassName = null;
        this.sourceMethodName = null;
        this.loggerName = null;
        this.parameters = null;
        this.resourceBundle = null;
        this.resourceBundleName = null;
        this.thrown = null;
    }

    /**
     * Gets the logging level.
     * 
     * @return the logging level.
     */
    public Level getLevel() {
        return level;
    }

    /**
     * Sets the logging level.
     * 
     * @param level
     *            the level to set.
     * @throws NullPointerException
     *             if {@code level} is {@code null}.
     */
    public void setLevel(Level level) {
        if (null == level) {
            // logging.4=The 'level' parameter is null.
            throw new java.lang.NullPointerException("The 'level' parameter is null."); //$NON-NLS-1$
        }
        this.level = level;
    }

    /**
     * Gets the name of the logger.
     * 
     * @return the logger name.
     */
    public String getLoggerName() {
        return loggerName;
    }

    /**
     * Sets the name of the logger.
     * 
     * @param loggerName
     *            the logger name to set.
     */
    public void setLoggerName(String loggerName) {
        this.loggerName = loggerName;
    }

    /**
     * Gets the raw message.
     * 
     * @return the raw message, may be {@code null}.
     */
    public String getMessage() {
        return message;
    }

    /**
     * Sets the raw message. When this record is formatted by a logger that has
     * a localization resource bundle that contains an entry for {@code message},
     * then the raw message is replaced with its localized version.
     * 
     * @param message
     *            the raw message to set, may be {@code null}.
     */
    public void setMessage(String message) {
        this.message = message;
    }

    /**
     * Gets the time when this event occurred, in milliseconds since 1970.
     * 
     * @return the time when this event occurred, in milliseconds since 1970.
     */
    public long getMillis() {
        return millis;
    }

    /**
     * Sets the time when this event occurred, in milliseconds since 1970.
     * 
     * @param millis
     *            the time when this event occurred, in milliseconds since 1970.
     */
    public void setMillis(long millis) {
        this.millis = millis;
    }

    /**
     * Gets the parameters.
     * 
     * @return the array of parameters or {@code null} if there are no
     *         parameters.
     */
    public Object[] getParameters() {
        return parameters;
    }

    /**
     * Sets the parameters.
     * 
     * @param parameters
     *            the array of parameters to set, may be {@code null}.
     */
    public void setParameters(Object[] parameters) {
        this.parameters = parameters;
    }

    /**
     * Gets the resource bundle used to localize the raw message during
     * formatting.
     * 
     * @return the associated resource bundle, {@code null} if none is
     *         available or the message is not localizable.
     */
    public ResourceBundle getResourceBundle() {
        return resourceBundle;
    }

    /**
     * Sets the resource bundle used to localize the raw message during
     * formatting.
     *
     * @param resourceBundle
     *            the resource bundle to set, may be {@code null}.
     */
    public void setResourceBundle(ResourceBundle resourceBundle) {
        this.resourceBundle = resourceBundle;
    }

    /**
     * Gets the name of the resource bundle.
     * 
     * @return the name of the resource bundle, {@code null} if none is
     *         available or the message is not localizable.
     */
    public String getResourceBundleName() {
        return resourceBundleName;
    }

    /**
     * Sets the name of the resource bundle.
     * 
     * @param resourceBundleName
     *            the name of the resource bundle to set.
     */
    public void setResourceBundleName(String resourceBundleName) {
        this.resourceBundleName = resourceBundleName;
    }

    /**
     * Gets the sequence number.
     * 
     * @return the sequence number.
     */
    public long getSequenceNumber() {
        return sequenceNumber;
    }

    /**
     * Sets the sequence number. It is usually not necessary to call this method
     * to change the sequence number because the number is allocated when this
     * instance is constructed.
     * 
     * @param sequenceNumber
     *            the sequence number to set.
     */
    public void setSequenceNumber(long sequenceNumber) {
        this.sequenceNumber = sequenceNumber;
    }

    /**
     * Gets the name of the class that is the source of this log record. This
     * information can be changed, may be {@code null} and is untrusted.
     * 
     * @return the name of the source class of this log record (possiblity {@code null})
     */
    public String getSourceClassName() {
        initSource();
        return sourceClassName;
    }

    /*
     *  Init the sourceClass and sourceMethod fields.
     */
    private void initSource() {
        if (!sourceInited) {
            java.lang.StackTraceElement[] elements = (new java.lang.Throwable()).getStackTrace();
            int i = 0;
            String current = null;
            bool isNotBreak = true;
            FINDLOG: for (; i < elements.Length && isNotBreak; i++) {
                current = elements[i].getClassName();
                if (current.equals(typeof(Logger).getClass().getName())) {
                    isNotBreak = false;
                    goto FINDLOG;//break FINDLOG;
                }
            }
            while (++i < elements.Length
                    && elements[i].getClassName().equals(current)) {
                // do nothing
            }
            if (i < elements.Length) {
                this.sourceClassName = elements[i].getClassName();
                this.sourceMethodName = elements[i].getMethodName();
            }
            sourceInited = true;
        }
    }

    /**
     * Sets the name of the class that is the source of this log record.
     * 
     * @param sourceClassName
     *            the name of the source class of this log record, may be
     *            {@code null}.
     */
    public void setSourceClassName(String sourceClassName) {
        sourceInited = true;
        this.sourceClassName = sourceClassName;
    }

    /**
     * Gets the name of the method that is the source of this log record.
     * 
     * @return the name of the source method of this log record.
     */
    public String getSourceMethodName() {
        initSource();
        return sourceMethodName;
    }

    /**
     * Sets the name of the method that is the source of this log record.
     * 
     * @param sourceMethodName
     *            the name of the source method of this log record, may be
     *            {@code null}.
     */
    public void setSourceMethodName(String sourceMethodName) {
        sourceInited = true;
        this.sourceMethodName = sourceMethodName;
    }

    /**
     * Gets a unique ID of the thread originating the log record. Every thread
     * becomes a different ID.
     * <p>
     * Notice : the ID doesn't necessary map the OS thread ID
     * </p>
     * 
     * @return the ID of the thread originating this log record.
     */
    public int getThreadID() {
        return threadID;
    }

    /**
     * Sets the ID of the thread originating this log record.
     * 
     * @param threadID
     *            the new ID of the thread originating this log record.
     */
    public void setThreadID(int threadID) {
        this.threadID = threadID;
    }

    /**
     * Gets the {@code Throwable} object associated with this log record.
     * 
     * @return the {@code Throwable} object associated with this log record.
     */
    public java.lang.Throwable getThrown() {
        return thrown;
    }

    /**
     * Sets the {@code Throwable} object associated with this log record.
     * 
     * @param thrown
     *            the new {@code Throwable} object to associate with this log
     *            record.
     */
    public void setThrown(java.lang.Throwable thrown) {
        this.thrown = thrown;
    }

    /*
     * Customized serialization.
     */
    private void writeObject(java.io.ObjectOutputStream outJ){// throws IOException {
        outJ.defaultWriteObject();
        outJ.writeByte(MAJOR);
        outJ.writeByte(MINOR);
        if (null == parameters) {
            outJ.writeInt(-1);
        } else {
            outJ.writeInt(parameters.Length);
            foreach (Object element in parameters) {
                outJ.writeObject(null == element ? null : element.toString());
            }
        }
    }

    /*
     * Customized deserialization.
     */
    private void readObject(java.io.ObjectInputStream inJ) {//throws IOException,            ClassNotFoundException {
        inJ.defaultReadObject();
        byte major = inJ.readByte();
        byte minor = inJ.readByte();
        // only check MAJOR version
        if (major != MAJOR) {
            // logging.5=Different version - {0}.{1}
            throw new java.io.IOException("Different version - "+ //$NON-NLS-1$
                    java.lang.Byte.valueOf(major)+"."+ java.lang.Byte.valueOf(minor));
        }

        int length = inJ.readInt();
        if (length >= 0) {
            parameters = new Object[length];
            for (int i = 0; i < parameters.Length; i++) {
                parameters[i] = inJ.readObject();
            }
        }
        if (null != resourceBundleName) {
            try {
                resourceBundle = Logger.loadResourceBundle(resourceBundleName);
            } catch (MissingResourceException e) {
                // Cannot find the specified resource bundle
                resourceBundle = null;
            }
        }
    }
}
}
