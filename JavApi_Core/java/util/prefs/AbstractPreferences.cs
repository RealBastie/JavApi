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

namespace biz.ritter.javapi.util.prefs{

/**
 * This abstract class is a partial implementation of the abstract class
 * Preferences, which can be used to simplify {@code Preferences} provider's
 * implementation. This class defines nine abstract SPI methods, which must be
 * implemented by a preference provider.
 * 
 * @since 1.4
 * @see Preferences
 */
public abstract class AbstractPreferences : Preferences {
    /*
     * ----------------------------------------------------------- Class fields
     * -----------------------------------------------------------
     */
    /** the unhandled events collection */
    private static readonly List<EventObject> events = new LinkedList<EventObject>();
    /** the event dispatcher thread */
    private static readonly EventDispatcher dispatcher = new EventDispatcher(
            "Preference Event Dispatcher"); //$NON-NLS-1$

    /*
     * ----------------------------------------------------------- Class
     * initializer -----------------------------------------------------------
     */
		static AbstractPreferences(){
        dispatcher.setDaemon(true);
        dispatcher.start();
        java.lang.Runtime.getRuntime().addShutdownHook(new IC_Thread());
    }
		class IC_Thread : java.lang.Thread {
			public override void run() {
				Preferences uroot = Preferences.userRoot();
				Preferences sroot = Preferences.systemRoot();
				try {
					uroot.flush();
				} catch (BackingStoreException e) {
					// ignore
				}
				try {
					sroot.flush();
				} catch (BackingStoreException e) {
					// ignore
				}
			}
		}

    /*
     * ----------------------------------------------------------- Instance
     * fields (package-private)
     * -----------------------------------------------------------
     */
    /** true if this node is in user preference hierarchy */
    bool userNode;

    /*
     * ----------------------------------------------------------- Instance
     * fields (private)
     * -----------------------------------------------------------
     */
    /** Marker class for 'lock' field. */
    private class Lock {
    }

    /**
     * The object used to lock this node.
     */
    protected internal readonly Object lockJ;

    /**
     * This field is true if this node is created while it doesn't exist in the
     * backing store. This field's default value is false, and it is checked
     * when the node creation is completed, and if it is true, the node change
     * event will be fired for this node's parent.
     */
    protected bool newNode;

    /** cached child nodes */
    private Map<String, AbstractPreferences> cachedNode;

    // the collections of listeners
    private List<EventListener> nodeChangeListeners;
    private List<EventListener> preferenceChangeListeners;

    // this node's name
    private String nodeName;

    // handler to this node's parent
    private AbstractPreferences parentPref;

    // true if this node has been removed
    private bool isRemovedJ;

    // handler to this node's root node
    private AbstractPreferences root;

    /*
     * ----------------------------------------------------------- Constructors
     * -----------------------------------------------------------
     */
    /**
     * Constructs a new {@code AbstractPreferences} instance using the given
     * parent node and node name.
     * 
     * @param parent
     *            the parent node of the new node or {@code null} to indicate
     *            that the new node is a root node.
     * @param name
     *            the name of the new node or an empty string to indicate that
     *            this node is called "root".
     * @throws IllegalArgumentException
     *             if the name contains a slash character or is empty if {@code
     *             parent} is not {@code null}.
     */
    protected AbstractPreferences(AbstractPreferences parent, String name) {
        if ((null == parent ^ name.length() == 0) || name.indexOf("/") >= 0) { //$NON-NLS-1$
            throw new java.lang.IllegalArgumentException();
        }
        root = null == parent ? this : parent.root;
        nodeChangeListeners = new LinkedList<EventListener>();
        preferenceChangeListeners = new LinkedList<EventListener>();
        isRemovedJ = false;
        cachedNode = new HashMap<String, AbstractPreferences>();
        nodeName = name;
        parentPref = parent;
        lockJ = new Lock();
        userNode = root.userNode;
    }

    /*
     * ----------------------------------------------------------- Methods
     * -----------------------------------------------------------
     */
    /**
     * Returns an array of all cached child nodes.
     * 
     * @return the array of cached child nodes.
     */
    protected /*final*/ AbstractPreferences[] cachedChildren() {
        return cachedNode.values().toArray(
                new AbstractPreferences[cachedNode.size()]);
    }

