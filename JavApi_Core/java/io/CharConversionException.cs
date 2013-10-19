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

namespace biz.ritter.javapi.io
{

/**
 * The top level class for character conversion exceptions.
 */
	[Serializable]
public class CharConversionException : IOException {

    private const long serialVersionUID = -8680016352018427031L;

    /**
     * Constructs a new {@code CharConversionException} with its stack trace
     * filled in.
     */
    public CharConversionException() :base(){
        
    }

    /**
     * Constructs a new {@code CharConversionException} with its stack trace and
     * detail message filled in.
     * 
     * @param detailMessage
     *            the detail message for this exception.
     */
    public CharConversionException(String detailMessage) :
        base(detailMessage){
    }
}
}