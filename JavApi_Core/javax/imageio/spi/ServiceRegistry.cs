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
using javax = biz.ritter.javapix;

namespace biz.ritter.javapix.imageio.spi
{
/**
 * @author Rustem V. Rafikov
 */

public class ServiceRegistry {

    CategoriesMap categories;

    public ServiceRegistry(java.util.Iterator<java.lang.Class> categoriesIterator) {
			this.categories = new CategoriesMap (this);
        if (null == categoriesIterator) {
				throw new java.lang.IllegalArgumentException("categories iterator should not be NULL");
        }
        while(categoriesIterator.hasNext()) {
            java.lang.Class c =  categoriesIterator.next();
            categories.addCategory(c);
        }
    }

    public static java.util.Iterator<T> lookupProviders<T>(java.lang.Class providerClass, java.lang.ClassLoader loader) {
        return new LookupProvidersIterator<T>(providerClass, loader);
    }

    public static java.util.Iterator<T> lookupProviders<T>(java.lang.Class providerClass) {
        return lookupProviders<T>(providerClass, java.lang.Thread.currentThread().getContextClassLoader());
    }

    public bool registerServiceProvider<T>(T provider, java.lang.Class category) {
        return categories.addProvider(provider, category);
    }

    public void registerServiceProviders(java.util.Iterator<Object> providers) {
        for (java.util.Iterator<Object> iterator = providers; iterator.hasNext();) {
            categories.addProvider(iterator.next(), null);
        }
    }

    public void registerServiceProvider(Object provider) {
        categories.addProvider(provider, null);
    }

    public bool deregisterServiceProvider<T>(T provider, java.lang.Class category) {
        return categories.removeProvider(provider, category);
    }

    public void deregisterServiceProvider(Object provider) {
        categories.removeProvider(provider);
    }

    public java.util.Iterator<T> getServiceProviders<T>(java.lang.Class category, Filter filter, bool useOrdering) {
        return new FilteredIterator<T>(filter, (java.util.Iterator<T>) categories.getProviders(category, useOrdering));
    }

    public java.util.Iterator<T> getServiceProviders<T>(java.lang.Class category, bool useOrdering) {
        return (java.util.Iterator<T>) categories.getProviders(category, useOrdering);
    }

    public T getServiceProviderByClass<T>(java.lang.Class providerClass) {
        return categories.getServiceProviderByClass<T>(providerClass);
    }

    public bool setOrdering<T>(java.lang.Class category, T firstProvider, T secondProvider) {
        return categories.setOrdering(category, firstProvider, secondProvider);
    }

    public bool unsetOrdering<T>(java.lang.Class category, T firstProvider, T secondProvider) {
        return categories.unsetOrdering(category, firstProvider, secondProvider);
    }

    public void deregisterAll(java.lang.Class category) {
        categories.removeAll(category);
    }

    public void deregisterAll() {
        categories.removeAll();
    }

    
    public void finalize() {//throws Throwable {
        deregisterAll();
    }

    public bool contains(Object provider) {
        if (provider == null) {
				throw new java.lang.IllegalArgumentException("Provider should be != NULL");
        }
        
        return categories.contains(provider);
    }

    public java.util.Iterator<java.lang.Class> getCategories() {
        return categories.list();
    }

    public interface Filter {
        bool filter(Object provider);
    }

    internal class CategoriesMap {
        java.util.Map<java.lang.Class, ProvidersMap> categories = new java.util.HashMap<java.lang.Class, ProvidersMap>();

        ServiceRegistry registry;

        public CategoriesMap(ServiceRegistry registry) {
            this.registry = registry;
        }

        internal bool contains(Object provider) {
            foreach (java.util.MapNS.Entry<java.lang.Class, ProvidersMap> e in categories.entrySet()) {
                ProvidersMap providers = e.getValue();
                if (providers.contains(provider)) {
                    return true;
                }
            }
            
            return false;
        }

