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

namespace biz.ritter.javapix.xml.crypto.dsig
{

//import javax.xml.crypto.dom.DOMStructure;
//import javax.xml.crypto.dsig.keyinfo.KeyInfoFactory;
//import javax.xml.crypto.dsig.spec.*;
//import javax.xml.crypto.dsig.dom.DOMSignContext;

/*import java.security.InvalidAlgorithmParameterException;
import java.security.NoSuchAlgorithmException;
import java.security.NoSuchProviderException;
import java.security.Provider;
import java.security.Security;
*/

/**
 * A factory for creating {@link XMLSignature} objects from scratch or 
 * for unmarshalling an <code>XMLSignature</code> object from a corresponding
 * XML representation.
 *
 * <h2>XMLSignatureFactory Type</h2>
 *
 * <p>Each instance of <code>XMLSignatureFactory</code> supports a specific
 * XML mechanism type. To create an <code>XMLSignatureFactory</code>, call one
 * of the static {@link #getInstance getInstance} methods, passing in the XML 
 * mechanism type desired, for example:
 *
 * <blockquote><code>
 * XMLSignatureFactory factory = XMLSignatureFactory.getInstance("DOM");
 * </code></blockquote>
 *
 * <p>The objects that this factory produces will be based
 * on DOM and abide by the DOM interoperability requirements as defined in the
 * <a href="../../../../overview-summary.html#DOM Mechanism Requirements">DOM
 * Mechanism Requirements</a> section of the API overview. See the
 * <a href="../../../../overview-summary.html#Service Provider">Service 
 * Providers</a> section of the API overview for a list of standard mechanism 
 * types.
 *
 * <p><code>XMLSignatureFactory</code> implementations are registered and loaded
 * using the {@link java.security.Provider} mechanism.  
 * For example, a service provider that supports the
 * DOM mechanism would be specified in the <code>Provider</code> subclass as:
 * <pre>
 *     put("XMLSignatureFactory.DOM", "org.example.DOMXMLSignatureFactory");
 * </pre>
 *
 * <p>An implementation MUST minimally support the default mechanism type: DOM. 
 *
 * <p>Note that a caller must use the same <code>XMLSignatureFactory</code>
 * instance to create the <code>XMLStructure</code>s of a particular 
 * <code>XMLSignature</code> that is to be generated. The behavior is
 * undefined if <code>XMLStructure</code>s from different providers or 
 * different mechanism types are used together.
 *
 * <p>Also, the <code>XMLStructure</code>s that are created by this factory
 * may contain state specific to the <code>XMLSignature</code> and are not
 * intended to be reusable.
 *
 * <h2>Creating XMLSignatures from scratch</h2>
 *
 * <p>Once the <code>XMLSignatureFactory</code> has been created, objects
 * can be instantiated by calling the appropriate method. For example, a
 * {@link Reference} instance may be created by invoking one of the
 * {@link #newReference newReference} methods.
 *
 * <h2>Unmarshalling XMLSignatures from XML</h2>
 *
 * <p>Alternatively, an <code>XMLSignature</code> may be created from an
 * existing XML representation by invoking the {@link #unmarshalXMLSignature
 * unmarshalXMLSignature} method and passing it a mechanism-specific 
 * {@link XMLValidateContext} instance containing the XML content:
 *
 * <pre>
 * DOMValidateContext context = new DOMValidateContext(key, signatureElement);
 * XMLSignature signature = factory.unmarshalXMLSignature(context);
 * </pre>
 *
 * Each <code>XMLSignatureFactory</code> must support the required 
 * <code>XMLValidateContext</code> types for that factory type, but may support 
 * others. A DOM <code>XMLSignatureFactory</code> must support {@link 
 * javax.xml.crypto.dsig.dom.DOMValidateContext} objects.
 * 
 * <h2>Signing and marshalling XMLSignatures to XML</h2>
 *
 * Each <code>XMLSignature</code> created by the factory can also be 
 * marshalled to an XML representation and signed, by invoking the 
 * {@link XMLSignature#sign sign} method of the 
 * {@link XMLSignature} object and passing it a mechanism-specific 
 * {@link XMLSignContext} object containing the signing key and 
 * marshalling parameters (see {@link DOMSignContext}).
 * For example: 
 *
 * <pre>
 *    DOMSignContext context = new DOMSignContext(privateKey, document);
 *    signature.sign(context);
 * </pre>
 *
 * <b>Concurrent Access</b>
 * <p>The static methods of this class are guaranteed to be thread-safe. 
 * Multiple threads may concurrently invoke the static methods defined in this 
 * class with no ill effects. 
 *
 * <p>However, this is not true for the non-static methods defined by this 
 * class. Unless otherwise documented by a specific provider, threads that 
 * need to access a single <code>XMLSignatureFactory</code> instance 
 * concurrently should synchronize amongst themselves and provide the 
 * necessary locking. Multiple threads each manipulating a different 
 * <code>XMLSignatureFactory</code> instance need not synchronize. 
 *
 * @author Sean Mullan
 * @author JSR 105 Expert Group
 */
public abstract class XMLSignatureFactory {

