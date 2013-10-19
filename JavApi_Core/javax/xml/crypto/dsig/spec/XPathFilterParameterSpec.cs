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
 * Parameters for the <a href="http://www.w3.org/TR/xmldsig-core/#sec-XPath">
 * XPath Filtering Transform Algorithm</a>.
 * The parameters include the XPath expression and an optional <code>Map</code> 
 * of additional namespace prefix mappings. The XML Schema Definition of
 * the XPath Filtering transform parameters is defined as:
 * <pre><code>
 * &lt;element name="XPath" type="string"/&gt;
 * </code></pre>
 *
 * @author Bastie - change to generic Map<String,String>
 * @author Sean Mullan
 * @author JSR 105 Expert Group
 * @see Transform
 */
public sealed class XPathFilterParameterSpec : TransformParameterSpec {

    private String xPath;
    private java.util.Map<String,String> nsMap;

    /**
     * Creates an <code>XPathFilterParameterSpec</code> with the specified 
     * XPath expression.
     *
     * @param xPath the XPath expression to be evaluated
     * @throws NullPointerException if <code>xPath</code> is <code>null</code>
     */
    public XPathFilterParameterSpec(String xPath) {
        if (xPath == null) {
            throw new java.lang.NullPointerException();
        }
        this.xPath = xPath;
        this.nsMap = new java.util.HashMap<String,String>();//Collections.EMPTY_MAP;
    }

    /**
     * Creates an <code>XPathFilterParameterSpec</code> with the specified 
     * XPath expression and namespace map. The map is copied to protect against
     * subsequent modification.
     *
     * @param xPath the XPath expression to be evaluated
     * @param namespaceMap the map of namespace prefixes. Each key is a
     *    namespace prefix <code>String</code> that maps to a corresponding
     *    namespace URI <code>String</code>.
     * @throws NullPointerException if <code>xPath</code> or
     *    <code>namespaceMap</code> are <code>null</code>
     * @throws ClassCastException if any of the map's keys or entries are not
     *    of type <code>String</code>
     */
    public XPathFilterParameterSpec(String xPath, java.util.Map<String,String> namespaceMap) {
        if (xPath == null || namespaceMap == null) {
            throw new java.lang.NullPointerException();
        }
        this.xPath = xPath;
        nsMap = unmodifiableCopyOfMap(namespaceMap);
        // generic using Map<String,String> also remove String checking
    }

    private static java.util.Map<String,String> unmodifiableCopyOfMap(java.util.Map<String,String> map) {
        return java.util.Collections<Object>.unmodifiableMap(new java.util.HashMap<String,String>(map));
    }

    /**
     * Returns the XPath expression to be evaluated.
     *
     * @return the XPath expression to be evaluated
     */
    public String getXPath() {
        return xPath;
    }

    /**
     * Returns a map of namespace prefixes. Each key is a namespace prefix 
     * <code>String</code> that maps to a corresponding namespace URI 
     * <code>String</code>.
     * <p>
     * This implementation returns an {@link Collections#unmodifiableMap 
     * unmodifiable map}.
     *
     * @return a <code>Map</code> of namespace prefixes to namespace URIs (may 
     *    be empty, but never <code>null</code>)
     */
    public java.util.Map<String,String> getNamespaceMap() {
        return nsMap;
    }
}
}