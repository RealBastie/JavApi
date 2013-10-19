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

namespace biz.ritter.javapi.util
{

/**
 * An {@code InvalidPropertiesFormatException} is thrown if loading the XML
 * document defining the properties does not follow the {@code Properties}
 * specification.
 * 
 * Even though this Exception inherits the {@code Serializable} interface, it is not
 * serializable. The methods used for serialization throw
 * {@code NotSerializableException}s.
 */
[Serializable]

public class InvalidPropertiesFormatException : java.io.IOException {
    
    private const long serialVersionUID = 7763056076009360219L;

    /**
     * Constructs a new {@code InvalidPropertiesFormatException} with the
     * current stack trace and message filled in.
     * 
     * @param m
     *           the detail message for the exception.
     */
		public InvalidPropertiesFormatException(String m) :base(m){
    }

    /**
     * Constructs a new {@code InvalidPropertiesFormatException} with the cause
     * for the Exception.
     * 
     * @param c
     *           the cause for the Exception.
     */
    public InvalidPropertiesFormatException(java.lang.Throwable c) {
        initCause(c);
    }
    
    private void writeObject(java.io.ObjectOutputStream outJ) 
		{//throws NotSerializableException{
        throw new java.io.NotSerializableException();        
    }
    
    private void readObject(java.io.ObjectInputStream inJ) 
		{//throws NotSerializableException{
        throw new java.io.NotSerializableException();        
    }
}
}