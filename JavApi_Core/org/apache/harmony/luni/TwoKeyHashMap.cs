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

using org.apache.harmony.luni.util;

namespace org.apache.harmony.luni
{

    /**
     * 
     * Reductive hash with two keys
     * 
     */
    public class TwoKeyHashMap<E, K, V> : java.util.AbstractMap<String, V>
    {

        static readonly float DEFAULT_LOAD_FACTOR = 0.75f;
        static readonly int DEFAULT_INITIAL_SIZE = 16;

        private java.util.Set<java.util.MapNS.Entry<String, V>> entrySetJ;
        private java.util.Collection<V> valuesJ;
        private int sizeJ;
        private int arrSize;
        private int modCount;

        private Entry<E, K, V>[] arr;

        private float loadFactor;
        int threshold = 0;

        /**
         * Constructs an empty HashMap
         */
        public TwoKeyHashMap() :
            this(DEFAULT_INITIAL_SIZE, DEFAULT_LOAD_FACTOR)
        {
        }

        /**
         * Constructs an empty HashMap
         * 
         * @param initialCapacity
         */
        public TwoKeyHashMap(int initialCapacity) :
            this(initialCapacity, DEFAULT_LOAD_FACTOR)
        {
        }

        /**
         * Constructs an empty HashMap
         * 
         * @param initialCapacity
         * @param initialLoadFactor
         */
        public TwoKeyHashMap(int initialCapacity, float initialLoadFactor)
        {
            if (initialCapacity < 0)
            {
                throw new java.lang.IllegalArgumentException("initialCapacity should be >= 0");
            }
            if (initialLoadFactor <= 0)
            {
                throw new java.lang.IllegalArgumentException(
                        "initialLoadFactor should be > 0");
            }
            loadFactor = initialLoadFactor;
            if (initialCapacity == java.lang.Integer.MAX_VALUE)
            {
                initialCapacity--;
            }
            arrSize = initialCapacity > 0 ? initialCapacity : 1;
            threshold = (int)(arrSize * loadFactor);
            arr = new Entry<E, K, V>[arrSize + 1];
        }

        /**
         * Returns a collection view of the values
         */
        public override java.util.Collection<V> values()
        {
            if (valuesJ == null)
            {
                valuesJ = new ValuesCollectionImpl(this);
            }
            return valuesJ;
        }

        /**
         * Returns a collection view of the mappings
         */
        public override java.util.Set<java.util.MapNS.Entry<String, V>> entrySet()
        {
            if (entrySetJ == null)
            {
                entrySetJ = new EntrySetImpl(this);
            }
            return entrySetJ;
        }

        /**
         * Clears the map
         */
        public override void clear()
        {
            modCount++;
            sizeJ = 0;
            java.util.Arrays<Object>.fill(arr, 0, arr.Length, null);
        }

        /**
         * Removes the mapping for the keys
         * 
         * @param key1
         * @param key2
         * @return
         */
        public V remove(Object key1, Object key2)
        {
            Entry<E, K, V> e = removeEntry(key1, key2);
            return (V)(null != e ? e.value : default(V));
        }

        /**
         * Associates the specified value with the specified keys in this map
         * 
         * @param key1
         * @param key2
         * @param value
         * @return
         */
        public V put(E key1, K key2, V value)
        {
            int index = 0;
            if (key1 == null && key2 == null)
            {
                index = arrSize;
                if (arr[index] == null)
                {
                    arr[index] = createEntry(0, default(E), default(K), value, null);
                    sizeJ++;
                    modCount++;
                    return default(V);
                }
                else
                {
                    V oldValue = arr[index].value;
                    arr[index].value = value;
                    return oldValue;
                }
            }

            int hash = key1.GetHashCode() + key2.GetHashCode();
            index = (hash & 0x7fffffff) % arrSize;
            Entry<E, K, V> e = arr[index];

            while (e != null)
            {
                if (hash == e.hash && key1.equals(e.getKey1())
                        && key2.equals(e.getKey2()))
                {
                    V oldValue = e.value;
                    e.value = value;
                    return oldValue;
                }
                e = e.next;
            }

            arr[index] = createEntry(hash, key1, key2, value, arr[index]);
            sizeJ++;
            modCount++;

            if (sizeJ > threshold)
            {
                rehash();
            }
            return default(V);
        }

        /**
         * Rehash the map
         * 
         */
        void rehash()
        {
            int newArrSize = (arrSize + 1) * 2 + 1;
            if (newArrSize < 0)
            {
                newArrSize = java.lang.Integer.MAX_VALUE - 1;
            }
            Entry<E, K, V>[] newArr = new Entry<E,K,V>[newArrSize + 1];

            for (int i = 0; i < arr.Length - 1; i++)
            {
                Entry<E, K, V> entry = arr[i];
                while (entry != null)
                {
                    Entry<E, K, V> next = entry.next;

                    int newIndex = (entry.hash & 0x7fffffff) % newArrSize;
                    entry.next = newArr[newIndex];
                    newArr[newIndex] = entry;

                    entry = next;
                }
            }
            newArr[newArrSize] = arr[arrSize]; // move null entry
            arrSize = newArrSize;

            // The maximum array size is reached, increased loadFactor
            // will keep array from further growing
            if (arrSize == java.lang.Integer.MAX_VALUE)
            {
                loadFactor *= 10;
            }
            threshold = (int)(arrSize * loadFactor);
            arr = newArr;
        }

