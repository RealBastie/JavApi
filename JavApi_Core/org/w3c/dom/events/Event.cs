/*
 * Copyright (c) 1999 World Wide Web Consortium,
 * (Massachusetts Institute of Technology, Institut National de Recherche
 *  en Informatique et en Automatique, Keio University).
 * All Rights Reserved. http://www.w3.org/Consortium/Legal/
 */
using System;

namespace org.w3c.dom.events
{

    /**
     * The <code>Event</code> interface is used to provide contextual information 
     * about an event to the handler processing the event.  An object which 
     * implements the <code>Event</code> interface is generally passed as the 
     * first parameter to an event handler.  More specific  context information 
     * is passed to event handlers by deriving additional interfaces from  
     * <code>Event</code> which contain information directly relating to the type 
     * of event they accompany.  These derived interfaces are also implemented by 
     * the object passed to the event listener. 
     * @since DOM Level 2
     */
    public interface Event
    {
        // PhaseType

        /**
         * The <code>type</code> property represents the event name as a string 
         * property. 
         */
        String getType();
        /**
         * The <code>target</code> property indicates the <code>EventTarget</code> 
         * to which the event  was originally dispatched. 
         */
        EventTarget getTarget();
        /**
         * The <code>currentNode</code> property indicates the <code>Node</code> 
         * whose <code>EventListener</code>s are currently being processed.  This 
         * is particularly  useful during capturing and bubbling. 
         */
        Node getCurrentNode();
        /**
         * The <code>eventPhase</code> property indicates which phase of event flow 
         * is currently  being evaluated. 
         */
        short getEventPhase();
        /**
         * The <code>bubbles</code> property indicates whether or not an event is a 
         * bubbling event.  If the event can bubble the value is true, else the 
         * value is false. 
         */
        bool getBubbles();
        /**
         * The <code>cancelable</code> property indicates whether or not an event 
         * can have its default action prevented.  If the default action can be 
         * prevented the value is true, else the value is false. 
         */
        bool getCancelable();
        /**
         * The <code>preventBubble</code> method is used to end the bubbling phase 
         * of  event flow. If this method is called by any 
         * <code>EventListener</code>s registered on the same 
         * <code>EventTarget</code> during bubbling, the bubbling phase will cease 
         * at that level and the event will not be propagated upward within the 
         * tree. 
         */
        void preventBubble();
        /**
         * The <code>preventCapture</code> method is used to end the capturing phase 
         * of  event flow. If this method is called by any 
         * <code>EventListener</code>s registered on the same 
         * <code>EventTarget</code> during capturing, the capturing phase will 
         * cease at that level and the event will not be propagated any further 
         * down. 
         */
        void preventCapture();
        /**
         * If an event is cancelable, the <code>preventCapture</code> method is used 
         * to signify that the event is to be canceled, meaning any default action 
         * normally taken by the implementation as a result of the event will not 
         * occur.  If, during any stage of event flow, the 
         * <code>preventDefault</code> method is called the event is canceled. Any 
         * default action associated with the event will not occur.  Calling this 
         * method for a non-cancelable event has no effect.  Once 
         * <code>preventDefault</code> has been called it will remain in effect 
         * throughout the remainder of the event's propagation. 
         */
        void preventDefault();
        /**
         * 
         * @param eventTypeArg Specifies the event type.  This type may be any event 
         *   type currently defined in this specification or a new event type.  Any 
         *   new event type must not begin with any upper, lower, or mixed case 
         *   version of the string  "DOM".  This prefix is reserved for future DOM 
         *   event sets.
         * @param canBubbleArg Specifies whether or not the event can bubble.
         * @param cancelableArg Specifies whether or not the event's default  action 
         *   can be prevented.
         */
        void initEvent(String eventTypeArg,
                                           bool canBubbleArg,
                                           bool cancelableArg);
    }

    public sealed class EventConstants
    {
        public const short BUBBLING_PHASE = 1;
        public const short CAPTURING_PHASE = 2;
        public const short AT_TARGET = 3;

    }
}