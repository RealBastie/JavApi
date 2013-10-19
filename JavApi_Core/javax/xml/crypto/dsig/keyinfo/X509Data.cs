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

namespace biz.ritter.javapix.xml.crypto.dsig.keyinfo
{


/**
 * A representation of the XML <code>X509Data</code> element as defined in 
 * the <a href="http://www.w3.org/TR/xmldsig-core/">
 * W3C Recommendation for XML-Signature Syntax and Processing</a>. An
 * <code>X509Data</code> object contains one or more identifers of keys 
 * or X.509 certificates (or certificates' identifiers or a revocation list). 
 * The XML Schema Definition is defined as:
 * 
 * <pre>
 *    &lt;element name="X509Data" type="ds:X509DataType"/&gt;
 *    &lt;complexType name="X509DataType"&gt; 
 *        &lt;sequence maxOccurs="unbounded"&gt; 
 *          &lt;choice&gt; 
 *            &lt;element name="X509IssuerSerial" type="ds:X509IssuerSerialType"/&gt;
 *            &lt;element name="X509SKI" type="base64Binary"/&gt;
 *            &lt;element name="X509SubjectName" type="string"/&gt; 
 *            &lt;element name="X509Certificate" type="base64Binary"/&gt;
 *            &lt;element name="X509CRL" type="base64Binary"/&gt; 
 *            &lt;any namespace="##other" processContents="lax"/&gt;
 *          &lt;/choice&gt;  
 *        &lt;/sequence&gt;
 *    &lt;/complexType&gt;
 *
 *    &lt;complexType name="X509IssuerSerialType"&gt; 
 *      &lt;sequence&gt; 
 *        &lt;element name="X509IssuerName" type="string"/&gt; 
 *        &lt;element name="X509SerialNumber" type="integer"/&gt; 
 *      &lt;/sequence&gt;
 *    &lt;/complexType&gt;
 * </pre>
 *
 * An <code>X509Data</code> instance may be created by invoking the
 * {@link KeyInfoFactory#newX509Data newX509Data} methods of the
 * {@link KeyInfoFactory} class and passing it a list of one or more 
 * {@link XMLStructure}s representing X.509 content; for example:
 * <pre>
 *   KeyInfoFactory factory = KeyInfoFactory.getInstance("DOM");
 *   X509Data x509Data = factory.newX509Data
 *       (Collections.singletonList("cn=Alice"));
 * </pre>
 *
 * @author Sean Mullan
 * @author JSR 105 Expert Group
 * @see KeyInfoFactory#newX509Data(List)
 */
//@@@ check for illegal combinations of data violating MUSTs in W3c spec
public interface X509Data : XMLStructure {

    /**
     * Returns an {@link java.util.Collections#unmodifiableList unmodifiable 
     * list} of the content in this <code>X509Data</code>. Valid types are 
     * {@link String} (subject names), <code>byte[]</code> (subject key ids), 
     * {@link java.security.cert.X509Certificate}, {@link X509CRL}, 
     * or {@link XMLStructure} ({@link X509IssuerSerial}
     * objects or elements from an external namespace). 
     *
     * @return an unmodifiable list of the content in this <code>X509Data</code>
     *    (never <code>null</code> or empty)
     */
    java.util.List<Object> getContent();
}
}