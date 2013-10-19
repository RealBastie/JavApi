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
     * A {@code FormatFlagsConversionMismatchException} will be thrown if a
     * conversion and the flags are incompatible.
     * 
     * @see java.lang.RuntimeException
     */
    [Serializable]
    public class FormatFlagsConversionMismatchException :
            IllegalFormatException, java.io.Serializable
    {

        private const long serialVersionUID = 19120414L;

        private String f;

        private char c;

        /**
         * Constructs a new {@code FormatFlagsConversionMismatchException} with the
         * flags and conversion specified.
         * 
         * @param f
         *           the flags.
         * @param c
         *           the conversion.
         */
        public FormatFlagsConversionMismatchException(String f, char c)
        {
            if (null == f)
            {
                throw new java.lang.NullPointerException();
            }
            this.f = f;
            this.c = c;
        }

        /**
         * Returns the incompatible format flag.
         * 
         * @return the incompatible format flag.
         */
        public String getFlags()
        {
            return f;
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
         * Returns the message string of the {@code FormatFlagsConversionMismatchException}.
         * 
         * @return the message string of the {@code FormatFlagsConversionMismatchException}.
         */

        public override String getMessage()
        {
            java.lang.StringBuilder buffer = new java.lang.StringBuilder();
            buffer.append("Mismatched Convertor =");
            buffer.append(c);
            buffer.append(", Flags= ");
            buffer.append(f);
            return buffer.toString();
        }
    }
}