    private String mechanismType;
    private java.security.Provider provider;

    /**
     * Default constructor, for invocation by subclasses.
     */
    protected XMLSignatureFactory() {}

    /**
     * Returns an <code>XMLSignatureFactory</code> that supports the
     * specified XML processing mechanism and representation type (ex: "DOM").
     *
     * <p>This method uses the standard JCA provider lookup mechanism to
     * locate and instantiate an <code>XMLSignatureFactory</code> 
     * implementation of the desired mechanism type. It traverses the list of 
     * registered security <code>Provider</code>s, starting with the most 
     * preferred <code>Provider</code>.  A new <code>XMLSignatureFactory</code> 
     * object from the first <code>Provider</code> that supports the specified 
     * mechanism is returned. 
     *
     * <p>Note that the list of registered providers may be retrieved via 
     * the {@link Security#getProviders() Security.getProviders()} method. 
     *
     * @param mechanismType the type of the XML processing mechanism and
     *    representation. See the <a 
     *    href="../../../../overview-summary.html#Service Provider">Service 
     *    Providers</a> section of the API overview for a list of standard 
     *    mechanism types.
     * @return a new <code>XMLSignatureFactory</code>
     * @throws NullPointerException if <code>mechanismType</code> is 
     *    <code>null</code>
     * @throws NoSuchMechanismException if no <code>Provider</code> supports an 
     *    <code>XMLSignatureFactory</code> implementation for the specified 
     *    mechanism
     * @see Provider
     */
    public static XMLSignatureFactory getInstance(String mechanismType) {
        if (mechanismType == null) {
            throw new java.lang.NullPointerException("mechanismType cannot be null");
        }

        return findInstance(mechanismType, null);
    }

    private static XMLSignatureFactory findInstance(String mechanismType,
        java.security.Provider provider) {

        if (provider == null) {
            provider = getProvider("XMLSignatureFactory", mechanismType);
        }
        java.security.Provider.Service ps = provider.getService("XMLSignatureFactory",
                                                  mechanismType);
        if (ps == null) {
            throw new NoSuchMechanismException("Cannot find " + mechanismType +
                                               " mechanism type");
        }
        try {
            XMLSignatureFactory fac = (XMLSignatureFactory)ps.newInstance(null);
            fac.mechanismType = mechanismType;
            fac.provider = provider;
            return fac;
        } catch (java.security.NoSuchAlgorithmException nsae) {
            throw new NoSuchMechanismException("Cannot find " + mechanismType +
                                               " mechanism type", nsae);
        }
    }

    private static java.security.Provider getProvider(String engine, String mech) {
        java.security.Provider[] providers = java.security.Security.getProviders(engine + "." + mech);
        if (providers == null) {
            throw new NoSuchMechanismException("Mechanism type " + mech +
                                               " not available");
        }
        return providers[0];
    }

