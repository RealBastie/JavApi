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
     * The <code>MutationEvent</code> interface provides specific contextual  
     * information associated with Mutation events. 
     * @since DOM Level 2
     */
    public interface MutationEvent : Event
    {
        /**
         *  <code>relatedNode</code> is used to identify a secondary node related to 
         * a mutation event. For example, if a mutation event is dispatched to a 
         * node indicating that its parent has changed, the <code>relatedNode</code>
         *  is the changed parent.  If an event is instead dispatch to a subtree 
         * indicating a node was changed within it, the <code>relatedNode</code> is 
         * the changed node. 
         */
        Node getRelatedNode();
        /**
         *  <code>prevValue</code> indicates the previous value of text nodes and 
         * attributes in attrModified and charDataModified events. 
         */
        String getPrevValue();
        /**
         *  <code>newValue</code> indicates the new value of text nodes and 
         * attributes in attrModified and charDataModified events. 
         */
        String getNewValue();
        /**
         *  <code>attrName</code> indicates the changed attr in the attrModified 
         * event. 
         */
        String getAttrName();
        /**
         * 
         * @param typeArg Specifies the event type.
         * @param canBubbleArg Specifies whether or not the event can bubble.
         * @param cancelableArg Specifies whether or not the event's default  action 
         *   can be prevent.
         * @param relatedNodeArg Specifies the <code>Event</code>'s related Node
         * @param prevValueArg Specifies the <code>Event</code>'s 
         *   <code>prevValue</code> property
         * @param newValueArg Specifies the <code>Event</code>'s 
         *   <code>newValue</code> property
         * @param attrNameArg Specifies the <code>Event</code>'s 
         *   <code>attrName</code> property
         */
        void initMutationEvent(String typeArg,
                                                   bool canBubbleArg,
                                                   bool cancelableArg,
                                                   Node relatedNodeArg,
                                                   String prevValueArg,
                                                   String newValueArg,
                                                   String attrNameArg);
    }

}