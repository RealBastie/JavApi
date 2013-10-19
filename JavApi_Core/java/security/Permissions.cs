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
 * {@code Permissions} represents a {@code PermissionCollection} where the
 * contained permissions can be of different types. The permissions are
 * organized in their appropriate {@code PermissionCollection} obtained by
 * {@link Permission#newPermissionCollection()}. For permissions which do not
 * provide a dedicated {@code PermissionCollection}, a default permission
 * collection, based on a hash table, will be used.
 */
    [Serializable]
public sealed class Permissions : PermissionCollection,
    java.io.Serializable {

    private static readonly long serialVersionUID = 4858622370623524688L;
/*
    private static readonly ObjectStreamField[] serialPersistentFields = {
        new ObjectStreamField("perms", Hashtable.class), //$NON-NLS-1$
        new ObjectStreamField("allPermission", PermissionCollection.class), }; //$NON-NLS-1$
*/
    // Hash to store PermissionCollection's
        [NonSerialized]
    private java.util.HashMap<java.lang.Class,PermissionCollection> klasses = new java.util.HashMap<java.lang.Class,PermissionCollection>();

    private bool allEnabled;  // = false;

    /**
     * Adds the given {@code Permission} to this heterogeneous {@code
     * PermissionCollection}. The {@code permission} is stored in its
     * appropriate {@code PermissionCollection}.
     *
     * @param permission
     *            the {@code Permission} to be added.
     * @throws SecurityException
     *             if this collection's {@link #isReadOnly()} method returns
     *             {@code true}.
     * @throws NullPointerException
     *             if {@code permission} is {@code null}.
     */
    public override void add(Permission permission) {
        if (isReadOnly()) {
            throw new java.lang.SecurityException("collection is read-only"); //$NON-NLS-1$
        }

        if (permission == null) {
            throw new java.lang.NullPointerException("invalid null permission"); //$NON-NLS-1$
        }

        java.lang.Class klass = permission.getClass();
        PermissionCollection klassMates = (PermissionCollection)klasses
            .get(klass);

        if (klassMates == null) {
            lock (klasses) {
                klassMates = (PermissionCollection)klasses.get(klass);
                if (klassMates == null) {

                    klassMates = permission.newPermissionCollection();
                    if (klassMates == null) {
                        klassMates = new PermissionsHash();
                    }
                    klasses.put(klass, klassMates);
                }
            }
        }
        klassMates.add(permission);

        if (klass == typeof(AllPermission).getClass()) {
            allEnabled = true;
        }
    }

    public override java.util.Enumeration<Permission> elements() {
        return new MetaEnumeration(klasses.values().iterator());
    }

    /**
     * An auxiliary implementation for enumerating individual permissions from a
     * collection of PermissionCollections.
     * 
     */
    sealed class MetaEnumeration : java.util.Enumeration<Permission> {

        private java.util.Iterator<PermissionCollection> pcIter;

        private java.util.Enumeration<Permission> current;

        /**
         * Initiates this enumeration.
         * 
         * @param outer an iterator over external collection of
         *        PermissionCollections
         */
        public MetaEnumeration(java.util.Iterator<PermissionCollection> outer) {
            pcIter = outer;
            current = getNextEnumeration();
        }

        private java.util.Enumeration<Permission> getNextEnumeration() {
            while (pcIter.hasNext()) {
                java.util.Enumeration<Permission> en = ((PermissionCollection)pcIter.next()).elements();
                if (en.hasMoreElements()) {
                    return en;
                }
            }
            return null;
        }

        /**
         * Indicates if there are more elements to enumerate.
         */
        public bool hasMoreElements() {
            return current != null /* && current.hasMoreElements() */;
        }

        /**
         * Returns next element.
         */
        public Permission nextElement() {
            if (current != null) {
                //assert current.hasMoreElements();
                Permission next = current.nextElement();
                if (!current.hasMoreElements()) {
                    current = getNextEnumeration();
                }

                return next;
            }
            throw new java.util.NoSuchElementException("no more elements"); //$NON-NLS-1$
        }
    }

    public override bool implies(Permission permission) {
        if (permission == null) {
            // RI compatible
            throw new java.lang.NullPointerException("Null permission"); //$NON-NLS-1$
        }
        if (allEnabled) {
            return true;
        }
        java.lang.Class klass = permission.getClass();
        PermissionCollection klassMates = null;

        UnresolvedPermissionCollection billets = (UnresolvedPermissionCollection)klasses
            .get(typeof(UnresolvedPermission).getClass());
        if (billets != null && billets.hasUnresolved(permission)) {
            // try to fill up klassMates with freshly resolved permissions
            lock (klasses) {
                klassMates = (PermissionCollection)klasses.get(klass);
                try {
                    klassMates = billets.resolveCollection(permission,
                                                           klassMates);
                } catch (java.lang.Exception ignore) {
                    //TODO log warning
                    ignore.printStackTrace();
                }

                if (klassMates != null) {
                    //maybe klassMates were just created
                    // so put them into common map
                    klasses.put(klass, klassMates);
                    // very uncommon case, but not improbable one
                    if (klass == typeof(AllPermission).getClass()) {
                        allEnabled = true;
                    }
                }
            }
        } else {
            klassMates = (PermissionCollection)klasses.get(klass);
        }

        if (klassMates != null) {
            return klassMates.implies(permission);
        }
        return false;
    }

    /* *
     * Reads the object from stream and checks for consistency.
     */
/*    private void readObject(java.io.ObjectInputStream in) throws IOException,
        ClassNotFoundException {
        ObjectInputStream.GetField fields = in.readFields();
        Map perms = (Map)fields.get("perms", null); //$NON-NLS-1$
        klasses = new HashMap();
        synchronized (klasses) {
            for (Iterator iter = perms.entrySet().iterator(); iter.hasNext();) {
                Map.Entry entry = (Map.Entry)  iter.next();
                Class key = (Class) entry.getKey();
                PermissionCollection pc = (PermissionCollection) entry.getValue();
                if (key != pc.elements().nextElement().getClass()) {
                    throw new InvalidObjectException(Messages.getString("security.22")); //$NON-NLS-1$
                }
                klasses.put(key, pc);
            }
        }
        allEnabled = fields.get("allPermission", null) != null; //$NON-NLS-1$
        if (allEnabled && !klasses.containsKey(AllPermission.class)) {
            throw new InvalidObjectException(Messages.getString("security.23")); //$NON-NLS-1$
        }
    }

    /**
     * Outputs fields via default mechanism.
     *
    private void writeObject(java.io.ObjectOutputStream out) throws IOException {
        ObjectOutputStream.PutField fields = out.putFields();
        fields.put("perms", new Hashtable(klasses)); //$NON-NLS-1$
        fields.put("allPermission", allEnabled ? klasses //$NON-NLS-1$
            .get(AllPermission.class) : null);
        out.writeFields();
    }*/
}
}
