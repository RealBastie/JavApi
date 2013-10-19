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

namespace org.apache.harmony.awt
{

    /**
     * List of AWT listeners. It is for 3 purposes.
     * 1. To support list modification from listeners
     * 2. To ensure call for all listeners as atomic operation
     * 3. To support system listeners that are needed for built-in AWT components
     */
    [Serializable]
    internal class ListenerList<T> : java.io.Serializable
    {
        private const long serialVersionUID = 9180703263299648154L;

        [NonSerialized]
        private java.util.ArrayList<T> systemList;
        private java.util.ArrayList<T> userList;

        public ListenerList()
            : base()
        {
        }

        /**
         * Adds system listener to this list.
         *
         * @param listener - listener to be added.
         */
        public void addSystemListener(T listener)
        {
            if (systemList == null)
            {
                systemList = new java.util.ArrayList<T>();
            }
            systemList.add(listener);
        }

        /**
         * Adds user (public) listener to this list.
         *
         * @param listener - listener to be added.
         */
        public void addUserListener(T listener)
        {
            if (listener == null)
            {
                return;
            }
            // transactionally replace old list
            lock (this)
            {
                if (userList == null)
                {
                    userList = new java.util.ArrayList<T>();
                    userList.add(listener);
                    return;
                }
                java.util.ArrayList<T> newList = new java.util.ArrayList<T>(userList);
                newList.add(listener);
                userList = newList;
            }
        }

        /**
         * Removes user (public) listener to this list.
         *
         * @param listener - listener to be removed.
         */
        public void removeUserListener(Object listener)
        {
            if (listener == null)
            {
                return;
            }
            // transactionally replace old list
            lock (this)
            {
                if (userList == null || !userList.contains(listener))
                {
                    return;
                }
                java.util.ArrayList<T> newList = new java.util.ArrayList<T>(userList);
                newList.remove(listener);
                userList = (newList.size() > 0 ? newList : null);
            }
        }

        /**
         * Gets all user (public) listeners in one array.
         *
         * @param emptyArray - empty array, it's for deriving particular listeners class.
         * @return array of all user listeners.
         */
        public AT[] getUserListeners<AT>(AT[] emptyArray)
        {
            lock (this)
            {
                return (userList != null ? userList.toArray(emptyArray) : emptyArray);

            }
        }

        /**
         * Gets all user (public) listeners in one list.
         *
         * @return list of all user listeners.
         */
        public java.util.List<T> getUserListeners()
        {
            lock (this)
            {
                if (userList == null || userList.isEmpty())
                {
                    return java.util.Collections<T>.emptyList();
                }
                return new java.util.ArrayList<T>(userList);
            }
        }

        public java.util.List<T> getSystemListeners()
        {
            lock (this)
            {
                if (systemList == null || systemList.isEmpty())
                {
                    return java.util.Collections<T>.emptyList();
                }
                return new java.util.ArrayList<T>(systemList);
            }
        }

        /**
         * Gets iterator for user listeners.
         *
         * @return iterator for user listeners.
         */
        public java.util.Iterator<T> getUserIterator()
        {
            lock (this)
            {
                if (userList == null)
                {
                    java.util.List<T> emptyList = java.util.Collections<T>.emptyList();
                    return emptyList.iterator();
                }
                return new ReadOnlyIterator<T>(userList.iterator());
            }
        }

        /**
         * Gets iterator for system listeners.
         *
         * @return iterator for system listeners.
         */
        public java.util.Iterator<T> getSystemIterator()
        {
            return systemList.iterator();
        }

        private static java.util.ArrayList<Object> getOnlySerializable(java.util.ArrayList<T> list)
        {
            if (list == null)
            {
                return null;
            }

            java.util.ArrayList<Object> result = new java.util.ArrayList<Object>();
            for (java.util.Iterator<T> it = list.iterator(); it.hasNext(); )
            {
                Object obj = it.next();
                if (obj is java.io.Serializable)
                {
                    result.add(obj);
                }
            }

            return (result.size() != 0) ? result : null;
        }

        private void writeObject(java.io.ObjectOutputStream stream)
        {//throws IOException {

            stream.defaultWriteObject();

            stream.writeObject(getOnlySerializable(systemList));
            stream.writeObject(getOnlySerializable(userList));
        }

        private void readObject(java.io.ObjectInputStream stream)
        {//throws IOException, ClassNotFoundException {

            stream.defaultReadObject();

            systemList = (java.util.ArrayList<T>)stream.readObject();
            userList = (java.util.ArrayList<T>)stream.readObject();
        }

    }
}
