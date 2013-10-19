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
 *  Copyright © 2011,2012 Sebastian Ritter
 */
using System;
using System.Collections.Generic;
using System.Text;

using java = biz.ritter.javapi;

//namespace biz.ritter.javapi.dotnet
namespace System
{
    public static class JavaExtensions
    {
        #region java.lang.String

        public static String[] split(this String str, String delim)
        {
            return new java.lang.StringJ(str).split(delim);
        }

        public static String valueOf(this String str, int value)
        {
            return java.lang.StringJ.valueOf(value);
        }
        public static String intern(this String str)
        {
            return String.Intern(str);
        }

        public static int compareTo(this String str, String other)
        {
            return str.CompareTo(other);
        }

        /*
         * Returns the character array for this string.
         */
        internal static char[] getValue(this String str)
        {
            return str.toCharArray();
        }
        /// <summary>
        /// Check string instance for matching giving regular expression.
        /// </summary>
        /// <param name="str">instance</param>
        /// <param name="expr">regular expression</param>
        /// <returns></returns>
        public static bool matches(this String str, String expr)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str, expr);
        }

        public static bool endsWith(this String str, String last)
        {
            return str.EndsWith(last);
        }
        public static String concat(this String str, String add)
        {
            return String.Concat(str, add);
        }
        /// <summary>
        /// Helper method to create a Java like String wrapper object instance. You can use this wrapper if
        /// implemented interface like java.lang.Appendable is needed.
        /// </summary>
        /// <param name="str">base .net String instance or null</param>
        /// <returns>Wrapper for given String instance</returns>
        public static java.lang.StringJ getWrapperInstance(this String str)
        {
            return new java.lang.StringJ(str);
        }
        public static char[] toCharArray(this String str)
        {
            return str.ToCharArray();
        }
        public static int lastIndexOf(this String str, String s)
        {
            return str.LastIndexOf(s);
        }
        /// <summary>
        /// Identify the last index of char - Java like.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int lastIndexOf(this String str, char c, int startIndex)
        {
            if (0 > startIndex) return -1;
            if (startIndex >= str.Length) startIndex = str.Length - 1;
            return str.LastIndexOf(c,startIndex);
        }
        public static int lastIndexOf(this String str, char c)
        {
            return str.LastIndexOf(c);
        }
        public static int length(this String str)
        {
            return str.Length;
        }
        public static String substring(this String str, int start)
        {
            return str.Substring(start);
        }
        public static String trim(this String str)
        {
            return str.Trim();
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
        public static String valueOf(this String str, bool value)
        {
            return value ? "true" : "false"; //$NON-NLS-1$ //$NON-NLS-2$
        }

        /// <summary>
        /// Extends System.String
        /// Encodes this {@code String} into a sequence of bytes using the
        /// platform's default charset, storing the result into a new byte array.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>The resultant byte array</returns>
        public static byte[] getBytes(this String str)
        {
            return new java.lang.StringJ(str).getBytes();
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
        public static byte[] getBytes(this String str, String charsetName)
        {
            return new java.lang.StringJ(str).getBytes(charsetName);
        }
        public static char charAt(this String str, int index)
        {
            return str[index];
        }

        public static void getChars(this String str, int srcBegin, int srcEnd, char[] dst, int dstBegin) {
            if (srcBegin < 0) {
                throw new java.lang.StringIndexOutOfBoundsException(srcBegin);
            }
            if (srcEnd > str.Length) {
                throw new java.lang.StringIndexOutOfBoundsException(srcEnd);
            }
            if (srcBegin > srcEnd) {
                throw new java.lang.StringIndexOutOfBoundsException(srcEnd - srcBegin);
            }
            java.lang.SystemJ.arraycopy(str.ToCharArray(), 0 + srcBegin, dst, dstBegin,
                 srcEnd - srcBegin);
        }

        public static String substring (this String str, int start, int end) {
            return str.Substring(start, end - start);
        }
        public static bool startsWith(this String str, String pre)
        {
            return str.StartsWith(pre);
        }
        public static bool startsWith(this String str, String pre, int startOffset)
        {
            return str.Substring(startOffset).StartsWith(pre);
        }
        public static int indexOf(this String str, char c)
        {
            return str.IndexOf(c);
        }
        public static int indexOf(this String str, String s)
        {
            return str.IndexOf(s);
        }
        public static int indexOf(this String str, String s, int startOffset)
        {
            return str.IndexOf(s, startOffset);
        }
        public static int indexOf(this String str, char c, int start)
        {
            return str.IndexOf(c,start);
        }
        public static String replace(this String str, char oldChar, char newChar)
        {
            return str.Replace(oldChar, newChar);
        }
        public static String replaceAll(this String str, String oldString, String newString)
        {
            return str.Replace(oldString, newString);
        }
        public static bool equalsIgnoreCase(this String str, String otherString)
        {
            return str.Equals(otherString, StringComparison.CurrentCultureIgnoreCase);
        }
        public static String toLowerCase(this String str, java.util.Locale locale)
        {
            return str.ToLower(locale.delegateInstance);
        }
        public static String toLowerCase(this String str)
        {
            return str.ToLower();
        }
        public static String toUpperCase(this String str)
        {
            return str.ToUpper();
        }
        public static String toUpperCase(this String str, java.util.Locale locale)
        {
            return str.ToUpper(locale.delegateInstance);
        }
        public static bool isEmpty(this String str)
        {
            return 0 == str.Length;
        }
        #endregion
        #region java.lang.StringBuilder
        public static StringBuilder deleteCharAt(this StringBuilder sb, int index)
        {
            return sb.Remove(index, 0);
        }
        public static StringBuilder append(this StringBuilder sb, String s)
        {
            return sb.Append(s);
        }
        public static StringBuilder append(this StringBuilder sb, double d)
        {
            return sb.Append(d);
        }
        public static StringBuilder append(this StringBuilder sb, char c)
        {
            return sb.Append(c);
        }
        public static StringBuilder append(this StringBuilder sb, int i)
        {
            return sb.Append(i);
        }
        public static StringBuilder append(this StringBuilder sb, Object o)
        {
            return sb.Append(o);
        }
        public static StringBuilder Insert(this StringBuilder sb, int offset, String s)
        {
            return sb.Insert(offset,s);
        }
        #endregion
        #region System.Exception
        public static String getMessage(this Exception e)
        {
            return e.Message;
        }
        #endregion
        #region java.lang.Object
        public static void wait(this Object o, long milliseconds)
        {
            try
            {
                TimeSpan ts = new TimeSpan(TimeSpan.TicksPerMillisecond * milliseconds);
                System.Threading.Monitor.Wait(o, ts);
            }
            catch (System.Threading.SynchronizationLockException sle)
            {
                throw new java.lang.IllegalMonitorStateException(sle.Message);
            }
            catch (System.Threading.ThreadInterruptedException tie)
            {
                throw new java.lang.InterruptedException(tie.Message);
            }
        }
        public static void finalize(this Object o)
        {
            
        }
        public static void wait(this Object o)
        {
            try
            {
                System.Threading.Monitor.Wait(o);
            }
            catch (System.Threading.SynchronizationLockException sle)
            {
                throw new java.lang.IllegalMonitorStateException(sle.Message);
            }
            catch (System.Threading.ThreadInterruptedException tie)
            {
                throw new java.lang.InterruptedException(tie.Message);
            }
        }
        public static void notify(this Object o)
        {
            try
            {
                System.Threading.Monitor.Pulse(o);
            }
            catch (System.Threading.SynchronizationLockException sle)
            {
                throw new java.lang.IllegalMonitorStateException(sle.Message);
            }
        }
        public static void notifyAll(this Object o)
        {
            try
            {
                System.Threading.Monitor.PulseAll(o);
            }
            catch (System.Threading.SynchronizationLockException sle)
            {
                throw new java.lang.IllegalMonitorStateException(sle.Message);
            }
        }
        public static String toString(this Object o)
        {
            if (null == o) return "null";
            return o.ToString();
        }
        public static int hashcode(this Object o)
        {
            return o.GetHashCode();
        }
        public static bool equals(this Object o, Object obj)
        {
            if (o != null)
            {
                return o.Equals(obj);
            }
            else if (obj == null)
            {
                return true;
            }
            return false;

        }

        private static java.util.HashMap<Type, java.lang.Class> loadedClasses = new java.util.HashMap<Type, java.lang.Class>();
        public static java.lang.Class getClass(this System.Object t)
        {
            Type runtimeType = t.GetType();
            Reflection.PropertyInfo propInfo = runtimeType.GetProperty("UnderlyingSystemType");
            Type type = null == propInfo ? runtimeType : (Type)propInfo.GetValue(t, null);

            java.lang.Class clazz = null;
            if (loadedClasses.containsKey(type))
            {
                clazz = loadedClasses.get(type);
            }
            else
            {
                clazz = new java.lang.Class(type);
                loadedClasses.put(type, clazz);
            }

            return clazz;
        }
        public static System.Object clone(this System.Object t)
        {
            throw new java.lang.CloneNotSupportedException();
        }
        /// <summary>
        /// All Java classes can get resources over method java.net.URL java.lang.Class.getResource();
        /// </summary>
        /// <param name="t">an instance</param>
        /// <param name="name">named resource</param>
        /// <returns></returns>
        public static java.net.URL getResource (this System.Object t, String name) {
            return t.getClass().getResource(name);
        }
        #endregion
        #region java.util.Calendar
        /// <summary>
        /// Convert enumeration value to int valie based on 
        /// </summary>
        /// <param name="dayOfWeek">enum value of System.DayOfWeek</param>
        /// <returns>Sunday == 1, Monday == 2, ...</returns>
        /// <see cref="System.DayOfWeek"/>
        internal static int toInt (this DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday : return 2;
                case DayOfWeek.Tuesday: return 3;
                case DayOfWeek.Wednesday: return 4;
                case DayOfWeek.Thursday: return 5;
                case DayOfWeek.Friday: return 6;
                case DayOfWeek.Saturday: return 7;
                case DayOfWeek.Sunday: return 1;
                default: throw new java.lang.IllegalArgumentException();
            }
        }
        #endregion

        #region java.util.Date
        /// <summary>
        /// Returns milliseconds since 01.01.1970
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long getTime(this DateTime s)
        {
            TimeSpan timeDiff = s - new DateTime(1970, 1, 1);
            long milliseconds = (long)timeDiff.TotalMilliseconds;
            return milliseconds;
        }

        #endregion
    }
}
