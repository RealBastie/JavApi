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

namespace biz.ritter.javapix.xml.crypto
{

/**
 * Indicates an exceptional condition that occured while executing a
 * transform algorithm.
 *
 * <p>A <code>TransformException</code> can contain a cause: another 
 * throwable that caused this <code>TransformException</code> to get thrown. 
 *
 * @see Transform#transform
 * @author Sean Mullan
 * @author JSR 105 Expert Group
 */
public class TransformException : java.lang.Exception {

    private const long serialVersionUID = 5082634801360427800L;

    /**
     * The throwable that caused this exception to get thrown, or null if this
     * exception was not caused by another throwable or if the causative
     * throwable is unknown. 
     *
     * @serial
     */
    private java.lang.Throwable cause;

    /**
     * Constructs a new <code>TransformException</code> with 
     * <code>null</code> as its detail message.
     */
    public TransformException() :base(){
        
    }

    /**
     * Constructs a new <code>TransformException</code> with the specified 
     * detail message. 
     *
     * @param message the detail message
     */
    public TransformException(String message) :base(message){
        
    }

    /**
     * Constructs a new <code>TransformException</code> with the 
     * specified detail message and cause.  
     * <p>Note that the detail message associated with
     * <code>cause</code> is <i>not</i> automatically incorporated in
     * this exception's detail message.
     *
     * @param message the detail message 
     * @param cause the cause (A <tt>null</tt> value is permitted, and 
     *	      indicates that the cause is nonexistent or unknown.)
     */
    public TransformException(String message, java.lang.Throwable cause) :base(message){
        this.cause = cause;
    }

    /**
     * Constructs a new <code>TransformException</code> with the specified 
     * cause and a detail message of 
     * <code>(cause==null ? null : cause.toString())</code>
     * (which typically contains the class and detail message of 
     * <code>cause</code>).
     *
     * @param cause the cause (A <tt>null</tt> value is permitted, and 
     *        indicates that the cause is nonexistent or unknown.)
     */
    public TransformException(java.lang.Throwable cause) :
        base(cause==null ? null : cause.toString()){
        this.cause = cause;
    }

    /**
     * Returns the cause of this <code>TransformException</code> or 
     * <code>null</code> if the cause is nonexistent or unknown.  (The 
     * cause is the throwable that caused this 
     * <code>TransformException</code> to get thrown.)
     *
     * @return the cause of this <code>TransformException</code> or 
     *         <code>null</code> if the cause is nonexistent or unknown.
     */
    public override java.lang.Throwable getCause() {
        return cause;
    }

    /**
     * Prints this <code>TransformException</code>, its backtrace and
     * the cause's backtrace to the standard error stream.
     */
    public override void printStackTrace() {
        base.printStackTrace();
        if (cause != null) {
            cause.printStackTrace();
        }
    }

    /**
     * Prints this <code>TransformException</code>, its backtrace and
     * the cause's backtrace to the specified print stream.
     *
     * @param s <code>PrintStream</code> to use for output
     */
    public void printStackTrace(java.io.PrintStream s) {
        this.printStackTrace(new java.io.PrintWriter(s));
    }

    /**
     * Prints this <code>TransformException</code>, its backtrace and
     * the cause's backtrace to the specified print writer.
     *
     * @param s <code>PrintWriter</code> to use for output
     */
    public override void printStackTrace(java.io.PrintWriter s) {
        base.printStackTrace(s);
        if (cause != null) {
            cause.printStackTrace(s);
        }
    }
}
}