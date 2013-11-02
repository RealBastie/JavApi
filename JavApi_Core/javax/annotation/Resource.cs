/*
 *  Licensed to the Apache Software Foundation (ASF) under one or more
 *  contributor license agreements.  See the NOTICE file distributed with
 *  this work for additional information regarding copyright ownership.
 *  The ASF licenses this file to You under the Apache License, Version 2.0
 *  (the "License"); you may not use this file except in compliance with
 *  the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */
using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapix.annotation
{
	[java.lang.annotation.Target (new java.lang.annotation.ElementType [] {
		java.lang.annotation.ElementType.TYPE,
		java.lang.annotation.ElementType.FIELD,
		java.lang.annotation.ElementType.METHOD
	})]
	[java.lang.annotation.Retention (java.lang.annotation.RetentionPolicy.RUNTIME)]
	public class Resource : java.lang.annotation.AbstractAnnotation
	{
		public enum AuthenticationType
		{
			CONTAINER,
			APPLICATION
		}

		public AuthenticationType authenticationTypeJ = AuthenticationType.CONTAINER;

		public AuthenticationType authenticationType ()
		{
			return this.authenticationTypeJ;
		}

		public String descriptionJ = "";

		public String description ()
		{
			return this.descriptionJ;
		}

		public String mappedNameJ = "";

		public String mappedName ()
		{
			return this.mappedNameJ;
		}

		public String nameJ = "";

		public String name ()
		{
			return this.nameJ;
		}

		public bool shareableJ = true;

		public bool shareable ()
		{
			return this.shareableJ;
		}

		public java.lang.Class typeJ = typeof(Object).getClass ();

		public java.lang.Class type ()
		{
			return this.typeJ;
		}
	}
}