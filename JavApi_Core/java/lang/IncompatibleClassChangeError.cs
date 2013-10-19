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
 */
using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.lang{


	/**
	 * {@code IncompatibleClassChangeError} is the superclass of all classes which
	 * represent errors that occur when inconsistent class files are loaded into
	 * the same running image.
	 * 
	 * @see Error
	 */
	public class IncompatibleClassChangeError : LinkageError {

	    private const long serialVersionUID = -4914975503642802119L;

	    /**
	     * Constructs a new {@code IncompatibleClassChangeError} that includes the
	     * current stack trace.
	     */
	    public IncompatibleClassChangeError() :base(){
	        
	    }

	    /**
	     * Constructs a new {@code IncompatibleClassChangeError} with the current
	     * stack trace and the specified detail message.
	     * 
	     * @param detailMessage
	     *            the detail message for this error.
	     */
	    public IncompatibleClassChangeError(String detailMessage) : base(detailMessage){
	        
	    }
	}

}