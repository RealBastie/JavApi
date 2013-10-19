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
     * A default {@code PermissionCollection} implementation that uses a hashtable.
     * Each hashtable entry stores a Permission object as both the key and the
     * value.
     * <p/>
     * This {@code PermissionCollection} is intended for storing &quot;neutral&quot;
     * permissions which do not require special collection.
     */
    [Serializable]
    internal sealed class PermissionsHash : PermissionCollection
    {

        private static readonly long serialVersionUID = -8491988220802933440L;

        private readonly java.util.Hashtable<Permission, Permission> perms = new java.util.Hashtable<Permission, Permission>();

        /**
         * Adds the argument to the collection.
         *
         * @param permission
         *            the permission to add to the collection.
         */
        public override void add(Permission permission)
        {
            perms.put(permission, permission);
        }

        /**
         * Returns an enumeration of the permissions in the receiver.
         *
         * @return Enumeration the permissions in the receiver.
         */
        public override java.util.Enumeration<Permission> elements()
        {
            return perms.elements();
        }

        /**
         * Indicates whether the argument permission is implied by the permissions
         * contained in the receiver.
         *
         * @return boolean <code>true</code> if the argument permission is implied
         *         by the permissions in the receiver, and <code>false</code> if
         *         it is not.
         * @param permission
         *            java.security.Permission the permission to check
         */
        public override bool implies(Permission permission)
        {
            for (java.util.Enumeration<Permission> elementsJ = elements(); elementsJ.hasMoreElements(); )
            {
                if (((Permission)elementsJ.nextElement()).implies(permission))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
