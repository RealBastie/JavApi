﻿/*
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
 *  Copyright © 2011 Sebastian Ritter
 */
using System;
using System.Collections.Generic;
using System.Collections;

using java = biz.ritter.javapi;

namespace biz.ritter.javapi.util
{
    
    public class Properties : java.util.Hashtable<String,String> 
    {
        public Properties()
        {
        }

        public Properties(Properties defaultJ)
        {
            this.copyPropertiesIntoThisInstance(defaultJ.GetEnumerator());
        }

        public void load(java.io.InputStream toLoad)
        {
            this.load(new java.io.InputStreamReader(toLoad));
        }

        public void load(java.io.Reader toLoad)
        {
            Kajabity.Tools.Java.JavaPropertyReader reader = new Kajabity.Tools.Java.JavaPropertyReader(this);
            reader.Parse(toLoad);
        }

        public void store(java.io.OutputStream toWrite, String comments)
        {
            this.store(new java.io.OutputStreamWriter(toWrite), comments);
        }
        public void store(java.io.Writer toWrite, String comments)
        {
            Kajabity.Tools.Java.JavaPropertyWriter writer = new Kajabity.Tools.Java.JavaPropertyWriter(this);
            writer.Write(toWrite, comments);
        }

        /// <summary>
        /// Set the property value for name
        /// </summary>
        /// <param name="name">Name of property</param>
        /// <param name="value">Value of property called name</param>
        /// <returns>same as Hashtable.put(key,value)</returns>
        /// <see cref="biz.ritter.javapi.util.Hashtable.put(String,String)"/>
        public Object setProperty(String name, String value)
        {
            return this.put(name, value);
        }

        /// <summary>
        /// Helpermethod for easier .net implementation
        /// </summary>
        /// <param name="defaultJ"></param>
        internal Properties(IDictionary defaultJ)
        {
            this.copyPropertiesIntoThisInstance(defaultJ.GetEnumerator());
        }

        protected virtual void copyPropertiesIntoThisInstance (IDictionaryEnumerator e) {
            while (e.MoveNext())
            {
                String key = e.Key.ToString();
                String value = e.Value.ToString();
                this.Add(key, value);
            }
        }

        public virtual void list(java.lang.PrintStream output)
        {
            output.println("-- listing properties --");
            IDictionaryEnumerator e = this.GetEnumerator();
            while (e.MoveNext())
            {
                String key = e.Key.ToString();
                String value = null == e.Value ? "" : e.Value.ToString();
                if (value.Length > 40)
                {
                    value = value.Substring(0,37)+"...";
                }
                output.println(key + "=" + value);
            }
        }
        public virtual String getProperty(String key)
        {
            return this.getProperty(key, null);
        }
        public virtual String getProperty(String key, String defaultValue)
        {
            if (this.ContainsKey(key))
            {
                return this[key];
            }
            else
            {
                return defaultValue;
            }
        }
        public virtual java.util.Enumeration<Object> propertyNames()
        {
            return new IAC_PropertyNameEnumeration(this);
        }
    }

    internal class IAC_PropertyNameEnumeration : Enumeration<Object> {

        private readonly IEnumerator root;
        private Object next = null;

        internal IAC_PropertyNameEnumeration(Properties p)
        {
            this.root = p.Keys.GetEnumerator();
        }
        public bool hasMoreElements()
        {
            bool more = root.MoveNext();
            next = this.root.Current;
            return more;
        }

        /**
         * Returns the next element in this {@code Enumeration}.
         * 
         * @return the next element..
         * @throws NoSuchElementException
         *             if there are no more elements.
         * @see #hasMoreElements
         */
        public Object nextElement()
        {
            bool more = next == null;
            if (!more) {
                more = this.hasMoreElements();
            }
            if (more)
            {
                return root.Current;
            }
            throw new NoSuchElementException();
        }
    }
}
