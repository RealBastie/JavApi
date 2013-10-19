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

namespace biz.ritter.javapi.util.prefs
{

    /**
     * This is the event class to indicate that one child of the preference node has
     * been added or deleted.
     * <p/>
     * Please note that although the class is marked as {@code Serializable} by
     * inheritance from {@code EventObject}, this type is not intended to be
     * serialized so the serialization methods do nothing but throw a {@code
     * NotSerializableException}.
     * 
     * @see java.util.prefs.Preferences
     * @see java.util.prefs.NodeChangeListener
     * 
     * @since 1.4
     */
    [Serializable]
    public class NodeChangeEvent : java.util.EventObject
    {

        private const long serialVersionUID = 8068949086596572957L;

        private readonly Preferences parent;
        private readonly Preferences child;

        /**
         * Constructs a new {@code NodeChangeEvent} instance.
         * 
         * @param p
         *            the {@code Preferences} instance that fired this event; this
         *            object is considered as the event source.
         * @param c
         *            the child {@code Preferences} instance that was added or
         *            deleted.
         */
        public NodeChangeEvent(Preferences p, Preferences c) :
            base(p)
        {
            parent = p;
            child = c;
        }

        /**
         * Gets the {@code Preferences} instance that fired this event.
         * 
         * @return the {@code Preferences} instance that fired this event.
         */
        public Preferences getParent()
        {
            return parent;
        }

        /**
         * Gets the child {@code Preferences} node that was added or removed.
         * 
         * @return the added or removed child {@code Preferences} node.
         */
        public Preferences getChild()
        {
            return child;
        }

        /**
         * This method always throws a {@code NotSerializableException}, because
         * this object cannot be serialized,
         */
        private void writeObject(java.io.ObjectOutputStream outJ)
        {//throws IOException {
            throw new java.io.NotSerializableException();
        }

        /**
         * This method always throws a {@code NotSerializableException}, because
         * this object cannot be serialized,
         */
        private void readObject(java.io.ObjectInputStream inJ)
        {//throws IOException,
            //ClassNotFoundException {
            throw new java.io.NotSerializableException();
        }
    }
}