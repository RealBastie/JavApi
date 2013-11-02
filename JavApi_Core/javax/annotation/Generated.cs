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

namespace biz.ritter.javapix.annotation{

	[java.lang.annotation.Documented]
	[java.lang.annotation.Retention(java.lang.annotation.RetentionPolicy.SOURCE)]
	[java.lang.annotation.Target(new java.lang.annotation.ElementType[] { java.lang.annotation.ElementType.PACKAGE, java.lang.annotation.ElementType.TYPE,
		java.lang.annotation.ElementType.ANNOTATION_TYPE, java.lang.annotation.ElementType.METHOD,
		java.lang.annotation.ElementType.CONSTRUCTOR, java.lang.annotation.ElementType.FIELD, java.lang.annotation.ElementType.LOCAL_VARIABLE,
		java.lang.annotation.ElementType.PARAMETER })]
public class Generated : java.lang.annotation.AbstractAnnotation {

		public String []valueJ;
    public String[] value() {
			return this.valueJ;
		}
		private String commentsJ;
    public String comments(){
			return this.commentsJ;
		}
		private String dateJ;
    public String date(){
			return this.dateJ;
		}
}
}