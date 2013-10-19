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

namespace biz.ritter.javapix.xml.crypto.dom
{

/**
 * A DOM-specific {@link URIReference}. The purpose of this class is to 
 * provide additional context necessary for resolving XPointer URIs or 
 * same-document references. 
 *
 * @author Sean Mullan
 * @author JSR 105 Expert Group
 */
public interface DOMURIReference : URIReference {

    /**
     * Returns the here node.
     *
     * @return the attribute or processing instruction node or the
     *    parent element of the text node that directly contains the URI 
     */
    org.w3c.dom.Node getHere();
}
}