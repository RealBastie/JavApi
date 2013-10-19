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
     * An {@code IllegalFormatConversionException} will be thrown when the parameter
     * is incompatible with the corresponding format specifier.
     * 
     * @see java.lang.RuntimeException
     *
     * @since 1.5
     */
    [Serializable]
    public class IllegalFormatConversionException : IllegalFormatException
            , java.io.Serializable
    {
        private const long serialVersionUID = 17000126L;

        private char c;

        private java.lang.Class arg;

        /**
         * Constructs a new {@code IllegalFormatConversionException} with the class
         * of the mismatched conversion and corresponding parameter.
         * 
         * @param c
         *           the class of the mismatched conversion.
         * @param arg
         *           the corresponding parameter.
         */
        public IllegalFormatConversionException(char c, java.lang.Class arg)
        {
            this.c = c;
            if (arg == null)
            {
                throw new java.lang.NullPointerException();
            }
            this.arg = arg;
        }

        /**
         * Returns the class of the mismatched parameter.
         * 
         * @return the class of the mismatched parameter.
         */
        public java.lang.Class getArgumentClass()
        {
            return arg;
        }

        /**
         * Returns the incompatible conversion.
         * 
         * @return the incompatible conversion.
         */
        public char getConversion()
        {
            return c;
        }

        /**
         * Returns the message string of the IllegalFormatConversionException.
         * 
         * @return the message string of the IllegalFormatConversionException.
         */

        public override String getMessage()
        {
            java.lang.StringBuilder buffer = new java.lang.StringBuilder();
            buffer.append(c);
            buffer.append(" is incompatible with ");
            buffer.append(arg.getName());
            return buffer.toString();
        }

    }
}