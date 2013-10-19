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

namespace biz.ritter.javapi.awt {

/**
 * RenderingHints
 * @author Alexey A. Petrenko
 */
public class RenderingHints : java.util.Map<Object, Object>, java.lang.Cloneable {
    public static readonly Key KEY_ALPHA_INTERPOLATION = new KeyImpl(1);
    public static readonly Object VALUE_ALPHA_INTERPOLATION_DEFAULT = new KeyValue(KEY_ALPHA_INTERPOLATION);
    public static readonly Object VALUE_ALPHA_INTERPOLATION_SPEED = new KeyValue(KEY_ALPHA_INTERPOLATION);
    public static readonly Object VALUE_ALPHA_INTERPOLATION_QUALITY = new KeyValue(KEY_ALPHA_INTERPOLATION);

    public static readonly Key KEY_ANTIALIASING = new KeyImpl(2);
    public static readonly Object VALUE_ANTIALIAS_DEFAULT = new KeyValue(KEY_ANTIALIASING);
    public static readonly Object VALUE_ANTIALIAS_ON = new KeyValue(KEY_ANTIALIASING);
    public static readonly Object VALUE_ANTIALIAS_OFF = new KeyValue(KEY_ANTIALIASING);

    public static readonly Key KEY_COLOR_RENDERING = new KeyImpl(3);
    public static readonly Object VALUE_COLOR_RENDER_DEFAULT = new KeyValue(KEY_COLOR_RENDERING);
    public static readonly Object VALUE_COLOR_RENDER_SPEED = new KeyValue(KEY_COLOR_RENDERING);
    public static readonly Object VALUE_COLOR_RENDER_QUALITY = new KeyValue(KEY_COLOR_RENDERING);

    public static readonly Key KEY_DITHERING = new KeyImpl(4);
    public static readonly Object VALUE_DITHER_DEFAULT = new KeyValue(KEY_DITHERING);
    public static readonly Object VALUE_DITHER_DISABLE = new KeyValue(KEY_DITHERING);
    public static readonly Object VALUE_DITHER_ENABLE = new KeyValue(KEY_DITHERING);

    public static readonly Key KEY_FRACTIONALMETRICS = new KeyImpl(5);
    public static readonly Object VALUE_FRACTIONALMETRICS_DEFAULT = new KeyValue(KEY_FRACTIONALMETRICS);
    public static readonly Object VALUE_FRACTIONALMETRICS_ON = new KeyValue(KEY_FRACTIONALMETRICS);
    public static readonly Object VALUE_FRACTIONALMETRICS_OFF = new KeyValue(KEY_FRACTIONALMETRICS);

    public static readonly Key KEY_INTERPOLATION = new KeyImpl(6);
    public static readonly Object VALUE_INTERPOLATION_BICUBIC = new KeyValue(KEY_INTERPOLATION);
    public static readonly Object VALUE_INTERPOLATION_BILINEAR = new KeyValue(KEY_INTERPOLATION);
    public static readonly Object VALUE_INTERPOLATION_NEAREST_NEIGHBOR = new KeyValue(KEY_INTERPOLATION);

    public static readonly Key KEY_RENDERING = new KeyImpl(7);
    public static readonly Object VALUE_RENDER_DEFAULT = new KeyValue(KEY_RENDERING);
    public static readonly Object VALUE_RENDER_SPEED = new KeyValue(KEY_RENDERING);
    public static readonly Object VALUE_RENDER_QUALITY = new KeyValue(KEY_RENDERING);

    public static readonly Key KEY_STROKE_CONTROL = new KeyImpl(8);
    public static readonly Object VALUE_STROKE_DEFAULT = new KeyValue(KEY_STROKE_CONTROL);
    public static readonly Object VALUE_STROKE_NORMALIZE = new KeyValue(KEY_STROKE_CONTROL);
    public static readonly Object VALUE_STROKE_PURE = new KeyValue(KEY_STROKE_CONTROL);

    public static readonly Key KEY_TEXT_ANTIALIASING = new KeyImpl(9);
    public static readonly Object VALUE_TEXT_ANTIALIAS_DEFAULT = new KeyValue(KEY_TEXT_ANTIALIASING);
    public static readonly Object VALUE_TEXT_ANTIALIAS_ON = new KeyValue(KEY_TEXT_ANTIALIASING);
    public static readonly Object VALUE_TEXT_ANTIALIAS_OFF = new KeyValue(KEY_TEXT_ANTIALIASING);