    /**
     * Returns an <code>XMLSignatureFactory</code> that supports the
     * requested XML processing mechanism and representation type (ex: "DOM"),
     * as supplied by the specified provider. Note that the specified 
     * <code>Provider</code> object does not have to be registered in the 
     * provider list. 
     *
     * @param mechanismType the type of the XML processing mechanism and
     *    representation. See the <a 
     *    href="../../../../overview-summary.html#Service Provider">Service 
     *    Providers</a> section of the API overview for a list of standard 
     *    mechanism types.
     * @param provider the <code>Provider</code> object
     * @return a new <code>XMLSignatureFactory</code>
     * @throws NullPointerException if <code>provider</code> or 
     *    <code>mechanismType</code> is <code>null</code>
     * @throws NoSuchMechanismException if an <code>XMLSignatureFactory</code> 
     *   implementation for the specified mechanism is not available 
     *   from the specified <code>Provider</code> object
     * @see Provider
     */
    public static XMLSignatureFactory getInstance(String mechanismType,
        java.security.Provider provider) { 
        if (mechanismType == null) {
            throw new java.lang.NullPointerException("mechanismType cannot be null");
        } else if (provider == null) {
            throw new java.lang.NullPointerException("provider cannot be null");
        }

        return findInstance(mechanismType, provider);
    }    

    /**
     * Returns an <code>XMLSignatureFactory</code> that supports the
     * requested XML processing mechanism and representation type (ex: "DOM"),
     * as supplied by the specified provider. The specified provider must be 
     * registered in the security provider list. 
     *
     * <p>Note that the list of registered providers may be retrieved via 
     * the {@link Security#getProviders() Security.getProviders()} method.
     *
     * @param mechanismType the type of the XML processing mechanism and
     *    representation. See the <a 
     *    href="../../../../overview-summary.html#Service Provider">Service 
     *    Providers</a> section of the API overview for a list of standard 
     *    mechanism types.
     * @param provider the string name of the provider
     * @return a new <code>XMLSignatureFactory</code>
     * @throws NoSuchProviderException if the specified provider is not 
     *    registered in the security provider list
     * @throws NullPointerException if <code>provider</code> or 
     *    <code>mechanismType</code> is <code>null</code>
     * @throws NoSuchMechanismException if an <code>XMLSignatureFactory</code> 
     *    implementation for the specified mechanism is not 
     *    available from the specified provider
     * @see Provider
     */
    public static XMLSignatureFactory getInstance(String mechanismType,
        String provider){// throws NoSuchProviderException {
        if (mechanismType == null) {
            throw new java.lang.NullPointerException("mechanismType cannot be null");
        } else if (provider == null) {
            throw new java.lang.NullPointerException("provider cannot be null");
        }

        java.security.Provider prov = java.security.Security.getProvider(provider);
        if (prov == null) {
            throw new java.security.NoSuchProviderException("cannot find provider named "
                + provider);
        }

        return findInstance(mechanismType, prov);
    }

    /**
     * Returns an <code>XMLSignatureFactory</code> that supports the
     * default XML processing mechanism and representation type ("DOM").
     *
     * <p>This method uses the standard JCA provider lookup mechanism to
     * locate and instantiate an <code>XMLSignatureFactory</code> 
     * implementation of the default mechanism type. It traverses the list of 
     * registered security <code>Provider</code>s, starting with the most 
     * preferred <code>Provider</code>.  A new <code>XMLSignatureFactory</code> 
     * object from the first <code>Provider</code> that supports the DOM 
     * mechanism is returned. 
     *
     * <p>Note that the list of registered providers may be retrieved via 
     * the {@link Security#getProviders() Security.getProviders()} method. 
     *
     * @return a new <code>XMLSignatureFactory</code>
     * @throws NoSuchMechanismException if no <code>Provider</code> supports an 
     *    <code>XMLSignatureFactory</code> implementation for the DOM 
     *    mechanism
     * @see Provider
     */
    public static XMLSignatureFactory getInstance() {
        return getInstance("DOM");
    }

    /**
     * Returns the type of the XML processing mechanism and representation
     * supported by this <code>XMLSignatureFactory</code> (ex: "DOM").
     *
     * @return the XML processing mechanism type supported by this
     *    <code>XMLSignatureFactory</code>
     */
    public String getMechanismType() {
        return mechanismType;
    }

