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
    public class DialogFormsPeer : System.Windows.Forms.Form, java.awt.peer.DialogPeer
    {
        public DialogFormsPeer()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
        }

        /// <summary>
        /// Set the title of frame
        /// </summary>
        /// <param name="newTitle">title</param>
        public void setTitle(String newTitle)
        {
            this.Text = newTitle;
        }
        /// <summary>
        /// Display or hides this dialog and contained child GUI element.
        /// </summary>
        /// <param name="showMe">true if to display, false if to hide</param>
        public void setVisible(bool showMe)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                this.Visible = showMe;
            }
            else
            {
                System.Windows.Forms.Application.Run(this);
            }
        }

        public void show()
        {
            this.setVisible(true);
        }
        public int getHeight()
        {
            return this.Height;
        }
        public int getWidth()
        {
            return this.Width;
        }
        public int getX()
        {
            return this.Left;
        }
        public int getY()
        {
            return this.Top;
        }
    }
}
