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
    public class Window : Component
    {
        /// <summary>
        /// Containts the implementation class for an AWT Dialog.
        /// </summary>
        internal java.awt.peer.WindowPeer delegateInstance;

        public Window()
        {
            this.delegateInstance = Toolkit.getDefaultToolkit().createWindow(this);
        }

        public override void setVisible(bool showMe)
        {
            this.delegateInstance.setVisible(showMe);
        }
        public override int getHeight()
        {
            return this.delegateInstance.getHeight();
        }
        public override int getWidth()
        {
            return this.delegateInstance.getWidth();
        }
        public override int getX()
        {
            return this.delegateInstance.getX();
        }
        public override int getY()
        {
            return this.delegateInstance.getY();
        }
    }
}
