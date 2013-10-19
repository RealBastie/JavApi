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
	internal class CanonicalizationMethodeConstants
	{
		/// <summary>
		/// The <a href="http://www.w3.org/TR/2001/REC-xml-c14n-20010315">Canonical 
		/// XML (without comments)</a> canonicalization method algorithm URI.
		/// </summary>
		internal String INCLUSIVE = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";
		
		/// <summary>
		/// The 
		/// <a href="http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments">
		/// Canonical XML with comments</a> canonicalization method algorithm URI.
		/// </summary>
		internal String INCLUSIVE_WITH_COMMENTS = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";
		
		/// <summary>
		/// The <a href="http://www.w3.org/2001/10/xml-exc-c14n#">Exclusive 
		/// Canonical XML (without comments)</a> canonicalization method algorithm
		// URI.
		/// </summary>
		internal String EXCLUSIVE = "http://www.w3.org/2001/10/xml-exc-c14n#";
		
		/// <summary>
		/// The <a href="http://www.w3.org/2001/10/xml-exc-c14n#WithComments">
		/// Exclusive Canonical XML with comments</a> canonicalization method 
		/// algorithm URI.
		/// </summary>
		internal String EXCLUSIVE_WITH_COMMENTS = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";
		
		public CanonicalizationMethodeConstants ()
		{
		}
	}
}

