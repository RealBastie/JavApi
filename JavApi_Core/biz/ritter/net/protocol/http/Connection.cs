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
 *  Copyright © 2012 Sebastian Ritter
 */
using System;
using java = biz.ritter.javapi;

using System.Net;

namespace biz.ritter.net.protocol.http
{
    internal class Connection : java.net.URLConnection
    {
        #region delegate to .net framework

        private HttpWebRequest httpRequest;
        private HttpWebResponse httpResponse;

        #endregion

        /// <summary>
        /// contains request properties
        /// </summary>
        private java.util.Properties requestProperties;
        /// <summary>
        /// Contains URL 
        /// </summary>
        private java.net.URL url;

        /// <summary>
        /// Create new instance with given URL
        /// </summary>
        /// <param name="url"></param>
        protected internal Connection(java.net.URL url) : base(url)
        {
            this.url = url;
            this.requestProperties = new java.util.Properties();
            this.addRequestProperty("User-Agent", "JavApi/"+java.lang.SystemJ.getProperty("java.version"));
            this.addRequestProperty("Referer", "http://www.Ritter.biz");
        }

        /// <summary>
        /// Add new request Properties
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <exception cref="java.lang.NullPointerException"></exception>
        /// <exception cref="java.lang.IllegalStateException"></exception>
        public void addRequestProperty(String name, String value)
        {
            if (null == name) throw new java.lang.NullPointerException("Request property name is null.");
            if (null != this.httpRequest) throw new java.lang.IllegalStateException("Can not set property for living connection.");
            this.requestProperties.put(name, value);
        }

        /// <summary>
        /// Return the http input stream
        /// </summary>
        /// <returns></returns>
        public override java.io.InputStream getInputStream()
        {
            this.httpRequest = (HttpWebRequest)HttpWebRequest.Create(this.url.toString());
            java.util.Enumeration<String> keys = this.requestProperties.keys();
            while (keys.hasMoreElements()) {
                String key = keys.nextElement();
                String value = this.requestProperties.get(key);
                switch (key)
                {
                        // Bad, bad .net - why we can not using same method for all headers...
                    case "Accept" :
                        this.httpRequest.Accept = value;
                        break;
                    case "Connection":
                        this.httpRequest.Connection = value;
                        break;
                    case "KeepAlive":
                        this.httpRequest.KeepAlive = value == "true";
                        break;
                    case "Content-Length":
                        this.httpRequest.ContentLength = java.lang.Long.parseLong(value);
                        break;
                    case "Content-Type":
                        this.httpRequest.ContentType = value;
                        break;
                    case "Expect":
                        this.httpRequest.Expect = value;
                        break;
                    case "Date":
                        throw new java.lang.UnsupportedOperationException("Not yet implemented");
                    case "Host":
                        this.httpRequest.Host = value;
                        break;
                    case "If-Modified-Since" :
                        throw new java.lang.UnsupportedOperationException("Not yet implemented");
                    case "Range":
                        throw new java.lang.UnsupportedOperationException("Not yet implemented");
                    case "Referer" :
                        this.httpRequest.Referer = value;
                        break;
                    case "Transfer-Encoding":
                        this.httpRequest.TransferEncoding = value;
                        break;
                    case "User-Agent":
                        this.httpRequest.UserAgent = value;
                        break;
                    default:
                        this.httpRequest.Headers.Add(key, value);
                        break;
                }
            }
            this.httpResponse = (HttpWebResponse) this.httpRequest.GetResponse();

            return new InputStreamWrapper(this.httpResponse.GetResponseStream());
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
        public override String getHeaderField(String key)
        {
            return null;
        }
    }
    /*
     
/// <summary>
/// Returns the content of a given web adress as string.
/// </summary>
/// <param name="Url">URL of the webpage</param>
/// <returns>Website content</returns>
public string DownloadWebPage(string Url)
{
    // Open a connection
    HttpWebRequest WebRequestObject = (HttpWebRequest)HttpWebRequest.Create(Url);
 
    // You can also specify additional header values like 
    // the user agent or the referer:
    WebRequestObject.UserAgent	= ".NET Framework/2.0";
    WebRequestObject.Referer	= "http://www.example.com/";
 
    // Request response:
    WebResponse Response = WebRequestObject.GetResponse();
 
    // Open data stream:
    Stream WebStream = Response.GetResponseStream();
 
    // Create reader object:
    StreamReader Reader = new StreamReader(WebStream);
 
    // Read the entire stream content:
    string PageContent = Reader.ReadToEnd();
 
    // Cleanup
    Reader.Close();
    WebStream.Close();
    Response.Close();
 
    return PageContent;
}
     */
}
