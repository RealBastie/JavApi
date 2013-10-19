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
 
namespace biz.ritter.javapi.io
{
	/**
 * Collects {@link FilePermission} objects and allows to query whether a
 * particular permission is implied by it.
 */
	[Serializable]
internal sealed class FilePermissionCollection : java.security.PermissionCollection,
        java.io.Serializable {

    private const long serialVersionUID = 2202956749081564585L;

    internal java.util.Vector<java.security.Permission> permissions = new java.util.Vector<java.security.Permission>();

    /**
     * Construct a new FilePermissionCollection
     */
    public FilePermissionCollection() :base(){
        
    }

    /**
     * Add a permission object to the permission collection.
     * 
     * @param permission
     *            the FilePermission object to add to the collection.
     * @throws IllegalArgumentException
     *             if {@code permission} is not an instance of
     *             {@code FilePermission}.
     * @throws IllegalStateException
     *             if this collection is read-only.
     * @see java.security.PermissionCollection#add(java.security.Permission)
     */
    
    public override void add(java.security.Permission permission) {
        if (isReadOnly()) {
            throw new java.lang.IllegalStateException();
        }
        if (permission is FilePermission) {
            permissions.addElement(permission);
        } else {
            throw new java.lang.IllegalArgumentException(permission.toString());
        }
    }

    /**
     * Returns an enumeration for the collection of permissions.
     * 
     * @return a permission enumeration for this permission collection.
     * @see java.security.PermissionCollection#elements()
     */
    
    public override java.util.Enumeration<java.security.Permission> elements() {
        return permissions.elements();
    }

    /**
     * Indicates whether this permissions collection implies a specific
     * {@code permission}.
     * 
     * @param permission
     *            the permission to check.
     * @see java.security.PermissionCollection#implies(java.security.Permission)
     */
    
    public override bool implies(java.security.Permission permission) {
        if (permission is FilePermission) {
            FilePermission fp = (FilePermission) permission;
            int matchedMask = 0;
            int i = 0;
            while (i < permissions.size()
                    && ((matchedMask & fp.mask) != fp.mask)) {
                // Cast will not fail since we added it
                matchedMask |= ((FilePermission) permissions.elementAt(i))
                        .impliesMask(permission);
                i++;
            }
            return ((matchedMask & fp.mask) == fp.mask);
        }
        return false;
    }
	}}
