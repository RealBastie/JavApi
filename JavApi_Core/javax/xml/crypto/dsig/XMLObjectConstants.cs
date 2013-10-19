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
using java = biz.ritter.javapi;

namespace biz.ritter.javapix.xml.crypto.dsig
{

	internal class XMLObjectConstants
	{
		private XMLObjectConstants ()
		{
		}
		/**
     * URI that identifies the <code>Object</code> element (this can be 
     * specified as the value of the <code>type</code> parameter of the 
     * {@link Reference} class to identify the referent's type).
     */
		internal const String TYPE = "http://www.w3.org/2000/09/xmldsig#Object";
		
	}
}

