/*
 *  Licensed to the Apache Software Foundation (ASF) under one or more
 *  contributor license agreements.  See the NOTICE file distributed with
 *  this work for additional information regarding copyright ownership.
 *  The ASF licenses this file to You under the Apache License, Version 2.0
 *  (the "License"); you may not use this file except in compliance with
 *  the License.  You may obtain a copy of the License at
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

namespace biz.ritter.javapi.util{

/**
 * WeakHashMap is an implementation of Map with keys which are WeakReferences. A
 * key/value mapping is removed when the key is no longer referenced. All
 * optional operations (adding and removing) are supported. Keys and values can
 * be any objects. Note that the garbage collector acts similar to a second
 * thread on this collection, possibly removing keys.
 * 
 * @since 1.2
 * @see HashMap
 * @see WeakReference
 */
public class WeakHashMap<K, V> : AbstractMap<K, V> {//implements Map<K, V> {

    private const int DEFAULT_SIZE = 16;

    private readonly java.lang.refj.ReferenceQueue<K> referenceQueue;

    int elementCount;

    internal Entry<K, V>[] elementData;

    private readonly int loadFactor;

    private int threshold;

    internal volatile int modCount;

    // Simple utility method to isolate unchecked cast for array creation
    internal static Entry<K, V>[] newEntryArray<K, V>(int size) {
        return new Entry<K,V>[size];
    }

    internal sealed class Entry<K, V> : java.lang.refj.WeakReference<K>,
            MapNS.Entry<K, V> {
        internal int hash;

        internal bool isNull;

        internal V valueJ;

        internal Entry<K, V> next;

        internal interface Type<R, K, V> {
            R get(MapNS.Entry<K, V> entry);
        }

		private readonly WeakHashMap<K,V> outerInstance;
        internal Entry(K key, V objectJ, java.lang.refj.ReferenceQueue<K> queue, WeakHashMap<K,V> outer) :
            base(key, queue){
            isNull = key == null;
            hash = isNull ? 0 : key.GetHashCode();
            valueJ = objectJ;
            this.outerInstance = outer;
        }

        public K getKey() {
            return base.get();
        }

        public V getValue() {
            return valueJ;
        }

        public V setValue(V objectJ) {
            V result = valueJ;
            valueJ = objectJ;
            return result;
        }

        public override bool Equals(Object other) {
            if (!(other is MapNS.Entry<K,V>)) {
                return false;
            }
            MapNS.Entry<Object, Object> entry = (MapNS.Entry<Object, Object>) other;
            Object key = base.get();
            return (key == null ? key == entry.getKey() : key.equals(entry
                    .getKey()))
                    && (valueJ == null ? null == entry.getValue() : valueJ
                            .equals(entry.getValue()));
        }

        
        public override int GetHashCode() {
            return hash + (valueJ == null ? 0 : valueJ.GetHashCode());
        }

        public override String ToString() {
            return base.get() + "=" + valueJ; //$NON-NLS-1$
        }
    }


    /**
     * Constructs a new empty {@code WeakHashMap} instance.
     */
    public WeakHashMap() :
        this(DEFAULT_SIZE){
    }

    /**
     * Constructs a new {@code WeakHashMap} instance with the specified
     * capacity.
     * 
     * @param capacity
     *            the initial capacity of this map.
     * @throws IllegalArgumentException
     *                if the capacity is less than zero.
     */
    public WeakHashMap(int capacity) {
        if (capacity >= 0) {
            elementCount = 0;
            elementData = newEntryArray<K,V>(capacity == 0 ? 1 : 0);
            loadFactor = 7500; // Default load factor of 0.75
            computeMaxSize();
            referenceQueue = new java.lang.refj.ReferenceQueue<K>();
        } else {
            throw new java.lang.IllegalArgumentException();
        }
    }

