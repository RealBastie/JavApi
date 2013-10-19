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
 *  
 */
/*
 * Copyright 2005 Sun Microsystems, Inc. All rights reserved.
 */
using System;
using java = biz.ritter.javapi;
using javax = biz.ritter.javapix;

namespace biz.ritter.javapix.xml.crypto.dsig.dom
{

/**
 * A DOM-specific {@link XMLSignContext}. This class contains additional methods
 * to specify the location in a DOM tree where an {@link XMLSignature}
 * object is to be marshalled when generating the signature.
 *
 * <p>Note that <code>DOMSignContext</code> instances can contain
 * information and state specific to the XML signature structure it is
 * used with. The results are unpredictable if a
 * <code>DOMSignContext</code> is used with different signature structures
 * (for example, you should not use the same <code>DOMSignContext</code>
 * instance to sign two different {@link XMLSignature} objects).
 *
 * @author Sean Mullan
 * @author JSR 105 Expert Group
 */
public class DOMSignContext : javax.xml.crypto.dom.DOMCryptoContext, XMLSignContext {

    private org.w3c.dom.Node parent;
    private org.w3c.dom.Node nextSibling;

    /**
     * Creates a <code>DOMSignContext</code> with the specified signing key
     * and parent node. The signing key is stored in a
     * {@link KeySelector#singletonKeySelector singleton KeySelector} that is
     * returned by the {@link #getKeySelector getKeySelector} method.
     * The marshalled <code>XMLSignature</code> will be added as the last 
     * child element of the specified parent node unless a next sibling node is 
     * specified by invoking the {@link #setNextSibling setNextSibling} method.
     *
     * @param signingKey the signing key
     * @param parent the parent node
     * @throws NullPointerException if <code>signingKey</code> or 
     *    <code>parent</code> is <code>null</code>
     */
    public DOMSignContext(java.security.Key signingKey, org.w3c.dom.Node parent) { 
        if (signingKey == null) {
            throw new java.lang.NullPointerException("signingKey cannot be null");
        } 
        if (parent == null) {
            throw new java.lang.NullPointerException("parent cannot be null");
        }
        setKeySelector(KeySelector.singletonKeySelector(signingKey));
        this.parent = parent;
    }

    /**
     * Creates a <code>DOMSignContext</code> with the specified signing key,
     * parent and next sibling nodes. The signing key is stored in a
     * {@link KeySelector#singletonKeySelector singleton KeySelector} that is
     * returned by the {@link #getKeySelector getKeySelector} method.
     * The marshalled <code>XMLSignature</code> will be inserted as a child 
     * element of the specified parent node and immediately before the 
     * specified next sibling node.
     *
     * @param signingKey the signing key
     * @param parent the parent node
     * @param nextSibling the next sibling node
     * @throws NullPointerException if <code>signingKey</code>, 
     *    <code>parent</code> or <code>nextSibling</code> is <code>null</code>
     */
    public DOMSignContext(java.security.Key signingKey, org.w3c.dom.Node parent, org.w3c.dom.Node nextSibling) { 
        if (signingKey == null) {
            throw new java.lang.NullPointerException("signingKey cannot be null");
        }
        if (parent == null) {
            throw new java.lang.NullPointerException("parent cannot be null");
        }
        if (nextSibling == null) {
            throw new java.lang.NullPointerException("nextSibling cannot be null");
        }
        setKeySelector(KeySelector.singletonKeySelector(signingKey));
        this.parent = parent;
        this.nextSibling = nextSibling;
    }

    /**
     * Creates a <code>DOMSignContext</code> with the specified key selector
     * and parent node. The marshalled <code>XMLSignature</code> will be added 
     * as the last child element of the specified parent node unless a next 
     * sibling node is specified by invoking the 
     * {@link #setNextSibling setNextSibling} method.
     *
     * @param ks the key selector
     * @param parent the parent node
     * @throws NullPointerException if <code>ks</code> or <code>parent</code> 
     *    is <code>null</code>
     */
    public DOMSignContext(KeySelector ks, org.w3c.dom.Node parent) {
        if (ks == null) {
            throw new java.lang.NullPointerException("key selector cannot be null");
        } 
        if (parent == null) {
            throw new java.lang.NullPointerException("parent cannot be null");
        }
        setKeySelector(ks);
        this.parent = parent;
    }

    /**
     * Creates a <code>DOMSignContext</code> with the specified key selector,
     * parent and next sibling nodes. The marshalled <code>XMLSignature</code> 
     * will be inserted as a child element of the specified parent node and 
     * immediately before the specified next sibling node.
     *
     * @param ks the key selector
     * @param parent the parent node
     * @param nextSibling the next sibling node
     * @throws NullPointerException if <code>ks</code>, <code>parent</code> or 
     *    <code>nextSibling</code> is <code>null</code>
     */
    public DOMSignContext(KeySelector ks, org.w3c.dom.Node parent, org.w3c.dom.Node nextSibling) { 
        if (ks == null) {
            throw new java.lang.NullPointerException("key selector cannot be null");
        }
        if (parent == null) {
            throw new java.lang.NullPointerException("parent cannot be null");
        }
        if (nextSibling == null) {
            throw new java.lang.NullPointerException("nextSibling cannot be null");
        }
        setKeySelector(ks);
        this.parent = parent;
        this.nextSibling = nextSibling;
    }

    /**
     * Sets the parent node.
     *
     * @param parent the parent node. The marshalled <code>XMLSignature</code> 
     *    will be added as a child element of this node.
     * @throws NullPointerException if <code>parent</code> is <code>null</code>
     * @see #getParent
     */
    public void setParent(org.w3c.dom.Node parent) {
        if (parent == null) {
            throw new java.lang.NullPointerException("parent is null");
        }
        this.parent = parent;
    }

    /**
     * Sets the next sibling node. 
     *
     * @param nextSibling the next sibling node. The marshalled 
     *    <code>XMLSignature</code> will be inserted immediately before this 
     *    node. Specify <code>null</code> to remove the current setting. 
     * @see #getNextSibling
     */
    public void setNextSibling(org.w3c.dom.Node nextSibling) {
        this.nextSibling = nextSibling;
    }

    /**
     * Returns the parent node.
     *
     * @return the parent node (never <code>null</code>)
     * @see #setParent(Node)
     */
    public org.w3c.dom.Node getParent() {
        return parent;
    }

    /**
     * Returns the nextSibling node.
     *
     * @return the nextSibling node, or <code>null</code> if not specified.
     * @see #setNextSibling(Node)
     */
    public org.w3c.dom.Node getNextSibling() {
        return nextSibling;
    }
}
}
