/*
 * Copyright (c) 1999 World Wide Web Consortium,
 * (Massachusetts Institute of Technology, Institut National de Recherche
 *  en Informatique et en Automatique, Keio University).
 * All Rights Reserved. http://www.w3.org/Consortium/Legal/
 */

namespace org.w3c.dom.events
{

    /**
     * The <code>EventListener</code> interface is the primary method for handling 
     * events.  Users implement the <code>EventListener</code> interface and 
     * register their listener  on an <code>EventTarget</code> using the 
     * <code>AddEventListener</code> method.  The  users should also remove their 
     * <code>EventListener</code> from its  <code>EventTarget</code> after they 
     * have completed using the listener. 
     * @since DOM Level 2
     */
    public interface EventListener
    {
        /**
         * This method is called whenever an event occurs of the type for which the 
         * <code> EventListener</code> interface was registered. 
         * @param evt The <code>Event</code> contains contextual information about 
         *   the event.  It also contains the <code>preventDefault</code>, 
         *   <code>preventBubble</code>, and <code> preventCapture</code> methods 
         *   which are used in determining the event's flow and  default action.
         */
         void handleEvent(Event evt);
    }

}