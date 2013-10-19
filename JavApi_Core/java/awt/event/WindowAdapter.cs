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

namespace biz.ritter.javapi.awt.eventj
{
    /// <summary>
    /// Empty implementation of <see cref="WindowListener"/>, <see cref="WindowFocusListener"/> and <see cref="WindowStateListener"/>
    /// </summary>
    public class WindowAdapter : WindowListener, WindowFocusListener, WindowStateListener
    {
        public virtual void windowActivated(WindowEvent e)
        { }
        public virtual void windowClosed(WindowEvent e)
        { }
        public virtual void windowClosing(WindowEvent e)
        { }
        public virtual void windowDeactivated(WindowEvent e)
        { }
        public virtual void windowDeiconified(WindowEvent e)
        { }
        public virtual void windowIconified(WindowEvent e)
        { }
        public virtual void windowOpened(WindowEvent e)
        { }
        public virtual void windowGainedFocus(WindowEvent e)
        { }
        public virtual void windowLostFocus(WindowEvent e)
        { }
        public virtual void windowStateChanged(WindowEvent e)
        { }
    }
}