    /**
     * Returns the child node with the specified name or {@code null} if it
     * doesn't exist. Implementers can assume that the name supplied to this
     * method will be a valid node name string (conforming to the node naming
     * format) and will not correspond to a node that has been cached or
     * removed.
     * 
     * @param name
     *            the name of the desired child node.
     * @return the child node with the given name or {@code null} if it doesn't
     *         exist.
     * @throws BackingStoreException
     *             if the backing store is unavailable or causes an operation
     *             failure.
     */
    protected AbstractPreferences getChild(String name)
		{// throws BackingStoreException {
        lock (lockJ) {
            checkState();
            AbstractPreferences result = null;
            String[] childrenNamesJ = childrenNames();
            for (int i = 0; i < childrenNamesJ.Length; i++) {
                if (childrenNamesJ[i].equals(name)) {
                    result = childSpi(name);
                    break;
                }
            }
            return result;
        }

    }

    /**
     * Returns whether this node has been removed by invoking the method {@code
     * removeNode()}.
     * 
     * @return {@code true}, if this node has been removed, {@code false}
     *         otherwise.
     */
    protected internal bool isRemoved() {
        lock (lockJ) {
            return isRemovedJ;
        }
    }

    /**
     * Flushes changes of this node to the backing store. This method should
     * only flush this node and should not include the descendant nodes. Any
     * implementation that wants to provide functionality to flush all nodes at
     * once should override the method {@link #flush() flush()}.
     * 
     * @throws BackingStoreException
     *             if the backing store is unavailable or causes an operation
     *             failure.
     */
		protected abstract void flushSpi ();// throws BackingStoreException;

    /**
     * Returns the names of all of the child nodes of this node or an empty
     * array if this node has no children. The names of cached children are not
     * required to be returned.
     * 
     * @return the names of this node's children.
     * @throws BackingStoreException
     *             if the backing store is unavailable or causes an operation
     *             failure.
     */
		protected abstract String[] childrenNamesSpi ();// throws BackingStoreException;

    /**
     * Returns the child preference node with the given name, creating it if it
     * does not exist. The caller of this method should ensure that the given
     * name is valid and that this node has not been removed or cached. If the
     * named node has just been removed, the implementation of this method must
     * create a new one instead of reactivating the removed one.
     * <p>
     * The new creation is not required to be persisted immediately until the
     * flush method will be invoked.
     * </p>
     * 
     * @param name
     *            the name of the child preference to be returned.
     * @return the child preference node.
     */
    protected abstract AbstractPreferences childSpi(String name);

    /**
     * Puts the given key-value pair into this node. Caller of this method
     * should ensure that both of the given values are valid and that this node
     * has not been removed.
     * 
     * @param name
     *            the given preference key.
     * @param value
     *            the given preference value.
     */
    protected abstract void putSpi(String name, String value);

    /**
     * Gets the preference value mapped to the given key. The caller of this
     * method should ensure that the given key is valid and that this node has
     * not been removed. This method should not throw any exceptions but if it
     * does, the caller will ignore the exception, regarding it as a {@code
     * null} return value.
     * 
     * @param key
     *            the given key to be searched for.
     * @return the preference value mapped to the given key.
     */
    protected abstract String getSpi(String key);

    /**
     * Returns an array of all preference keys of this node or an empty array if
     * no preferences have been found. The caller of this method should ensure
     * that this node has not been removed.
     * 
     * @return the array of all preference keys.
     * @throws BackingStoreException
     *             if the backing store is unavailable or causes an operation
     *             failure.
     */
		protected abstract String[] keysSpi();// throws BackingStoreException;

    /**
     * Removes this node from the preference hierarchy tree. The caller of this
     * method should ensure that this node has no child nodes, which means the
     * method {@link Preferences#removeNode() Preferences.removeNode()} should
     * invoke this method multiple-times in bottom-up pattern. The removal is
     * not required to be persisted until after it is flushed.
     * 
     * @throws BackingStoreException
     *             if the backing store is unavailable or causes an operation
     *             failure.
     */
		protected abstract void removeNodeSpi();// throws BackingStoreException;

    /**
     * Removes the preference with the specified key. The caller of this method
     * should ensure that the given key is valid and that this node has not been
     * removed.
     * 
     * @param key
     *            the key of the preference that is to be removed.
     */
    protected abstract void removeSpi(String key);

