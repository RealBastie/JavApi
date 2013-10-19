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

namespace biz.ritter.javapix.xml.crypto.dsig
{

/**
 * A representation of the XML <code>PGPData</code> element as defined in 
 * the <a href="http://www.w3.org/TR/xmldsig-core/">
 * W3C Recommendation for XML-Signature Syntax and Processing</a>. A
 * <code>PGPData</code> object is used to convey information related to 
 * PGP public key pairs and signatures on such keys. The XML Schema Definition 
 * is defined as:
 * 
 * <pre>
 *    &lt;element name="PGPData" type="ds:PGPDataType"/&gt; 
 *    &lt;complexType name="PGPDataType"&gt; 
 *      &lt;choice&gt;
 *        &lt;sequence&gt;
 *          &lt;element name="PGPKeyID" type="base64Binary"/&gt; 
 *          &lt;element name="PGPKeyPacket" type="base64Binary" minOccurs="0"/&gt; 
 *          &lt;any namespace="##other" processContents="lax" minOccurs="0"
 *           maxOccurs="unbounded"/&gt;
 *        &lt;/sequence&gt;
 *        &lt;sequence&gt;
 *          &lt;element name="PGPKeyPacket" type="base64Binary"/&gt; 
 *          &lt;any namespace="##other" processContents="lax" minOccurs="0"
 *           maxOccurs="unbounded"/&gt;
 *        &lt;/sequence&gt;
 *      &lt;/choice&gt;
 *    &lt;/complexType&gt;
 * </pre>
 *
 * A <code>PGPData</code> instance may be created by invoking one of the
 * {@link KeyInfoFactory#newPGPData newPGPData} methods of the {@link
 * KeyInfoFactory} class, and passing it 
 * <code>byte</code> arrays representing the contents of the PGP public key 
 * identifier and/or PGP key material packet, and an optional list of
 * elements from an external namespace.
 *
 * @author Sean Mullan
 * @author JSR 105 Expert Group
 * @see KeyInfoFactory#newPGPData(byte[])
 * @see KeyInfoFactory#newPGPData(byte[], byte[], List)
 * @see KeyInfoFactory#newPGPData(byte[], List)
 */
public interface PGPData : XMLStructure {

    /**
     * Returns the PGP public key identifier of this <code>PGPData</code> as 
     * defined in <a href="http://www.ietf.org/rfc/rfc2440.txt">RFC 2440</a>, 
     * section 11.2.
     *
     * @return the PGP public key identifier (may be <code>null</code> if 
     *    not specified). Each invocation of this method returns a new clone 
     *    to protect against subsequent modification.
     */
    byte[] getKeyId();

    /**
     * Returns the PGP key material packet of this <code>PGPData</code> as
     * defined in <a href="http://www.ietf.org/rfc/rfc2440.txt">RFC 2440</a>, 
     * section 5.5.
     *
     * @return the PGP key material packet (may be <code>null</code> if not 
     *    specified). Each invocation of this method returns a new clone to 
     *    protect against subsequent modification.
     */
    byte[] getKeyPacket();

    /**
     * Returns an {@link Collections#unmodifiableList unmodifiable list}
     * of {@link XMLStructure}s representing elements from an external 
     * namespace. 
     *
     * @return an unmodifiable list of <code>XMLStructure</code>s (may be 
     *    empty, but never <code>null</code>)
     */
    java.util.List<Object> getExternalElements();
}
}