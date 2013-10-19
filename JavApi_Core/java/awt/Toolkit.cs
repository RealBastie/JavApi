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
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt
{
    public abstract class Toolkit
    {
        /// <summary>
        /// Create a Frame.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected internal abstract java.awt.peer.FramePeer createFrame(java.awt.Frame target);

        /// <summary>
        /// Create a Dialog.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected internal abstract java.awt.peer.DialogPeer createDialog(java.awt.Dialog target);

        /// <summary>
        /// Create a Window.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected internal abstract java.awt.peer.WindowPeer createWindow(java.awt.Window target);

        /// <summary>
        /// Gets the default toolkit.
        /// </summary>
        /// <returns></returns>
        public static Toolkit getDefaultToolkit()
        {
            String isHeadless = java.lang.SystemJ.getProperty("java.awt.headless", "false");
            String toolkitClass = java.lang.SystemJ.getProperty("awt.toolkit", "biz.ritter.awt.forms.FormsToolkit");
            if ("true".equals(isHeadless)) throw new AWTError("Headless is not yet implemented");
            try
            {
                Toolkit result = (Toolkit)java.lang.Class.forName(toolkitClass).newInstance();
                return result;
            }
            catch
            {
                throw new AWTError("Toolkit implementation " + toolkitClass + " not found or instantable.");
            }
        }

        /// <summary>
        /// Method for Apache Harmony compatiblity
        /// </summary>
        internal void lockAWT(){}
        /// <summary>
        /// Method for Apache Harmony compatiblity
        /// </summary>
        internal void unlockAWT() { }

        //protected internal abstract EventQueue getSystemEventQueueImpl();
    }
}