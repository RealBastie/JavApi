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

namespace biz.ritter.javapi.lang.reflect{

/**
 * {@code Proxy} defines methods for creating dynamic proxy classes and instances.
 * A proxy class implements a declared set of interfaces and delegates method
 * invocations to an {@code InvocationHandler}.
 *
 * @see InvocationHandler
 * @since 1.3
 */
public class Proxy : java.io.Serializable {

    private const long serialVersionUID = -2222568056686623797L;

    // maps class loaders to created classes by interface names
    private static readonly java.util.Map<java.lang.ClassLoader, java.util.Map<String, java.lang.refj.WeakReference<java.lang.Class>>> loaderCache = new java.util.WeakHashMap<java.lang.ClassLoader, java.util.Map<String, java.lang.refj.WeakReference<java.lang.Class>>>();

    // to find previously created types
    private static readonly java.util.Map<Class, String> proxyCache = new java.util.WeakHashMap<Class, String>();

    private static int NextClassNameIndex = 0;

    /**
     * The invocation handler on which the method calls are dispatched.
     */
    protected InvocationHandler h;

    private Proxy() {
    }

    /**
     * Constructs a new {@code Proxy} instance with the specified invocation
     * handler.
     *
     * @param h
     *            the invocation handler for the newly created proxy
     */
    protected Proxy(InvocationHandler h) {
        this.h = h;
    }





    /**
     * Indicates whether or not the specified class is a dynamically generated
     * proxy class.
     * 
     * @param cl
     *            the class
     * @return {@code true} if the class is a proxy class, {@code false}
     *         otherwise
     * @throws NullPointerException
     *                if the class is {@code null}
     */
    public static bool isProxyClass(Class cl) {
        if (cl == null) {
            throw new NullPointerException();
        }
        lock (proxyCache) {
            return proxyCache.containsKey(cl);
        }
    }

    /**
     * Returns the invocation handler of the specified proxy instance.
     * 
     * @param proxy
     *            the proxy instance
     * @return the invocation handler of the specified proxy instance
     * @throws IllegalArgumentException
     *                if the supplied {@code proxy} is not a proxy object
     */
    public static InvocationHandler getInvocationHandler(Object proxy)
		{//throws IllegalArgumentException {

        if (isProxyClass(proxy.getClass())) {
            return ((Proxy) proxy).h;
        }

			throw new IllegalArgumentException("not a proxy instance"); //$NON-NLS-1$
    }

}
}