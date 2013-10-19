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

namespace biz.ritter.javapi.beans
{
    [Serializable]
public class PropertyChangeSupport : java.io.Serializable {

    private static readonly long serialVersionUID = 6401253773779951803l;
        [NonSerialized]
    private java.util.AbstractList<PropertyChangeListener> globalListeners = new java.util.ArrayList<PropertyChangeListener>();

    private java.util.Hashtable<String, PropertyChangeSupport> children = new java.util.Hashtable<String, PropertyChangeSupport>();

    private Object source;

    // for serialization compatibility
    private int propertyChangeSupportSerializedDataVersion = 1;

    public PropertyChangeSupport(Object sourceBean) {
        if (sourceBean == null) {
            throw new java.lang.NullPointerException();
        }
        this.source = sourceBean;
    }

    public void firePropertyChange(String propertyName, Object oldValue,
            Object newValue) {
        PropertyChangeEvent eventJ = createPropertyChangeEvent(propertyName,
                oldValue, newValue);
        doFirePropertyChange(eventJ);
    }

    public void fireIndexedPropertyChange(String propertyName, int index,
            Object oldValue, Object newValue) {

        // nulls and equals check done in doFire...
        doFirePropertyChange(new IndexedPropertyChangeEvent(source,
                propertyName, oldValue, newValue, index));
    }

    public void removePropertyChangeListener(String propertyName,
            PropertyChangeListener listener) {
        lock(this){
        if ((propertyName != null) && (listener != null)) {
            PropertyChangeSupport listeners = children.get(propertyName);

            if (listeners != null) {
                listeners.removePropertyChangeListener(listener);
            }
        }
        }
    }

    public void addPropertyChangeListener(String propertyName,
            PropertyChangeListener listener) {
        lock (this){
        if ((listener != null) && (propertyName != null)) {
            PropertyChangeSupport listeners = children.get(propertyName);

            if (listeners == null) {
                listeners = new PropertyChangeSupport(source);
                children.put(propertyName, listeners);
            }

            // RI compatibility
            if (listener is PropertyChangeListenerProxy) {
                PropertyChangeListenerProxy proxy = (PropertyChangeListenerProxy) listener;

                listeners
                        .addPropertyChangeListener(new PropertyChangeListenerProxy(
                                proxy.getPropertyName(),
                                (PropertyChangeListener) proxy.getListener()));
            } else {
                listeners.addPropertyChangeListener(listener);
            }
        }
        }
    }

    public PropertyChangeListener[] getPropertyChangeListeners(
            String propertyName) {
        lock(this){
        PropertyChangeSupport listeners = null;

        if (propertyName != null) {
            listeners = children.get(propertyName);
        }

        return (listeners == null) ? new PropertyChangeListener[0]
                : listeners.getPropertyChangeListeners();
    }
    }

    public void firePropertyChange(String propertyName, bool oldValue,
            bool newValue) {
        PropertyChangeEvent eventJ = createPropertyChangeEvent(propertyName,
                oldValue, newValue);
        doFirePropertyChange(eventJ);
    }

    public void fireIndexedPropertyChange(String propertyName, int index,
            bool oldValue, bool newValue) {

        if (oldValue != newValue) {
            fireIndexedPropertyChange(propertyName, index, java.lang.Boolean
                    .valueOf(oldValue), java.lang.Boolean.valueOf(newValue));
        }
    }

    public void firePropertyChange(String propertyName, int oldValue,
            int newValue) {
        PropertyChangeEvent eventJ = createPropertyChangeEvent(propertyName,
                oldValue, newValue);
        doFirePropertyChange(eventJ);
    }

    public void fireIndexedPropertyChange(String propertyName, int index,
            int oldValue, int newValue) {

        if (oldValue != newValue) {
            fireIndexedPropertyChange(propertyName, index,
                    java.lang.Integer.valueOf(oldValue), java.lang.Integer.valueOf(newValue));
        }
    }

    public bool hasListeners(String propertyName) {
        lock(this){
        if(globalListeners.size() > 0){
            return true;
        }
        bool result = false;
        if (propertyName != null) {
            PropertyChangeSupport listeners = children.get(propertyName);
            result = (listeners != null && listeners.hasListeners(propertyName));
        }
        return result;
        }
    }

