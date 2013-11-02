/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements. See the NOTICE file distributed with this
 * work for additional information regarding copyright ownership. The ASF
 * licenses this file to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 */

using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.lang.annotation
{
	/**
 * Indicates that an annotation type has changed since it was compiled or
 * serialized.
 *
 * @since 1.5
 */
	public class AnnotationTypeMismatchException : java.lang.RuntimeException
	{
		private const long serialVersionUID = 8125925355765570191L;
		private java.lang.reflect.Method elementJ;
		private String foundTypeJ;

		/**
     * Constructs an instance for the given type element and the type found.
     *
     * @param element
     *            the annotation type element.
     * @param foundType
     *            the invalid type that was found. This is actually the textual
     *            type description found in the binary class representation,
     *            so it may not be human-readable.
     */
		public AnnotationTypeMismatchException (java.lang.reflect.Method element, String foundType) :
			base("The annotation element, "+element+", doesn't match the type "+foundType+".")
		{ //$NON-NLS-1$
			this.elementJ = element;
			this.foundTypeJ = foundType;
		}

		/**
     * Returns the method object for the invalid type.
     *
     * @return a {@link Method} instance.
     */
		public java.lang.reflect.Method element ()
		{
			return elementJ;
		}

		/**
     * Returns the invalid type.
     *
     * @return a string describing the invalid data.
     */
		public String foundType ()
		{
			return foundTypeJ;
		}
	}
}