    /**
     * Synchronizes this node with the backing store. This method should only
     * synchronize this node and should not include the descendant nodes. An
     * implementation that wants to provide functionality to synchronize all
     * nodes at once should override the method {@link #sync() sync()}.
     * 
     * @throws BackingStoreException
     *             if the backing store is unavailable or causes an operation
     *             failure.
     */
		protected abstract void syncSpi();// throws BackingStoreException;

    /*
     * ----------------------------------------------------------- Methods
     * inherited from Preferences
     * -----------------------------------------------------------
     */
    
    public override String absolutePath() {
        if (parentPref == null) {
            return "/"; //$NON-NLS-1$
        } else if (parentPref == root) {
            return "/" + nodeName; //$NON-NLS-1$
        }
        return parentPref.absolutePath() + "/" + nodeName; //$NON-NLS-1$
    }

    
    public override String[] childrenNames(){// throws BackingStoreException {
        lock (lockJ) {
            checkState();
            TreeSet<String> result = new TreeSet<String>(cachedNode.keySet());
            String[] names = childrenNamesSpi();
            for (int i = 0; i < names.Length; i++) {
                result.add(names[i]);
            }
            return result.toArray(new String[result.size()]);
        }
    }

    
    public override void clear() {//throws BackingStoreException {
        lock (lockJ) {
            String[] keyList = keys();
            for (int i = 0; i < keyList.Length; i++) {
                remove(keyList[i]);
            }
        }
    }

    
    public override void exportNode(java.io.OutputStream ostream) //throws IOException,
		{//BackingStoreException {
        if (ostream == null) {
            // prefs.5=Stream is null
				throw new java.lang.NullPointerException("Stream is null"); //$NON-NLS-1$
        }
        checkState();
        XMLParser.exportPrefs(this, ostream, false);

    }

    
    public override void exportSubtree(java.io.OutputStream ostream) //throws IOException,
		{//BackingStoreException {
        if (ostream == null) {
            // prefs.5=Stream is null
				throw new java.lang.NullPointerException("Stream is null"); //$NON-NLS-1$
        }
        checkState();
        XMLParser.exportPrefs(this, ostream, true);
    }

    
    public override void flush(){// throws BackingStoreException {
        lock (lockJ) {
            flushSpi();
        }
        AbstractPreferences[] cc = cachedChildren();
        int i;
        for (i = 0; i < cc.Length; i++) {
            cc[i].flush();
        }
    }

    
    public override String get(String key, String deflt) {
        if (key == null) {
            throw new java.lang.NullPointerException();
        }
        String result = null;
        lock (lockJ) {
            checkState();
            try {
                result = getSpi(key);
            } catch (Exception e) {
                // ignored
            }
        }
        return (result == null ? deflt : result);
    }

    
    public override bool getBoolean(String key, bool deflt) {
        String result = get(key, null);
        if (result == null) {
            return deflt;
        }
        if ("true".equalsIgnoreCase(result)) { //$NON-NLS-1$
            return true;
        } else if ("false".equalsIgnoreCase(result)) { //$NON-NLS-1$
            return false;
        } else {
            return deflt;
        }
    }

    
    public override byte[] getByteArray(String key, byte[] deflt) {
        java.lang.StringJ svalue = get(key, null);
        if (svalue == null) {
            return deflt;
        }
        if (svalue.length() == 0) {
            return new byte[0];
        }
        try {
            byte[] bavalue = svalue.getBytes("US-ASCII"); //$NON-NLS-1$
            if (bavalue.Length % 4 != 0) {
                return deflt;
            }
			return new java.lang.StringJ(System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(svalue))).getBytes ();
            //return Base64.decode(bavalue);
        } catch (Exception e) {
            return deflt;
        }
    }

    
    public override double getDouble(String key, double deflt) {
        String result = get(key, null);
        if (result == null) {
            return deflt;
        }
        try {
            return java.lang.Double.parseDouble(result);
        } catch (java.lang.NumberFormatException e) {
            return deflt;
        }
    }

    
    public override float getFloat(String key, float deflt) {
        String result = get(key, null);
        if (result == null) {
            return deflt;
        }
        try {
            return java.lang.Float.parseFloat(result);
        } catch (java.lang.NumberFormatException e) {
            return deflt;
        }
    }

    
    public override int getInt(String key, int deflt) {
        String result = get(key, null);
        if (result == null) {
            return deflt;
        }
        try {
            return java.lang.Integer.parseInt(result);
        } catch (java.lang.NumberFormatException e) {
            return deflt;
        }
    }

    
    public override long getLong(String key, long deflt) {
        String result = get(key, null);
        if (result == null) {
            return deflt;
        }
        try {
            return java.lang.Long.parseLong(result);
        } catch (java.lang.NumberFormatException e) {
            return deflt;
        }
    }

    
    public override bool isUserNode() {
        return root == Preferences.userRoot();
    }

    
    public override String[] keys(){// throws BackingStoreException {
        lock (lockJ) {
            checkState();
            return keysSpi();
        }
    }

    
    public override String name() {
        return nodeName;
    }

    
    public override Preferences node(String name) {
        AbstractPreferences startNode = null;
        lock (lockJ) {
            checkState();
            validateName(name);
            if ("".equals(name)) { //$NON-NLS-1$
                return this;
            } else if ("/".equals(name)) { //$NON-NLS-1$
                return root;
            }
            if (name.startsWith("/")) { //$NON-NLS-1$
                startNode = root;
                name = name.substring(1);
            } else {
                startNode = this;
            }
        }
        try {
            return startNode.nodeImpl(name, true);
        } catch (BackingStoreException e) {
            // should not happen
            return null;
        }
    }

