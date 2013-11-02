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
     * The <code>UIEvent</code> interface provides specific contextual  
     * information associated with User Interface events.
     * @since DOM Level 2
     */
    public interface UIEvent : Event
    {
        /**
         * The <code>view</code> attribute identifies the <code>AbstractView</code> 
         * from which the event was generated.
         */
        AbstractView getView();
        /**
         * Specifies some detail information about the <code>Event</code>, depending 
         * on the type of event.
         */
        short getDetail();
        /**
         * 
         * @param typeArg Specifies the event type.
         * @param canBubbleArg Specifies whether or not the event can bubble.
         * @param cancelableArg Specifies whether or not the event's default  action 
         *   can be prevent.
         * @param viewArg Specifies the <code>Event</code>'s 
         *   <code>AbstractView</code>.
         * @param detailArg Specifies the <code>Event</code>'s detail.
         */
        void initUIEvent(String typeArg,
                                             bool canBubbleArg,
                                             bool cancelableArg,
                                             AbstractView viewArg,
                                             short detailArg);
    }

}