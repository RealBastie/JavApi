/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

// $Id: SecuritySupport.java 670282 2008-06-22 01:00:42Z mrglavas $

using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapix.xml.parsers
{
	/**
 * This class is duplicated for each JAXP subpackage so keep it in sync.
 * It is package private and therefore is not exposed as part of the JAXP
 * API.
 *
 * Security related methods that only work on J2SE 1.2 and newer.
 */
	internal sealed class SecuritySupport
	{
		private SecuritySupport ()
		{
		}

		internal static java.lang.ClassLoader getContextClassLoader ()
		{
			java.lang.ClassLoader cl = null;
			try {
				cl = java.lang.Thread.currentThread ().getContextClassLoader ();
			} catch (java.lang.SecurityException ex) {
			}
			return cl;
		
		}

		internal static String getSystemProperty (String propName)
		{
			return java.lang.SystemJ.getProperty (propName);
		}

		internal static java.io.FileInputStream getFileInputStream (java.io.File file)
        //throws FileNotFoundException
		{
			return new java.io.FileInputStream (file);
		}

		internal static java.io.InputStream getResourceAsStream (java.lang.ClassLoader cl,
		                                                          String name)
		{
			java.io.InputStream ris;
			if (cl == null) {
				ris = java.lang.ClassLoader.getSystemResourceAsStream (name);
			} else {
				ris = cl.getResourceAsStream (name);
			}
			return ris;
		}

		internal static bool doesFileExist (java.io.File f)
		{
			return f.exists ();
		}
	}
}