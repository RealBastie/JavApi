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

namespace biz.ritter.javapi.lang
{
    public class Thread : Runnable 
    {
        private java.util.HashMap<ThreadLocal<Object>,Object> threadLocalStore = new java.util.HashMap<ThreadLocal<Object>,Object>();
        private Runnable runnable;
        System.Threading.Thread delegateInstance;

        private ClassLoader contextClassLoader = null;

        /// <summary>
        /// Return the current thread
        /// </summary>
        /// <returns></returns>
        public static java.lang.Thread currentThread()
        {
            java.lang.Thread result = new java.lang.Thread();
            result.delegateInstance = System.Threading.Thread.CurrentThread;
            return result;
        }

        /// <summary>
        /// Return the System ClassLoader
        /// </summary>
        /// <returns></returns>
        public ClassLoader getContextClassLoader()
        {
            if (null == contextClassLoader)
            {
                this.contextClassLoader =  ClassLoader.getSystemClassLoader();
            }
            return this.contextClassLoader;
        }

        /// <summary>
        /// Set the context class loader
        /// </summary>
        /// <param name="cl"></param>
        public void setContextClassLoader(ClassLoader cl)
        {
            if (null == cl) throw new NullPointerException("Classloader can not be null");
            this.contextClassLoader = cl;
        }

        /// <summary>
        /// Set the name of Thread instance to newName
        /// </summary>
        /// <param name="newName"></param>
        public void setName(String newName)
        {
            delegateInstance.Name = newName;
        }
        /// <summary>
        /// Get the name of Thread instance
        /// </summary>
        /// <returns></returns>
        public String getName()
        {
            return delegateInstance.Name;
        }
        /// <summary>
        /// Set this Thread instance to background / daemon thread or not.
        /// </summary>
        /// <param name="onOrOff"></param>
        public void setDaemon(bool onOrOff)
        {
            delegateInstance.IsBackground = true;
        }
        /// <summary>
        /// Return Thread instance is background / deamon thread information
        /// </summary>
        /// <returns></returns>
        public bool isDaemon()
        {
            return delegateInstance.IsBackground;
        }
        /// <summary>
        /// Construct new Thread instance
        /// </summary>
        public Thread()
        {
            this.runnable = this;
            this.delegateInstance = new System.Threading.Thread(new System.Threading.ThreadStart(this.runnable.run));
        }
        /// <summary>
        /// Construct new Thread instance for giving Runnable instance
        /// </summary>
        /// <param name="toRun"></param>
        public Thread(Runnable toRun)
        {
            this.runnable = toRun;
            this.delegateInstance = new System.Threading.Thread(new System.Threading.ThreadStart(this.runnable.run));
        }
        /// <summary>
        /// Construct new named Thread instance
        /// </summary>
        public Thread(String name) : this()
        {
            this.runnable = this;
            this.setName(name);
        }
        /// <summary>
        /// Construct new named Thread instance for giving Runnable instance
        /// </summary>
        /// <param name="toRun"></param>
        /// <param name="name"></param>
        public Thread(Runnable toRun, String name)
        {
            this.runnable = toRun;
            this.delegateInstance = new System.Threading.Thread(new System.Threading.ThreadStart(this.runnable.run));
            this.setName(name);
        }

        /// <summary>
        /// All todo is implemented in run method.
        /// </summary>
        public virtual void run () {}
        /// <summary>
        /// Start the Thread instance
        /// </summary>
        public void start() {
            try
            {
                this.delegateInstance.Start();
            }
            catch (System.Threading.ThreadStateException tse)
            {
                throw new IllegalThreadStateException(tse.Message);
            }
        }
            
        /// <summary>
        /// Return the State of Thread instance
        /// </summary>
        /// <returns></returns>
        public Thread.State getState()
        {
            switch (this.delegateInstance.ThreadState)
            {
                case System.Threading.ThreadState.Running :
                    return State.RUNNABLE;
                case System.Threading.ThreadState.Unstarted :
                    return State.NEW;
                case System.Threading.ThreadState.WaitSleepJoin :
                    return State.WAITING;
                case System.Threading.ThreadState.Stopped | System.Threading.ThreadState.Aborted :
                    return State.TERMINATED;
                default:
                    return State.RUNNABLE;
            }
        }

        /// <summary>
        /// Thread is started but not died.
        /// </summary>
        /// <returns>Thread running true / false</returns>
        public bool isAlive()
        {
            return State.RUNNABLE == this.getState();
        }

        ///<summary>
        /// A representation of a thread's state. A given thread may only be in one state at a time.
        ///</summary>
        public enum State
        {
            /**
             * The thread has been created, but has never been started.
             */
            NEW,
            /**
             * The thread may be run.
             */
            RUNNABLE,
            /**
             * The thread is blocked and waiting for a lock.
             */
            BLOCKED,
            /**
             * The thread is waiting.
             */
            WAITING,
            /**
             * The thread is waiting for a specified amount of time.
             */
            TIMED_WAITING,
            /**
             * The thread has been terminated.
             */
            TERMINATED
        }

        /// <summary>
        /// Let the Thread instance sleeping
        /// </summary>
        /// <param name="millis"></param>
        public static void sleep(int millis)
        {
            if (millis < 0) throw new IllegalArgumentException("Sleep time need to be greater or equals zero");
            System.Threading.Thread.Sleep(millis);
        }

        internal void setThreadLocal (ThreadLocal<Object> tl, Object obj) {
            this.threadLocalStore.put(tl, obj);
        }
        internal Object getThreadLocal (ThreadLocal<Object> tl) {
            return this.threadLocalStore.get(tl);
        }


        public void interrupt()
        {
            try
            {
                this.delegateInstance.Interrupt();
            }
            catch (System.Security.SecurityException se)
            {
                throw new java.lang.SecurityException(se.Message);
            }
        }
        public void stop() {
            this.stop(new ThreadDeath());
        }
        public void stop(java.lang.Throwable t)
        {
            try
            {
                this.delegateInstance.Abort();
            }
            catch (System.Threading.ThreadAbortException) { }
            catch (System.Threading.ThreadStateException) { }
            throw t;
        }
    }
}
