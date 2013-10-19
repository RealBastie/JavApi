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
    public class WindowEvent : ComponentEvent
    {

        private const long serialVersionUID = -1567959133147912127L;

        public const int WINDOW_FIRST = 200;

        public const int WINDOW_OPENED = 200;

        public const int WINDOW_CLOSING = 201;

        public const int WINDOW_CLOSED = 202;

        public const int WINDOW_ICONIFIED = 203;

        public const int WINDOW_DEICONIFIED = 204;

        public const int WINDOW_ACTIVATED = 205;

        public const int WINDOW_DEACTIVATED = 206;

        public const int WINDOW_GAINED_FOCUS = 207;

        public const int WINDOW_LOST_FOCUS = 208;

        public const int WINDOW_STATE_CHANGED = 209;

        public const int WINDOW_LAST = 209;

        private Window oppositeWindow;
        private int oldState;
        private int newState;

        public WindowEvent(Window source, int id) :
            this(source, id, null)
        {
        }

        public WindowEvent(Window source, int id, Window opposite) :
            this(source, id, opposite, Frame.NORMAL, Frame.NORMAL)
        {
        }

        public WindowEvent(Window source, int id, int oldState, int newState) :
            this(source, id, null, oldState, newState)
        {
        }

        public WindowEvent(Window source, int id, Window opposite,
                           int oldState, int newState) :
            base(source, id)
        {

            oppositeWindow = opposite;
            this.oldState = oldState;
            this.newState = newState;
        }

        public int getNewState()
        {
            return newState;
        }

        public int getOldState()
        {
            return oldState;
        }

        public Window getOppositeWindow()
        {
            return oppositeWindow;
        }

        public Window getWindow()
        {
            return (Window)source;
        }

        public override String paramString()
        {
            /* The format is based on 1.5 release behavior 
             * which can be revealed by the following code:
             * 
             * WindowEvent e = new WindowEvent(new Frame(), 
             *          WindowEvent.WINDOW_OPENED); 
             * System.out.println(e);
             */

            String typeString = null;

            switch (id)
            {
                case WINDOW_OPENED:
                    typeString = "WINDOW_OPENED"; //$NON-NLS-1$
                    break;
                case WINDOW_CLOSING:
                    typeString = "WINDOW_CLOSING"; //$NON-NLS-1$
                    break;
                case WINDOW_CLOSED:
                    typeString = "WINDOW_CLOSED"; //$NON-NLS-1$
                    break;
                case WINDOW_ICONIFIED:
                    typeString = "WINDOW_ICONIFIED"; //$NON-NLS-1$
                    break;
                case WINDOW_DEICONIFIED:
                    typeString = "WINDOW_DEICONIFIED"; //$NON-NLS-1$
                    break;
                case WINDOW_ACTIVATED:
                    typeString = "WINDOW_ACTIVATED"; //$NON-NLS-1$
                    break;
                case WINDOW_DEACTIVATED:
                    typeString = "WINDOW_DEACTIVATED"; //$NON-NLS-1$
                    break;
                case WINDOW_GAINED_FOCUS:
                    typeString = "WINDOW_GAINED_FOCUS"; //$NON-NLS-1$
                    break;
                case WINDOW_LOST_FOCUS:
                    typeString = "WINDOW_LOST_FOCUS"; //$NON-NLS-1$
                    break;
                case WINDOW_STATE_CHANGED:
                    typeString = "WINDOW_STATE_CHANGED"; //$NON-NLS-1$
                    break;
                default:
                    typeString = "unknown type"; //$NON-NLS-1$
                    break;
            }

            return typeString + ",opposite=" + oppositeWindow + //$NON-NLS-1$
                    ",oldState=" + oldState + ",newState=" + newState; //$NON-NLS-1$ //$NON-NLS-2$
        }

    }
}