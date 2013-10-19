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
	internal sealed class SignatureMethodConstants
	{
		/// <summary>
		/// The <a href="http://www.w3.org/2000/09/xmldsig#dsa-sha1">DSAwithSHA1</a>
		/// (DSS) signature method algorithm URI.
		/// </summary>
		internal String DSA_SHA1 = "http://www.w3.org/2000/09/xmldsig#dsa-sha1";
		
		/// <summary>
		/// The <a href="http://www.w3.org/2000/09/xmldsig#rsa-sha1">RSAwithSHA1</a>
		/// (PKCS #1) signature method algorithm URI.
		/// </summary>
		internal String RSA_SHA1 = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
		
		/// <summary>
		/// The <a href="http://www.w3.org/2000/09/xmldsig#hmac-sha1">HMAC-SHA1</a>
		/// MAC signature method algorithm URI 
		/// </summary>
		internal String HMAC_SHA1 = "http://www.w3.org/2000/09/xmldsig#hmac-sha1";
		
		private SignatureMethodConstants ()
		{
		}
	}
}

