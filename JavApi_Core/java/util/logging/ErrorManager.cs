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
 * An error reporting facility for {@link Handler} implementations to record any
 * error that may happen during logging. {@code Handlers} should report errors
 * to an {@code ErrorManager}, instead of throwing exceptions, which would
 * interfere with the log issuer's execution.
 */
public class ErrorManager {

    /**
     * The error code indicating a failure that does not fit in any of the
     * specific types of failures that follow.
     */
    public const int GENERIC_FAILURE = 0;

    /**
     * The error code indicating a failure when writing to an output stream.
     */
    public const int WRITE_FAILURE = 1;

    /**
     * The error code indicating a failure when flushing an output stream.
     */
    public const int FLUSH_FAILURE = 2;

    /**
     * The error code indicating a failure when closing an output stream.
     */
    public const int CLOSE_FAILURE = 3;

    /**
     * The error code indicating a failure when opening an output stream.
     */
    public const int OPEN_FAILURE = 4;

    /**
     * The error code indicating a failure when formatting the error messages.
     */
    public const int FORMAT_FAILURE = 5;

    private static readonly String[] FAILURES = new String[] { "GENERIC_FAILURE",
            "WRITE_FAILURE", "FLUSH_FAILURE", "CLOSE_FAILURE", "OPEN_FAILURE",
            "FORMAT_FAILURE" };

    /**
     * An indicator for determining if the error manager has been called at
     * least once before.
     */
    private bool called;

    /**
     * Constructs an instance of {@code ErrorManager}.
     */
    public ErrorManager() :base(){
    }

    /**
     * Reports an error using the given message, exception and error code. This
     * implementation will write out the message to {@link System#err} on the
     * first call and all subsequent calls are ignored. A subclass of this class
     * should override this method.
     * 
     * @param message
     *            the error message, which may be {@code null}.
     * @param exception
     *            the exception associated with the error, which may be
     *            {@code null}.
     * @param errorCode
     *            the error code that identifies the type of error; see the
     *            constant fields of this class for possible values.
     */
    public void error(String message, java.lang.Exception exception, int errorCode) {
        lock (this) {
            if (called) {
                return;
            }
            called = true;
        }
        java.lang.SystemJ.err.println(this.getClass().getName()
                + ": " + FAILURES[errorCode]); //$NON-NLS-1$
        if (message != null) {
            // logging.1E=Error message - {0}
            java.lang.SystemJ.err.println("Error message - "+ message); //$NON-NLS-1$
        }
        if (exception != null) {
            // logging.1F=Exception - {0}
            java.lang.SystemJ.err.println("Exception - "+ exception); //$NON-NLS-1$
        }
    }
}}
