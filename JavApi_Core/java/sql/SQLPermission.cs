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
     * A Permission relating to security access control in the {@code java.sql}
     * package.
     * <p>
     * Currently, the only permission supported has the name " {@code setLog}". The
     * {@code setLog} permission controls whether a Java application or applet can
     * open a logging stream using the {@code DriverManager.setLogWriter} method or
     * the {@code DriverManager.setLogStream} method. This is a potentially
     * dangerous operation since the logging stream can contain sensitive
     * information such as usernames and passwords.
     * 
     * @see DriverManager
     */
    public sealed class SQLPermission : java.security.BasicPermission, java.security.Guard,
            java.io.Serializable
    {

        private const long serialVersionUID = -1439323187199563495L;

        /**
         * Creates a new {@code SQLPermission} object with the specified name.
         * 
         * @param name
         *            the name to use for this {@code SQLPermission}.
         */
        public SQLPermission(String name)
            : base(name)
        {

        }

        /**
         * Creates a new {@code SQLPermission} object with the specified name.
         * 
         * @param name
         *            is the name of the {@code SQLPermission}. Currently only
         *            {@code "setLog"} is allowed.
         * @param actions
         *            is currently unused and should be set to {@code null}.
         */
        public SQLPermission(String name, String actions)
            : base(name, null)
        {

        }
    }
}
