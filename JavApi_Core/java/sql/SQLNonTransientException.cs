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
    [Serializable]
    public class SQLNonTransientException : SQLException
    {

        private const long serialVersionUID = -9104382843534716547L;

        /**
         * Creates an SQLNonTransientException object. The Reason string is set to
         * null, the SQLState string is set to null and the Error Code is set to 0.
         */
        public SQLNonTransientException()
            : base()
        {
        }

        /**
         * Creates an SQLNonTransientException object. The Reason string is set to
         * the given reason string, the SQLState string is set to null and the Error
         * Code is set to 0.
         * 
         * @param reason
         *            the string to use as the Reason string
         */
        public SQLNonTransientException(String reason) :
            base(reason, null, 0)
        {
        }

        /**
         * Creates an SQLNonTransientException object. The Reason string is set to
         * the given reason string, the SQLState string is set to the given SQLState
         * string and the Error Code is set to 0.
         * 
         * @param reason
         *            the string to use as the Reason string
         * @param sqlState
         *            the string to use as the SQLState string
         */
        public SQLNonTransientException(String reason, String sqlState) :
            base(reason, sqlState, 0)
        {
        }

        /**
         * Creates an SQLNonTransientException object. The Reason string is set to
         * the given reason string, the SQLState string is set to the given SQLState
         * string and the Error Code is set to the given error code value.
         * 
         * @param reason
         *            the string to use as the Reason string
         * @param sqlState
         *            the string to use as the SQLState string
         * @param vendorCode
         *            the integer value for the error code
         */
        public SQLNonTransientException(String reason, String sqlState,
                int vendorCode) :
            base(reason, sqlState, vendorCode)
        {
        }

        /**
         * Creates an SQLNonTransientException object. The Reason string is set to
         * the null if cause == null or cause.toString() if cause!=null,and the
         * cause Throwable object is set to the given cause Throwable object.
         * 
         * @param cause
         *            the Throwable object for the underlying reason this
         *            SQLException
         */
        public SQLNonTransientException(java.lang.Throwable cause) :
            base(cause)
        {
        }

        /**
         * Creates an SQLNonTransientException object. The Reason string is set to
         * the given and the cause Throwable object is set to the given cause
         * Throwable object.
         * 
         * @param reason
         *            the string to use as the Reason string
         * @param cause
         *            the Throwable object for the underlying reason this
         *            SQLException
         */
        public SQLNonTransientException(String reason, java.lang.Throwable cause) :
            base(reason, cause)
        {
        }

        /**
         * Creates an SQLNonTransientException object. The Reason string is set to
         * the given reason string, the SQLState string is set to the given SQLState
         * string and the cause Throwable object is set to the given cause Throwable
         * object.
         * 
         * @param reason
         *            the string to use as the Reason string
         * @param sqlState
         *            the string to use as the SQLState string
         * @param cause
         *            the Throwable object for the underlying reason this
         *            SQLException
         */
        public SQLNonTransientException(String reason, String sqlState,
                java.lang.Throwable cause) :
            base(reason, sqlState, cause)
        {
        }

        /**
         * Creates an SQLNonTransientException object. The Reason string is set to
         * the given reason string, the SQLState string is set to the given SQLState
         * string , the Error Code is set to the given error code value, and the
         * cause Throwable object is set to the given cause Throwable object.
         * 
         * @param reason
         *            the string to use as the Reason string
         * @param sqlState
         *            the string to use as the SQLState string
         * @param vendorCode
         *            the integer value for the error code
         * @param cause
         *            the Throwable object for the underlying reason this
         *            SQLException
         */
        public SQLNonTransientException(String reason, String sqlState,
                int vendorCode, java.lang.Throwable cause) :
            base(reason, sqlState, vendorCode, cause)
        {
        }
    }
}
