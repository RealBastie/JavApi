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
     * An exception, which is subclass of SQLException, is thrown when one or more
     * client info properties could not be set on a Connection.
     */
    [Serializable]
    public class SQLClientInfoException : SQLException
    {
        private const long serialVersionUID = -4319604256824655880L;

        private java.util.Map<String, ClientInfoStatus> failedProperties = null;

        /**
         * Creates an SQLClientInfoException object. The Reason string is set to
         * null, the SQLState string is set to null and the Error Code is set to 0.
         */
        public SQLClientInfoException()
            : base()
        {
        }

        /**
         * Creates an SQLClientInfoException object. The Reason string is set to the
         * given reason string, the SQLState string is set to null and the Error
         * Code is set to 0, and the Map&lt;String,ClientInfoStatus&gt; object is set to
         * the failed properties.
         * 
         * @param failedProperties
         *            the Map&lt;String,ClientInfoStatus&gt; object to use as the
         *            property values
         */
        public SQLClientInfoException(java.util.Map<String, ClientInfoStatus> failedProperties)
            : base()
        {
            this.failedProperties = failedProperties;
        }

        /**
         * Creates an SQLClientInfoException object. The Reason string is set to the
         * null if cause == null or cause.toString() if cause!=null, the cause
         * Throwable object is set to the given cause Throwable object, and the Map&lt;String,ClientInfoStatus&gt;
         * object is set to the failed properties.
         * 
         * @param failedProperties
         *            the Map&lt;String,ClientInfoStatus&gt; object to use as the
         *            property values
         * @param cause
         *            the Throwable object for the underlying reason this
         *            SQLException
         */
        public SQLClientInfoException(
                java.util.Map<String, ClientInfoStatus> failedProperties, java.lang.Throwable cause)
            : base(cause)
        {
            this.failedProperties = failedProperties;
        }

        /**
         * Creates an SQLClientInfoException object. The Reason string is set to
         * reason, and the Map&lt;String,ClientInfoStatus&gt; object is set to the failed
         * properties.
         * 
         * @param reason
         *            the string to use as the Reason string
         * @param failedProperties
         *            the Map&lt;String,ClientInfoStatus&gt; object to use as the
         *            property values
         */
        public SQLClientInfoException(String reason,
                java.util.Map<String, ClientInfoStatus> failedProperties)
            : base(reason)
        {
            this.failedProperties = failedProperties;
        }

        /**
         * Creates an SQLClientInfoException object. The Reason string is set to
         * reason, the cause Throwable object is set to the given cause Throwable
         * object, and the Map&lt;String,ClientInfoStatus&gt; object is set to the failed
         * properties.
         * 
         * @param reason
         *            the string to use as the Reason string
         * @param failedProperties
         *            the Map&lt;String,ClientInfoStatus&gt; object to use as the
         *            property values
         * @param cause
         *            the Throwable object for the underlying reason this
         *            SQLException
         */
        public SQLClientInfoException(String reason,
                java.util.Map<String, ClientInfoStatus> failedProperties, java.lang.Throwable cause)
            : base(reason, cause)
        {
            this.failedProperties = failedProperties;
        }

        /**
         * Creates an SQLClientInfoException object. The Reason string is set to
         * reason, the SQLState string is set to the sqlState, the Error Code is set
         * to the vendorCode and the Map&lt;String,ClientInfoStatus&gt; object is set to
         * the failed properties.
         * 
         * @param reason
         *            the string to use as the Reason string
         * @param sqlState
         *            the string to use as the SQLState string
         * @param vendorCode
         *            the integer value for the error code
         * @param failedProperties
         *            the Map&lt;String,ClientInfoStatus&gt; object to use as the
         *            property values
         * 
         */
        public SQLClientInfoException(String reason, String sqlState,
                int vendorCode, java.util.Map<String, ClientInfoStatus> failedProperties) :
            base(reason, sqlState, vendorCode)
        {
            this.failedProperties = failedProperties;
        }

        /**
         * Creates an SQLClientInfoException object. The Reason string is set to
         * reason, the SQLState string is set to the sqlState, the Error Code is set
         * to the vendorCode the cause Throwable object is set to the given cause
         * Throwable object, and the Map&lt;String,ClientInfoStatus&gt; object is set to
         * the failed properties.
         * 
         * @param reason
         *            the string to use as the Reason string
         * @param sqlState
         *            the string to use as the SQLState string
         * @param vendorCode
         *            the integer value for the error code
         * @param failedProperties
         *            the Map&lt;String,ClientInfoStatus&gt; object to use as the
         *            property values
         * @param cause
         *            the Throwable object for the underlying reason this
         *            SQLException
         */
        public SQLClientInfoException(String reason, String sqlState,
                int vendorCode, java.util.Map<String, ClientInfoStatus> failedProperties,
                java.lang.Throwable cause) :
            base(reason, sqlState, vendorCode, cause)
        {
            this.failedProperties = failedProperties;
        }

        /**
         * Creates an SQLClientInfoException object. The Reason string is set to
         * reason, the SQLState string is set to the sqlState, and the Map&lt;String,ClientInfoStatus&gt;
         * object is set to the failed properties.
         * 
         * @param reason
         *            the string to use as the Reason string
         * @param sqlState
         *            the string to use as the SQLState string
         * @param failedProperties
         *            the Map&lt;String,ClientInfoStatus&gt; object to use as the
         *            property values
         */
        public SQLClientInfoException(String reason, String sqlState,
                java.util.Map<String, ClientInfoStatus> failedProperties) :
            base(reason, sqlState)
        {
            this.failedProperties = failedProperties;
        }

        /**
         * Creates an SQLClientInfoException object. The Reason string is set to
         * reason, the SQLState string is set to the sqlState, the Error Code is set
         * to the vendorCode, and the Map&lt;String,ClientInfoStatus&gt; object is set to
         * the failed properties.
         * 
         * @param reason
         *            the string to use as the Reason string
         * @param sqlState
         *            the string to use as the SQLState string
         * @param failedProperties
         *            the Map&lt;String,ClientInfoStatus&gt; object to use as the
         *            property values
         * @param cause
         *            the Throwable object for the underlying reason this
         *            SQLException
         */
        public SQLClientInfoException(String reason, String sqlState,
                java.util.Map<String, ClientInfoStatus> failedProperties, java.lang.Throwable cause) :
            base(reason, sqlState, cause)
        {
            this.failedProperties = failedProperties;
        }

        /**
         * returns that the client info properties which could not be set
         * 
         * @return the list of ClientInfoStatus objects indicate client info
         *         properties
         */
        public java.util.Map<String, ClientInfoStatus> getFailedProperties()
        {
            return failedProperties;
        }
    }
}
