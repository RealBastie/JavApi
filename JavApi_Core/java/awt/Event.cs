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

namespace biz.ritter.javapi.awt
{
    /**
     * @author Dmitry A. Durnev
     */

    [Serializable]
    public class Event : java.io.Serializable
    {
        private static readonly long serialVersionUID = 5488922509400504703L;

        public const int SHIFT_MASK = 1;

        public const int CTRL_MASK = 2;

        public const int META_MASK = 4;

        public const int ALT_MASK = 8;

        public const int HOME = 1000;

        public const int END = 1001;

        public const int PGUP = 1002;

        public const int PGDN = 1003;

        public const int UP = 1004;

        public const int DOWN = 1005;

        public const int LEFT = 1006;

        public const int RIGHT = 1007;

        public const int F1 = 1008;

        public const int F2 = 1009;

        public const int F3 = 1010;

        public const int F4 = 1011;

        public const int F5 = 1012;

        public const int F6 = 1013;

        public const int F7 = 1014;

        public const int F8 = 1015;

        public const int F9 = 1016;

        public const int F10 = 1017;

        public const int F11 = 1018;

        public const int F12 = 1019;

        public const int PRINT_SCREEN = 1020;

        public const int SCROLL_LOCK = 1021;

        public const int CAPS_LOCK = 1022;

        public const int NUM_LOCK = 1023;

        public const int PAUSE = 1024;

        public const int INSERT = 1025;

        public const int ENTER = 10;

        public const int BACK_SPACE = 8;

        public const int TAB = 9;

        public const int ESCAPE = 27;

        public const int DELETE = 127;

        public const int WINDOW_DESTROY = 201;

        public const int WINDOW_EXPOSE = 202;

        public const int WINDOW_ICONIFY = 203;

        public const int WINDOW_DEICONIFY = 204;

        public const int WINDOW_MOVED = 205;

        public const int KEY_PRESS = 401;

        public const int KEY_RELEASE = 402;

        public const int KEY_ACTION = 403;

        public const int KEY_ACTION_RELEASE = 404;

        public const int MOUSE_DOWN = 501;

        public const int MOUSE_UP = 502;

        public const int MOUSE_MOVE = 503;

        public const int MOUSE_ENTER = 504;

        public const int MOUSE_EXIT = 505;

        public const int MOUSE_DRAG = 506;

        public const int SCROLL_LINE_UP = 601;

        public const int SCROLL_LINE_DOWN = 602;

        public const int SCROLL_PAGE_UP = 603;

        public const int SCROLL_PAGE_DOWN = 604;

        public const int SCROLL_ABSOLUTE = 605;

        public const int SCROLL_BEGIN = 606;

        public const int SCROLL_END = 607;

        public const int LIST_SELECT = 701;

        public const int LIST_DESELECT = 702;

        public const int ACTION_EVENT = 1001;

        public const int LOAD_FILE = 1002;

        public const int SAVE_FILE = 1003;

        public const int GOT_FOCUS = 1004;

        public const int LOST_FOCUS = 1005;

        public Object target;

        public long when;

        public int id;

        public int x;

        public int y;

        public int key;

        public int modifiers;

        public int clickCount;

        public Object arg;

        public Event evt;

        public Event(Object target, int id, Object arg) :
            this(target, 0l, id, 0, 0, 0, 0, arg)
        {
        }

        public Event(Object target, long when, int id, int x, int y, int key, int modifiers) :
            this(target, when, id, x, y, key, modifiers, null)
        {
        }

        public Event(Object target, long when, int id, int x, int y, int key, int modifiers, Object arg)
        {
            this.target = target;
            this.when = when;
            this.id = id;
            this.x = x;
            this.y = y;
            this.key = key;
            this.modifiers = modifiers;
            this.arg = arg;
        }

        public override String ToString()
        {
            /* The format is based on 1.5 release behavior 
             * which can be revealed by the following code:
             * 
             * Event e = new Event(new Button(), 0l, Event.KEY_PRESS, 
             *                     0, 0, Event.TAB, Event.SHIFT_MASK, "arg");
             * System.out.println(e);
             */

            return this.getClass().getName() + "[" + paramString() + "]"; //$NON-NLS-1$ //$NON-NLS-2$
        }

        protected String paramString()
        {
            return "id=" + id + ",x=" + x + ",y=" + y + //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
            (key != 0 ? ",key=" + key + getModifiersString() : "") + //$NON-NLS-1$ //$NON-NLS-2$
            ",target=" + target + //$NON-NLS-1$
            (arg != null ? ",arg=" + arg : ""); //$NON-NLS-1$ //$NON-NLS-2$
        }

        private String getModifiersString()
        {
            String strMod = ""; //$NON-NLS-1$
            if (shiftDown())
            {
                strMod += ",shift"; //$NON-NLS-1$
            }
            if (controlDown())
            {
                strMod += ",control"; //$NON-NLS-1$
            }
            if (metaDown())
            {
                strMod += ",meta"; //$NON-NLS-1$
            }
            return strMod;
        }

        public void translate(int dx, int dy)
        {
            x += dx;
            y += dy;
        }

        public bool controlDown()
        {
            return (modifiers & CTRL_MASK) != 0;
        }

        public bool metaDown()
        {
            return (modifiers & META_MASK) != 0;
        }

        public bool shiftDown()
        {
            return (modifiers & SHIFT_MASK) != 0;
        }

    }

}