    /**
     * Returns the provider of this <code>XMLSignatureFactory</code>.
     *
     * @return the provider of this <code>XMLSignatureFactory</code>
     */
    public java.security.Provider getProvider() {
        return provider;
    }

    /**
     * Creates an <code>XMLSignature</code> and initializes it with the contents
     * of the specified <code>SignedInfo</code> and <code>KeyInfo</code> 
     * objects.
     *
     * @param si the signed info
     * @param ki the key info (may be <code>null</code>)
     * @return an <code>XMLSignature</code>
     * @throws NullPointerException if <code>si</code> is <code>null</code>
     */
    public abstract XMLSignature newXMLSignature(SignedInfo si, javax.xml.crypto.dsig.keyinfo.KeyInfo ki);

    /**
     * Creates an <code>XMLSignature</code> and initializes it with the
     * specified parameters.
     *
     * @param si the signed info
     * @param ki the key info (may be <code>null</code>)
     * @param objects a list of {@link XMLObject}s (may be empty or
     *    <code>null</code>)
     * @param id the Id (may be <code>null</code>)
     * @param signatureValueId the SignatureValue Id (may be <code>null</code>)
     * @return an <code>XMLSignature</code>
     * @throws NullPointerException if <code>si</code> is <code>null</code>
     * @throws ClassCastException if any of the <code>objects</code> are not of
     *    type <code>XMLObject</code> 
     */ 
    public abstract XMLSignature newXMLSignature(SignedInfo si, javax.xml.crypto.dsig.keyinfo.KeyInfo ki,
        java.util.List<Object> objects, String id, String signatureValueId);

    /**
     * Creates a <code>Reference</code> with the specified URI and digest
     * method.
     *
     * @param uri the reference URI (may be <code>null</code>)
     * @param dm the digest method
     * @return a <code>Reference</code>
     * @throws IllegalArgumentException if <code>uri</code> is not RFC 2396
     *    compliant
     * @throws NullPointerException if <code>dm</code> is <code>null</code>
     */
    public abstract Reference newReference(String uri, DigestMethod dm);

    /**
     * Creates a <code>Reference</code> with the specified parameters.
     *
     * @param uri the reference URI (may be <code>null</code>)
     * @param dm the digest method
     * @param transforms a list of {@link Transform}s. The list is defensively 
     *    copied to protect against subsequent modification. May be 
     *    <code>null</code> or empty.
     * @param type the reference type, as a URI (may be <code>null</code>)
     * @param id the reference ID (may be <code>null</code>)
     * @return a <code>Reference</code>
     * @throws ClassCastException if any of the <code>transforms</code> are 
     *    not of type <code>Transform</code>
     * @throws IllegalArgumentException if <code>uri</code> is not RFC 2396
     *    compliant
     * @throws NullPointerException if <code>dm</code> is <code>null</code>
     */
    public abstract Reference newReference(String uri, DigestMethod dm, 
        java.util.List<Object> transforms, String type, String id);

    /**
     * Creates a <code>Reference</code> with the specified parameters and
     * pre-calculated digest value. 
     *
     * <p>This method is useful when the digest value of a 
     * <code>Reference</code> has been previously computed. See for example,
     * the 
     * <a href="http://www.oasis-open.org/committees/tc_home.php?wg_abbrev=dss">
     * OASIS-DSS (Digital Signature Services)</a> specification.
     * 
     * @param uri the reference URI (may be <code>null</code>)
     * @param dm the digest method
     * @param transforms a list of {@link Transform}s. The list is defensively 
     *    copied to protect against subsequent modification. May be 
     *    <code>null</code> or empty.
     * @param type the reference type, as a URI (may be <code>null</code>)
     * @param id the reference ID (may be <code>null</code>)
     * @param digestValue the digest value. The array is cloned to protect
     *    against subsequent modification.
     * @return a <code>Reference</code>
     * @throws ClassCastException if any of the <code>transforms</code> are 
     *    not of type <code>Transform</code>
     * @throws IllegalArgumentException if <code>uri</code> is not RFC 2396
     *    compliant
     * @throws NullPointerException if <code>dm</code> or 
     *    <code>digestValue</code> is <code>null</code>
     */
    public abstract Reference newReference(String uri, DigestMethod dm, 
        java.util.List<Object> transforms, String type, String id, byte[] digestValue);

