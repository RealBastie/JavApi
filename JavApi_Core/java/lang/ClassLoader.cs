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
 *  Copyright © 2011 Sebastian Ritter
 */
using System;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.lang
{
    public class ClassLoader
    {
        protected ClassLoader parent;

        private static ClassLoader systemClassLoader;
        static ClassLoader()
        {
            systemClassLoader = new ClassLoader(null);
        }

        protected ClassLoader(ClassLoader parent)
        {
            this.parent = parent;
        }

        public java.lang.Class loadClass(String className)
        {
            return new Class(Type.GetType (className));
        }

        public static ClassLoader getSystemClassLoader() {
            return systemClassLoader;
        }
        internal static void setSystemClassLoader(ClassLoader cl)
        {
            systemClassLoader = cl;
        }

        /// <summary>
        /// Creates an InputStream for given resource. Resources are define as path with slash separator /. 
        /// Returns null if no resource found.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>InputSteam or null</returns>
        public java.io.InputStream getResourceAsStream(String name)
        {
            java.io.InputStream result = null;
            if (null != getParent())
            {
                result = getParent().getResourceAsStream(name);
            }
            if (null == result)
            {
                java.net.URL url = this.findResource(name);
            }
            return result;
        }
        public static java.io.InputStream getSystemResourceAsStream(String name)
        {
            return systemClassLoader.getResourceAsStream(name);
        }

        public ClassLoader getParent()
        {
            return this.parent;
        }

        public java.net.URL getResource(String name)
        {
            /*think TODO:
             * NullPointerException
             * Protocol "file"
             * OS root - for example Linux /, Windows <exec-drive>:\
             */
            return new java.net.URL(name);
        }

        /// <summary>
        /// Find the resource with given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected java.net.URL findResource(String name)
        {
            Trace.AutoFlush = true;
            Trace.Indent();
            java.net.URL result = null;

            //Steps:
            // * 1. Look in self assembly...
            // * 2. Locate Directory...
            // * 3. List Directory
            // * 4. Load Assembly
            // * 5. Look for Resource
            // * 6.1 If resource not found - unload
            // * 6.2 If resource found - create URL and return

            System.IO.Stream stream = null;

            // Look simple...
            Trace.TraceInformation("Look simple with LoadResource...");
            // Look in ${JAVAPI_HOME}/lib/endorsed for assemblies
            Trace.TraceInformation("Look in ${JAVAPI_HOME}/lib/endorsed...");
            // Look in sun.boot.class.path specific directories for assemblies
            Trace.TraceInformation("Look in sun.boot.class.path...");
            // Look in JavApi assembly for resource
            Trace.TraceInformation("Look in "+this.GetType().Assembly.FullName);
            stream = this.getStreamFromAssembly(name, this.GetType().Assembly);
            if (null != stream)
            {
                stream.Close();
                result = new java.net.URL("assembly://" + this.GetType().Assembly.GetName() + "/" + name);
            }
            else
            {

                // Look in ${JAVAPI_HOME}/lib/ext path for assemblies
                Trace.TraceInformation("Look in ${JAVAPI_HOME}/lib/ext...");
                // Look in java.ext.dirs specific directories for assemblies
                Trace.TraceInformation("Look in java.ext.dirs...");
                // Look in java.class.path specific directories for assemblies
                Trace.TraceInformation("Look in java.class.path...");
                // Look in run directory for assembles
                Trace.TraceInformation("Look in execution directory...");
            }

            if (result == null) Trace.TraceWarning("Resource locating not fully implemented!");

            Trace.TraceInformation("Found resource at " + result.ToString());
            Trace.Unindent();
            return result;
        }

        private System.IO.Stream getStreamFromAssembly(String name, Assembly ass)
        {
            try
            {
                return ass.GetManifestResourceStream(ass.GetName().Name + "." + name);
            }
            catch {
                return null;
            }
        }
    }
}