    /**
     * Constructs a new {@code WeakHashMap} instance with the specified capacity
     * and load factor.
     * 
     * @param capacity
     *            the initial capacity of this map.
     * @param loadFactor
     *            the initial load factor.
     * @throws IllegalArgumentException
     *             if the capacity is less than zero or the load factor is less
     *             or equal to zero.
     */
    public WeakHashMap(int capacity, float loadFactor) {
        if (capacity >= 0 && loadFactor > 0) {
            elementCount = 0;
            elementData = newEntryArray<K,V>(capacity == 0 ? 1 : capacity);
            this.loadFactor = (int) (loadFactor * 10000);
            computeMaxSize();
            referenceQueue = new java.lang.refj.ReferenceQueue<K>();
        } else {
            throw new java.lang.IllegalArgumentException();
        }
    }

    /**
     * Constructs a new {@code WeakHashMap} instance containing the mappings
     * from the specified map.
     * 
     * @param map
     *            the mappings to add.
     */
    public WeakHashMap(Map<K, V> map) :
        this(map.size() < 6 ? 11 : map.size() * 2){
        putAllImpl(map);
    }

    /**
     * Removes all mappings from this map, leaving it empty.
     * 
     * @see #isEmpty()
     * @see #size()
     */
    
    public override void clear() {
        if (elementCount > 0) {
            elementCount = 0;
            java.util.Arrays<Entry<K,V>>.fill(elementData, null);
            modCount++;
            while (referenceQueue.poll() != null) {
                // do nothing
            }
        }
    }

    private void computeMaxSize() {
        threshold = (int) ((long) elementData.Length * loadFactor / 10000);
    }

    /**
     * Returns whether this map contains the specified key.
     * 
     * @param key
     *            the key to search for.
     * @return {@code true} if this map contains the specified key,
     *         {@code false} otherwise.
     */
    
    public override bool containsKey(Object key) {
        return getEntry(key) != null;
    }

    /**
     * Returns a set containing all of the mappings in this map. Each mapping is
     * an instance of {@link Map.Entry}. As the set is backed by this map,
     * changes in one will be reflected in the other. It does not support adding
     * operations.
     *
     * @return a set of the mappings.
     */
    
    public override Set<MapNS.Entry<K, V>> entrySet() {
        poll();
        return new IAC_EntrySet(this);
    }

	internal class IAC_EntrySet : AbstractSet<MapNS.Entry<K,V>> {
            private readonly WeakHashMap<K,V> outerInstance;
            public IAC_EntrySet (WeakHashMap<K,V> outer) {
            	this.outerInstance = outer;
            }
            public override int size() {
                return this.outerInstance.size();
            }

            
            public override void clear() {
                this.outerInstance.clear();
            }

            
            public override bool remove(Object objectJ) {
                if (contains(objectJ)) {
                    this.outerInstance.remove(((MapNS.Entry<Object, Object>) objectJ).getKey());
                    return true;
                }
                return false;
            }

            
            public override bool contains(Object objectJ) {
                if (objectJ is MapNS.Entry<K,V>) {
                    Entry<K, V> entry = outerInstance.getEntry(((MapNS.Entry<Object, Object>) objectJ)
                            .getKey());
                    if (entry != null) {
                        Object key = entry.get();
                        if (key != null || entry.isNull) {
                            return objectJ.Equals(entry);
                        }
                    }
                }
                return false;
            }

            
            
            
            public override Iterator<MapNS.Entry<K, V>> iterator() {
                return new HashIterator<MapNS.Entry<K, V>,K,V>(
                    new IAC_Entry() ,this.outerInstance
                );
            }
            internal class IAC_Entry : Entry<K,V>.Type<MapNS.Entry<K, V>, K, V>{
                public MapNS.Entry<K, V> get(MapNS.Entry<K, V> entry) {
                    return entry;
                }
            }
            
            /*
            public Iterator<V> iterator2() {
                return new HashIterator<V,K,V>(
                   new IAC_EntryValue(), this.outerInstance
                );
            }
            internal class IAC_EntryValue : Entry<K,V>.Type<V, K, V>{
                public V get(MapNS.Entry<K, V> entry) {
                    return entry.getValue();
                }
            }
            */
            
	}
	
    /**
     * Returns a set of the keys contained in this map. The set is backed by
     * this map so changes to one are reflected by the other. The set does not
     * support adding.
     * 
     * @return a set of the keys.
     */
    