    /**
     * Creates a <code>Reference</code> with the specified parameters.
     *
     * <p>This method is useful when a list of transforms have already been
     * applied to the <code>Reference</code>. See for example,
     * the 
     * <a href="http://www.oasis-open.org/committees/tc_home.php?wg_abbrev=dss">
     * OASIS-DSS (Digital Signature Services)</a> specification.
     *
     * <p>When an <code>XMLSignature</code> containing this reference is 
     * generated, the specified <code>transforms</code> (if non-null) are 
     * applied to the specified <code>result</code>. The 
     * <code>Transforms</code> element of the resulting <code>Reference</code> 
     * element is set to the concatenation of the 
     * <code>appliedTransforms</code> and <code>transforms</code>.
     * 
     * @param uri the reference URI (may be <code>null</code>)
     * @param dm the digest method
     * @param appliedTransforms a list of {@link Transform}s that have 
     *    already been applied. The list is defensively 
     *    copied to protect against subsequent modification. The list must 
     *    contain at least one entry.
     * @param result the result of processing the sequence of 
     *    <code>appliedTransforms</code>
     * @param transforms a list of {@link Transform}s that are to be applied
     *    when generating the signature. The list is defensively copied to 
     *    protect against subsequent modification. May be <code>null</code> 
     *    or empty.
     * @param type the reference type, as a URI (may be <code>null</code>)
     * @param id the reference ID (may be <code>null</code>)
     * @return a <code>Reference</code>
     * @throws ClassCastException if any of the transforms (in either list) 
     *    are not of type <code>Transform</code>
     * @throws IllegalArgumentException if <code>uri</code> is not RFC 2396
     *    compliant or <code>appliedTransforms</code> is empty
     * @throws NullPointerException if <code>dm</code>, 
     *    <code>appliedTransforms</code> or <code>result</code> is 
     *    <code>null</code>
     */
    public abstract Reference newReference(String uri, DigestMethod dm, 
        java.util.List<Object> appliedTransforms, Data result, java.util.List<Object> transforms, String type, 
        String id);

    /**
     * Creates a <code>SignedInfo</code> with the specified canonicalization
     * and signature methods, and list of one or more references. 
     *
     * @param cm the canonicalization method
     * @param sm the signature method
     * @param references a list of one or more {@link Reference}s. The list is
     *    defensively copied to protect against subsequent modification.
     * @return a <code>SignedInfo</code>
     * @throws ClassCastException if any of the references are not of
     *    type <code>Reference</code> 
     * @throws IllegalArgumentException if <code>references</code> is empty
     * @throws NullPointerException if any of the parameters
     *    are <code>null</code>
     */
    public abstract SignedInfo newSignedInfo(CanonicalizationMethod cm,
        SignatureMethod sm, java.util.List<Object> references);

    /**
     * Creates a <code>SignedInfo</code> with the specified parameters.
     *
     * @param cm the canonicalization method
     * @param sm the signature method
     * @param references a list of one or more {@link Reference}s. The list is
     *    defensively copied to protect against subsequent modification.
     * @param id the id (may be <code>null</code>)
     * @return a <code>SignedInfo</code>
     * @throws ClassCastException if any of the references are not of
     *    type <code>Reference</code> 
     * @throws IllegalArgumentException if <code>references</code> is empty
     * @throws NullPointerException if <code>cm</code>, <code>sm</code>, or
     *    <code>references</code> are <code>null</code>
     */
    public abstract SignedInfo newSignedInfo(CanonicalizationMethod cm,
        SignatureMethod sm, java.util.List<Object> references, String id);

