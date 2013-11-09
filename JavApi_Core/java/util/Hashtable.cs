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
 *  
 *  Copyright © 2013 Sebastian Ritter
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using java = biz.ritter.javapi;

namespace biz.ritter.javapi.util
{
    /// <summary>
    /// <strong>In change to Java this class extends java.util.AbstractMap and implements
    /// java.util.Dictionary.</strong>
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class Hashtable<K,V> : AbstractMap<K,V>, Dictionary<K,V>
    {
        public Hashtable() : base()
        {
        }

		public Hashtable(int defaultSize) : base () {}

        public override void clear()
        {
            lock (this)
            {
                base.clear();
            }
        }
        public override bool containsKey(object key)
        {
            lock (this)
            {
                return base.containsKey(key);
            }
        }
        public override bool containsValue(object value)
        {
            lock (this)
            {
                return base.containsValue(value);
            }
        }
        public override Set<MapNS.Entry<K, V>> entrySet()
        {
            lock (this)
            {
                return base.entrySet();
            }
        }
        public override V get(object key)
        {
            lock (this)
            {
                return base.get(key);
            }
        }
        public override bool isEmpty()
        {
            lock (this)
            {
                return base.isEmpty();
            }
        }
        public override Set<K> keySet()
        {
            lock (this)
            {
                return base.keySet();
            }
        }
        public override V put(K key, V value)
        {
            lock (this)
            {
                return base.put(key, value);
            }
        }
        public override void putAll(Map<K, V> map)
        {
            lock (this)
            {
                base.putAll(map);
            }
        }
        public override V remove(object key)
        {
            lock (this)
            {
                return base.remove(key);
            }
        }
        public override int size()
        {
            lock (this)
            {
                return base.size();
            }
        }
        public override Collection<V> values()
        {
            lock (this)
            {
                return base.values();
            }
        }
        public virtual Enumeration<K> keys()
        {
            lock (this)
            {
                return new dotnet.util.wrapper.EnumeratorWrapper<K>(this.Keys.AsEnumerable().GetEnumerator());
            }
        }
        public virtual Enumeration<V> elements()
        {
            lock (this)
            {
                return new dotnet.util.wrapper.EnumeratorWrapper<V>(this.Values.AsEnumerable());
            }
        }
    }
}