        internal bool setOrdering<T>(java.lang.Class category, T firstProvider, T secondProvider) {
            ProvidersMap providers = categories.get(category);
            
            if (providers == null) {
					throw new java.lang.IllegalArgumentException("Unknown category:"+category);
            }
            
            return providers.setOrdering(firstProvider, secondProvider);
        }
        
        internal bool unsetOrdering<T>(java.lang.Class category, T firstProvider, T secondProvider) {
            ProvidersMap providers = categories.get(category);
            
            if (providers == null) {
					throw new java.lang.IllegalArgumentException("Unknown category:"+ category);
            }
            
            return providers.unsetOrdering(firstProvider, secondProvider);
        }
        
        internal java.util.Iterator<Object> getProviders(java.lang.Class category, bool useOrdering) {
            ProvidersMap providers = categories.get(category);
            if (null == providers) {
					throw new java.lang.IllegalArgumentException("Unknown category:"+ category);
            }
            return providers.getProviders(useOrdering);
        }
        
        internal T getServiceProviderByClass<T>(java.lang.Class providerClass) {
        	foreach (java.util.MapNS.Entry<java.lang.Class, ProvidersMap> e in categories.entrySet()) {
        		if (e.getKey().isAssignableFrom(providerClass)) {
        			T provider = e.getValue().getServiceProviderByClass(providerClass);
        			if (provider != null) {
        				return provider;
        			}
        		}
        	}
				return default(T);
        }

        internal java.util.Iterator<java.lang.Class> list() {
            return categories.keySet().iterator();
        }

        internal void addCategory(java.lang.Class category) {
            categories.put(category, new ProvidersMap());
        }

        /**
         * Adds a provider to the category. If <code>category</code> is
         * <code>null</code> then the provider will be added to all categories
         * which the provider is assignable from.
         * @param provider provider to add
         * @param category category to add provider to
         * @return if there were such provider in some category
         */
        internal bool addProvider(Object provider, java.lang.Class category) {
            if (provider == null) {
					throw new java.lang.IllegalArgumentException("Provider should be != NULL");
            }

            bool rt;
            if (category == null) {
                rt = findAndAdd(provider);
            } else {
                rt  = addToNamed(provider, category);
            }

            if (provider is RegisterableService) {
                ((RegisterableService) provider).onRegistration(registry, category);
            }

            return rt;
        }

        private bool addToNamed(Object provider, java.lang.Class category) {
            if (!category.isAssignableFrom(provider.getClass())) {
                throw new java.lang.ClassCastException();
            }
            Object obj = categories.get(category);

            if (null == obj) {
                throw new java.lang.IllegalArgumentException(Messages.getString("imageio.92", category));
            }

            return ((ProvidersMap) obj).addProvider(provider);
        }

        private bool findAndAdd(Object provider) {
            bool rt = false;
            foreach (Entry<java.lang.Class, ProvidersMap> e in categories.entrySet()) {
                if (e.getKey().isAssignableFrom(provider.getClass())) {
                    rt |= e.getValue().addProvider(provider);
                }
            }
            return rt;
        }
        
        internal bool removeProvider(Object provider, java.lang.Class category) {
            if (provider == null) {
					throw new java.lang.IllegalArgumentException("Provider should be != NULL");
            }
            
            if (!category.isAssignableFrom(provider.getClass())) {
                throw new java.lang.ClassCastException();
            }
            
            Object obj = categories.get(category);
            
            if (obj == null) {
                throw new java.lang.IllegalArgumentException(Messages.getString("imageio.92", category));
            }
            
            return ((ProvidersMap) obj).removeProvider(provider, registry, category);
        }
        
        internal void removeProvider(Object provider) {
            if (provider == null) {
					throw new java.lang.IllegalArgumentException("Provider should be != NULL");
            }
            
            foreach (Entry<java.lang.Class, ProvidersMap> e in categories.entrySet()) {
                ProvidersMap providers = e.getValue();
                providers.removeProvider(provider, registry, e.getKey());
            }
        }
        
