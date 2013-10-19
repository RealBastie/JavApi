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
     * An {@code IllegalFormatPrecisionException} will be thrown if the precision is
     * a negative other than -1 or in other cases where precision is not supported.
     * 
     * @see java.lang.RuntimeException
     */
    [Serializable]
    public class IllegalFormatPrecisionException : IllegalFormatException
    {
        private const long serialVersionUID = 18711008L;

        private int p;

        /**
         * Constructs a new {@code IllegalFormatPrecisionException} with specified
         * precision.
         * 
         * @param p
         *           the precision.
         */
        public IllegalFormatPrecisionException(int p)
        {
            this.p = p;
        }

        /**
         * Returns the precision associated with the exception.
         * 
         * @return the precision.
         */
        public int getPrecision()
        {
            return p;
        }

        /**
         * Returns the message of the exception.
         * 
         * @return the message of the exception.
         */

        public override String getMessage()
        {
            return java.lang.StringJ.valueOf(p);
        }

    }
}