    public override Set<K> keySet() {
        poll();
        if (keySetJ == null) {
            keySetJ = new IAC_KeySet(this);
        }
        return keySetJ;
    }
    internal class IAC_KeySet : AbstractSet<K>{
    	private readonly WeakHashMap<K,V> outerInstance;
    	public IAC_KeySet (WeakHashMap<K,V> outer) {
    	    this.outerInstance = outer;
    	}
                
                public override bool contains(Object objectJ) {
                    return this.outerInstance.containsKey(objectJ);
                }

                public override int size() {
                    return this.outerInstance.size();
                }

                public override void clear() {
                    this.outerInstance.clear();
                }

                public override bool remove(Object key) {
                    if (this.outerInstance.containsKey(key)) {
                        this.outerInstance.remove(key);
                        return true;
                    }
                    return false;
                }

                public override Iterator<K> iterator() {
                    return new HashIterator<K,K,V>(new IAC_KeyValue(), this.outerInstance);
                }
                internal class IAC_KeyValue : Entry<K,V>.Type<K, K, V>{
                      public K get(MapNS.Entry<K, V> entry) {
                            return entry.getKey();
                        }
                    }
                  

                public override Object[] toArray() {
                    Collection<K> coll = new ArrayList<K>(size());

                    for (Iterator<K> iter = iterator(); iter.hasNext();) {
                        coll.add(iter.next());
                    }
                    return coll.toArray();
                }

                public override T[] toArray<T>(T[] contents) {
                    Collection<K> coll = new ArrayList<K>(size());

                    for (Iterator<K> iter = iterator(); iter.hasNext();) {
                        coll.add(iter.next());
                    }
                    return coll.toArray(contents);
                }
    }

    /**
     * Returns a collection of the values contained in this map. The collection
     * is backed by this map so changes to one are reflected by the other. The
     * collection supports remove, removeAll, retainAll and clear operations,
     * and it does not support add or addAll operations.
     * <p/>
     * This method returns a collection which is the subclass of
     * AbstractCollection. The iterator method of this subclass returns a
     * "wrapper object" over the iterator of map's entrySet(). The size method
     * wraps the map's size method and the contains method wraps the map's
     * containsValue method.
     * <p/>
     * The collection is created when this method is called at first time and
     * returned in response to all subsequent calls. This method may return
     * different Collection when multiple calls to this method, since it has no
     * synchronization performed.
     * 
     * @return a collection of the values contained in this map.
     */
    
    public override Collection<V> values() {
        poll();
        if (valuesCollection == null) {
            valuesCollection = new IAC_ValuesCollection(this);
        }
        return valuesCollection;
    }
    class IAC_ValuesCollection : AbstractCollection<V> {
                private readonly WeakHashMap<K,V> outerInstance;
                public IAC_ValuesCollection (WeakHashMap<K,V> outer){
                	this.outerInstance = outer;
                }
                public override int size() {
                    return this.outerInstance.size();
                }

                public override void clear() {
                    this.outerInstance.clear();
                }

                public override bool contains(Object objectJ) {
                    return this.outerInstance.containsValue(objectJ);
                }

                public override Iterator<V> iterator() {
                    return new HashIterator<V,K,V>(new IAC_EntryValue(), this.outerInstance);
                }
                internal class IAC_EntryValue : Entry<K,V>.Type<V, K, V>{
                        public V get(MapNS.Entry<K, V> entry) {
                            return entry.getValue();
                        }
                }
            }
        

    /**
     * Returns the value of the mapping with the specified key.
     * 
     * @param key
     *            the key.
     * @return the value of the mapping with the specified key, or {@code null}
     *         if no mapping for the specified key is found.
     */
    
    public override V get(Object key) {
        poll();
        if (key != null) {
            int index = (key.GetHashCode() & 0x7FFFFFFF) % elementData.Length;
            Entry<K, V> entry1 = elementData[index];
            while (entry1 != null) {
                if (key.equals(entry1.get())) {
                    return entry1.valueJ;
                }
                entry1 = entry1.next;
            }
            return default(V);
        }
        Entry<K, V> entry2 = elementData[0];
        while (entry2 != null) {
            if (entry2.isNull) {
                return entry2.valueJ;
            }
            entry2 = entry2.next;
        }
        return default(V);
    }

