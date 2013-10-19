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
     * @author Pavel Dolgov
     */

    /**
     * ReadOnlyIterator
     */
    internal sealed class ReadOnlyIterator<E> : java.util.Iterator<E>
    {

        private readonly java.util.Iterator<E> it;

        public ReadOnlyIterator(java.util.Iterator<E> it)
        {
            if (it == null)
            {
                throw new java.lang.NullPointerException();
            }
            this.it = it;
        }

        public void remove()
        {
            // awt.50=Iterator is read-only
            throw new java.lang.UnsupportedOperationException("Iterator is read-only"); //$NON-NLS-1$
        }

        public bool hasNext()
        {
            return it.hasNext();
        }

        public E next()
        {
            return it.next();
        }
    }
}