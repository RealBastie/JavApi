/*
 * Copyright (c) 1999 World Wide Web Consortium,
 * (Massachusetts Institute of Technology, Institut National de Recherche
 *  en Informatique et en Automatique, Keio University).
 * All Rights Reserved. http://www.w3.org/Consortium/Legal/
 */

namespace org.w3c.dom.traversal
{

    /**
     * <code>DocumentTraversal</code> contains methods that creates Iterators to 
     * traverse a node and its children in document order (depth first, pre-order 
     * traversal, which is equivalent to the order in which the start tags occur 
     * in the text representation of the document).
     * @since DOM Level 2
     */
    public interface DocumentTraversal
    {
        /**
         * 
         * @param root The node which will be iterated together with its children. 
         *   The iterator is initially positioned just before this node. The 
         *   whatToShow flags and the filter, if any, are not considered when 
         *   setting this position.
         * @param whatToShow This flag specifies which node types may appear in the 
         *   logical view of the tree presented by the Iterator. See the 
         *   description of Iterator for the set of possible values. These flags 
         *   can be combined using OR.These flags can be combined using 
         *   <code>OR</code>.
         * @param filter The Filter to be used with this TreeWalker, or null to 
         *   indicate no filter.
         * @param entityReferenceExpansion The value of this flag determines whether 
         *   entity reference nodes are expanded.
         * @return The newly created <code>NodeIterator</code>.
         */
        NodeIterator createNodeIterator(Node root,
                                                     int whatToShow,
                                                     NodeFilter filter,
                                                     bool entityReferenceExpansion);
        /**
         * Create a new TreeWalker over the subtree rooted by the specified node.
         * @param root The node which will serve as the root for the 
         *   <code>TreeWalker</code>. The currentNode of the TreeWalker is set to 
         *   this node. The whatToShow flags and the NodeFilter are not considered 
         *   when setting this value; any node type will be accepted as the root. 
         *   The root must not be null.
         * @param whatToShow This flag specifies which node types may appear in the 
         *   logical view of the tree presented by the Iterator. See the 
         *   description of TreeWalker for the set of possible values. These flags 
         *   can be combined using OR.These flags can be combined using 
         *   <code>OR</code>.
         * @param filter The Filter to be used with this TreeWalker, or null to 
         *   indicate no filter.
         * @param entityReferenceExpansion The value of this flag determines whether 
         *   entity reference nodes are expanded.
         * @return The newly created <code>TreeWalker</code>.
         * @exception DOMException
         *    Raises the exception NOT_SUPPORTED_ERR if the specified root node is 
         *   null.
         */
        TreeWalker createTreeWalker(Node root,
                                                   int whatToShow,
                                                   NodeFilter filter,
                                                   bool entityReferenceExpansion);//                                             throws DOMException;
    }

}