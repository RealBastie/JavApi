/*
 * Copyright (c) 1999 World Wide Web Consortium,
 * (Massachusetts Institute of Technology, Institut National de Recherche
 *  en Informatique et en Automatique, Keio University).
 * All Rights Reserved. http://www.w3.org/Consortium/Legal/
 */

namespace org.w3c.dom.traversal
{

    /**
     * Filters are objects that know how to "filter out" nodes. If an Iterator or 
     * <code>TreeWalker</code> is given a filter, before it returns the next 
     * node, it applies the filter. If the filter says to accept the node, the 
     * Iterator returns it; otherwise, the Iterator looks for the next node and 
     * pretends that the node that was rejected was not there.
     * <p>The DOM does not provide any filters. Filter is just an interface that 
     * users can implement to provide their own filters. 
     * <p>Filters do not need to know how to iterate, nor do they need to know 
     * anything about the data structure that is being iterated. This makes it 
     * very easy to write filters, since the only thing they have to know how to 
     * do is evaluate a single node. One filter may be used with a number of 
     * different kinds of Iterators, encouraging code reuse.
     * @since DOM Level 2
     */
    public interface NodeFilter
    {
        /**
         * Test whether a specified node is visible in the logical view of a 
         * TreeWalker or NodeIterator. This function will be called by the 
         * implementation of TreeWalker and NodeIterator; it is not intended to be 
         * called directly from user code.
         * @param n The node to check to see if it passes the filter or not.
         * @return a constant to determine whether the node is accepted, rejected, 
         *   or skipped, as defined above.
         */
        short acceptNode(Node n);
    }
    public sealed class NodeFilterConstants
    {
        // Constants returned by acceptNode
        public const short FILTER_ACCEPT = 1;
        public const short FILTER_REJECT = 2;
        public const short FILTER_SKIP = 3;


    }
}