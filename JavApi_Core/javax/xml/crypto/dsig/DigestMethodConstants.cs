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

namespace biz.ritter.javapix.xml.crypto.dsig
{

	internal class DigestMethodConstants
	{
		private DigestMethodConstants ()
		{
		}
		/**
     * The <a href="http://www.w3.org/2000/09/xmldsig#sha1">
     * SHA1</a> digest method algorithm URI.
     */
		internal String SHA1 = "http://www.w3.org/2000/09/xmldsig#sha1";
		
		/**
     * The <a href="http://www.w3.org/2001/04/xmlenc#sha256">
     * SHA256</a> digest method algorithm URI.
     */
		internal String SHA256 = "http://www.w3.org/2001/04/xmlenc#sha256";
		
		/**
     * The <a href="http://www.w3.org/2001/04/xmlenc#sha512">
     * SHA512</a> digest method algorithm URI.
     */
		internal String SHA512 = "http://www.w3.org/2001/04/xmlenc#sha512";
		
		/**
     * The <a href="http://www.w3.org/2001/04/xmlenc#ripemd160">
     * RIPEMD-160</a> digest method algorithm URI.
     */
		internal String RIPEMD160 = "http://www.w3.org/2001/04/xmlenc#ripemd160";
		
	}
}