    Entry<K, V> getEntry(Object key) {
        poll();
        if (key != null) {
            int index = (key.GetHashCode() & 0x7FFFFFFF) % elementData.Length;
            Entry<K, V> entry1 = elementData[index];
            while (entry1 != null) {
                if (key.equals(entry1.get())) {
                    return entry1;
                }
                entry1 = entry1.next;
            }
            return null;
        }
        Entry<K, V> entry2 = elementData[0];
        while (entry2 != null) {
            if (entry2.isNull) {
                return entry2;
            }
            entry2 = entry2.next;
        }
        return null;
    }

    /**
     * Returns whether this map contains the specified value.
     * 
     * @param value
     *            the value to search for.
     * @return {@code true} if this map contains the specified value,
     *         {@code false} otherwise.
     */
    public override bool containsValue(Object valueJ) {
        poll();
        if (valueJ != null) {
            for (int i = elementData.Length; --i >= 0;) {
                Entry<K, V> entry = elementData[i];
                while (entry != null) {
                    K key = entry.get();
                    if ((key != null || entry.isNull)
                            && valueJ.equals(entry.valueJ)) {
                        return true;
                    }
                    entry = entry.next;
                }
            }
        } else {
            for (int i = elementData.Length; --i >= 0;) {
                Entry<K, V> entry = elementData[i];
                while (entry != null) {
                    K key = entry.get();
                    if ((key != null || entry.isNull) && entry.valueJ == null) {
                        return true;
                    }
                    entry = entry.next;
                }
            }
        }
        return false;
    }

    /**
     * Returns the number of elements in this map.
     * 
     * @return the number of elements in this map.
     */
    public override bool isEmpty() {
        return size() == 0;
    }

    void poll() {
        Entry<K, V> toRemove;
        while ((toRemove = (Entry<K, V>) referenceQueue.poll()) != null) {
            removeEntry(toRemove);
        }
    }

    internal void removeEntry(Entry<K, V> toRemove) {
        Entry<K, V> entry, last = null;
        int index = (toRemove.hash & 0x7FFFFFFF) % elementData.Length;
        entry = elementData[index];
        // Ignore queued entries which cannot be found, the user could
        // have removed them before they were queued, i.e. using clear()
        while (entry != null) {
            if (toRemove == entry) {
                modCount++;
                if (last == null) {
                    elementData[index] = entry.next;
                } else {
                    last.next = entry.next;
                }
                elementCount--;
                break;
            }
            last = entry;
            entry = entry.next;
        }
    }

    /**
     * Maps the specified key to the specified value.
     * 
     * @param key
     *            the key.
     * @param value
     *            the value.
     * @return the value of any previous mapping with the specified key or
     *         {@code null} if there was no mapping.
     */
    public override V put(K key, V valueJ) {
        poll();
        int index = 0;
        Entry<K, V> entry;
        if (key != null) {
            index = (key.GetHashCode() & 0x7FFFFFFF) % elementData.Length;
            entry = elementData[index];
            while (entry != null && !key.equals(entry.get())) {
                entry = entry.next;
            }
        } else {
            entry = elementData[0];
            while (entry != null && !entry.isNull) {
                entry = entry.next;
            }
        }
        if (entry == null) {
            modCount++;
            if (++elementCount > threshold) {
                rehash();
                index = key == null ? 0 : (key.GetHashCode() & 0x7FFFFFFF)
                        % elementData.Length;
            }
            entry = new Entry<K, V>(key, valueJ, referenceQueue, this);
            entry.next = elementData[index];
            elementData[index] = entry;
            return default(V);
        }
        V result = entry.valueJ;
        entry.valueJ = valueJ;
        return result;
    }

