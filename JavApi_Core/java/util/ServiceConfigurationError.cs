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

namespace biz.ritter.javapi.util
{

/**
 * An Error that can be thrown when something wrong occurs in loading a service
 * provider.
 */
    [Serializable]
public class ServiceConfigurationError : java.lang.Error {
    
    private const long serialVersionUID = 74132770414881L;

    /**
     * The constructor
     * 
     * @param msg
     *            the message of this error
     */
    public ServiceConfigurationError(String msg) :base(msg){
    }

    /**
     * The constructor
     * 
     * @param msg
     *            the message of this error
     * @param cause 
     *            the cause of this error
     */
    public ServiceConfigurationError(String msg, java.lang.Throwable cause) :
        base(msg, cause){
    }
}
}