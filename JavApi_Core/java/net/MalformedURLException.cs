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

namespace biz.ritter.javapi.net
{

    /**
     * This exception is thrown when a program attempts to create an URL from an
     * incorrect specification.
     * 
     * @see URL
     */
    [Serializable]
    public class MalformedURLException : java.io.IOException
    {

        private static readonly long serialVersionUID = -182787522200415866L;

        /**
         * Constructs a new instance of this class with its walkback filled in.
         */
        public MalformedURLException()
            : base()
        {
        }

        /**
         * Constructs a new instance of this class with its walkback and message
         * filled in.
         * 
         * @param detailMessage
         *            the detail message for this exception instance.
         */
        public MalformedURLException(String detailMessage)
            : base(detailMessage)
        {
        }
    }
}
