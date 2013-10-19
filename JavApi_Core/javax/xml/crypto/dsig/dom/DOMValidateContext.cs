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
 * A DOM-specific {@link XMLValidateContext}. This class contains additional 
 * methods to specify the location in a DOM tree where an {@link XMLSignature} 
 * is to be unmarshalled and validated from.
 *
 * <p>Note that the behavior of an unmarshalled <code>XMLSignature</code> 
 * is undefined if the contents of the underlying DOM tree are modified by the 
 * caller after the <code>XMLSignature</code> is created.
 *
 * <p>Also, note that <code>DOMValidateContext</code> instances can contain
 * information and state specific to the XML signature structure it is
 * used with. The results are unpredictable if a
 * <code>DOMValidateContext</code> is used with different signature structures
 * (for example, you should not use the same <code>DOMValidateContext</code>
 * instance to validate two different {@link XMLSignature} objects).
 *
 * @author Sean Mullan
 * @author JSR 105 Expert Group
 * @see XMLSignatureFactory#unmarshalXMLSignature(XMLValidateContext)
 */
public class DOMValidateContext : javax.xml.crypto.dom.DOMCryptoContext, 
     XMLValidateContext {

    private org.w3c.dom.Node node;

    /**
     * Creates a <code>DOMValidateContext</code> containing the specified key
     * selector and node.
     *
     * @param ks a key selector for finding a validation key
     * @param node the node
     * @throws NullPointerException if <code>ks</code> or <code>node</code> is 
     *    <code>null</code>
     */
    public DOMValidateContext(KeySelector ks, org.w3c.dom.Node node) { 
        if (ks == null) {
            throw new java.lang.NullPointerException("key selector is null");
        } 
        if (node == null) {
            throw new java.lang.NullPointerException("node is null");
        }
        setKeySelector(ks);
        this.node = node;
    }

    /**
     * Creates a <code>DOMValidateContext</code> containing the specified key
     * and node. The validating key will be stored in a 
     * {@link KeySelector#singletonKeySelector singleton KeySelector} that
     * is returned when the {@link #getKeySelector getKeySelector} 
     * method is called.
     *
     * @param validatingKey the validating key
     * @param node the node
     * @throws NullPointerException if <code>validatingKey</code> or 
     *    <code>node</code> is <code>null</code>
     */
    public DOMValidateContext(java.security.Key validatingKey, org.w3c.dom.Node node) { 
        if (validatingKey == null) {
            throw new java.lang.NullPointerException("validatingKey is null");
        }
        if (node == null) {
            throw new java.lang.NullPointerException("node is null");
        }
        setKeySelector(KeySelector.singletonKeySelector(validatingKey));
        this.node = node;
    }

    /**
     * Sets the node.
     *
     * @param node the node 
     * @throws NullPointerException if <code>node</code> is <code>null</code>
     * @see #getNode
     */
    public void setNode(org.w3c.dom.Node node) {
        if (node == null) {
            throw new java.lang.NullPointerException();
        }
        this.node = node;
    }

    /**
     * Returns the node.
     *
     * @return the node (never <code>null</code>)
     * @see #setNode(Node)
     */
    public org.w3c.dom.Node getNode() {
        return node;
    }
}
}