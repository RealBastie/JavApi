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
 * Thrown when the virtual machine notices that a program tries to reference,
 * on a class or object, a field that does not exist.
 * <p>
 * Note that this can only occur when inconsistent class files are being loaded.
 */
public class NoSuchFieldError : IncompatibleClassChangeError {

    private const long serialVersionUID = -3456430195886129035L;

    /**
     * Constructs a new {@code NoSuchFieldError} that includes the current stack
     * trace.
     */
    public NoSuchFieldError() :base(){
        
    }

    /**
     * Constructs a new {@code NoSuchFieldError} with the current stack trace
     * and the specified detail message.
     * 
     * @param detailMessage
     *            the detail message for this error.
     */
    public NoSuchFieldError(String detailMessage) :base(detailMessage){
        
    }
}
}