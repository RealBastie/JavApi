/*
 * Copyright (c) 1999 World Wide Web Consortium,
 * (Massachusetts Institute of Technology, Institut National de Recherche
 *  en Informatique et en Automatique, Keio University).
 * All Rights Reserved. http://www.w3.org/Consortium/Legal/
 */
using System;
using org.w3c.dom.views;

namespace org.w3c.dom.events
{

    /**
     * The <code>MouseEvent</code> interface provides specific contextual  
     * information associated with Mouse events.
     * <p>The <code>detail</code> attribute inherited from <code>UIEvent</code> 
     * indicates the number of times a mouse button has been pressed and released 
     * over the same screen location during a user action.  The attribute value 
     * is 1 when the user begins this action and increments by 1 for each full 
     * sequence of pressing and releasing. If the user moves the mouse between 
     * the mousedown and mouseup the value will be set to 0, indicating that no 
     * click is occurring.
     * @since DOM Level 2
     */
    public interface MouseEvent : UIEvent
    {
        /**
         * <code>screenX</code> indicates the horizontal coordinate at which the 
         * event occurred in relative to the origin of the screen coordinate system.
         */
        int getScreenX();
        /**
         * <code>screenY</code> indicates the vertical coordinate at which the event 
         * occurred relative to the origin of the screen coordinate system.
         */
        int getScreenY();
        /**
         * <code>clientX</code> indicates the horizontal coordinate at which the 
         * event occurred relative to the DOM implementation's client area.
         */
        int getClientX();
        /**
         * <code>clientY</code> indicates the vertical coordinate at which the event 
         * occurred relative to the DOM implementation's client area.
         */
        int getClientY();
        /**
         * <code>ctrlKey</code> indicates whether the 'ctrl' key was depressed 
         * during the firing of the event.
         */
        bool getCtrlKey();
        /**
         * <code>shiftKey</code> indicates whether the 'shift' key was depressed 
         * during the firing of the event.
         */
        bool getShiftKey();
        /**
         * <code>altKey</code> indicates whether the 'alt' key was depressed during 
         * the firing of the event.  On some platforms this key may map to an 
         * alternative key name.
         */
        bool getAltKey();
        /**
         * <code>metaKey</code> indicates whether the 'meta' key was depressed 
         * during the firing of the event.  On some platforms this key may map to 
         * an alternative key name.
         */
        bool getMetaKey();
        /**
         * During mouse events caused by the depression or release of a mouse 
         * button, <code>button</code> is used to indicate which mouse button 
         * changed state.
         */
        short getButton();
        /**
         * <code>relatedNode</code> is used to identify a secondary node related to 
         * a UI event.
         */
        Node getRelatedNode();
        /**
         * 
         * @param typeArg Specifies the event type.
         * @param canBubbleArg Specifies whether or not the event can bubble.
         * @param cancelableArg Specifies whether or not the event's default  action 
         *   can be prevent.
         * @param viewArg Specifies the <code>Event</code>'s 
         *   <code>AbstractView</code>.
         * @param detailArg Specifies the <code>Event</code>'s mouse click count.
         * @param screenXArg Specifies the <code>Event</code>'s screen x coordinate
         * @param screenYArg Specifies the <code>Event</code>'s screen y coordinate
         * @param clientXArg Specifies the <code>Event</code>'s client x coordinate
         * @param clientYArg Specifies the <code>Event</code>'s client y coordinate
         * @param ctrlKeyArg Specifies whether or not control key was depressed 
         *   during the <code>Event</code>.
         * @param altKeyArg Specifies whether or not alt key was depressed during 
         *   the  <code>Event</code>.
         * @param shiftKeyArg Specifies whether or not shift key was depressed 
         *   during the <code>Event</code>.
         * @param metaKeyArg Specifies whether or not meta key was depressed during 
         *   the  <code>Event</code>.
         * @param buttonArg Specifies the <code>Event</code>'s mouse button.
         * @param relatedNodeArg Specifies the <code>Event</code>'s related Node.
         */
        void initMouseEvent(String typeArg,
                                                bool canBubbleArg,
                                                bool cancelableArg,
                                                AbstractView viewArg,
                                                short detailArg,
                                                int screenXArg,
                                                int screenYArg,
                                                int clientXArg,
                                                int clientYArg,
                                                bool ctrlKeyArg,
                                                bool altKeyArg,
                                                bool shiftKeyArg,
                                                bool metaKeyArg,
                                                short buttonArg,
                                                Node relatedNodeArg);
    }

}