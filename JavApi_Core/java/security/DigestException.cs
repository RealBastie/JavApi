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
     *{@code DigestException} is a general message digest exception.
     */
    [Serializable]
    public class DigestException : GeneralSecurityException
    {

        private static readonly long serialVersionUID = 5821450303093652515L;

        /**
         * Constructs a new instance of {@code DigestException} with the
         * given message.
         *
         * @param msg
         *            the detail message for this exception.
         */
        public DigestException(String msg) :
            base(msg)
        {
        }

        /**
         * Constructs a new instance of {@code DigestException}.
         */
        public DigestException()
        {
        }

        /**
         * Constructs a new instance of {@code DigestException} with the
         * given message and the cause.
         *
         * @param message
         *            the detail message for this exception.
         * @param cause
         *            the exception which is the cause for this exception.
         */
        public DigestException(String message, java.lang.Throwable cause) :
            base(message, cause)
        {
        }

        /**
         * Constructs a new instance of {@code DigestException} with the
         * cause.
         *
         * @param cause
         *            the exception which is the cause for this exception.
         */
        public DigestException(java.lang.Throwable cause) :
            base(cause)
        {
        }
    }
}