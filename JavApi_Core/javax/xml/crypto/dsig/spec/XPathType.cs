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
 * The XML Schema Definition of the <code>XPath</code> element as defined in the
 * <a href="http://www.w3.org/TR/xmldsig-filter2">
 * W3C Recommendation for XML-Signature XPath Filter 2.0</a>:
 * <pre><code>
 * &lt;schema xmlns="http://www.w3.org/2001/XMLSchema"
 *         xmlns:xf="http://www.w3.org/2002/06/xmldsig-filter2"
 *         targetNamespace="http://www.w3.org/2002/06/xmldsig-filter2"
 *         version="0.1" elementFormDefault="qualified"&gt;
 *
 * &lt;element name="XPath"
 *          type="xf:XPathType"/&gt;
 *
 * &lt;complexType name="XPathType"&gt;
 *   &lt;simpleContent&gt;
 *     &lt;extension base="string"&gt;
 *       &lt;attribute name="Filter"&gt;
 *         &lt;simpleType&gt;
 *           &lt;restriction base="string"&gt;
 *             &lt;enumeration value="intersect"/&gt;
 *             &lt;enumeration value="subtract"/&gt;
 *             &lt;enumeration value="union"/&gt;
 *           &lt;/restriction&gt;
 *         &lt;/simpleType&gt;
 *       &lt;/attribute&gt;
 *     &lt;/extension&gt;
 *   &lt;/simpleContent&gt;
 * &lt;/complexType&gt;
 * </code></pre>
 * 
 * @author Bastie - change to generic Map<String,String>
 * @author Sean Mullan
 * @author JSR 105 Expert Group
 * @see XPathFilter2ParameterSpec
 */
public class XPathType {

    /**
     * Represents the filter set operation.
     */
    public class Filter {
        private readonly String operation;

        private Filter(String operation) {
            this.operation = operation;
        }

        /**
         * Returns the string form of the operation.
         *
         * @return the string form of the operation
         */
        public String toString() {
            return operation;
        }

        /**
         * The intersect filter operation.
         */
        public static readonly Filter INTERSECT = new Filter("intersect");

        /**
         * The subtract filter operation.
         */
        public static readonly Filter SUBTRACT = new Filter("subtract");

        /**
         * The union filter operation.
         */
        public static readonly Filter UNION = new Filter("union");
    }

    private readonly String expression;
    private readonly Filter filter;
    private java.util.Map<String,String> nsMap; 

    /**
     * Creates an <code>XPathType</code> instance with the specified XPath
     * expression and filter.
     *
     * @param expression the XPath expression to be evaluated
     * @param filter the filter operation ({@link Filter#INTERSECT},
     *    {@link Filter#SUBTRACT}, or {@link Filter#UNION})
     * @throws NullPointerException if <code>expression</code> or
     *    <code>filter</code> is <code>null</code>
     */
    public XPathType(String expression, Filter filter) {
        if (expression == null) {
            throw new java.lang.NullPointerException("expression cannot be null");
        }
        if (filter == null) {
            throw new java.lang.NullPointerException("filter cannot be null");
        }
        this.expression = expression;
        this.filter = filter;
        this.nsMap = new java.util.HashMap<String,String>();
        //this.nsMap = java.util.Collections<Object>.EMPTY_MAP;
    }

    /**
     * Creates an <code>XPathType</code> instance with the specified XPath
     * expression, filter, and namespace map. The map is copied to protect
     * against subsequent modification.
     *
     * @param expression the XPath expression to be evaluated
     * @param filter the filter operation ({@link Filter#INTERSECT},
     *    {@link Filter#SUBTRACT}, or {@link Filter#UNION})
     * @param namespaceMap the map of namespace prefixes. Each key is a
     *    namespace prefix <code>String</code> that maps to a corresponding
     *    namespace URI <code>String</code>.
     * @throws NullPointerException if <code>expression</code>,
     *    <code>filter</code> or <code>namespaceMap</code> are
     *    <code>null</code>
     * @throws ClassCastException if any of the map's keys or entries are
     *    not of type <code>String</code>
     */
    public XPathType(String expression, Filter filter, java.util.Map<String,String> namespaceMap) :
        this(expression, filter){
        if (namespaceMap == null) {
            throw new java.lang.NullPointerException("namespaceMap cannot be null");
        }
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
    public String getExpression() {
        return expression;
    }

    /**
     * Returns the filter operation.
     *
     * @return the filter operation
     */
    public Filter getFilter() {
        return filter;
    }

    /**
     * Returns a map of namespace prefixes. Each key is a namespace prefix
     * <code>String</code> that maps to a corresponding namespace URI
     * <code>String</code>.
     * <p>
     * This implementation returns an {@link Collections#unmodifiableMap
     * unmodifiable map}.
     *
     * @return a <code>Map</code> of namespace prefixes to namespace URIs
     *    (may be empty, but never <code>null</code>)
     */
    public java.util.Map<String,String> getNamespaceMap() {
        return nsMap;
    }
}
}