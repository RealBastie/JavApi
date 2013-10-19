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

namespace biz.ritter.javapi.awt.eventj
{
    /**
     * @author Michael Danilov
     */
    [Serializable]
    public class InvocationEvent : AWTEvent, ActiveEvent
    {

        private const long serialVersionUID = 436056344909459450L;

        public const int INVOCATION_FIRST = 1200;

        public const int INVOCATION_DEFAULT = 1200;

        public const int INVOCATION_LAST = 1200;

        protected java.lang.Runnable runnable;

        protected Object notifier;

        protected bool catchExceptions;

        private long when;
        private java.lang.Throwable throwable;

        public InvocationEvent(Object source, java.lang.Runnable runnable) :
            this(source, runnable, null, false)
        {
        }

        public InvocationEvent(Object source, java.lang.Runnable runnable,
                               Object notifier, bool catchExceptions) :
            this(source, INVOCATION_DEFAULT, runnable, notifier, catchExceptions)
        {
        }

        protected InvocationEvent(Object source, int id, java.lang.Runnable runnable,
                Object notifier, bool catchExceptions)
            :
                base(source, id)
        {

            // awt.18C=Cannot invoke null runnable
            System.Diagnostics.Debug.Assert(runnable != null, "Cannot invoke null runnable"); //$NON-NLS-1$

            if (source == null)
            {
                // awt.18D=Source is null
                throw new java.lang.IllegalArgumentException("Source is null"); //$NON-NLS-1$
            }
            this.runnable = runnable;
            this.notifier = notifier;
            this.catchExceptions = catchExceptions;

            throwable = null;
            when = java.lang.SystemJ.currentTimeMillis();
        }

        public void dispatch()
        {
            if (!catchExceptions)
            {
                runAndNotify();
            }
            else
            {
                try
                {
                    runAndNotify();
                }
                catch (java.lang.Throwable t)
                {
                    throwable = t;
                }
            }
        }

        private void runAndNotify()
        {
            if (notifier != null)
            {
                lock (notifier)
                {
                    try
                    {
                        runnable.run();
                    }
                    finally
                    {
                        notifier.notifyAll();
                    }
                }
            }
            else
            {
                runnable.run();
            }
        }

        public java.lang.Exception getException()
        {
            return (throwable != null && throwable is java.lang.Exception) ?
                    (java.lang.Exception)throwable : null;
        }

        public java.lang.Throwable getThrowable()
        {
            return throwable;
        }

        public long getWhen()
        {
            return when;
        }

        public String paramString()
        {
            /* The format is based on 1.5 release behavior 
             * which can be revealed by the following code:
             * 
             * InvocationEvent e = new InvocationEvent(new Component(){},
             *       new Runnable() { public void run(){} });
             * System.out.println(e);
             */

            return ((id == INVOCATION_DEFAULT ? "INVOCATION_DEFAULT" : "unknown type") + //$NON-NLS-1$ //$NON-NLS-2$
                    ",runnable=" + runnable + //$NON-NLS-1$
                    ",notifier=" + notifier + //$NON-NLS-1$
                    ",catchExceptions=" + catchExceptions + //$NON-NLS-1$
                    ",when=" + when); //$NON-NLS-1$
        }

    }
}