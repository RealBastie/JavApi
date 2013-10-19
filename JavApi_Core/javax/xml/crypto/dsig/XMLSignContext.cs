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

namespace biz.ritter.javapix.xml.crypto.dsig
{

/**
 * Contains context information for generating XML Signatures. This interface
 * is primarily intended for type-safety.
 *
 * <p>Note that <code>XMLSignContext</code> instances can contain
 * information and state specific to the XML signature structure it is
 * used with. The results are unpredictable if an
 * <code>XMLSignContext</code> is used with different signature structures
 * (for example, you should not use the same <code>XMLSignContext</code>
 * instance to sign two different {@link XMLSignature} objects).
 * <p>
 * <b><a name="Supported Properties"></a>Supported Properties</b>
 * <p>The following properties can be set using the 
 * {@link #setProperty setProperty} method.
 * <ul>
 *   <li><code>javax.xml.crypto.dsig.cacheReference</code>: value must be a
 *      {@link Boolean}. This property controls whether or not the digested
 *      {@link Reference} objects will cache the dereferenced content and 
 *      pre-digested input for subsequent retrieval via the
 *      {@link Reference#getDereferencedData Reference.getDereferencedData} and
 *	{@link Reference#getDigestInputStream Reference.getDigestInputStream}
 *      methods. The default value if not specified is 
 *	<code>Boolean.FALSE</code>.
 * </ul>
 *
 * @author Sean Mullan
 * @author JSR 105 Expert Group
 * @see XMLSignature#sign(XMLSignContext)
 */
public interface XMLSignContext : XMLCryptoContext {}}
