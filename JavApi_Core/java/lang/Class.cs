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
 *  Copyright © 2011,2012 Sebastian Ritter
 */
using System;
using System.Reflection;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.lang
{
    public class Class
    {

        internal readonly Type delegateInstance;
        public Class(Type t)
        {
            this.delegateInstance = t;
        }
        /// <summary>
        /// Return the method
        /// </summary>
        /// <param name="name"></param>
        /// <param name="paramTypes"></param>
        /// <returns></returns>
        public java.lang.reflect.Method getMethod(String name, Class[] paramTypes)
        {
            Type[] pTypes = new Type[paramTypes.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                pTypes[i] = paramTypes[i].delegateInstance;
            }
            return this.getMethod(name, pTypes);
        }

        /// <summary>
        /// In addition, a method is provided to implement some nearlier at .net framework.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="paramTypes"></param>
        /// <returns></returns>
        /// <exception cref="java.lang.NoSuchMethodException"></exception>
        public java.lang.reflect.Method getMethod(String name, Type[] paramTypes)
        {
            if (null == name) throw new java.lang.NullPointerException();
            MethodInfo mi = this.delegateInstance.GetMethod(name, paramTypes);
            if (null == mi) throw new java.lang.NoSuchMethodException();
            return new java.lang.reflect.Method(mi);
        }

        /// <summary>
        /// Return the constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="paramTypes"></param>
        /// <returns></returns>
        public java.lang.reflect.Constructor getConstructor(Class[] paramTypes)
        {
            Type[] pTypes = new Type[paramTypes.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                pTypes[i] = paramTypes[i].delegateInstance;
            }
            return this.getConstructor(pTypes);
        }

        public java.lang.reflect.Constructor getConstructor(Type[] paramType)
        {
            System.Reflection.ConstructorInfo ci = this.delegateInstance.GetConstructor(paramType);
            return new java.lang.reflect.Constructor(ci);
        }

        /// <summary>
        /// This helper method returns the underlying .net type.
        /// </summary>
        /// <returns>The type that wrapped.</returns>
        public Type getDelegateInstance()
        {
            return this.delegateInstance;
        }
        /// <summary>
        /// Return the name
        /// </summary>
        /// <returns>class name</returns>
        public String getName()
        {
            return this.delegateInstance.Name;
        }

        /// <summary>
        /// Get all declared Method of this type.
        /// </summary>
        /// <returns>method array</returns>
        public java.lang.reflect.Method[] getDeclaredMethods()
        {
            MethodInfo [] mi = this.delegateInstance.GetMethods();
            java.lang.reflect.Method[] result = new java.lang.reflect.Method[mi.Length];
            for (int i = 0; i < mi.Length; i++)
            {
                result[i] = new java.lang.reflect.Method(mi[i]);
            }
            return result;
        }

        /// <summary>
        /// Return the class from which the current class directly inherit.
        /// </summary>
        /// <returns>super class</returns>
        public Class getSuperclass()
        {
            return new Class(this.delegateInstance.BaseType);
        }

        public bool isAssignableFrom(Class c)
        {
            return this.delegateInstance.IsAssignableFrom(c.delegateInstance);
        }

        /// <summary>
        /// Check given object is instance of class type.
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>true if obj is type</returns>
        public bool isInstance(Object obj)
        {
            return this.delegateInstance.IsInstanceOfType(obj);
        }

        /// <summary>
        /// Create a new instance by calling default constructor
        /// </summary>
        /// <returns>instance of type</returns>
        public Object newInstance()
        {
            Object result = Activator.CreateInstance(this.delegateInstance);
            return result;
        }
        /// <summary>
        /// Search the system for given class and return this.
        /// </summary>
        /// <param name="className">see System.Type.AssemblyQualifedName</param>
        /// <returns></returns>
        public static Class forName(String className) {//throws ClassNotFoundException {
            return forName(className, true, ClassLoader.getSystemClassLoader());
        }
        /// <summary>
        /// Search the system for given class and return this.
        /// </summary>
        /// <param name="className">see System.Type.AssemblyQualifedName</param>
        /// <param name="cl">ClassLoader - ignored</param>
        /// <param name="initialize">ignored - ever like true</param>
        /// <returns></returns>
        public static Class forName(String className, bool initialize, ClassLoader cl)
        {//throws ClassNotFoundException {
            try
            {
                System.Type t = System.Type.GetType(className);
                if (null == t) throw new java.lang.ClassNotFoundException(className);
                return new Class(t);
            }
            catch
            {
                throw new java.lang.ClassNotFoundException(className);
            }

        }
        /// <summary>
        /// Returns the systen class loader
        /// </summary>
        /// <returns></returns>
        public ClassLoader getClassLoader()
        {
            return ClassLoader.getSystemClassLoader();
        }

        public override int GetHashCode()
        {
            return this.delegateInstance.GetHashCode();
        }
        public override bool Equals(Object obj)
        {
            return this.delegateInstance.Equals(obj);
        }
        public override string ToString()
        {
            return this.delegateInstance.ToString();
        }

        public Object[] getSigners()
        {
            return null;
        }

        public Package getPackage()
        {
            return new Package(this);
        }

        public java.net.URL getResource(String name)
        {
            String root = "";
            if (name.startsWith("/"))
            {
                name = name.substring(1);
            }
            else
            {
                String package = this.getClass().getPackage().getName();
                root = package.replace('.', '/') + '/';
            }
            name = root + name;
            return getClassLoader().getResource(name);
        }
        public java.io.InputStream getResourceAsStream(String name)
        {
            java.net.URL url = getResource(name);
            return url.openStream();
        }
    }
}
