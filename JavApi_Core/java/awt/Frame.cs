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
using System.Text;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt
{
    [Serializable]
    public class Frame
    {
        /// <summary>
        /// Containts the implementation class for an AWT Frame.
        /// </summary>
        internal java.awt.peer.FramePeer delegateInstance;

        private const long serialVersionUID = 2673458971256075116L;

        [Obsolete]
        public const int DEFAULT_CURSOR = 0;

        [Obsolete]
        public const int CROSSHAIR_CURSOR = 1;

        [Obsolete]
        public const int TEXT_CURSOR = 2;

        [Obsolete]
        public const int WAIT_CURSOR = 3;

        [Obsolete]
        public const int SW_RESIZE_CURSOR = 4;

        [Obsolete]
        public const int SE_RESIZE_CURSOR = 5;

        [Obsolete]
        public const int NW_RESIZE_CURSOR = 6;

        [Obsolete]
        public const int NE_RESIZE_CURSOR = 7;

        [Obsolete]
        public const int N_RESIZE_CURSOR = 8;

        [Obsolete]
        public const int S_RESIZE_CURSOR = 9;

        [Obsolete]
        public const int W_RESIZE_CURSOR = 10;

        [Obsolete]
        public const int E_RESIZE_CURSOR = 11;

        [Obsolete]
        public const int HAND_CURSOR = 12;

        [Obsolete]
        public const int MOVE_CURSOR = 13;

        public const int NORMAL = 0;

        public const int ICONIFIED = 1;

        public const int MAXIMIZED_HORIZ = 2;

        public const int MAXIMIZED_VERT = 4;

        public const int MAXIMIZED_BOTH = 6;

        public Frame()
        {
            this.delegateInstance = Toolkit.getDefaultToolkit().createFrame(this);
        }
        public Frame(String title)
            : this()
        {
            delegateInstance.setTitle(title);
        }
        public virtual void setTitle(String newTitle)
        {
            this.delegateInstance.setTitle(newTitle);
        }
        public virtual void setVisible(bool showMe)
        {
            this.delegateInstance.setVisible(showMe);
        }

        public virtual void pack()
        {
            this.delegateInstance.pack();
        }
        public virtual void setSize(int width, int height)
        {
            this.delegateInstance.setSize(width, height);
        }
    }
}