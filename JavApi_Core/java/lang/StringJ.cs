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

using System.Collections.Generic;
using System.Text;

namespace biz.ritter.javapi.lang
{
    public sealed class StringJ : CharSequence
    {
        private static readonly char[] ascii;
        static StringJ (){
            ascii = new char[128];
            for (int i = 0; i < ascii.Length; i++) {
                ascii[i] = (char) i;
            }
        }

        private int offset;
        private String delegateInstance;
        private int count;
        public StringJ (String delegateInstance) {
            this.delegateInstance = delegateInstance;
            this.offset = 0;
            this.count = this.delegateInstance.Length;
        }
        public StringJ(byte[] byteArray, String encoding)
            : this(System.Text.Encoding.GetEncoding(encoding).GetString(byteArray))
        {
        }
        public StringJ(byte[] utf8EncodedByteArray)
            : this(System.Text.Encoding.GetEncoding("utf-8").GetString(utf8EncodedByteArray))
        {
        }
        public StringJ(byte[] utf8EncodedByteArray, int offset, int length)
            : this(System.Text.Encoding.GetEncoding("utf-8").GetString(utf8EncodedByteArray,offset,length))
        {
        }
        public StringJ(byte[] byteArray, int offset, int length, String encoding)
            : this(System.Text.Encoding.GetEncoding(encoding).GetString(byteArray, offset, length))
        {
        }
        public StringJ(char[] utf8EncodedCharArray)
            : this(new String (utf8EncodedCharArray))
        {
        }
		public StringJ(char[] data, int offset, int length) : this (new String(data,offset,length)) {}
        internal StringJ(int start, int length, char[] data) : this (new String(data,start,length)) {
        }
        public char charAt(int index)
        {
            return this.delegateInstance.charAt(index);
        }
        public int length()
        {
            return this.delegateInstance.length();
        }
        public CharSequence subSequence(int start, int end)
        {
            StringJ cs = new StringJ(this.delegateInstance.Substring(start, end - start));
            return cs;
        }
        public override string ToString()
        {
            return this.delegateInstance.toString();
        }
        public string toString()
        {
            return this.delegateInstance.toString();
        }
        public bool regionMatches(int thisStart, StringJ other, int start, int length)
        {
            if (null == other) {
                throw new java.lang.NullPointerException();
            }
            if (start < 0 || other.count - start < length) {
                return false;
            }
            if (thisStart < 0 || count - thisStart < length) {
                return false;
            }
            if (length <= 0) {
                return true;
            }
            int o1 = offset + thisStart, o2 = other.offset + start;
            for (int i = 0; i < length; ++i) {
                if (this.delegateInstance[o1 + i] != other.delegateInstance[o2 + i]) {
                    return false;
                }
            }
            return true;
        }
        public bool regionMatches(bool ignoreCase, int thisStart, StringJ other, int start, int length) {
            if (!ignoreCase) {
                return this.regionMatches(thisStart, other, start, length);
            }

            if (other != null) {
                if (thisStart < 0 || length > count - thisStart) {
                    return false;
                }
                if (start < 0 || length > other.count - start) {
                    return false;
                }

                thisStart += offset;
                start += other.offset;
                int end = thisStart + length;
                char c1, c2;
                char[] target = other.delegateInstance.ToCharArray();
                while (thisStart < end) {
                    if ((c1 = other.delegateInstance[thisStart++]) != (c2 = target[start++])
                            && c1.ToString().ToUpper() != c2.ToString().ToUpper()
                            // Required for unicode that we test both cases
                            && c1.ToString().ToLower() != c2.ToString().ToLower())
                    {
                        return false;
                    }
                }
                return true;
            }
            throw new java.lang.NullPointerException();
        }
        public int hashcode()
        {
            return this.delegateInstance.hashcode();
        }
        public StringJ trim()
        {
            return new StringJ(this.delegateInstance.Trim());
        }
        /// <summary>
        /// Check string instance for matching giving regular expression.
        /// </summary>
        /// <param name="expr">regular expression</param>
        /// <returns></returns>
        public bool matches(String expr)
        {
            return this.delegateInstance.matches(expr);
        }

        /**
         * Creates a new string containing the specified characters in the character
         * array. Modifying the character array after creating the string has no
         * effect on the string.
         * 
         * @param data
         *            the array of characters.
         * @param start
         *            the starting offset in the character array.
         * @param length
         *            the number of characters to use.
         * @return the new string.
         * @throws IndexOutOfBoundsException
         *             if {@code length &lt; 0}, {@code start &lt; 0} or {@code start +
         *             length &gt; data.length}
         * @throws NullPointerException
         *             if {@code data} is {@code null}.
         */
        public static StringJ valueOf(char[] data, int start, int length)
        {
            return new StringJ(start, length, data);
        }

