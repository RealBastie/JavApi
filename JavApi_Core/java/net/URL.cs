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
using org.apache.harmony.luni.util;

namespace biz.ritter.javapi.net
{
    public sealed class URL : java.io.Serializable
    {

        /**
         * Cache for storing protocol handler
         */
        private static java.util.Hashtable<String, URLStreamHandler> streamHandlers = new java.util.Hashtable<String, URLStreamHandler>();
        /**
         * The factory responsible for producing URL Stream (protocol) Handler
         */
        private static URLStreamHandlerFactory streamHandlerFactory;

        private static readonly NetPermission specifyStreamHandlerPermission = new NetPermission("specifyStreamHandler"); //$NON-NLS-1$

        private int hashCode;

        /**
         * The receiver's filename.
         * 
         * @serial the file of this URL
         * 
         */
        private String file;

        /**
         * The receiver's protocol identifier.
         * 
         * @serial the protocol of this URL (http, file)
         * 
         */
        private String protocol = null;

        /**
         * The receiver's host name.
         * 
         * @serial the host of this URL
         * 
         */
        private String host;

        /**
         * The receiver's port number.
         * 
         * @serial the port of this URL
         * 
         */
        private int port = -1;

        /**
         * The receiver's authority.
         * 
         * @serial the authority of this URL
         * 
         */
        private String authority = null;

        /**
         * The receiver's userInfo.
         */
        [NonSerialized]
        private String userInfo = null;

        /**
         * The receiver's path.
         */
        [NonSerialized]
        private String path = null;

        /**
         * The receiver's query.
         */
        [NonSerialized]
        private String query = null;

        /**
         * The receiver's reference.
         * 
         * @serial the reference of this URL
         * 
         */
        private String refJ = null;

        [NonSerialized]
        internal URLStreamHandler strmHandler;

        /**
         * Creates a new URL instance by parsing the string {@code spec}.
         * 
         * @param spec
         *            the URL string representation which has to be parsed.
         * @throws MalformedURLException
         *             if the given string {@code spec} could not be parsed as a
         *             URL.
         */
        public URL(String spec) ://throws MalformedURLException {
            this((URL)null, spec, (URLStreamHandler)null)
        {
        }

        /**
         * Creates a new URL to the specified resource {@code spec}. This URL is
         * relative to the given {@code context}. If the protocol of the parsed URL
         * does not match with the protocol of the context URL, then the newly
         * created URL is absolute and bases only on the given URL represented by
         * {@code spec}. Otherwise the protocol is defined by the context URL.
         * 
         * @param context
         *            the URL which is used as the context.
         * @param spec
         *            the URL string representation which has to be parsed.
         * @throws MalformedURLException
         *             if the given string {@code spec} could not be parsed as a URL
         *             or an invalid protocol has been found.
         */
        public URL(URL context, String spec) :// throws MalformedURLException {
            this(context, spec, (URLStreamHandler)null)
        {
        }

