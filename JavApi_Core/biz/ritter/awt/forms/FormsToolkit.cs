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

namespace biz.ritter.awt.forms
{
    /// <summary>
    /// FormsToolkit is the AWT implementation based on Windows Forms
    /// </summary>
    public class FormsToolkit : java.awt.Toolkit
    {
        /// <summary>
        /// Create a FramePeer object
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected internal override java.awt.peer.FramePeer createFrame(java.awt.Frame target)
        {
            return new FrameFormsPeer();
        }
        /// <summary>
        /// Create a DialogPeer object
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected internal override java.awt.peer.DialogPeer createDialog(java.awt.Dialog target)
        {
            return new DialogFormsPeer();
        }
        /// <summary>
        /// Create a WindowPeer object
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected internal override java.awt.peer.WindowPeer createWindow(java.awt.Window target)
        {
            return new WindowFormsPeer();
        }
    }
}
