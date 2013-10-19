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
    public class Component
    {

        internal bool deprecatedEventHandler = true;

        private ComponentOrientation orientation;

        private Dimension maximumSizeJ;

        private Dimension minimumSizeJ;

        private Dimension preferredSizeJ;

        /// <summary>
        /// Means only size should be changed. (Basties note: excluded von Apache WTK NativeWindow Interface)
        /// </summary>
        internal const int BOUNDS_NOMOVE = 1;

        /// <summary>
        /// Means only position should be changed. (Basties note: excluded von Apache WTK NativeWindow Interface)
        /// </summary>
        internal const int BOUNDS_NOSIZE = 2;

        /// <summary>
        /// The parent container, wich included this component.
        /// </summary>
        internal Container parent;
        /// <summary>
        /// The GUI toolkit
        /// </summary>
        [NonSerialized]
        protected internal readonly Toolkit toolkit = Toolkit.getDefaultToolkit();

        /// <summary>
        /// Flag for (in)validate component.
        /// </summary>
        private bool valid;
        [NonSerialized]
        internal readonly ComponentBehavior behaviour;

        private int boundsMaskParam;

        /// <summary>
        /// Constant for component alignment
        /// </summary>
        public const float TOP_ALIGNMENT = 0.0f;

        /// <summary>
        /// Constant for component alignment
        /// </summary>
        public const float CENTER_ALIGNMENT = 0.5f;

        /// <summary>
        /// Constant for component alignment
        /// </summary>
        public const float BOTTOM_ALIGNMENT = 1.0f;

        /// <summary>
        /// Constant for component alignment
        /// </summary>
        public const float LEFT_ALIGNMENT = 0.0f;

        /// <summary>
        /// Constant for component alignment
        /// </summary>
        public const float RIGHT_ALIGNMENT = 1.0f;

        /// <summary>
        /// Component x location
        /// </summary>
        int x;

        /// <summary>
        /// Component y location
        /// </summary>
        int y;

        /// <summary>
        /// Component width
        /// </summary>
        internal int width;

        /// <summary>
        /// Component height
        /// </summary>
        internal int height;

        private bool visible = true;

        public bool isVisible()
        {
            return this.visible;
        }
        public virtual void setVisible(bool showMe)
        {
            this.visible = showMe;
        }

        public virtual int getWidth()
        {
            return this.width;
        }
        public virtual int getY()
        {
            return y;
        }
        public virtual int getX()
        {
            return x;
        }
        public virtual int getHeight()
        {
            return this.height;
        }

        protected internal virtual Insets getNativeInsets()
        {
            return new Insets(0, 0, 0, 0);
        }

        protected internal virtual Insets getInsets()
        {
            return new Insets(0, 0, 0, 0);
        }
        public virtual void setBounds(Rectangle r)
        {
            toolkit.lockAWT();
            try
            {
                setBounds(r.x, r.y, r.width, r.height);
            }
            finally
            {
                toolkit.unlockAWT();
            }
        }

        [Obsolete]
        public virtual void reshape(int x, int y, int w, int h)
        {
            toolkit.lockAWT();
            try
            {
                setBounds(x, y, w, h, boundsMaskParam, true);
                boundsMaskParam = 0;
            }
            finally
            {
                toolkit.unlockAWT();
            }
        }

        public virtual void setBounds(int x, int y, int w, int h)
        {
            toolkit.lockAWT();
            try
            {
                reshape(x, y, w, h);
            }
            finally
            {
                toolkit.unlockAWT();
            }
        }
        /**
         * Update the component bounds and post the appropriate events
         */
        void setBounds(int x, int y, int w, int h, int bMask, bool updateBehavior)
        {
            int oldX = this.x;
            int oldY = this.y;
            int oldW = this.width;
            int oldH = this.height;
            setBoundsFields(x, y, w, h, bMask);
            // Moved
            if ((oldX != this.x) || (oldY != this.y))
            {
                invalidateRealParent();
                postEvent(new java.awt.eventj.ComponentEvent(this, java.awt.eventj.ComponentEvent.COMPONENT_MOVED));
                spreadHierarchyBoundsEvents(this, java.awt.eventj.HierarchyEvent.ANCESTOR_MOVED);
            }
            // Resized
            if ((oldW != this.width) || (oldH != this.height))
            {
                invalidate();
                postEvent(new java.awt.eventj.ComponentEvent(this, java.awt.eventj.ComponentEvent.COMPONENT_RESIZED));
                spreadHierarchyBoundsEvents(this, java.awt.eventj.HierarchyEvent.ANCESTOR_RESIZED);
            }
            if (updateBehavior)
            {
                behaviour.setBounds(this.x, this.y, this.width, this.height, bMask);
            }
            notifyInputMethod(new Rectangle(x, y, w, h));
        }
        private void invalidateRealParent()
        {
            Container realParent = getRealParent();
            if ((realParent != null) && realParent.isValid())
            {
                realParent.invalidate();
            }
        }

        public void invalidate()
        {
            toolkit.lockAWT();
            try
            {
                valid = false;
                resetDefaultSize();
                invalidateRealParent();
            }
            finally
            {
                toolkit.unlockAWT();
            }
        }
        /**
         * Calls InputContextImpl.notifyClientWindowChanged.
         */
        internal virtual void notifyInputMethod(Rectangle bounds)
        {
            // only Window actually notifies IM of bounds change
        }

        private void setBoundsFields(int x, int y, int w, int h, int bMask)
        {
            if ((bMask & BOUNDS_NOSIZE) == 0)
            {
                this.width = w;
                this.height = h;
            }
            if ((bMask & BOUNDS_NOMOVE) == 0)
            {
                this.x = x;
                this.y = y;
            }
        }

        // to be overridden in standard components such as Button and List
        protected internal virtual void resetDefaultSize()
        {
        }
        public bool isValid()
        {
            toolkit.lockAWT();
            try
            {
                return valid && behaviour.isDisplayable();
            }
            finally
            {
                toolkit.unlockAWT();
            }
        }
        [Obsolete]
        public virtual bool postEvent(Event evt)
        {
            bool handled = handleEvent(evt);
            if (handled)
            {
                return true;
            }
            // propagate non-handled events up to parent
            Component par = parent;
            // try to call postEvent only on components which
            // override any of deprecated method handlers
            // while (par != null && !par.deprecatedEventHandler) {
            // par = par.parent;
            // }
            // translate event coordinates before posting it to parent
            if (par != null)
            {
                evt.translate(x, y);
                par.postEvent(evt);
            }
            return false;
        }

        [Obsolete]
        public virtual bool handleEvent(Event evt)
        {
            switch (evt.id)
            {
                case Event.ACTION_EVENT:
                    return action(evt, evt.arg);
                case Event.GOT_FOCUS:
                    return gotFocus(evt, null);
                case Event.LOST_FOCUS:
                    return lostFocus(evt, null);
                case Event.MOUSE_DOWN:
                    return mouseDown(evt, evt.x, evt.y);
                case Event.MOUSE_DRAG:
                    return mouseDrag(evt, evt.x, evt.y);
                case Event.MOUSE_ENTER:
                    return mouseEnter(evt, evt.x, evt.y);
                case Event.MOUSE_EXIT:
                    return mouseExit(evt, evt.x, evt.y);
                case Event.MOUSE_MOVE:
                    return mouseMove(evt, evt.x, evt.y);
                case Event.MOUSE_UP:
                    return mouseUp(evt, evt.x, evt.y);
                case Event.KEY_ACTION:
                case Event.KEY_PRESS:
                    return keyDown(evt, evt.key);
                case Event.KEY_ACTION_RELEASE:
                case Event.KEY_RELEASE:
                    return keyUp(evt, evt.key);
                default:
                    break;
            }
            return false;// event not handled
        }


        internal void postEvent(AWTEvent e)
        {
            throw new java.lang.UnsupportedOperationException("Not supported: getToolkit().getSystemEventQueueImpl().postEvent(e);");
            //getToolkit().getSystemEventQueueImpl().postEvent(e);
        }
        /**
         * Gets only parent of a child component, but not owner of a window.
         * 
         * @return parent of child component, null if component is a top-level
         *         (Window instance)
         */
        internal Container getRealParent()
        {
            return (!(this is Window) ? getParent() : null);
        }
        public virtual Container getParent()
        {
            toolkit.lockAWT();
            try
            {
                return parent;
            }
            finally
            {
                toolkit.unlockAWT();
            }
        }
        void spreadHierarchyBoundsEvents(Component changed, int id)
        {
            // To be inherited by Container
        }
        [Obsolete]
        public bool action(Event evt, Object what)
        {
            // to be overridden: do nothing,
            // just return false to propagate event up to the parent container
            return false;
        }
        [Obsolete]
        public bool gotFocus(Event evt, Object what)
        {
            // to be overridden: do nothing,
            // just return false to propagate event up to the parent container
            return false;
        }
        [Obsolete]
        public bool lostFocus(Event evt, Object what)
        {
            // to be overridden: do nothing,
            // just return false to propagate event up to the parent container
            return false;
        }

        [Obsolete]
        public bool mouseDown(Event evt, int x, int y)
        {
            // to be overridden: do nothing,
            // just return false to propagate event up to the parent container
            return false;
        }

        [Obsolete]
        public bool mouseDrag(Event evt, int x, int y)
        {
            // to be overridden: do nothing,
            // just return false to propagate event up to the parent container
            return false;
        }

        [Obsolete]
        public bool mouseEnter(Event evt, int x, int y)
        {
            // to be overridden: do nothing,
            // just return false to propagate event up to the parent container
            return false;
        }

        [Obsolete]
        public bool mouseExit(Event evt, int x, int y)
        {
            // to be overridden: do nothing,
            // just return false to propagate event up to the parent container
            return false;
        }

        [Obsolete]
        public bool mouseMove(Event evt, int x, int y)
        {
            // to be overridden: do nothing,
            // just return false to propagate event up to the parent container
            return false;
        }

        [Obsolete]
        public bool mouseUp(Event evt, int x, int y)
        {
            // to be overridden: do nothing,
            // just return false to propagate event up to the parent container
            return false;
        }
        [Obsolete]
        public bool keyDown(Event evt, int key)
        {
            // to be overridden: do nothing,
            // just return false to propagate event up to the parent container
            return false;
        }

        [Obsolete]
        public bool keyUp(Event evt, int key)
        {
            // to be overridden: do nothing,
            // just return false to propagate event up to the parent container
            return false;
        }

        public Toolkit getToolkit()
        {
            return toolkit;
        }
    public Dimension getMinimumSize() {
        toolkit.lockAWT();
        try {
            return minimumSize();
        } finally {
            toolkit.unlockAWT();
        }
    }

    [Obsolete]
    public Dimension minimumSize() {
        toolkit.lockAWT();
        try {
            if (isMinimumSizeSet()) {
                return new Dimension(this.maximumSizeJ);// (Dimension)minimumSize.clone();
            }
            Dimension defSize = getDefaultMinimumSize();
            if (defSize != null) {
                return (Dimension)defSize.clone();
            }
            return isDisplayable()? new Dimension(1, 1) : new Dimension(width, height);
        } finally {
            toolkit.unlockAWT();
        }
    }

    public Dimension getPreferredSize() {
        toolkit.lockAWT();
        try {
            return preferredSize();
        } finally {
            toolkit.unlockAWT();
        }
    }

    [Obsolete]
    public Dimension preferredSize() {
        toolkit.lockAWT();
        try {
            if (isPreferredSizeSet()) {
                return new Dimension(preferredSizeJ);
            }
            Dimension defSize = getDefaultPreferredSize();
            if (defSize != null) {
                return new Dimension(defSize);
            }
            return new Dimension(getMinimumSize());
        } finally {
            toolkit.unlockAWT();
        }
    }
    public bool isDisplayable()
    {
        toolkit.lockAWT();
        try
        {
            return behaviour.isDisplayable();
        }
        finally
        {
            toolkit.unlockAWT();
        }
    }

    // to be overridden in standard components such as Button and List
    protected internal Dimension getDefaultMinimumSize()
    {
        return null;
    }

    // to be overridden in standard components such as Button and List
    protected internal Dimension getDefaultPreferredSize()
    {
        return null;
    }
    public bool isMaximumSizeSet()
    {
        toolkit.lockAWT();
        try
        {
            return maximumSizeJ != null;
        }
        finally
        {
            toolkit.unlockAWT();
        }
    }

    public bool isMinimumSizeSet()
    {
        toolkit.lockAWT();
        try
        {
            return minimumSizeJ != null;
        }
        finally
        {
            toolkit.unlockAWT();
        }
    }

    public bool isPreferredSizeSet()
    {
        toolkit.lockAWT();
        try
        {
            return preferredSizeJ != null;
        }
        finally
        {
            toolkit.unlockAWT();
        }
    }
    public ComponentOrientation getComponentOrientation()
    {
        toolkit.lockAWT();
        try
        {
            return orientation;
        }
        finally
        {
            toolkit.unlockAWT();
        }
    }

    protected Component() {
        toolkit.lockAWT();
        try {
            orientation = ComponentOrientation.UNKNOWN;
/*            redrawManager = null;
            traversalIDs = this is Container ? KeyboardFocusManager.contTraversalIDs
                    : KeyboardFocusManager.compTraversalIDs;
            foreach (int element in traversalIDs) {
                traversalKeys.put(new Integer(element), null);
            }
            behaviour = createBehavior();
            deriveCoalescerFlag();
*/        } finally {
            toolkit.unlockAWT();
        }
    }
    }
}