    // Object factory methods
    /**
     * Creates an <code>XMLObject</code> from the specified parameters.
     *
     * @param content a list of {@link XMLStructure}s. The list
     *    is defensively copied to protect against subsequent modification.
     *    May be <code>null</code> or empty.
     * @param id the Id (may be <code>null</code>)
     * @param mimeType the mime type (may be <code>null</code>)
     * @param encoding the encoding (may be <code>null</code>)
     * @return an <code>XMLObject</code>
     * @throws ClassCastException if <code>content</code> contains any 
     *    entries that are not of type {@link XMLStructure}
     */
    public abstract XMLObject newXMLObject(java.util.List<Object> content, String id, 
        String mimeType, String encoding);

    /**
     * Creates a <code>Manifest</code> containing the specified 
     * list of {@link Reference}s. 
     *
     * @param references a list of one or more <code>Reference</code>s. The list
     *    is defensively copied to protect against subsequent modification.
     * @return a <code>Manifest</code>
     * @throws NullPointerException if <code>references</code> is 
     *    <code>null</code>
     * @throws IllegalArgumentException if <code>references</code> is empty
     * @throws ClassCastException if <code>references</code> contains any 
     *    entries that are not of type {@link Reference}
     */
    public abstract Manifest newManifest(java.util.List<Object> references);

    /**
     * Creates a <code>Manifest</code> containing the specified 
     * list of {@link Reference}s and optional id. 
     *
     * @param references a list of one or more <code>Reference</code>s. The list
     *    is defensively copied to protect against subsequent modification.
     * @param id the id (may be <code>null</code>)
     * @return a <code>Manifest</code>
     * @throws NullPointerException if <code>references</code> is 
     *    <code>null</code>
     * @throws IllegalArgumentException if <code>references</code> is empty
     * @throws ClassCastException if <code>references</code> contains any 
     *    entries that are not of type {@link Reference}
     */
    public abstract Manifest newManifest(java.util.List<Object> references, String id);

    /**
     * Creates a <code>SignatureProperty</code> containing the specified 
     * list of {@link XMLStructure}s, target URI and optional id. 
     *
     * @param content a list of one or more <code>XMLStructure</code>s. The list
     *    is defensively copied to protect against subsequent modification.
     * @param target the target URI of the Signature that this property applies 
     *    to
     * @param id the id (may be <code>null</code>)
     * @return a <code>SignatureProperty</code>
     * @throws NullPointerException if <code>content</code> or 
     *    <code>target</code> is <code>null</code>
     * @throws IllegalArgumentException if <code>content</code> is empty
     * @throws ClassCastException if <code>content</code> contains any 
     *    entries that are not of type {@link XMLStructure}
     */
    public abstract SignatureProperty newSignatureProperty
        (java.util.List<Object> content, String target, String id);

    /**
     * Creates a <code>SignatureProperties</code> containing the specified 
     * list of {@link SignatureProperty}s and optional id. 
     *
     * @param properties a list of one or more <code>SignatureProperty</code>s. 
     *    The list is defensively copied to protect against subsequent 
     *    modification.
     * @param id the id (may be <code>null</code>)
     * @return a <code>SignatureProperties</code>
     * @throws NullPointerException if <code>properties</code>
     *    is <code>null</code>
     * @throws IllegalArgumentException if <code>properties</code> is empty
     * @throws ClassCastException if <code>properties</code> contains any 
     *    entries that are not of type {@link SignatureProperty}
     */
    public abstract SignatureProperties newSignatureProperties
        (java.util.List<Object> properties, String id);

    // Algorithm factory methods
    /**
     * Creates a <code>DigestMethod</code> for the specified algorithm URI 
     * and parameters.
     *
     * @param algorithm the URI identifying the digest algorithm
     * @param params algorithm-specific digest parameters (may be 
     *    <code>null</code>)
     * @return the <code>DigestMethod</code>
     * @throws InvalidAlgorithmParameterException if the specified parameters
     *    are inappropriate for the requested algorithm
     * @throws NoSuchAlgorithmException if an implementation of the
     *    specified algorithm cannot be found
     * @throws NullPointerException if <code>algorithm</code> is 
     *    <code>null</code>
     */
    public abstract DigestMethod newDigestMethod(String algorithm, 
        javax.xml.crypto.dsig.spec.DigestMethodParameterSpec paramsJ) ;//throws NoSuchAlgorithmException,
        //InvalidAlgorithmParameterException;

