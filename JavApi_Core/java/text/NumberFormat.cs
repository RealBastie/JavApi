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

namespace biz.ritter.javapi.text
{
    public class NumberFormat 
    {

        protected java.math.RoundingMode roundingMode = java.math.RoundingMode.UNNECESSARY;

        /// <summary>
        /// Use factory methods to create instance.
        /// </summary>
        protected NumberFormat (){}

        private bool parseIntegerOnly = false;

        public static NumberFormat getIntegerInstance()
        {
            NumberFormat nf = new NumberFormat ();
            nf.roundingMode = java.math.RoundingMode.HALF_EVEN;
            nf.setParseIntegerOnly (true);
            return nf;
        }

        public void setParseIntegerOnly (bool b) {
            this.parseIntegerOnly = b;
            this.roundingMode = b ? java.math.RoundingMode.HALF_EVEN : java.math.RoundingMode.UNNECESSARY;
        }
        public bool isParseIntegerOnly () {
            return this.parseIntegerOnly;
        }

        public static NumberFormat getInstance()
        {
            return new NumberFormat();
        }

        public String format (long l) {
            String pattern = "{0:g}";
            return String.Format (pattern,l);
        }
        public String format (double d){
            String pattern = "{0:g}";
            if (this.isParseIntegerOnly()) {
                pattern = "{0:0}";
                if (d != (long)d)
                {
                    long low = (long)d;
                    if (d - .5 <= low) // 1 Nachkommastelle mit Wert 5 oder kleiner
                    {
                        d = low;
                    }
                    else {
                        d = low + 1;
                    }
                }
            }
            return String.Format (pattern,d);
        }

        public java.lang.Number parse(String text)
        {
            Decimal bigDecimal;
            Decimal.TryParse(text, out bigDecimal);
            if (Decimal.Compare(bigDecimal, new Decimal(java.lang.Double.MAX_VALUE)) > 0 ||
                Decimal.Compare(bigDecimal, new Decimal(java.lang.Double.MIN_VALUE)) < 0)
                throw new ParseException("Value outside java.lang.Double range - sorry.", -1);
            else
            {
                return new java.lang.Double(Decimal.ToDouble(bigDecimal));
            }
        }

        public java.math.RoundingMode getRoundingMode()
        {
            throw new java.lang.UnsupportedOperationException();
        }
        public void setRoundingMode(java.math.RoundingMode mode)
        {
            throw new java.lang.UnsupportedOperationException();
        }
    }
}
