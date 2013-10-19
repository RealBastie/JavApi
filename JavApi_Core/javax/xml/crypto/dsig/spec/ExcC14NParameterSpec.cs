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

namespace biz.ritter.javapix.xml.crypto.dsig.spec
{

/**
 * Parameters for the W3C Recommendation:
 * <a href="http://www.w3.org/TR/xml-exc-c14n/">
 * Exclusive XML Canonicalization (C14N) algorithm</a>. The
 * parameters include an optional inclusive namespace prefix list. The XML 
 * Schema Definition of the Exclusive XML Canonicalization parameters is
 * defined as:
 * <pre><code>
 * &lt;schema xmlns="http://www.w3.org/2001/XMLSchema"
 *         xmlns:ec="http://www.w3.org/2001/10/xml-exc-c14n#"
 *         targetNamespace="http://www.w3.org/2001/10/xml-exc-c14n#"
 *         version="0.1" elementFormDefault="qualified"&gt;
 *
 * &lt;element name="InclusiveNamespaces" type="ec:InclusiveNamespaces"/&gt;
 * &lt;complexType name="InclusiveNamespaces"&gt;
 *   &lt;attribute name="PrefixList" type="xsd:string"/&gt;
 * &lt;/complexType&gt;
 * &lt;/schema&gt;
 * </code></pre>
 *
 * @author Sean Mullan
 * @author JSR 105 Expert Group
 * @see CanonicalizationMethod
 */
public sealed class ExcC14NParameterSpec : C14NMethodParameterSpec {

    private java.util.List<Object> preList;

    /**
     * Indicates the default namespace ("#default").
     */
    public static readonly String DEFAULT = "#default";

    /**
     * Creates a <code>ExcC14NParameterSpec</code> with an empty prefix 
     * list.
     */
    public ExcC14NParameterSpec() {
        preList = java.util.Collections<Object>.EMPTY_LIST;
    }

    /**
     * Creates a <code>ExcC14NParameterSpec</code> with the specified list
     * of prefixes. The list is copied to protect against subsequent 
     * modification.
     *
     * @param prefixList the inclusive namespace prefix list. Each entry in
     *    the list is a <code>String</code> that represents a namespace prefix.
     * @throws NullPointerException if <code>prefixList</code> is 
     *    <code>null</code>
     * @throws ClassCastException if any of the entries in the list are not
     *    of type <code>String</code>
     */
    public ExcC14NParameterSpec(java.util.List<Object> prefixList) {
        if (prefixList == null) {
            throw new java.lang.NullPointerException("prefixList cannot be null");
        }
        this.preList = unmodifiableCopyOfList(prefixList);
        for (int i = 0, size = preList.size(); i < size; i++) {
            if (!(preList.get(i) is String || preList.get(i) is java.lang.StringJ)) {
                throw new java.lang.ClassCastException("not a String");
            }
        }
    }

    private static java.util.List<Object> unmodifiableCopyOfList(java.util.List<Object> list) {
        return java.util.Collections<Object>.unmodifiableList(new java.util.ArrayList<Object>(list));
    }

    /**
     * Returns the inclusive namespace prefix list. Each entry in the list
     * is a <code>String</code> that represents a namespace prefix.
     *
     * <p>This implementation returns an {@link
     * java.util.Collections#unmodifiableList unmodifiable list}.
     *
     * @return the inclusive namespace prefix list (may be empty but never
     *    <code>null</code>)
     */
    public java.util.List<Object> getPrefixList() {
        return preList;
    }
}
}