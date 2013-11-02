/*
 * Copyright (c) 1999 World Wide Web Consortium,
 * (Massachusetts Institute of Technology, Institut National de Recherche
 *  en Informatique et en Automatique, Keio University).
 * All Rights Reserved. http://www.w3.org/Consortium/Legal/
 */

namespace org.w3c.dom.traversal
{

    /**
     * <code>TreeWalker</code> objects are used to navigate a document tree or 
     * subtree using the view of the document defined by its 
     * <code>whatToShow</code> flags and any filters that are defined for the 
     * <code>TreeWalker</code>. Any function which performs navigation using a 
     * <code>TreeWalker</code> will automatically support any view defined by a 
     * <code>TreeWalker</code>.
     * <p>Omitting nodes from the logical view of a subtree can result in a 
     * structure that is substantially different from the same subtree in the 
     * complete, unfiltered document. Nodes that are siblings in the TreeWalker 
     * view may be children of different, widely separated nodes in the original 
     * view. For instance, consider a Filter that skips all nodes except for Text 
     * nodes and the root node of a document. In the logical view that results, 
     * all text nodes will be siblings and appear as direct children of the root 
     * node, no matter how deeply nested the structure of the original document.
     * @since DOM Level 2
     */
    public interface TreeWalker
    {
        /**
         * This attribute determines which node types are presented via the 
         * TreeWalker.
         */
        int getWhatToShow();

        /**
         * The filter used to screen nodes.
         */
        NodeFilter getFilter();
        /**
         * The value of this flag determines whether entity reference nodes are 
         * expanded. To produce a view of the document that has entity references 
         * expanded and does not expose the entity reference node itself, use the 
         * whatToShow flags to hide the entity reference node and set 
         * expandEntityReferences to true when creating the iterator. To produce a 
         * view of the document that has entity reference nodes but no entity 
         * expansion, use the whatToShow flags to show the entity reference node 
         * and set expandEntityReferences to true.
         */
        bool getExpandEntityReferences();
        /**
         * The current node.
         * <br>The value must not be null. Attempting to set it to null will raise a 
         * NOT_SUPPORTED_ERR exception. When setting a node, the whatToShow flags 
         * and any Filter associated with the TreeWalker are not checked. The 
         * currentNode may be set to any Node of any type.
         */
        Node getCurrentNode();
        void setCurrentNode(Node currentNode);
        /**
         * Moves to and returns the parent node of the current node. If there is no 
         * parent node, or if the current node is the root node from which this 
         * TreeWalker was created, retains the current position and returns null.
         * @return The new parent node, or null if the current node has no parent in 
         *   the TreeWalker's logical view.
         */
        Node parentNode();
        /**
         * Moves the <code>TreeWalker</code> to the first child of the current node, 
         * and returns the new node. If the current node has no children, returns 
         * <code>null</code>, and retains the current node.
         * @return The new node, or <code>null</code> if the current node has no 
         *   children.
         */
        Node firstChild();
        /**
         * Moves the <code>TreeWalker</code> to the last child of the current node, 
         * and returns the new node. If the current node has no children, returns 
         * <code>null</code>, and retains the current node.
         * @return The new node, or <code>null</code> if the current node has no 
         *   children.
         */
        Node lastChild();
        /**
         * Moves the <code>TreeWalker</code> to the previous sibling of the current 
         * node, and returns the new node. If the current node has no previous 
         * sibling, returns <code>null</code>, and retains the current node.
         * @return The new node, or <code>null</code> if the current node has no 
         *   previous sibling.
         */
        Node previousSibling();
        /**
         * Moves the <code>TreeWalker</code> to the next sibling of the current 
         * node, and returns the new node. If the current node has no next sibling, 
         * returns <code>null</code>, and retains the current node.
         * @return The new node, or <code>null</code> if the current node has no 
         *   next sibling.
         */
        Node nextSibling();
        /**
         * Moves the <code>TreeWalker</code> to the previous node in document order 
         * relative to the current node, and returns the new node. If the current 
         * node has no previous node, returns <code>null</code>, and retains the 
         * current node. 
         * @return The new node, or <code>null</code> if the current node has no 
         *   previous node.
         */
        Node previousNode();
        /**
         * Moves the <code>TreeWalker</code> to the next node in document order 
         * relative to the current node, and returns the new node. If the current 
         * node has no next node, returns <code>null</code>, and retains the 
         * current node.
         * @return The new node, or <code>null</code> if the current node has no 
         *   next node.
         */
        Node nextNode();
    }

    public sealed class TreeWalkerConstants
    {
        // Constants for whatToShow
        public const int SHOW_ALL = 0x0000FFFF;
        public const int SHOW_ELEMENT = 0x00000001;
        public const int SHOW_ATTRIBUTE = 0x00000002;
        public const int SHOW_TEXT = 0x00000004;
        public const int SHOW_CDATA_SECTION = 0x00000008;
        public const int SHOW_ENTITY_REFERENCE = 0x00000010;
        public const int SHOW_ENTITY = 0x00000020;
        public const int SHOW_PROCESSING_INSTRUCTION = 0x00000040;
        public const int SHOW_COMMENT = 0x00000080;
        public const int SHOW_DOCUMENT = 0x00000100;
        public const int SHOW_DOCUMENT_TYPE = 0x00000200;
        public const int SHOW_DOCUMENT_FRAGMENT = 0x00000400;
        public const int SHOW_NOTATION = 0x00000800;

    }
}