        /**
         * Creates a new URL to the specified resource {@code spec}. This URL is
         * relative to the given {@code context}. The {@code handler} will be used
         * to parse the URL string representation. If this argument is {@code null}
         * the default {@code URLStreamHandler} will be used. If the protocol of the
         * parsed URL does not match with the protocol of the context URL, then the
         * newly created URL is absolute and bases only on the given URL represented
         * by {@code spec}. Otherwise the protocol is defined by the context URL.
         * 
         * @param context
         *            the URL which is used as the context.
         * @param spec
         *            the URL string representation which has to be parsed.
         * @param handler
         *            the specific stream handler to be used by this URL.
         * @throws MalformedURLException
         *             if the given string {@code spec} could not be parsed as a URL
         *             or an invalid protocol has been found.
         */
        public URL(URL context, String spec, URLStreamHandler handler)
        {//throws MalformedURLException {
            if (handler != null)
            {
                java.lang.SecurityManager sm = java.lang.SystemJ.getSecurityManager();
                if (sm != null)
                {
                    sm.checkPermission(specifyStreamHandlerPermission);
                }
                strmHandler = handler;
            }

            if (spec == null)
            {
                throw new MalformedURLException();
            }
            spec = spec.trim();

            // The spec includes a protocol if it includes a colon character
            // before the first occurrence of a slash character. Note that,
            // "protocol" is the field which holds this URLs protocol.
            int index;
            try
            {
                index = spec.indexOf(':');
            }
            catch (java.lang.NullPointerException e)
            {
                throw new MalformedURLException(e.toString());
            }
            int startIPv6Addr = spec.indexOf('[');
            if (index >= 0)
            {
                if ((startIPv6Addr == -1) || (index < startIPv6Addr))
                {
                    protocol = spec.substring(0, index);
                    // According to RFC 2396 scheme part should match
                    // the following expression:
                    // alpha *( alpha | digit | "+" | "-" | "." )
                    char c = protocol.charAt(0);
                    bool valid = ('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z');
                    for (int i = 1; valid && (i < protocol.length()); i++)
                    {
                        c = protocol.charAt(i);
                        valid = ('a' <= c && c <= 'z') ||
                                ('A' <= c && c <= 'Z') ||
                                ('0' <= c && c <= '9') ||
                                (c == '+') ||
                                (c == '-') ||
                                (c == '.');
                    }
                    if (!valid)
                    {
                        protocol = null;
                        index = -1;
                    }
                    else
                    {
                        // Ignore case in protocol names.
                        // Scheme is defined by ASCII characters.
                        protocol = Util.toASCIILowerCase(protocol);
                    }
                }
            }

            if (protocol != null)
            {
                // If the context was specified, and it had the same protocol
                // as the spec, then fill in the receiver's slots from the values
                // in the context but still allow them to be over-ridden later
                // by the values in the spec.
                if (context != null && protocol.equals(context.getProtocol()))
                {
                    String cPath = context.getPath();
                    if (cPath != null && cPath.startsWith("/"))
                    { //$NON-NLS-1$
                        set(protocol, context.getHost(), context.getPort(), context
                                .getAuthority(), context.getUserInfo(), cPath,
                                context.getQuery(), null);
                    }
                    if (strmHandler == null)
                    {
                        strmHandler = context.strmHandler;
                    }
                }
            }
            else
            {
                // If the spec did not include a protocol, then the context
                // *must* be specified. Fill in the receiver's slots from the
                // values in the context, but still allow them to be over-ridden
                // by the values in the ("relative") spec.
                if (context == null)
                {
                    throw new MalformedURLException("Protocol not found: " + spec); //$NON-NLS-1$
                }
                set(context.getProtocol(), context.getHost(), context.getPort(),
                        context.getAuthority(), context.getUserInfo(), context
                                .getPath(), context.getQuery(), null);
                if (strmHandler == null)
                {
                    strmHandler = context.strmHandler;
                }
            }

            // If the stream handler has not been determined, set it
            // to the default for the specified protocol.
            if (strmHandler == null)
            {
                setupStreamHandler();
                if (strmHandler == null)
                {
                    throw new MalformedURLException("Unknown protocol: " + protocol); //$NON-NLS-1$
                }
            }

            // Let the handler parse the URL. If the handler throws
            // any exception, throw MalformedURLException instead.
            //
            // Note: We want "index" to be the index of the start of the scheme
            // specific part of the URL. At this point, it will be either
            // -1 or the index of the colon after the protocol, so we
            // increment it to point at either character 0 or the character
            // after the colon.
            try
            {
                strmHandler.parseURL(this, spec, ++index, spec.length());
            }
            catch (Exception e)
            {
                throw new MalformedURLException(e.toString());
            }

            if (port < -1)
            {
                throw new MalformedURLException("Port out of range: " + port); //$NON-NLS-1$
            }
        }

        /**
         * Creates a new URL instance using the given arguments. The URL uses the
         * default port for the specified protocol.
         * 
         * @param protocol
         *            the protocol of the new URL.
         * @param host
         *            the host name or IP address of the new URL.
         * @param file
         *            the name of the resource.
         * @throws MalformedURLException
         *             if the combination of all arguments do not represent a valid
         *             URL or the protocol is invalid.
         */
        public URL(String protocol, String host, String file)
            ://throws MalformedURLException {
        this(protocol, host, -1, file, (URLStreamHandler)null)
        {
        }

        /**
         * Creates a new URL instance using the given arguments. The URL uses the
         * specified port instead of the default port for the given protocol.
         * 
         * @param protocol
         *            the protocol of the new URL.
         * @param host
         *            the host name or IP address of the new URL.
         * @param port
         *            the specific port number of the URL. {@code -1} represents the
         *            default port of the protocol.
         * @param file
         *            the name of the resource.
         * @throws MalformedURLException
         *             if the combination of all arguments do not represent a valid
         *             URL or the protocol is invalid.
         */
        public URL(String protocol, String host, int port, String file)
            ://throws MalformedURLException {
        this(protocol, host, port, file, (URLStreamHandler)null)
        {
        }

