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
    public abstract class URLConnection
    {
        /// <summary>
        /// No default constructor allowed
        /// </summary>
        private URLConnection() { }

        /// <summary>
        /// Create a new URLConnection instance with given URL
        /// </summary>
        /// <param name="url"></param>
        protected internal URLConnection (URL url) {}

        public abstract java.io.InputStream getInputStream();

        ///<summary>
        /// Gets the MIME-type of the content specified by the response header field
        /// {@code content-type} or {@code null} if type is unknown.
        ///</summary>
        ///<returns>the value of the response header field {@code content-type}.</returns>
        public virtual String getContentType()
        {
            return getHeaderField("Content-Type"); //$NON-NLS-1$
        }

        /**
         * Gets the value of the header field specified by {@code key} or {@code
         * null} if there is no field with this name. The current implementation of
         * this method returns always {@code null}.
         * 
         * @param key
         *            the name of the header field.
         * @return the value of the header field.
         */
        public virtual String getHeaderField(String key)
        {
            return null;
        }
    }
}
