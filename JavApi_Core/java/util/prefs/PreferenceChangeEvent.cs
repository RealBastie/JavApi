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
     * This is the event class to indicate that a preference has been added, deleted
     * or updated.
     * <p>
     * Please note that although the class is marked as {@code Serializable} by
     * inheritance from {@code EventObject}, this type is not intended to be serialized
     * so the serialization methods do nothing but throw a {@code NotSerializableException}.
     *
     * @see java.util.prefs.Preferences
     * @see java.util.prefs.PreferenceChangeListener
     *
     * @since 1.4
     */
    public class PreferenceChangeEvent : java.util.EventObject
    {

        private const long serialVersionUID = 793724513368024975L;

        private readonly Preferences node;

        private readonly String key;

        private readonly String value;

        /**
         * Construct a new {@code PreferenceChangeEvent} instance.
         *
         * @param p
         *            the {@code Preferences} instance that fired this event; this object is
         *            considered as the event's source.
         * @param k
         *            the changed preference key.
         * @param v
         *            the new value of the changed preference, this value can be
         *            {@code null}, which means the preference has been removed.
         */
        public PreferenceChangeEvent(Preferences p, String k, String v) :
            base(p)
        {
            node = p;
            key = k;
            value = v;
        }

        /**
         * Gets the key of the changed preference.
         *
         * @return the changed preference's key.
         */
        public String getKey()
        {
            return key;
        }

        /**
         * Gets the new value of the changed preference or {@code null} if the
         * preference has been removed.
         *
         * @return the new value of the changed preference or {@code null} if the
         *         preference has been removed.
         */
        public String getNewValue()
        {
            return value;
        }

        /**
         * Gets the {@code Preferences} instance that fired this event.
         *
         * @return the {@code Preferences} instance that fired this event.
         */
        public Preferences getNode()
        {
            return node;
        }

        /**
         * This method always throws a {@code NotSerializableException},
         * because this object cannot be serialized,
         */
        private void writeObject(java.io.ObjectOutputStream outJ)
        {//throws IOException {
            throw new java.io.NotSerializableException();
        }

        /**
         * This method always throws a {@code NotSerializableException},
         * because this object cannot be serialized,
         */
        private void readObject(java.io.ObjectInputStream inJ)
        {//throws IOException{
            throw new java.io.NotSerializableException();
        }
    }
}