    private void validateName(String name) {
        if (name.endsWith("/") && name.length() > 1) { //$NON-NLS-1$
            // prefs.6=Name cannot end with '/'
				throw new java.lang.IllegalArgumentException("Name cannot end with '/'"); //$NON-NLS-1$
        }
        if (name.indexOf("//") >= 0) { //$NON-NLS-1$
            // prefs.7=Name cannot contains consecutive '/'
				throw new java.lang.IllegalArgumentException("Name cannot contains consecutive '/'"); //$NON-NLS-1$
        }
    }

    private AbstractPreferences nodeImpl(String path, bool createNew)
		{//throws BackingStoreException {

        String[] names = path.split("/");//$NON-NLS-1$
        AbstractPreferences currentNode = this;
        AbstractPreferences temp = null;

        for (int i = 0; i < names.Length; i++) {
            String name = names[i];
            lock (currentNode.lockJ) {
                temp = currentNode.cachedNode.get(name);
                if (temp == null) {
                    temp = getNodeFromBackend(createNew, currentNode, name);
                }
            }
            currentNode = temp;
            if (null == currentNode) {
                break;
            }
        }
        return currentNode;
    }

    private AbstractPreferences getNodeFromBackend(bool createNew,
            AbstractPreferences currentNode, String name)
		{//throws BackingStoreException {
        if (name.length() > MAX_NAME_LENGTH) {
            // prefs.8=Name length is too long: {0}
				throw new java.lang.IllegalArgumentException("Name length is too long: "+
                    name);
        }
        AbstractPreferences temp;
        if (createNew) {
            temp = currentNode.childSpi(name);
            currentNode.cachedNode.put(name, temp);
            if (temp.newNode && currentNode.nodeChangeListeners.size() > 0) {
                currentNode.notifyChildAdded(temp);
            }
        } else {
            temp = currentNode.getChild(name);
        }
        return temp;
    }

    
    public override bool nodeExists(String name) {//throws BackingStoreException {
        if (null == name) {
            throw new java.lang.NullPointerException();
        }
        AbstractPreferences startNode = null;
        lock (lockJ) {
            if (isRemoved()) {
                if ("".equals(name)) { //$NON-NLS-1$
                    return false;
                }
                // prefs.9=This node has been removed
					throw new java.lang.IllegalStateException("This node has been removed"); //$NON-NLS-1$
            }
            validateName(name);
            if ("".equals(name) || "/".equals(name)) { //$NON-NLS-1$ //$NON-NLS-2$
                return true;
            }
            if (name.startsWith("/")) { //$NON-NLS-1$
                startNode = root;
                name = name.substring(1);
            } else {
                startNode = this;
            }
        }
        try {
            Preferences result = startNode.nodeImpl(name, false);
            return null == result ? false : true;
        } catch (java.lang.IllegalArgumentException e) {
            return false;
        }
    }

    
    public override Preferences parent() {
        checkState();
        return parentPref;
    }