        /**
         * Converts the specified integer to its string representation.
         * 
         * @param value
         *            the integer.
         * @return the integer converted to a string.
         */
        public static String valueOf(int value)
        {
            return Integer.toString(value);
        }

        public void getChars(int srcBegin, int srcEnd, char[] dst, int dstBegin)
        {
            if (srcBegin < 0)
            {
                throw new StringIndexOutOfBoundsException(srcBegin);
            }
            if (srcEnd > this.delegateInstance.Length)
            {
                throw new StringIndexOutOfBoundsException(srcEnd);
            }
            if (srcBegin > srcEnd)
            {
                throw new StringIndexOutOfBoundsException(srcEnd - srcBegin);
            }
            SystemJ.arraycopy(this.delegateInstance.ToCharArray(), 0 + srcBegin, dst, dstBegin,
                 srcEnd - srcBegin);
        }

        /**
         * Converts the specified boolean to its string representation. When the
         * boolean is {@code true} return {@code "true"}, otherwise return {@code
         * "false"}.
         * 
         * @param value
         *            the boolean.
         * @return the boolean converted to a string.
         */
        public static String valueOf(bool value)
        {
            return value ? "true" : "false"; //$NON-NLS-1$ //$NON-NLS-2$
        }

        /**
         * Creates a new string containing the characters in the specified character
         * array. Modifying the character array after creating the string has no
         * effect on the string.
         * 
         * @param data
         *            the array of characters.
         * @return the new string.
         * @throws NullPointerException
         *             if {@code data} is {@code null}.
         */
        public static String valueOf(char[] data)
        {
            return new String(data, 0, data.Length);
        }

        /**
         * Converts the specified character to its string representation.
         * 
         * @param value
         *            the character.
         * @return the character converted to a string.
         */
        public static String valueOf(char value)
        {
            StringJ s;
            if (value < 128)
            {
                s = new StringJ(value, 1, ascii);
            }
            else
            {
                s = new StringJ(0, 1, new char[] { value });
            }
            //s.hashCode = value; //TODO Check way to implement this...
            return s.ToString();
        }

        /**
         * Converts the specified double to its string representation.
         * 
         * @param value
         *            the double.
         * @return the double converted to a string.
         */
        public static String valueOf(double value)
        {
            return Double.toString(value);
        }

        /**
         * Converts the specified float to its string representation.
         * 
         * @param value
         *            the float.
         * @return the float converted to a string.
         */
        public static String valueOf(float value)
        {
            return Float.toString(value);
        }

        /**
         * Converts the specified long to its string representation.
         * 
         * @param value
         *            the long.
         * @return the long converted to a string.
         */
        public static String valueOf(long value)
        {
            return Long.toString(value);
        }

        /**
         * Converts the specified object to its string representation. If the object
         * is null return the string {@code "null"}, otherwise use {@code
         * toString()} to get the string representation.
         * 
         * @param value
         *            the object.
         * @return the object converted to a string, or the string {@code "null"}.
         */
        public static String valueOf(Object value)
        {
            return value != null ? value.toString() : "null"; //$NON-NLS-1$
        }

        /*
         * Returns the character array for this string.
         */
        internal char[] getValue()
        {
            return this.delegateInstance.toCharArray();
        }

        /// <summary>
        /// Implicit convert java.lang.String to .net System.String type
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static implicit operator System.String(StringJ s)
        {
            return s.delegateInstance;
        }
        /// <summary>
        /// Implicit convert .net System.String to java.lang.String type
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static implicit operator StringJ(System.String s)
        {
            return new StringJ(s);
        }

        /// <summary>
        /// Extends System.String
        /// Encodes this {@code String} into a sequence of bytes using the
        /// platform's default charset, storing the result into a new byte array.
        /// </summary>
        /// <returns>The resultant byte array</returns>
        public byte[] getBytes()
        {
            String fileEncoding = java.lang.SystemJ.getProperty("file.encoding"); // check java specific property
            if (null == fileEncoding) // not found? Ok, use system default
            {
                fileEncoding = Encoding.Default.WebName;
            }
            return getBytes(fileEncoding);
        }
        /// <summary>
        /// Extends System.String
        /// Encodes this {@code String} into a sequence of bytes using the named
        /// charset, storing the result into a new byte array.
        /// 
        /// </summary>
        /// <exception cref="java.io.UnsupportedEncodingException">If the named charset is not supported</exception>
        /// <param name="str"></param>
        /// <param name="charsetName"></param>
        /// <returns></returns>
        public byte[] getBytes(String charsetName)
        {
            try
            {
                System.Text.Encoding enc = Encoding.GetEncoding(charsetName);
                return enc.GetBytes(this);
            }
            catch (ArgumentException toThrow)
            {
                throw new java.io.UnsupportedEncodingException(charsetName);
            }
        }

        public String[] split(String delim)
        {
            return this.delegateInstance.Split(new String[] { delim }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
