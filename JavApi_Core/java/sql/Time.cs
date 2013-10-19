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

namespace biz.ritter.javapi.sql
{
    /**
     * Java representation of an SQL {@code TIME} value. Provides utilities to 
     * format and parse the time's representation as a String in JDBC escape format.
     */
    [Serializable]
    public class Time : java.util.Date
    {

        private const long serialVersionUID = 8397324403548013681L;

        /**
         * Constructs a {@code Time} object using the supplied values for <i>Hour</i>,
         * <i>Minute</i> and <i>Second</i>. The <i>Year</i>, <i>Month</i> and
         * <i>Day</i> elements of the {@code Time} object are set to the date
         * of the Epoch (January 1, 1970).
         * <p/>
         * Any attempt to access the <i>Year</i>, <i>Month</i> or <i>Day</i>
         * elements of a {@code Time} object will result in an {@code
         * IllegalArgumentException}.
         * <p/>
         * The result is undefined if any argument is out of bounds.
         *
         * @deprecated Use the constructor {@link #Time(long)}.
         * @param theHour
         *            a value in the range {@code [0,23]}.
         * @param theMinute
         *            a value in the range {@code [0,59]}.
         * @param theSecond
         *            a value in the range {@code [0,59]}.
         */
        [Obsolete]
        public Time(int theHour, int theMinute, int theSecond) :
            base(70, 0, 1, theHour, theMinute, theSecond)
        {
        }

        /**
         * Constructs a {@code Time} object using a supplied time specified in
         * milliseconds.
         * 
         * @param theTime
         *            a {@code Time} specified in milliseconds since the
         *            <i>Epoch</i> (January 1st 1970, 00:00:00.000).
         */
        public Time(long theTime) :
            base(theTime)
        {
        }

        /**
         * @deprecated This method is deprecated and must not be used. An SQL
         *             {@code Time} object does not have a {@code Date} component.
         * @return does not return anything.
         * @throws IllegalArgumentException
         *             if this method is called.
         */
        [Obsolete]
        public override int getDate()
        {
            throw new java.lang.IllegalArgumentException();
        }

        /**
         * @deprecated This method is deprecated and must not be used. An SQL
         *             {@code Time} object does not have a <i>Day</i> component.
         * @return does not return anything.
         * @throws IllegalArgumentException
         *             if this method is called.
         */
        [Obsolete]
        public int getDay()
        {
            throw new java.lang.IllegalArgumentException();
        }

        /**
         * @deprecated This method is deprecated and must not be used. An SQL
         *             {@code Time} object does not have a <i>Month</i> component.
         * @return does not return anything.
         * @throws IllegalArgumentException
         *             if this method is called.
         */
        [Obsolete]
        public override int getMonth()
        {
            throw new java.lang.IllegalArgumentException();
        }

        /**
         * @deprecated This method is deprecated and must not be used. An SQL
         *             {@code Time} object does not have a <i>Year</i> component.
         * @return does not return anything.
         * @throws IllegalArgumentException
         *             if this method is called.
         */
        [Obsolete]
        public override int getYear()
        {
            throw new java.lang.IllegalArgumentException();
        }

        /**
         * @deprecated This method is deprecated and must not be used. An SQL
         *             {@code Time} object does not have a {@code Date} component.
         * @throws IllegalArgumentException
         *             if this method is called.
         */
        [Obsolete]
        public void setDate(int i)
        {
            throw new java.lang.IllegalArgumentException();
        }

        /**
         * @deprecated This method is deprecated and must not be used. An SQL
         *             {@code Time} object does not have a <i>Month</i> component.
         * @throws IllegalArgumentException
         *             if this method is called.
         */
        [Obsolete]
        public void setMonth(int i)
        {
            throw new java.lang.IllegalArgumentException();
        }

        /**
         * @deprecated This method is deprecated and must not be used. An SQL
         *             {@code Time} object does not have a <i>Year</i> component.
         * @throws IllegalArgumentException
         *             if this method is called.
         */
        [Obsolete]
        public void setYear(int i)
        {
            throw new java.lang.IllegalArgumentException();
        }

        /**
         * Sets the time for this {@code Time} object to the supplied milliseconds
         * value.
         * 
         * @param time
         *            A time value expressed as milliseconds since the <i>Epoch</i>.
         *            Negative values are milliseconds before the Epoch. The Epoch
         *            is January 1 1970, 00:00:00.000.
         */
        public override void setTime(long time)
        {
            base.setTime(time);
        }

        /**
         * Formats the {@code Time} as a String in JDBC escape format: {@code
         * hh:mm:ss}.
         * 
         * @return A String representing the {@code Time} value in JDBC escape
         *         format: {@code HH:mm:ss}
         */
        public override String ToString()
        {
            java.lang.StringBuilder sb = new java.lang.StringBuilder(8);

            format(getHours(), 2, sb);
            sb.append(':');
            format(getMinutes(), 2, sb);
            sb.append(':');
            format(getSeconds(), 2, sb);

            return sb.toString();
        }

        private const String PADDING = "00";  //$NON-NLS-1$

        /* 
        * Private method to format the time 
        */
        private void format(int date, int digits, java.lang.StringBuilder sb)
        {
            String str = java.lang.StringJ.valueOf(date);
            if (digits - str.length() > 0)
            {
                sb.append(PADDING.substring(0, digits - str.length()));
            }
            sb.append(str);
        }

        /**
         * Creates a {@code Time} object from a string holding a time represented in
         * JDBC escape format: {@code hh:mm:ss}.
         * <p/>
         * An exception occurs if the input string does not comply with this format.
         *
         * @param timeString
         *            A String representing the time value in JDBC escape format:
         *            {@code hh:mm:ss}.
         * @return The {@code Time} object set to a time corresponding to the given
         *         time.
         * @throws IllegalArgumentException
         *             if the supplied time string is not in JDBC escape format.
         */
        public static Time valueOf(String timeString)
        {
            if (timeString == null)
            {
                throw new java.lang.IllegalArgumentException();
            }
            int firstIndex = timeString.indexOf(':');
            int secondIndex = timeString.indexOf(':', firstIndex + 1);
            // secondIndex == -1 means none or only one separator '-' has been
            // found.
            // The string is separated into three parts by two separator characters,
            // if the first or the third part is null string, we should throw
            // IllegalArgumentException to follow RI
            if (secondIndex == -1 || firstIndex == 0
                    || secondIndex + 1 == timeString.length())
            {
                throw new java.lang.IllegalArgumentException();
            }
            // parse each part of the string
            int hour = java.lang.Integer.parseInt(timeString.substring(0, firstIndex));
            int minute = java.lang.Integer.parseInt(timeString.substring(firstIndex + 1,
                    secondIndex));
            int second = java.lang.Integer.parseInt(timeString.substring(secondIndex + 1,
                    timeString.length()));
            return new Time(hour, minute, second);
        }
    }
}