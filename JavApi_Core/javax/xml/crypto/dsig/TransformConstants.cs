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
using System;

namespace biz.ritter.javapix.xml.crypto.dsig
{
	internal sealed class TransformConstants
	{
		private TransformConstants (){}
			
		/// <summary>
		/// The <a href="http://www.w3.org/2000/09/xmldsig#base64">Base64</a> transform algorithm URI.
		/// </summary>
		internal const String BASE64 = "http://www.w3.org/2000/09/xmldsig#base64";

		/// <summary>
		/// The <a href="http://www.w3.org/2000/09/xmldsig#enveloped-signature">
		/// Enveloped Signature</a> transform algorithm URI.
		///</summary>
		internal const String ENVELOPED = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";
			
		/// <summary>
		/// The <a href="http://www.w3.org/TR/1999/REC-xpath-19991116">XPath</a> 
		/// transform algorithm URI.
		/// </summary>
		internal const String XPATH = "http://www.w3.org/TR/1999/REC-xpath-19991116";
			
		/// <summary>
		/// The <a href="http://www.w3.org/2002/06/xmldsig-filter2">
		/// XPath Filter 2</a> transform algorithm URI.
		/// </summary>
		internal const String XPATH2 = "http://www.w3.org/2002/06/xmldsig-filter2";
			
		/// <summary>
		/// The <a href="http://www.w3.org/TR/1999/REC-xslt-19991116">XSLT</a> 
		/// transform algorithm URI.
		/// </summary>
		internal const String XSLT = "http://www.w3.org/TR/1999/REC-xslt-19991116";
			

	}
}

