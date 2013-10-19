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

namespace biz.ritter.javapi.lang
{
    public class SecurityManager
    {
    /**
     * Flag to indicate whether a security check is in progress.
     * 
     * @deprecated Use {@link #checkPermission}
     */
        [Obsolete]
    protected bool inCheck;

        public void checkSecurityAccess(String target)
        {
            if (null == target) throw new java.lang.NullPointerException();
            if (0 == target.length()) throw new IllegalArgumentException();

            // Here can be some security checks and if it fails, throw a new SecurityException
        }
        /**
         * Checks whether the calling thread is allowed to access the resource being
         * guarded by the specified permission object.
         *
         * @param permission
         *            the permission to check.
         * @throws SecurityException
         *             if the requested {@code permission} is denied according to
         *             the current security policy.
         */
        public void checkPermission(java.security.Permission permission)
        {
            try
            {
                inCheck = true;
                java.security.AccessController.checkPermission(permission);
            }
            finally
            {
                inCheck = false;
            }
        }

    }
}
