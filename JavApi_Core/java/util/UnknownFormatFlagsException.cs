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
 * An {@code UnknownFormatFlagsException} will be thrown if there is
 * an unknown flag.
 * 
 * @see java.lang.RuntimeException
 */
 [Serializable]
public class UnknownFormatFlagsException : IllegalFormatException {

    private const long serialVersionUID = 19370506L;

    private String flags;

    /**
     * Constructs a new {@code UnknownFormatFlagsException} with the specified
     * flags.
     * 
     * @param f
     *           the specified flags.
     */
    public UnknownFormatFlagsException(String f) {
        if (null == f) {
            throw new java.lang.NullPointerException();
        }
        flags = f;
    }

    /**
     * Returns the flags associated with the exception.
     * 
     * @return the flags associated with the exception.
     */
    public String getFlags() {
        return flags;
    }

    /**
     * Returns the message associated with the exception.
     * 
     * @return the message associated with the exception.
     */
    public override String getMessage() {
        // luni.46=The flags are {0}
        return "The flags are "+flags;//Messages.getString("luni.46", flags); //$NON-NLS-1$
    }
}
}