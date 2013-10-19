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
/*import java.security.KeyException;
import java.security.PublicKey;
import java.security.interfaces.DSAPublicKey;
import java.security.interfaces.RSAPublicKey;
import javax.xml.crypto.XMLStructure;
*/
/**
 * A representation of the XML <code>KeyValue</code> element as defined
 * in the <a href="http://www.w3.org/TR/xmldsig-core/">
 * W3C Recommendation for XML-Signature Syntax and Processing</a>. A 
 * <code>KeyValue</code> object contains a single public key that may be
 * useful in validating the signature. The XML schema definition is defined as:
 *
 * <pre>
 *    &lt;element name="KeyValue" type="ds:KeyValueType"/&gt;
 *    &lt;complexType name="KeyValueType" mixed="true"&gt;
 *      &lt;choice&gt;
 *        &lt;element ref="ds:DSAKeyValue"/&gt;
 *        &lt;element ref="ds:RSAKeyValue"/&gt;
 *        &lt;any namespace="##other" processContents="lax"/&gt;
 *      &lt;/choice&gt;
 *    &lt;/complexType&gt;
 *
 *    &lt;element name="DSAKeyValue" type="ds:DSAKeyValueType"/&gt;
 *    &lt;complexType name="DSAKeyValueType"&gt;
 *      &lt;sequence&gt;
 *        &lt;sequence minOccurs="0"&gt;
 *          &lt;element name="P" type="ds:CryptoBinary"/&gt;
 *          &lt;element name="Q" type="ds:CryptoBinary"/&gt;
 *        &lt;/sequence&gt;
 *        &lt;element name="G" type="ds:CryptoBinary" minOccurs="0"/&gt; 
 *        &lt;element name="Y" type="ds:CryptoBinary"/&gt; 
 *        &lt;element name="J" type="ds:CryptoBinary" minOccurs="0"/&gt;
 *        &lt;sequence minOccurs="0"&gt;
 *          &lt;element name="Seed" type="ds:CryptoBinary"/&gt; 
 *          &lt;element name="PgenCounter" type="ds:CryptoBinary"/&gt; 
 *        &lt;/sequence&gt;
 *      &lt;/sequence&gt;
 *    &lt;/complexType&gt;
 *
 *    &lt;element name="RSAKeyValue" type="ds:RSAKeyValueType"/&gt;
 *    &lt;complexType name="RSAKeyValueType"&gt;
 *      &lt;sequence&gt;
 *        &lt;element name="Modulus" type="ds:CryptoBinary"/&gt; 
 *        &lt;element name="Exponent" type="ds:CryptoBinary"/&gt;
 *      &lt;/sequence&gt;
 *    &lt;/complexType&gt;
 * </pre>
 * A <code>KeyValue</code> instance may be created by invoking the
 * {@link KeyInfoFactory#newKeyValue newKeyValue} method of the
 * {@link KeyInfoFactory} class, and passing it a {@link 
 * java.security.PublicKey} representing the value of the public key. Here is 
 * an example of creating a <code>KeyValue</code> from a {@link DSAPublicKey} 
 * of a {@link java.security.cert.Certificate} stored in a 
 * {@link java.security.KeyStore}:
 * <pre>
 * KeyStore keyStore = KeyStore.getInstance(KeyStore.getDefaultType());
 * PublicKey dsaPublicKey = keyStore.getCertificate("myDSASigningCert").getPublicKey();
 * KeyInfoFactory factory = KeyInfoFactory.getInstance("DOM");
 * KeyValue keyValue = factory.newKeyValue(dsaPublicKey);
 * </pre>
 *
 * This class returns the <code>DSAKeyValue</code> and 
 * <code>RSAKeyValue</code> elements as objects of type 
 * {@link DSAPublicKey} and {@link RSAPublicKey}, respectively. Note that not 
 * all of the fields in the schema are accessible as parameters of these 
 * types. 
 * 
 * @author Sean Mullan
 * @author JSR 105 Expert Group
 * @see KeyInfoFactory#newKeyValue(PublicKey)
 */
public interface KeyValue : XMLStructure {

    /**
     * Returns the public key of this <code>KeyValue</code>. 
     *
     * @return the public key of this <code>KeyValue</code>
     * @throws KeyException if this <code>KeyValue</code> cannot be converted
     *    to a <code>PublicKey</code>
     */
    java.security.PublicKey getPublicKey();// throws KeyException;
}
}