    /**
     * Creates a <code>SignatureMethod</code> for the specified algorithm URI 
     * and parameters.
     *
     * @param algorithm the URI identifying the signature algorithm
     * @param params algorithm-specific signature parameters (may be 
     *    <code>null</code>)
     * @return the <code>SignatureMethod</code>
     * @throws InvalidAlgorithmParameterException if the specified parameters
     *    are inappropriate for the requested algorithm
     * @throws NoSuchAlgorithmException if an implementation of the
     *    specified algorithm cannot be found
     * @throws NullPointerException if <code>algorithm</code> is 
     *    <code>null</code>
     */
    public abstract SignatureMethod newSignatureMethod(String algorithm, 
        javax.xml.crypto.dsig.spec.SignatureMethodParameterSpec paramsJ) ;//throws NoSuchAlgorithmException,
        //InvalidAlgorithmParameterException;

    /**
     * Creates a <code>Transform</code> for the specified algorithm URI 
     * and parameters.
     *
     * @param algorithm the URI identifying the transform algorithm
     * @param params algorithm-specific transform parameters (may be 
     *    <code>null</code>)
     * @return the <code>Transform</code>
     * @throws InvalidAlgorithmParameterException if the specified parameters
     *    are inappropriate for the requested algorithm
     * @throws NoSuchAlgorithmException if an implementation of the
     *    specified algorithm cannot be found
     * @throws NullPointerException if <code>algorithm</code> is 
     *    <code>null</code>
     */
    public abstract Transform newTransform(String algorithm, 
        TransformParameterSpec paramsJ) ;//throws NoSuchAlgorithmException,
        //InvalidAlgorithmParameterException;

    /**
     * Creates a <code>Transform</code> for the specified algorithm URI 
     * and parameters. The parameters are specified as a mechanism-specific
     * <code>XMLStructure</code> (ex: {@link DOMStructure}). This method is 
     * useful when the parameters are in XML form or there is no standard 
     * class for specifying the parameters.
     *
     * @param algorithm the URI identifying the transform algorithm
     * @param params a mechanism-specific XML structure from which to
     *   unmarshal the parameters from (may be <code>null</code> if
     *   not required or optional)
     * @return the <code>Transform</code>
     * @throws ClassCastException if the type of <code>params</code> is
     *   inappropriate for this <code>XMLSignatureFactory</code>
     * @throws InvalidAlgorithmParameterException if the specified parameters
     *    are inappropriate for the requested algorithm
     * @throws NoSuchAlgorithmException if an implementation of the
     *    specified algorithm cannot be found
     * @throws NullPointerException if <code>algorithm</code> is 
     *    <code>null</code>
     */
    public abstract Transform newTransform(String algorithm, 
        XMLStructure paramsJ) ;//throws NoSuchAlgorithmException,
        //InvalidAlgorithmParameterException;

    /**
     * Creates a <code>CanonicalizationMethod</code> for the specified 
     * algorithm URI and parameters.
     *
     * @param algorithm the URI identifying the canonicalization algorithm
     * @param params algorithm-specific canonicalization parameters (may be 
     *    <code>null</code>)
     * @return the <code>CanonicalizationMethod</code>
     * @throws InvalidAlgorithmParameterException if the specified parameters
     *    are inappropriate for the requested algorithm
     * @throws NoSuchAlgorithmException if an implementation of the
     *    specified algorithm cannot be found
     * @throws NullPointerException if <code>algorithm</code> is 
     *    <code>null</code>
     */
    public abstract CanonicalizationMethod newCanonicalizationMethod(
        String algorithm, C14NMethodParameterSpec paramsJ);// 
        //throws NoSuchAlgorithmException, InvalidAlgorithmParameterException;

