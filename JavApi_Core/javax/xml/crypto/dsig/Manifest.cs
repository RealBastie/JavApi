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
 * A representation of the XML <code>Manifest</code> element as defined in 
 * the <a href="http://www.w3.org/TR/xmldsig-core/">
 * W3C Recommendation for XML-Signature Syntax and Processing</a>.
 * The XML Schema Definition is defined as:
 * <pre><code>
 * &lt;element name="Manifest" type="ds:ManifestType"/&gt; 
 *   &lt;complexType name="ManifestType"&gt;
 *     &lt;sequence>
 *       &lt;element ref="ds:Reference" maxOccurs="unbounded"/&gt; 
 *     &lt;/sequence&gt;  
 *     &lt;attribute name="Id" type="ID" use="optional"/&gt; 
 *   &lt;/complexType&gt;
 * </code></pre>
 *
 * A <code>Manifest</code> instance may be created by invoking
 * one of the {@link XMLSignatureFactory#newManifest newManifest} 
 * methods of the {@link XMLSignatureFactory} class; for example: 
 *
 * <pre>
 *   XMLSignatureFactory factory = XMLSignatureFactory.getInstance("DOM");
 *   List references = Collections.singletonList(factory.newReference
 *       ("#reference-1", DigestMethod.SHA1));
 *   Manifest manifest = factory.newManifest(references, "manifest-1");
 * </pre>
 *
 * @author Sean Mullan
 * @author JSR 105 Expert Group
 * @see XMLSignatureFactory#newManifest(List)
 * @see XMLSignatureFactory#newManifest(List, String)
 */
public interface Manifest : XMLStructure {
 
    /**
     * Returns the Id of this <code>Manifest</code>.
     *
     * @return the Id  of this <code>Manifest</code> (or <code>null</code> 
     *    if not specified)
     */
    String getId();
    
    /**
     * Returns an {@link java.util.Collections#unmodifiableList unmodifiable 
     * list} of one or more {@link Reference}s that are contained in this
     * <code>Manifest</code>. 
     *
     * @return an unmodifiable list of one or more <code>Reference</code>s 
     */
    java.util.List<Object> getReferences();
}
}