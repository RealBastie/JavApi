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

namespace biz.ritter.javapi.lang
{

    /**
     * Implements a thread-local storage, that is, a variable for which each thread
     * has its own value. All threads share the same {@code ThreadLocal} object,
     * but each sees a different value when accessing it, and changes made by one
     * thread do not affect the other threads. The implementation supports
     * {@code null} values.
     *
     * @see java.lang.Thread
     */
    public class ThreadLocal<T>
    {
        /**
         * Creates a new thread-local variable.
         */
        public ThreadLocal()
            : base()
        {
        }

        /**
         * Returns the value of this variable for the current thread. If an entry
         * doesn't yet exist for this variable on this thread, this method will
         * create an entry, populating the value with the result of
         * {@link #initialValue()}.
         *
         * @return the current value of the variable for the calling thread.
         */
        public T get()
        {
            ThreadLocal<Object> tlo = (ThreadLocal<Object>)(Object)this;
            return (T)Thread.currentThread().getThreadLocal(tlo);
        }

        /**
         * Provides the initial value of this variable for the current thread.
         * The default implementation returns {@code null}.
         *
         * @return the initial value of the variable.
         */
        protected T initialValue()
        {
            return default(T);
        }

        /**
         * Sets the value of this variable for the current thread. If set to
         * {@code null}, the value will be set to null and the underlying entry will
         * still be present.
         *
         * @param value the new value of the variable for the caller thread.
         */
        public void set(T value)
        {
            ThreadLocal<Object> tlo = (ThreadLocal<Object>) (Object)this;
            Thread.currentThread().setThreadLocal(tlo, value);
        }

        /**
         * Removes the entry for this variable in the current thread. If this call
         * is followed by a {@link #get()} before a {@link #set},
         * {@code #get()} will call {@link #initialValue()} and create a new
         * entry with the resulting value.
         *
         * @since 1.5
         */
        public void remove()
        {
            ThreadLocal<Object> tlo = (ThreadLocal<Object>)(Object)this;
            Thread.currentThread().setThreadLocal(tlo, initialValue());
        }
    }
}