    private java.util.HashMap<Object, Object> map = new java.util.HashMap<Object, Object>();
    
    public RenderingHints(java.util.Map<Key, Object> map) :base(){
        if (map != null) {
            putAll((java.util.Map<Object,Object>)map);
        }
    }

    public RenderingHints(Key key, Object value) :base() {
        put(key, value);
    }

    public void add(RenderingHints hints) {
        map.putAll(hints.map);
    }

    public Object put(Object key, Object value) {
        if (!((Key)key).isCompatibleValue(value)) {
            throw new java.lang.IllegalArgumentException();
        }

        return map.put(key, value);
    }

    public Object remove(Object key) {
        return map.remove(key);
    }

    public Object get(Object key) {
        return map.get(key);
    }

    public java.util.Set<Object> keySet() {
        return map.keySet();
    }

    public java.util.Set<java.util.MapNS.Entry<Object, Object>> entrySet() {
        return map.entrySet();
    }

    public void putAll(java.util.Map<Object, Object> m) {
        if (m is RenderingHints) {
            map.putAll(((RenderingHints) m).map);
        } else {
            java.util.Set<java.util.MapNS.Entry<Object,Object>> entries = m.entrySet();

            if (entries != null){
                java.util.Iterator<java.util.MapNS.Entry<Object,Object>> it = entries.iterator();
                while (it.hasNext()) {
                    java.util.MapNS.Entry<Object,Object> entry = it.next();
                    Key key = (Key) entry.getKey();
                    Object val = entry.getValue();
                    put(key, val);
                }
            }
        }
    }

    public java.util.Collection<Object> values() {
        return map.values();
    }

    public bool containsValue(Object value) {
        return map.containsValue(value);
    }

    public bool containsKey(Object key) {
        if (key == null) {
            throw new java.lang.NullPointerException();
        }

        return map.containsKey(key);
    }

    public bool isEmpty() {
        return map.isEmpty();
    }

    public void clear() {
        map.clear();
    }

    public int size() {
        return map.size();
    }

    public override bool Equals(Object o) {
        if (!(o is java.util.Map<Object, Object>)) {
            return false;
        }

        java.util.Map<Object, Object> m = (java.util.Map<Object, Object>)o;
        java.util.Set<Object> keys = keySet();
        if (!keys.equals(m.keySet())) {
            return false;
        }

        java.util.Iterator<Object> it = keys.iterator();
        while (it.hasNext()) {
            Key key = (Key)it.next();
            Object v1 = get(key);
            Object v2 = m.get(key);
            if (!(v1==null?v2==null:v1.equals(v2))) {
                return false;
            }
        }
        return true;
    }

    public override int GetHashCode() {
        return map.GetHashCode();
    }

    public Object clone() {
        RenderingHints clone = new RenderingHints(null);
        clone.map = (java.util.HashMap<Object, Object>)this.map.clone();
        return clone;
    }

    public override String ToString() {
        return "RenderingHints["+map.toString()+"]"; //$NON-NLS-1$ //$NON-NLS-2$
    }

    /**
     * Key
     */
    public abstract class Key {
        private readonly int key;

        protected Key(int key) {
            this.key = key;
        }

        public override bool Equals(Object o) {
            return this == o;
        }

        public override int GetHashCode() {
            return java.lang.SystemJ.identityHashCode(this);
        }

        protected int intKey() {
            return key;
        }

        public abstract bool isCompatibleValue(Object val);
    }

    /**
     * Private implementation of Key class
     */
    private class KeyImpl : Key {

        protected internal KeyImpl(int key) :base(key) {
        }

        public override bool isCompatibleValue(Object val) {
            if (!(val is KeyValue)) {
                return false;
            }

            return ((KeyValue)val).key == this;
        }
    }

    /**
     * Private class KeyValue is used as value for Key class instance.
     */
    private class KeyValue {
        internal readonly Key key;

        protected internal KeyValue(Key key) {
            this.key = key;
        }
    }
}
}