        internal void removeAll(java.lang.Class category) {
            Object obj = categories.get(category);
            
            if (obj == null) {
                throw new java.lang.IllegalArgumentException(Messages.getString("imageio.92", category));
            }
            
            ((ProvidersMap) obj).clear(registry);         
        }
        
        internal void removeAll() {
            foreach (java.util.MapNS.Entry<java.lang.Class, ProvidersMap> e in categories.entrySet()) {
                removeAll(e.getKey());                
            }
        }
    }

    private class ProvidersMap {

			java.util.Map<java.lang.Class, Object> providers = new java.util.HashMap<java.lang.Class, Object>();
			java.util.Map<Object, ProviderNode> nodeMap = new java.util.HashMap<Object, ProviderNode>();

        bool addProvider(Object provider) {
            ProviderNode node =  new ProviderNode(provider);
            nodeMap.put(provider, node);
            Object obj = providers.put(provider.getClass(), provider);
            
            if (obj !=  null) {
                nodeMap.remove(obj);
                return false;
            }
            
            return true;
        }

        bool contains(Object provider) {
            return providers.containsValue(provider);
        }

        bool removeProvider(Object provider,
                ServiceRegistry registry, java.lang.Class category) {
            
            Object obj = providers.get(provider.getClass());
            if ((obj == null) || (obj != provider)) {
                return false;
            }
            
            providers.remove(provider.getClass());
            nodeMap.remove(provider);
            
            if (provider is RegisterableService) {
                ((RegisterableService) provider).onDeregistration(registry, category);
            }            
            
            return true;
        }
        
        internal void clear(ServiceRegistry registry) {
            foreach (java.util.MapNS.Entry<java.lang.Class, Object> e in providers.entrySet()) {
                Object provider = e.getValue();
                
                if (provider is RegisterableService) {
                    ((RegisterableService) provider).onDeregistration(registry, e.getKey());
                }
            }
            
            providers.clear();
            nodeMap.clear();
        }

        java.util.Iterator<java.lang.Class> getProviderClasses() {
            return providers.keySet().iterator();
        }

        internal java.util.Iterator<Object> getProviders(bool useOrdering) {
            if (useOrdering) {
                return new OrderedProviderIterator(nodeMap.values().iterator());              
            }
            
            return providers.values().iterator();
        }
        
		internal T getServiceProviderByClass<T>(java.lang.Class providerClass) {
        	return (T)providers.get(providerClass);
        }
        
        public bool setOrdering<T>(T firstProvider, T secondProvider) {
            if (firstProvider == secondProvider) {
                throw new java.lang.IllegalArgumentException(Messages.getString("imageio.98"));
            }
            
            if ((firstProvider == null) || (secondProvider == null)) {
					throw new java.lang.IllegalArgumentException("Provider should be != NULL");
            }
           
            ProviderNode firstNode = nodeMap.get(firstProvider);
            ProviderNode secondNode = nodeMap.get(secondProvider);
                    
            // if the ordering is already set, return false
            if ((firstNode == null) || (firstNode.contains(secondNode))) {
                return false;
            }
            
            // put secondProvider into firstProvider's outgoing nodes list
            firstNode.addOutEdge(secondNode);
            // increase secondNode's incoming edge by 1
            secondNode.addInEdge();         
            
            return true;
        }
        
        public bool unsetOrdering<T>(T firstProvider, T secondProvider) {
            if (firstProvider == secondProvider) {
                throw new java.lang.IllegalArgumentException(Messages.getString("imageio.98"));
            }
            
            if ((firstProvider == null) || (secondProvider == null)) {
					throw new java.lang.IllegalArgumentException("Provider should be != NULL");
            }
            
            ProviderNode firstNode = nodeMap.get(firstProvider);
            ProviderNode secondNode = nodeMap.get(secondProvider); 
                    
            // if the ordering is not set, return false
            if ((firstNode == null) || (!firstNode.contains(secondNode))) {
                return false;
            }
                    
            // remove secondProvider from firstProvider's outgoing nodes list
            firstNode.removeOutEdge(secondNode);
            // decrease secondNode's incoming edge by 1
            secondNode.removeInEdge();
                    
            return true;            
        }
    }

