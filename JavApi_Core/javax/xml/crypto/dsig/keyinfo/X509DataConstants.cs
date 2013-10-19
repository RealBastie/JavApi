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

namespace biz.ritter.javapix.xml.crypto.dsig.keyinfo {
	internal class X509DataConstants
	{
		private X509DataConstants ()
		{
		}
		/**
     * URI identifying the X509Data KeyInfo type:
     * http://www.w3.org/2000/09/xmldsig#X509Data. This can be specified as
     * the value of the <code>type</code> parameter of the
     * {@link RetrievalMethod} class to describe a remote
     * <code>X509Data</code> structure.
     */
		internal const String TYPE = "http://www.w3.org/2000/09/xmldsig#X509Data";
		
		/**
     * URI identifying the binary (ASN.1 DER) X.509 Certificate KeyInfo type:
     * http://www.w3.org/2000/09/xmldsig#rawX509Certificate. This can be 
     * specified as the value of the <code>type</code> parameter of the
     * {@link RetrievalMethod} class to describe a remote X509 Certificate.
     */
		internal const String RAW_X509_CERTIFICATE_TYPE =
			"http://www.w3.org/2000/09/xmldsig#rawX509Certificate";
		
	}
}

