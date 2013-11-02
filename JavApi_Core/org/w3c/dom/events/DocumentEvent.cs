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
     *  The <code>DocumentEvent</code> interface provides a mechanism by which the 
     * a user can create an Event of a type supported by the implementation. It 
     * is expected that the <code>DocumentEvent</code> interface will be 
     * implemented on the same object which implements the <code>Document</code> 
     * interface in an implementation which supports the Event model. 
     * @since DOM Level 2
     */
    public interface DocumentEvent
    {
        /**
         * 
         * @param type The <code>type</code> paramater specifies the type of 
         *   <code>Event</code> to be created.  If the <code>Event</code> type 
         *   specified is supported by the implementation  this method will return 
         *   a new <code>Event</code> of the type requested.  If the 
         *   <code> Event</code> is to be dispatched via the 
         *   <code>dispatchEvent</code> method the  appropriate event init method 
         *   must be called after creation in order to initialize the 
         *   <code>Event</code>'s values.
         * @return The newly created <code>Event</code>
         * @exception DOMException
         *   UNSUPPORTED_EVENT_TYPE: Raised if the implementation does not support 
         *   the type of <code>Event</code> requested
         */
         Event createEvent(String type)
                                              ;//throwsDOMException;
    }

}