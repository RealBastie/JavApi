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

namespace biz.ritter.javapi.security
{

    /**
     * {@code GeneralSecurityException} is a general security exception and the
     * baseclass for all security specific exceptions.
     */
    public class GeneralSecurityException : java.lang.Exception
    {

        private static readonly long serialVersionUID = 894798122053539237L;

        /**
         * Constructs a new instance of {@code GeneralSecurityException} with the
         * given message.
         *
         * @param msg
         *            the detail message for this exception.
         */
        public GeneralSecurityException(String msg) :
            base(msg)
        {
        }

        /**
         * Constructs a new instance of {@code GeneralSecurityException}.
         */
        public GeneralSecurityException()
        {
        }

        /**
         * Constructs a new instance of {@code GeneralSecurityException} with the
         * given message and the cause.
         *
         * @param message
         *            the detail message for this exception.
         * @param cause
         *            the exception which is the cause for this exception.
         */
        public GeneralSecurityException(String message, java.lang.Throwable cause) :
            base(message, cause)
        {
        }

        /**
         * Constructs a new instance of {@code GeneralSecurityException} with the
         * cause.
         *
         * @param cause
         *            the exception which is the cause for this exception.
         */
        public GeneralSecurityException(java.lang.Throwable cause) :
            base(cause)
        {
        }
    }
}