        /**
         * Creates a new URL instance using the given arguments. The URL uses the
         * specified port instead of the default port for the given protocol.
         *
         * @param protocol
         *            the protocol of the new URL.
         * @param host
         *            the host name or IP address of the new URL.
         * @param port
         *            the specific port number of the URL. {@code -1} represents the
         *            default port of the protocol.
         * @param file
         *            the name of the resource.
         * @param handler
         *            the stream handler to be used by this URL.
         * @throws MalformedURLException
         *             if the combination of all arguments do not represent a valid
         *             URL or the protocol is invalid.
         * @throws SecurityException
         *             if {@code handler} is non-{@code null}, and a security
         *             manager is installed that disallows user-defined protocol
         *             handlers.
         */
        public URL(String protocol, String host, int port, String file,
                URLStreamHandler handler)
        {// throws MalformedURLException {
            if (port < -1)
            {
                throw new MalformedURLException("Port out of range: " + port); //$NON-NLS-1$
            }

            if (host != null && host.indexOf(":") != -1 && host.charAt(0) != '[')
            { //$NON-NLS-1$
                host = "[" + host + "]"; //$NON-NLS-1$ //$NON-NLS-2$
            }

            if (protocol == null)
            {
                throw new java.lang.NullPointerException("Unknown protocol: " + "null"); //$NON-NLS-1$ //$NON-NLS-2$
            }

            this.protocol = protocol;
            this.host = host;
            this.port = port;

            // Set the fields from the arguments. Handle the case where the
            // passed in "file" includes both a file and a reference part.
            int index = -1;
            index = file.indexOf("#", file.lastIndexOf("/")); //$NON-NLS-1$ //$NON-NLS-2$
            if (index >= 0)
            {
                this.file = file.substring(0, index);
                refJ = file.substring(index + 1);
            }
            else
            {
                this.file = file;
            }
            fixURL(false);

            // Set the stream handler for the URL either to the handler
            // argument if it was specified, or to the default for the
            // receiver's protocol if the handler was null.
            if (handler == null)
            {
                setupStreamHandler();
                if (strmHandler == null)
                {
                    throw new MalformedURLException("Unknown protocol: " + protocol); //$NON-NLS-1$
                }
            }
            else
            {
                java.lang.SecurityManager sm = java.lang.SystemJ.getSecurityManager();
                if (sm != null)
                {
                    sm.checkPermission(specifyStreamHandlerPermission);
                }
                strmHandler = handler;
            }
        }

        /// <summary>
        /// Same as openConnection.getInputStream ()
        /// </summary>
        /// <returns></returns>
        public java.io.InputStream openStream()
        {
            return openConnection().getInputStream();
        }

        public URLConnection openConnection()
        {
            return this.strmHandler.openConnection(this);
        }

