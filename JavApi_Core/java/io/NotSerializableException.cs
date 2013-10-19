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
     * Signals that an object that is not serializable has been passed into the
     * {@code ObjectOutput.writeObject()} method. This can happen if the object
     * does not implement {@code Serializable} or {@code Externalizable}, or if it
     * is serializable but it overrides {@code writeObject(ObjectOutputStream)} and
     * explicitly prevents serialization by throwing this type of exception.
     * 
     * @see ObjectOutput#writeObject(Object)
     * @see ObjectOutputStream#writeObject(Object)
     */
    [Serializable]
    public class NotSerializableException : ObjectStreamException
    {

        private const long serialVersionUID = 2906642554793891381L;

        /**
         * Constructs a new {@code NotSerializableException} with its stack trace
         * filled in.
         */
        public NotSerializableException()
            : base()
        {
        }

        /**
         * Constructs a new {@link NotSerializableException} with its stack trace
         * and detail message filled in.
         * 
         * @param detailMessage
         *            the detail message for this exception.
         */
        public NotSerializableException(String detailMessage) :
            base(detailMessage)
        {
        }
    }
}