    public void removePropertyChangeListener(
            PropertyChangeListener listener) {
        lock (this) {
        if (listener is PropertyChangeListenerProxy) {
            String name = ((PropertyChangeListenerProxy) listener)
                    .getPropertyName();
            PropertyChangeListener lst = (PropertyChangeListener) ((PropertyChangeListenerProxy) listener)
                    .getListener();

            removePropertyChangeListener(name, lst);
        } else {
            globalListeners.remove(listener);
        }
        }
    }

    public void addPropertyChangeListener(
            PropertyChangeListener listener) {
        lock (this){
        if (listener is PropertyChangeListenerProxy) {
            String name = ((PropertyChangeListenerProxy) listener)
                    .getPropertyName();
            PropertyChangeListener lst = (PropertyChangeListener) ((PropertyChangeListenerProxy) listener)
                    .getListener();
            addPropertyChangeListener(name, lst);
        } else if(listener != null){
            globalListeners.add(listener);
        }
        }
    }

    public PropertyChangeListener[] getPropertyChangeListeners() {
        lock(this){}
        java.util.ArrayList<PropertyChangeListener> result = new java.util.ArrayList<PropertyChangeListener>(
                globalListeners);
        java.util.Iterator<String> it = children.keySet().iterator();
        while (it.hasNext()) {
            String propertyName = it.next();
            PropertyChangeSupport namedListener = children
                    .get(propertyName);
            PropertyChangeListener[] listeners = namedListener
                    .getPropertyChangeListeners();
            for (int i = 0; i < listeners.Length; i++) {
                result.add(new PropertyChangeListenerProxy(propertyName,
                        listeners[i]));
            }
        }
        return result.toArray(new PropertyChangeListener[0]);
    }
    

    private void writeObject(java.io.ObjectOutputStream oos) {//throws IOException {
        oos.defaultWriteObject();
        PropertyChangeListener[] gListeners = globalListeners
                .toArray(new PropertyChangeListener[0]);
        for (int i = 0; i < gListeners.Length; i++) {
            if (gListeners[i] is java.io.Serializable) {
                oos.writeObject(gListeners[i]);
            }
        }
        // Denotes end of list
        oos.writeObject(null);

    }

    private void readObject(java.io.ObjectInputStream ois) //throws IOException,
    {//ClassNotFoundException {
        ois.defaultReadObject();
        this.globalListeners = new java.util.LinkedList<PropertyChangeListener>(); //!+ Basties note: Why is above the init with ArrayList but here LinkedList???
        if (null == this.children) {
            this.children = new java.util.Hashtable<String, PropertyChangeSupport>();
        }
        Object listener = null;
        do {
            // Reads a listener _or_ proxy
            listener = ois.readObject();
            if (listener != null) {
                addPropertyChangeListener((PropertyChangeListener) listener);
            }
        } while (listener != null);
    }

    public void firePropertyChange(PropertyChangeEvent eventJ) {
        doFirePropertyChange(eventJ);
    }

    private PropertyChangeEvent createPropertyChangeEvent(String propertyName,
            Object oldValue, Object newValue) {
        return new PropertyChangeEvent(source, propertyName, oldValue, newValue);
    }

    private PropertyChangeEvent createPropertyChangeEvent(String propertyName,
            bool oldValue, bool newValue) {
        return new PropertyChangeEvent(source, propertyName, oldValue, newValue);
    }

    private PropertyChangeEvent createPropertyChangeEvent(String propertyName,
            int oldValue, int newValue) {
        return new PropertyChangeEvent(source, propertyName, oldValue, newValue);
    }

    private void doFirePropertyChange(PropertyChangeEvent eventJ) {
        Object oldValue = eventJ.getOldValue();
        Object newValue = eventJ.getNewValue();
        if (oldValue != null && newValue != null && oldValue.equals(newValue)) {
            return;
        }

        // Collect up the global listeners
        PropertyChangeListener[] gListeners;
        lock(this) {
            gListeners = globalListeners.toArray(new PropertyChangeListener[0]);
        }

        // Fire the events for global listeners
        for (int i = 0; i < gListeners.Length; i++) {
            gListeners[i].propertyChange(eventJ);
        }

        // Fire the events for the property specific listeners if any
        if (eventJ.getPropertyName() != null) {
            PropertyChangeSupport namedListener = children
                    .get(eventJ.getPropertyName());
            if (namedListener != null) {
                namedListener.firePropertyChange(eventJ);
            }
        }

    }

}
}