        protected internal void set(String protocol, String host, int port, String file, String refJ)
        {
            if (this.protocol == null)
            {
                this.protocol = protocol;
            }
            this.host = host;
            this.file = file;
            this.port = port;
            this.refJ = refJ;
            hashCode = 0;
            fixURL(true);

        }
        /**
        * Sets the properties of this URL using the provided arguments. Only a
        * {@code URLStreamHandler} can use this method to set fields of the
        * existing URL instance. A URL is generally constant.
        * 
        * @param protocol
        *            the protocol to be set.
        * @param host
        *            the host name to be set.
        * @param port
        *            the port number to be set.
        * @param authority
        *            the authority to be set.
        * @param userInfo
        *            the user-info to be set.
        * @param path
        *            the path to be set.
        * @param query
        *            the query to be set.
        * @param ref
        *            the reference to be set.
        */
        protected internal void set(String protocol, String host, int port, String authority, String userInfo, String file, String query, String refJ)
        {
            String filePart = path;
            if (query != null && !query.equals(""))
            { //$NON-NLS-1$
                if (filePart != null)
                {
                    filePart = filePart + "?" + query; //$NON-NLS-1$
                }
                else
                {
                    filePart = "?" + query; //$NON-NLS-1$
                }
            }
            this.set(protocol, host, port, filePart, refJ);
            this.authority = authority;
            this.userInfo = userInfo;
            this.path = path;
            this.query = query;

        }
        public String getHost()
        {
            return this.host;
        }
        public String getProtocol()
        {
            return this.protocol;
        }
        public int getPort()
        {
            return this.port;
        }
        public String getRef()
        {
            return this.refJ;
        }
        public String getFile()
        {
            return this.file;
        }
        public String getPath()
        {
            return this.path;
        }
        public String getQuery()
        {
            return this.query;
        }
        public String getAuthority()
        {
            return this.authority;
        }
        public String getUserInfo()
        {
            return this.userInfo;
        }
        void fixURL(bool fixHost)
        {
            int index;
            if (host != null && host.length() > 0)
            {
                authority = host;
                if (port != -1)
                {
                    authority = authority + ":" + port; //$NON-NLS-1$
                }
            }
            if (fixHost)
            {
                if (host != null && (index = host.lastIndexOf('@')) > -1)
                {
                    userInfo = host.substring(0, index);
                    host = host.substring(index + 1);
                }
                else
                {
                    userInfo = null;
                }
            }
            if (file != null && (index = file.indexOf('?')) > -1)
            {
                query = file.substring(index + 1);
                path = file.substring(0, index);
            }
            else
            {
                query = null;
                path = file;
            }
        }
        /**
         * Sets the receiver's stream handler to one which is appropriate for its
         * protocol. Throws a MalformedURLException if no reasonable handler is
         * available.
         * <p/>
         * Note that this will overwrite any existing stream handler with the new
         * one. Senders must check if the strmHandler is null before calling the
         * method if they do not want this behavior (a speed optimization).
         */
        void setupStreamHandler()
        {
            String className = null;

            // Check for a cached (previously looked up) handler for
            // the requested protocol.
            strmHandler = streamHandlers.get(protocol);
            if (strmHandler != null)
            {
                return;
            }

            // If there is a stream handler factory, then attempt to
            // use it to create the handler.
            if (streamHandlerFactory != null)
            {
                strmHandler = streamHandlerFactory.createURLStreamHandler(protocol);
                if (strmHandler != null)
                {
                    streamHandlers.put(protocol, strmHandler);
                    return;
                }
            }

            // Check if there is a list of packages which can provide handlers.
            // If so, then walk this list looking for an applicable one.
            String packageList = java.security.AccessController
                    .doPrivileged(new PriviAction<String>(
                            "java.protocol.handler.pkgs")); //$NON-NLS-1$
            if (packageList != null)
            {
                java.util.StringTokenizer st = new java.util.StringTokenizer(packageList, "|"); //$NON-NLS-1$
                while (st.hasMoreTokens())
                {
                    className = st.nextToken() + "." + protocol + ".Handler"; //$NON-NLS-1$ //$NON-NLS-2$

                    try
                    {
                        strmHandler = (URLStreamHandler)java.lang.Class.forName(className,
                                true, java.lang.ClassLoader.getSystemClassLoader())
                                .newInstance();
                        if (strmHandler != null)
                        {
                            streamHandlers.put(protocol, strmHandler);
                        }
                        return;
                    }
                    catch (java.lang.IllegalAccessException e)
                    {
                    }
                    catch (java.lang.InstantiationException e)
                    {
                    }
                    catch (java.lang.ClassNotFoundException e)
                    {
                    }
                }
            }

            // No one else has provided a handler, so try our internal one.

            className = "org.apache.harmony.luni.internal.net.www.protocol." + protocol //$NON-NLS-1$
                    + ".Handler"; //$NON-NLS-1$
            try
            {
                strmHandler = (URLStreamHandler)java.lang.Class.forName(className)
                        .newInstance();
            }
            catch (java.lang.IllegalAccessException e)
            {
            }
            catch (java.lang.InstantiationException e)
            {
            }
            catch (java.lang.ClassNotFoundException e)
            {
            }
            if (strmHandler != null)
            {
                streamHandlers.put(protocol, strmHandler);
            }

        }

        /**
         * Returns a string containing a concise, human-readable representation of
         * this URL. The returned string is the same as the result of the method
         * {@code toExternalForm()}.
         * 
         * @return the string representation of this URL.
         */
        public override String ToString()
        {
            return toExternalForm();
        }

        /**
         * Returns a string containing a concise, human-readable representation of
         * this URL.
         * 
         * @return the string representation of this URL.
         */
        public String toExternalForm()
        {
            if (strmHandler == null)
            {
                return "unknown protocol(" + protocol + ")://" + host + file; //$NON-NLS-1$ //$NON-NLS-2$
            }
            return strmHandler.toExternalForm(this);
        }

    }
}