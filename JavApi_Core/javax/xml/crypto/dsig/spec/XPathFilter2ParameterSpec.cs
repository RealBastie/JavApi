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
 * Parameters for the W3C Recommendation
 * <a href="http://www.w3.org/TR/xmldsig-filter2/">
 * XPath Filter 2.0 Transform Algorithm</a>.
 * The parameters include a list of one or more {@link XPathType} objects.
 *
 * @author Bastie - change to generic List<XPathType>
 * @author Sean Mullan
 * @author JSR 105 Expert Group
 * @see Transform
 * @see XPathFilterParameterSpec
 */
public sealed class XPathFilter2ParameterSpec : TransformParameterSpec {

    private readonly java.util.List<XPathType> xPathList;

    /**
     * Creates an <code>XPathFilter2ParameterSpec</code>.
     *
     * @param xPathList a list of one or more {@link XPathType} objects. The 
     *    list is defensively copied to protect against subsequent modification.
     * @throws ClassCastException if <code>xPathList</code> contains any
     *    entries that are not of type {@link XPathType}
     * @throws IllegalArgumentException if <code>xPathList</code> is empty
     * @throws NullPointerException if <code>xPathList</code> is 
     *    <code>null</code>
     */
    public XPathFilter2ParameterSpec(java.util.List<XPathType> xPathList) {
        if (xPathList == null) {
            throw new java.lang.NullPointerException("xPathList cannot be null");
        }
        this.xPathList = unmodifiableCopyOfList(xPathList);
        if (this.xPathList.isEmpty()) {
            throw new java.lang.IllegalArgumentException("xPathList cannot be empty");
        }
        // generic using List<XPathType> also remove XPathType checking
    }

    private static java.util.List<XPathType> unmodifiableCopyOfList(java.util.List<XPathType> list) {
        return java.util.Collections<XPathType>.unmodifiableList(new java.util.ArrayList<XPathType>(list));
    }

    /**
     * Returns a list of one or more {@link XPathType} objects. 
     * <p>
     * This implementation returns an {@link Collections#unmodifiableList
     * unmodifiable list}.
     *
     * @return a <code>List</code> of <code>XPathType</code> objects
     *    (never <code>null</code> or empty)
     */
    public java.util.List<XPathType> getXPathList() {
        return xPathList;
    }
}}