    private class FilteredIterator<E> : java.util.Iterator<E> {

        private Filter filter;
			private java.util.Iterator<E> backend;
        private E nextObj;

			public FilteredIterator(Filter filter, java.util.Iterator<E> backend) {
            this.filter = filter;
            this.backend = backend;
            findNext();
        }

        public E next() {
            if (nextObj == null) {
					throw new java.util.NoSuchElementException();
            }
            E tmp = nextObj;
            findNext();
            return tmp;
        }

        public bool hasNext() {
            return nextObj != null;
        }

        public void remove() {
            throw new java.lang.UnsupportedOperationException();
        }

        /**
         * Sets nextObj to a next provider matching the criterion given by the filter
         */
        private void findNext() {
				nextObj = default(E);
            while (backend.hasNext()) {
                E o = backend.next();
                if (filter.filter(o)) {
                    nextObj = o;
                    return;
                }
            }
        }
    }
    
    private class ProviderNode {
        // number of incoming edges
        private int incomingEdges;  
        // all outgoing nodes
			private java.util.Set<Object> outgoingNodes; 
        private Object provider;
                
        public ProviderNode(Object provider) {
            incomingEdges = 0;
				outgoingNodes = new java.util.HashSet<Object>();
            this.provider = provider;
        }
            
        public Object getProvider() {
            return provider;
        }
        
			public java.util.Iterator<Object> getOutgoingNodes() {
            return outgoingNodes.iterator();
        }
        
        public bool addOutEdge(Object secondProvider) {
            return outgoingNodes.add(secondProvider);
        }
        
        public bool removeOutEdge<T>(Object provider) {
            return outgoingNodes.remove(provider);
        }
        
        public void addInEdge() {
            incomingEdges++;
        }
        
        public void removeInEdge() {
            incomingEdges--;
        }
        
        public int getIncomingEdges() {
            return incomingEdges;
        }
        
        public bool contains(Object provider) {
            return outgoingNodes.contains(provider);
        }
    }

    /**
     * The iterator implements Kahn topological sorting algorithm.
     * @see <a href="http://en.wikipedia.org/wiki/Topological_sorting">Wikipedia</a>
     * for further reference.
     */
    private class OrderedProviderIterator : java.util.Iterator<Object> {

        // the stack contains nodes which has no lesser nodes
        // except those already returned by the iterator
        private java.util.Stack<ProviderNode> firstNodes = new java.util.Stack<ProviderNode>();

        // a dynamic counter of incoming nodes
        // when a node is returned by iterator, the counters for connected
        // nodes decrement
        private java.util.Map<ProviderNode, java.lang.Integer> incomingEdges = new java.util.HashMap<ProviderNode, java.lang.Integer>();
        
			public OrderedProviderIterator(java.util.Iterator<Object> it) {
            // find all the nodes that with no incoming edges and
            // add them to firstNodes
            while (it.hasNext()) {
                ProviderNode node = (ProviderNode) it.next();
                incomingEdges.put(node, new java.lang.Integer(node.getIncomingEdges()));
                if (node.getIncomingEdges() == 0) {
                    firstNodes.push(node);
                }
            }
        }
            
        public bool hasNext() {
            return !firstNodes.empty();
        }

        public Object next() {
            if (firstNodes.empty()) {
               throw new java.util.NoSuchElementException();
            }
                            
            // get a node from firstNodes
            ProviderNode node = firstNodes.pop();
                            
            // find all the outgoing nodes
				java.util.Iterator<Object> it = node.getOutgoingNodes();
            while (it.hasNext()) {
                ProviderNode outNode = (ProviderNode) it.next();
                
                // remove the incoming edge from the node.
                int edges = incomingEdges.get(outNode);
                edges--;
                incomingEdges.put(outNode, new java.lang.Integer(edges));
                
                // add to the firstNodes if this node's incoming edge is equal to 0
                if (edges == 0) {
                    firstNodes.push(outNode);
                }
            }
            
            incomingEdges.remove(node);
                            
            return node.getProvider();
        }
        
