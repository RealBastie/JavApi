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

namespace biz.ritter.javapi.sql
{

    /**
     * An exception, which is subclass of SQLException, is thrown when the
     * connection operation that failed will not succeed when the operation is
     * retried without the cause of the failure being corrected.
     */
    [Serializable]
    public class SQLNonTransientConnectionException :
           SQLNonTransientException
    {

        private const long serialVersionUID = -5852318857474782892L;

        /**
         * Creates an SQLNonTransientConnectionException object. The Reason string
         * is set to null, the SQLState string is set to null and the Error Code is
         * set to 0.
         */
        public SQLNonTransientConnectionException() :
            base()
        {
        }

        /**
         * Creates an SQLNonTransientConnectionException object. The Reason string
         * is set to the given reason string, the SQLState string is set to null and
         * the Error Code is set to 0.
         * 
         * @param reason
         *            the string to use as the Reason string
         */
        public SQLNonTransientConnectionException(String reason) :
            base(reason, null, 0)
        {
        }

        /**
         * Creates an SQLNonTransientConnectionException object. The Reason string
         * is set to the given reason string, the SQLState string is set to the
         * given SQLState string and the Error Code is set to 0.
         * 
         * @param reason
         *            the string to use as the Reason string
         * @param sqlState
         *            the string to use as the SQLState string
         */
        public SQLNonTransientConnectionException(String reason, String sqlState) :
            base(reason, sqlState, 0)
        {
        }

        /**
         * Creates an SQLNonTransientConnectionException object. The Reason string
         * is set to the given reason string, the SQLState string is set to the
         * given SQLState string and the Error Code is set to the given error code
         * value.
         * 
         * @param reason
         *            the string to use as the Reason string
         * @param sqlState
         *            the string to use as the SQLState string
         * @param vendorCode
         *            the integer value for the error code
         */
        public SQLNonTransientConnectionException(String reason, String sqlState,
                int vendorCode) :
            base(reason, sqlState, vendorCode)
        {
        }

        /**
         * Creates an SQLNonTransientConnectionException object. The Reason string
         * is set to the null if cause == null or cause.toString() if
         * cause!=null,and the cause java.lang.Throwable object is set to the given cause
         * java.lang.Throwable object.
         * 
         * @param cause
         *            the java.lang.Throwable object for the underlying reason this
         *            SQLException
         */
        public SQLNonTransientConnectionException(java.lang.Throwable cause) :
            base(cause)
        {
        }

        /**
         * Creates an SQLNonTransientConnectionException object. The Reason string
         * is set to the given and the cause java.lang.Throwable object is set to the given
         * cause java.lang.Throwable object.
         * 
         * @param reason
         *            the string to use as the Reason string
         * @param cause
         *            the java.lang.Throwable object for the underlying reason this
         *            SQLException
         */
        public SQLNonTransientConnectionException(String reason, java.lang.Throwable cause) :
            base(reason, cause)
        {
        }

        /**
         * Creates an SQLNonTransientConnectionException object. The Reason string
         * is set to the given reason string, the SQLState string is set to the
         * given SQLState string and the cause java.lang.Throwable object is set to the given
         * cause java.lang.Throwable object.
         * 
         * @param reason
         *            the string to use as the Reason string
         * @param sqlState
         *            the string to use as the SQLState string
         * @param cause
         *            the java.lang.Throwable object for the underlying reason this
         *            SQLException
         */
        public SQLNonTransientConnectionException(String reason, String sqlState,
                java.lang.Throwable cause) :
            base(reason, sqlState, cause)
        {
        }

        /**
         * Creates an SQLNonTransientConnectionException object. The Reason string
         * is set to the given reason string, the SQLState string is set to the
         * given SQLState string , the Error Code is set to the given error code
         * value, and the cause java.lang.Throwable object is set to the given cause java.lang.Throwable
         * object.
         * 
         * @param reason
         *            the string to use as the Reason string
         * @param sqlState
         *            the string to use as the SQLState string
         * @param vendorCode
         *            the integer value for the error code
         * @param cause
         *            the java.lang.Throwable object for the underlying reason this
         *            SQLException
         */
        public SQLNonTransientConnectionException(String reason, String sqlState,
                int vendorCode, java.lang.Throwable cause) :
            base(reason, sqlState, vendorCode, cause)
        {
        }
    }
}