        /**
         * Answers whether this map contains a mapping for the specified keys.
         * 
         * @param key1 first key
         * @param key2 second key
         * @return true if this map contains a mapping for the specified keys, and
         *         false otherwise.
         */
        public bool containsKey(Object key1, Object key2)
        {
            return findEntry(key1, key2) != null;
        }

        /**
         * Return the value by keys
         * 
         * @param key1
         * @param key2
         * @return
         */
        public V get(Object key1, Object key2)
        {
            Entry<E, K, V> e = findEntry(key1, key2);
            if (e != null)
            {
                return e.value;
            }
            return default(V);
        }

        /**
         * Returns true if this map contains no key-value mappings
         */
        public override bool isEmpty()
        {
            return sizeJ == 0;
        }

        /**
         * Returns the number of mappings
         */
        public override int size()
        {
            return sizeJ;
        }

        /**
         * Creates new entry
         * 
         * @param hashCode
         * @param key1
         * @param key2
         * @param value
         * @param next
         * @return
         */
        Entry<E, K, V> createEntry(int hashCode, E key1, K key2, V value,
                Entry<E, K, V> next)
        {
            return new Entry<E, K, V>(hashCode, key1, key2, value, next);
        }

        /**
         * Creates entries iterator
         * 
         * @return
         */
        java.util.Iterator<java.util.MapNS.Entry<String, V>> createEntrySetIterator()
        {
            return new EntryIteratorImpl(this);
        }

        /**
         * Creates values iterator
         * 
         * @return
         */
        java.util.Iterator<V> createValueCollectionIterator()
        {
            return new ValueIteratorImpl(this);
        }

        /**
         * Entry implementation for the TwoKeyHashMap class
         * 
         */
        public class Entry<E, K, V> : java.util.MapNS.Entry<String, V>
        {
            internal int hash;
            E key1;
            K key2;
            internal V value;
            internal Entry<E, K, V> next;

            public Entry(int hash, E key1, K key2, V value, Entry<E, K, V> next)
            {
                this.hash = hash;
                this.key1 = key1;
                this.key2 = key2;
                this.value = value;
                this.next = next;
            }

            public String getKey()
            {
                return key1.toString() + key2.toString();
            }

            public E getKey1()
            {
                return key1;
            }

            public K getKey2()
            {
                return key2;
            }

            public V getValue()
            {
                return value;
            }

            public V setValue(V value)
            {
                V oldValue = this.value;
                this.value = value;
                return oldValue;
            }

            public bool equals(Object obj)
            {
                if (!(obj is Entry<E, K, V>))
                {
                    return false;
                }

                Entry<Object, Object, Object> e = (Entry<Object, Object, Object>)obj;
                Object getKey1 = e.getKey1();
                Object getKey2 = e.getKey2();
                Object getValue = e.getValue();
                if ((key1 == null && getKey1 != null)
                        || (key2 == null && getKey2 != null)
                        || (value == null && getValue != null)
                        || !key1.equals(e.getKey1()) || !key2.equals(e.getKey2())
                        || !value.equals(getValue))
                {
                    return false;
                }
                return true;
            }

            public override int GetHashCode()
            {
                int hash1 = (key1 == null ? 0 : key1.GetHashCode());
                int hash2 = (key2 == null ? 0 : key2.GetHashCode());
                return (hash1 + hash2) ^ (value == null ? 0 : value.GetHashCode());
            }

        }

        class EntrySetImpl : java.util.AbstractSet<java.util.MapNS.Entry<String, V>>
        {

            private readonly TwoKeyHashMap<E, K, V> root;

            public EntrySetImpl(TwoKeyHashMap<E, K, V> root)
            {
                this.root = root;
            }

            public override int size()
            {
                return root.sizeJ;
            }

            public override void clear()
            {
                root.clear();
            }

            public override bool isEmpty()
            {
                return root.sizeJ == 0;
            }

            public override bool contains(Object obj)
            {
                if (!(obj is Entry<E, K, V>))
                {
                    return false;
                }

                Entry<Object, Object, Object> entry = (Entry<Object, Object, Object>)obj;
                Entry<E, K, V> entry2 = root.findEntry(entry.getKey1(), entry.getKey2());
                if (entry2 == null)
                {
                    return false;
                }
                Object value = entry.getValue();
                Object value2 = entry2.getValue();
                return value == null ? value2 == null : value.equals(value2);
            }

            public override bool remove(Object obj)
            {
                if (!(obj is Entry<E, K, V>))
                {
                    return false;
                }
                return root.removeEntry(((Entry<E, K, V>)obj).getKey1(), ((Entry<E, K, V>)obj).getKey2()) != null;
            }