    /**
     * Creates a <code>CanonicalizationMethod</code> for the specified 
     * algorithm URI and parameters. The parameters are specified as a 
     * mechanism-specific <code>XMLStructure</code> (ex: {@link DOMStructure}). 
     * This method is useful when the parameters are in XML form or there is 
     * no standard class for specifying the parameters.
     *
     * @param algorithm the URI identifying the canonicalization algorithm
     * @param params a mechanism-specific XML structure from which to
     *   unmarshal the parameters from (may be <code>null</code> if
     *   not required or optional)
     * @return the <code>CanonicalizationMethod</code>
     * @throws ClassCastException if the type of <code>params</code> is
     *   inappropriate for this <code>XMLSignatureFactory</code>
     * @throws InvalidAlgorithmParameterException if the specified parameters
     *    are inappropriate for the requested algorithm
     * @throws NoSuchAlgorithmException if an implementation of the
     *    specified algorithm cannot be found
     * @throws NullPointerException if <code>algorithm</code> is 
     *    <code>null</code>
     */
    public abstract CanonicalizationMethod newCanonicalizationMethod(
        String algorithm, XMLStructure paramsJ); 
        //throws NoSuchAlgorithmException, InvalidAlgorithmParameterException;

    /**
     * Returns a <code>KeyInfoFactory</code> that creates <code>KeyInfo</code>
     * objects. The returned <code>KeyInfoFactory</code> has the same 
     * mechanism type and provider as this <code>XMLSignatureFactory</code>.
     *
     * @return a <code>KeyInfoFactory</code>
     * @throws NoSuchMechanismException if a <code>KeyFactory</code> 
     *    implementation with the same mechanism type and provider
     *    is not available
     */
    public javax.xml.crypto.dsig.keyinfo.KeyInfoFactory getKeyInfoFactory() {
        return javax.xml.crypto.dsig.keyinfo.KeyInfoFactory.getInstance(getMechanismType(), getProvider());
    }

    /**
     * Unmarshals a new <code>XMLSignature</code> instance from a
     * mechanism-specific <code>XMLValidateContext</code> instance.
     *
     * @param context a mechanism-specific context from which to unmarshal the
     *    signature from
     * @return the <code>XMLSignature</code>
     * @throws NullPointerException if <code>context</code> is 
     *    <code>null</code>
     * @throws ClassCastException if the type of <code>context</code> is
     *    inappropriate for this factory
     * @throws MarshalException if an unrecoverable exception occurs 
     *    during unmarshalling
     */
    public abstract XMLSignature unmarshalXMLSignature
        (XMLValidateContext context) ;//throws MarshalException;

    /**
     * Unmarshals a new <code>XMLSignature</code> instance from a
     * mechanism-specific <code>XMLStructure</code> instance.
     * This method is useful if you only want to unmarshal (and not
     * validate) an <code>XMLSignature</code>.
     *
     * @param xmlStructure a mechanism-specific XML structure from which to 
     *    unmarshal the signature from
     * @return the <code>XMLSignature</code>
     * @throws NullPointerException if <code>xmlStructure</code> is 
     *    <code>null</code>
     * @throws ClassCastException if the type of <code>xmlStructure</code> is
     *    inappropriate for this factory
     * @throws MarshalException if an unrecoverable exception occurs 
     *    during unmarshalling
     */
    public abstract XMLSignature unmarshalXMLSignature
        (XMLStructure xmlStructure) ;//throws MarshalException;

    /**
     * Indicates whether a specified feature is supported.
     *
     * @param feature the feature name (as an absolute URI)
     * @return <code>true</code> if the specified feature is supported,
     *    <code>false</code> otherwise
     * @throws NullPointerException if <code>feature</code> is <code>null</code>
     */
    public abstract bool isFeatureSupported(String feature);

    /**
     * Returns a reference to the <code>URIDereferencer</code> that is used by 
     * default to dereference URIs in {@link Reference} objects.
     *
     * @return a reference to the default <code>URIDereferencer</code> (never
     *    <code>null</code>)
     */
    public abstract URIDereferencer getURIDereferencer();
}
}