    private void checkState() {
        if (isRemoved()) {
            // prefs.9=This node has been removed
				throw new java.lang.IllegalStateException("This node has been removed"); //$NON-NLS-1$
        }
    }

    
    public override void put(String key, String value) {
        if (null == key || null == value) {
            throw new java.lang.NullPointerException();
        }
        if (key.length() > MAX_KEY_LENGTH || value.length() > MAX_VALUE_LENGTH) {
            throw new java.lang.IllegalArgumentException();
        }
        lock (lockJ) {
            checkState();
            putSpi(key, value);
        }
        notifyPreferenceChange(key, value);
    }

    
    public override void putBoolean(String key, bool value) {
        String sval = java.lang.StringJ.valueOf(value);
        put(key, sval);
    }

    
    public override void putByteArray(String key, byte[] value) {
        try {
			put (key, Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(new java.lang.StringJ(value))));
            //put(key, Base64.encode(value, "US-ASCII")); //$NON-NLS-1$
        } catch (java.io.UnsupportedEncodingException e) {
            throw new java.lang.AssertionError(e);
        }
    }

    
    public override void putDouble(String key, double value) {
        String sval = java.lang.Double.toString(value);
        put(key, sval);
    }

    
    public override void putFloat(String key, float value) {
        String sval = java.lang.Float.toString(value);
        put(key, sval);
    }

    
    public override void putInt(String key, int value) {
        String sval = java.lang.Integer.toString(value);
        put(key, sval);
    }

    
    public override void putLong(String key, long value) {
        String sval = java.lang.Long.toString(value);
        put(key, sval);
    }

    
    public override void remove(String key) {
        lock (lockJ) {
            checkState();
            removeSpi(key);
        }
        notifyPreferenceChange(key, null);
    }

    
    public override void removeNode(){// throws BackingStoreException {
        if (root == this) {
            // prefs.A=Cannot remove root node
				throw new java.lang.UnsupportedOperationException("Cannot remove root node!"); //$NON-NLS-1$
        }
        lock (parentPref.lockJ) {
            removeNodeImpl();
        }
    }

    private void removeNodeImpl(){// throws BackingStoreException {
        lock (lockJ) {
            checkState();
            String[] childrenNames = childrenNamesSpi();
            for (int i = 0; i < childrenNames.Length; i++) {
                if (null == cachedNode.get(childrenNames[i])) {
                    AbstractPreferences child = childSpi(childrenNames[i]);
                    cachedNode.put(childrenNames[i], child);
                }
            }

            java.util.Collection<AbstractPreferences> values = cachedNode.values();
            AbstractPreferences[] children = values
                    .toArray(new AbstractPreferences[values.size()]);
            foreach (AbstractPreferences child in children) {
                child.removeNodeImpl();
            }
            removeNodeSpi();
            isRemovedJ = true;
            parentPref.cachedNode.remove(nodeName);
        }
        if (parentPref.nodeChangeListeners.size() > 0) {
            parentPref.notifyChildRemoved(this);
        }
    }

    
    public override void addNodeChangeListener(NodeChangeListener ncl) {
        if (null == ncl) {
            throw new java.lang.NullPointerException();
        }
        checkState();
        lock (nodeChangeListeners) {
            nodeChangeListeners.add(ncl);
        }
    }

    
    public override void addPreferenceChangeListener(PreferenceChangeListener pcl) {
        if (null == pcl) {
            throw new java.lang.NullPointerException();
        }
        checkState();
        lock (preferenceChangeListeners) {
            preferenceChangeListeners.add(pcl);
        }
    }

    
    public override void removeNodeChangeListener(NodeChangeListener ncl) {
        checkState();
        lock (nodeChangeListeners) {
            int pos;
            if ((pos = nodeChangeListeners.indexOf(ncl)) == -1) {
                throw new java.lang.IllegalArgumentException();
            }
            nodeChangeListeners.remove(pos);
        }
    }

    
    public override void removePreferenceChangeListener(PreferenceChangeListener pcl) {
        checkState();
        lock (preferenceChangeListeners) {
            int pos;
            if ((pos = preferenceChangeListeners.indexOf(pcl)) == -1) {
                throw new java.lang.IllegalArgumentException();
            }
            preferenceChangeListeners.remove(pos);
        }
    }

    
    public override void sync(){// throws BackingStoreException {
        lock (lockJ) {
            checkState();
            syncSpi();
        }
        AbstractPreferences[] cc = cachedChildren();
        int i;
        for (i = 0; i < cc.Length; i++) {
            cc[i].sync();
        }
    }

    
    public override String ToString() {
        java.lang.StringBuilder sb = new java.lang.StringBuilder();
        sb.append(isUserNode() ? "User" : "System"); //$NON-NLS-1$ //$NON-NLS-2$
        sb.append(" Preference Node: "); //$NON-NLS-1$
        sb.append(absolutePath());
        return sb.toString();
    }

    private void notifyChildAdded(Preferences child) {
        NodeChangeEvent nce = new NodeAddEvent(this, child);
        lock (events) {
            events.add(nce);
            events.notifyAll();
        }
    }

    private void notifyChildRemoved(Preferences child) {
        NodeChangeEvent nce = new NodeRemoveEvent(this, child);
        lock (events) {
            events.add(nce);
            events.notifyAll();
        }
    }

    private void notifyPreferenceChange(String key, String newValue) {
        PreferenceChangeEvent pce = new PreferenceChangeEvent(this, key,
                newValue);
        lock (events) {
            events.add(pce);
            events.notifyAll();
        }
    }

    private class EventDispatcher : java.lang.Thread {
        internal EventDispatcher(String name) :
            base(name){
        }

        
        public override void run() {
            while (true) {
                EventObject eventJ = null;
                try {
                    eventJ = getEventObject();
                } catch (java.lang.InterruptedException e) {
                    e.printStackTrace();
                    continue;
                }
                AbstractPreferences pref = (AbstractPreferences) eventJ
                        .getSource();
                if (eventJ is NodeAddEvent) {
                    dispatchNodeAdd((NodeChangeEvent) eventJ,
                            pref.nodeChangeListeners);
                } else if (eventJ is NodeRemoveEvent) {
                    dispatchNodeRemove((NodeChangeEvent) eventJ,
                            pref.nodeChangeListeners);
                } else if (eventJ is PreferenceChangeEvent) {
                    dispatchPrefChange((PreferenceChangeEvent) eventJ,
                            pref.preferenceChangeListeners);
                }
            }
        }

        private EventObject getEventObject() {//throws InterruptedException {
            lock (events) {
                if (events.isEmpty()) {
                    events.wait();
                }
                EventObject eventJ = events.get(0);
                events.remove(0);
                return eventJ;
            }
        }

        private void dispatchPrefChange(PreferenceChangeEvent eventJ,
                List<EventListener> preferenceChangeListeners) {
            lock (preferenceChangeListeners) {
                Iterator<EventListener> i = preferenceChangeListeners
                        .iterator();
                while (i.hasNext()) {
                    PreferenceChangeListener pcl = (PreferenceChangeListener) i
                            .next();
                    pcl.preferenceChange(eventJ);
                }
            }
        }

        private void dispatchNodeRemove(NodeChangeEvent eventJ,
                List<EventListener> nodeChangeListeners) {
            lock (nodeChangeListeners) {
                Iterator<EventListener> i = nodeChangeListeners.iterator();
                while (i.hasNext()) {
                    NodeChangeListener ncl = (NodeChangeListener) i.next();
                    ncl.childRemoved(eventJ);
                }
            }
        }

        private void dispatchNodeAdd(NodeChangeEvent eventJ,
                List<EventListener> nodeChangeListeners) {
            lock (nodeChangeListeners) {
                Iterator<EventListener> i = nodeChangeListeners.iterator();
                while (i.hasNext()) {
                    NodeChangeListener ncl = (NodeChangeListener) i.next();
                    ncl.childAdded(eventJ);
                }
            }
        }
    }

    private class NodeAddEvent : NodeChangeEvent {
        // The base class is NOT serializable, so this class isn't either.
        private static readonly long serialVersionUID = 1L;

        public NodeAddEvent(Preferences p, Preferences c) :
            base(p, c){
        }
    }

    private class NodeRemoveEvent : NodeChangeEvent {
        // The base class is NOT serializable, so this class isn't either.
        private static readonly long serialVersionUID = 1L;

        public NodeRemoveEvent(Preferences p, Preferences c) :
            base(p, c){
        }
    }
}
}