        public void remove() {
            throw new java.lang.UnsupportedOperationException();
        }
    }
    
    internal class LookupProvidersIterator<T> : java.util.Iterator<T> {

			private java.util.Set<String> providerNames = new java.util.HashSet<String>();
			private java.util.Iterator<String> it = null;
        private java.lang.ClassLoader loader = null;
        
        public LookupProvidersIterator(java.lang.Class providerClass, java.lang.ClassLoader loader) {
            this.loader = loader;
            
				java.util.Enumeration<java.net.URL> e = null;
            try {
                e = loader.getResources("META-INF/services/"+providerClass.getName()); //$NON-NLS-1$
                while (e.hasMoreElements()) {
						java.util.Set<String> names = parse((java.net.URL)e.nextElement());
                    providerNames.addAll(names);
                }
            } catch (java.io.IOException e1) {
                // Ignored
            }

            it = providerNames.iterator();
        }
        
        /*
         * Parse the provider-configuration file as specified 
         * @see <a href="http://java.sun.com/j2se/1.5.0/docs/guide/jar/jar.html#Provider Configuration File">JAR File Specification</a>
         */
			private java.util.Set<String> parse(java.net.URL u) {
            java.io.InputStream input = null;
            java.io.BufferedReader reader = null;
				java.util.Set<String> names = new java.util.HashSet<String>();
            try {
                input = u.openStream();
                reader = new java.io.BufferedReader(new java.io.InputStreamReader(input, "utf-8")); //$NON-NLS-1$
                
                String line;
                while ((line = reader.readLine()) != null) {
                    // The comment character is '#' (0x23)
                    // on each line all characters following the first comment character are ignored
                    int sharpIndex = line.indexOf('#');
                    if (sharpIndex>=0) {
                        line = line.substring(0, sharpIndex);
                    }
                    
                    // Whitespaces are ignored
                    line = line.trim();
                    
                    if (line.length()>0) {
                        // a java class name, check if identifier correct
                        char[] namechars = line.toCharArray();
                        for (int i = 0; i < namechars.Length; i++) {
                            if (!(java.lang.Character.isJavaIdentifierPart(namechars[i]) || namechars[i] == '.')) {
                                throw new ServiceConfigurationError(Messages.getString("imageio.99", line));
                            }
                        }
                        names.add(line);
                    }
                }
            } catch (java.io.IOException e) {
                throw new ServiceConfigurationError(e.toString());
            } finally {
                try {
                    if (reader != null) {
                        reader.close();
                    }
                    if (input != null) {
                        input.close();
                    }
                } catch (java.io.IOException e) {
                    throw new ServiceConfigurationError(e.toString());
                }
            }
            
            return names;
        }
        
        public bool hasNext() {
            return it.hasNext();
        }

        public Object next() {
            if (!hasNext()) {
                throw new java.util.NoSuchElementException();
            }
            
            String name = (String)it.next();
            try {
                return java.lang.Class.forName(name, true, loader).newInstance();
            } catch (Exception e) {
                throw new ServiceConfigurationError(e.toString());
            }
        }

        public void remove() {
            throw new java.lang.UnsupportedOperationException();   
        }
    }
    
    /**
     * An Error that can be thrown when something wrong occurs in loading a service
     * provider.
     */
    class ServiceConfigurationError : java.lang.Error {
        
        private const long serialVersionUID = 74132770414881L;

        /**
         * The constructor
         * 
         * @param msg
         *            the message of this error
         */
        public ServiceConfigurationError(String msg) :
            base(msg){
        }

        /**
         * The constructor
         * 
         * @param msg
         *            the message of this error
         * @param cause 
         *            the cause of this error
         */
        public ServiceConfigurationError(String msg, java.lang.Throwable cause) :
            base(msg, cause){
        }
    }
	}
}