            public override java.util.Iterator<java.util.MapNS.Entry<String, V>> iterator()
            {
                return root.createEntrySetIterator();
            }
        }

        // Iterates Entries inside the Map
        class EntryIteratorImpl : java.util.Iterator<java.util.MapNS.Entry<String, V>>
        {
            private int startModCount;
            private bool found;
            private int curr = -1;
            private int returned_index = -1;
            private Entry<E, K, V> curr_entry;
            private Entry<E, K, V> returned_entry;
            private readonly TwoKeyHashMap<E, K, V> root;

            internal EntryIteratorImpl(TwoKeyHashMap<E, K, V> root)
            {
                this.root = root;
                startModCount = root.modCount;
            }

            public bool hasNext()
            {
                if (found)
                {
                    return true;
                }
                if (curr_entry != null)
                {
                    curr_entry = curr_entry.next;
                }
                if (curr_entry == null)
                {
                    for (curr++; curr < root.arr.Length && root.arr[curr] == null; curr++)
                    {
                    }

                    if (curr < root.arr.Length)
                    {
                        curr_entry = root.arr[curr];
                    }
                }
                return found = (curr_entry != null);
            }

            public java.util.MapNS.Entry<String, V> next()
            {
                if (root.modCount != startModCount)
                {
                    throw new java.util.ConcurrentModificationException();
                }
                if (!hasNext())
                {
                    throw new java.util.NoSuchElementException();
                }

                found = false;
                returned_index = curr;
                returned_entry = curr_entry;
                return (java.util.MapNS.Entry<String, V>)curr_entry;
            }

            public void remove()
            {
                if (returned_index == -1)
                {
                    throw new java.lang.IllegalStateException();
                }

                if (root.modCount != startModCount)
                {
                    throw new java.util.ConcurrentModificationException();
                }

                Entry<E, K, V> p = null;
                Entry<E, K, V> e = root.arr[returned_index];
                while (e != returned_entry)
                {
                    p = e;
                    e = e.next;
                }
                if (p != null)
                {
                    p.next = returned_entry.next;
                }
                else
                {
                    root.arr[returned_index] = returned_entry.next;
                }
                root.sizeJ--;
                root.modCount++;
                startModCount++;
                returned_index = -1;
            }
        }

        private Entry<E, K, V> findEntry(Object key1, Object key2)
        {
            if (key1 == null && key2 == null)
            {
                return arr[arrSize];
            }

            int hash = key1.GetHashCode() + key2.GetHashCode();
            int index = (hash & 0x7fffffff) % arrSize;
            Entry<E, K, V> e = arr[index];

            while (e != null)
            {
                if (hash == e.hash && key1.equals(e.getKey1())
                        && key2.equals(e.getKey2()))
                {
                    return e;
                }
                e = e.next;
            }
            return null;
        }

        // Removes entry
        private Entry<E, K, V> removeEntry(Object key1, Object key2)
        {
            int index = 0;
            if (key1 == null && key2 == null)
            {
                index = arrSize;
                if (arr[index] != null)
                {
                    Entry<E, K, V> ret = arr[index];
                    arr[index] = null;
                    sizeJ--;
                    modCount++;
                    return ret;
                }
                return null;
            }

            int hash = key1.GetHashCode() + key2.GetHashCode();
            index = (hash & 0x7fffffff) % arrSize;

            Entry<E, K, V> e = arr[index];
            Entry<E, K, V> prev = e;
            while (e != null)
            {
                if (hash == e.hash && key1.equals(e.getKey1())
                        && key2.equals(e.getKey2()))
                {
                    if (prev == e)
                    {
                        arr[index] = e.next;
                    }
                    else
                    {
                        prev.next = e.next;
                    }
                    sizeJ--;
                    modCount++;
                    return e;
                }

                prev = e;
                e = e.next;
            }
            return null;
        }

        /**
         * An instance is returned by the values() call.
         */
        class ValuesCollectionImpl : java.util.AbstractCollection<V>
        {
            private readonly TwoKeyHashMap<E, K, V> root;

            public ValuesCollectionImpl(TwoKeyHashMap<E, K, V> root)
            {
                this.root = root;
            }

            public override int size()
            {
                return root.sizeJ;
            }

            public override void clear()
            {
                root.clear();
            }

            public override bool isEmpty()
            {
                return root.sizeJ == 0;
            }

            public override java.util.Iterator<V> iterator()
            {
                return root.createValueCollectionIterator();
            }

            public override bool contains(Object obj)
            {
                return root.containsValue(obj);
            }
        }

        class ValueIteratorImpl : java.util.Iterator<V>
        {
            private EntryIteratorImpl itr;

            internal ValueIteratorImpl(TwoKeyHashMap<E,K,V> root)
                : base()
            {
                this.itr = new EntryIteratorImpl(root);
            }

            public V next()
            {
                return itr.next().getValue();
            }

            public void remove()
            {
                itr.remove();
            }

            public bool hasNext()
            {
                return itr.hasNext();
            }
        }
    }
}