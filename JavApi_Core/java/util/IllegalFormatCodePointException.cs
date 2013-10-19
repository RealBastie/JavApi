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
     * An {@code IllegalFormatCodePointException} will be thrown if an invalid
     * Unicode code point (defined by {@link Character#isValidCodePoint(int)}) is
     * passed as a parameter to a Formatter.
     * 
     * @see java.lang.RuntimeException
     */
    [Serializable]
    public class IllegalFormatCodePointException : IllegalFormatException
            , java.io.Serializable
    {
        private const long serialVersionUID = 19080630L;

        private int c;

        /**
         * Constructs a new {@code IllegalFormatCodePointException} which is
         * specified by the invalid Unicode code point.
         * 
         * @param c
         *           the invalid Unicode code point.
         */
        public IllegalFormatCodePointException(int c)
        {
            this.c = c;
        }

        /**
         * Returns the invalid Unicode code point.
         * 
         * @return the invalid Unicode code point.
         */
        public int getCodePoint()
        {
            return c;
        }

        /**
         * Returns the message string of the IllegalFormatCodePointException.
         * 
         * @return the message string of the IllegalFormatCodePointException.
         */

        public override String getMessage()
        {
            java.lang.StringBuilder buffer = new java.lang.StringBuilder();
            buffer.append("Code point is ");
            char[] chars = java.lang.Character.toChars(c);
            for (int i = 0; i < chars.Length; i++)
            {
                buffer.append(chars[i]);
            }
            return buffer.toString();
        }
    }
}