    private void rehash() {
        int length = elementData.Length << 1;
        if (length == 0) {
            length = 1;
        }
        Entry<K, V>[] newData = newEntryArray<K,V>(length);
        for (int i = 0; i < elementData.Length; i++) {
            Entry<K, V> entry = elementData[i];
            while (entry != null) {
                int index = entry.isNull ? 0 : (entry.hash & 0x7FFFFFFF)
                        % length;
                Entry<K, V> next = entry.next;
                entry.next = newData[index];
                newData[index] = entry;
                entry = next;
            }
        }
        elementData = newData;
        computeMaxSize();
    }

    /**
     * Copies all the mappings in the given map to this map. These mappings will
     * replace all mappings that this map had for any of the keys currently in
     * the given map.
     * 
     * @param map
     *            the map to copy mappings from.
     * @throws NullPointerException
     *             if {@code map} is {@code null}.
     */
    public override void putAll(Map<K, V> map) {
        putAllImpl(map);
    }

    /**
     * Removes the mapping with the specified key from this map.
     * 
     * @param key
     *            the key of the mapping to remove.
     * @return the value of the removed mapping or {@code null} if no mapping
     *         for the specified key was found.
     */
    public override V remove(Object key) {
        poll();
        int index = 0;
        Entry<K, V> entry, last = null;
        if (key != null) {
            index = (key.GetHashCode() & 0x7FFFFFFF) % elementData.Length;
            entry = elementData[index];
            while (entry != null && !key.equals(entry.get())) {
                last = entry;
                entry = entry.next;
            }
        } else {
            entry = elementData[0];
            while (entry != null && !entry.isNull) {
                last = entry;
                entry = entry.next;
            }
        }
        if (entry != null) {
            modCount++;
            if (last == null) {
                elementData[index] = entry.next;
            } else {
                last.next = entry.next;
            }
            elementCount--;
            return entry.valueJ;
        }
        return default(V);
    }

    /**
     * Returns the number of elements in this map.
     * 
     * @return the number of elements in this map.
     */
    
    public override int size() {
        poll();
        return elementCount;
    }

    private void putAllImpl(Map<K, V> map) {
        if (map.entrySet() != null) {
            base.putAll(map);
        }
    }
}

    internal class HashIterator<R,K,V> : Iterator<R> {
    	private readonly WeakHashMap<K,V> outerInstance;
    
        private int position = 0, expectedModCount;

        private WeakHashMap<K,V>.Entry<K, V> currentEntry, nextEntry;

        private K nextKey;

        readonly WeakHashMap<K,V>.Entry<K,V>.Type<R, K, V> type;

        internal HashIterator(WeakHashMap<K,V>.Entry<K,V>.Type<R, K, V> type, WeakHashMap<K,V> outer) {
            this.type = type;
            expectedModCount = this.outerInstance.modCount;
            this.outerInstance = outer;
        }

        public bool hasNext() {
            if (nextEntry != null && (nextKey != null || nextEntry.isNull)) {
                return true;
            }
            while (true) {
                if (nextEntry == null) {
                    while (position < this.outerInstance.elementData.Length) {
                        if ((nextEntry = this.outerInstance.elementData[position++]) != null) {
                            break;
                        }
                    }
                    if (nextEntry == null) {
                        return false;
                    }
                }
                // ensure key of next entry is not gc'ed
                nextKey = nextEntry.get();
                if (nextKey != null || nextEntry.isNull) {
                    return true;
                }
                nextEntry = nextEntry.next;
            }
        }

        public R next() {
            if (expectedModCount == this.outerInstance.modCount) {
                if (hasNext()) {
                    currentEntry = nextEntry;
                    nextEntry = currentEntry.next;
                    R result = type.get(currentEntry);
                    // free the key
                    nextKey = default(K);
                    return result;
                }
                throw new NoSuchElementException();
            }
            throw new ConcurrentModificationException();
        }

        public void remove() {
            if (expectedModCount == this.outerInstance.modCount) {
                if (currentEntry != null) {
                    this.outerInstance.removeEntry(currentEntry);
                    currentEntry = null;
                    expectedModCount++;
                    // cannot poll() as that would change the expectedModCount
                } else {
                    throw new java.lang.IllegalStateException();
                }
            } else {
                throw new ConcurrentModificationException();
            }
        }
    }
}