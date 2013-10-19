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
 *  Copyright © 2011 Sebastian Ritter
 */
using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.util
{
    public class Date : java.io.Serializable, java.lang.Cloneable, java.lang.Comparable<Date>
    {
        [NonSerialized]
        internal long milliseconds;

        /// <summary>
        /// Create new Date instance with given year, month, day.
        /// <seealso cref="java.util.Calendar.set"/>
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        [Obsolete]
        public Date(int year, int month, int day)
        {
            TimeSpan timeDiff = new DateTime(year,month,day) - new DateTime(1970, 1, 1);
            this.milliseconds = (long)timeDiff.TotalMilliseconds;
        }

        /// <summary>
        /// Create a new Data with given paramerts
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="dayOfMonth"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        public Date(int year, int month, int dayOfMonth, int hour, int minute, int second) 
        {
            TimeSpan timeDiff = new DateTime(year, month, dayOfMonth, hour, minute, second) - new DateTime(1970, 1, 1);
            this.milliseconds = (long)timeDiff.TotalMilliseconds;
        }

        /// <summary>
        /// The year since 1900
        /// </summary>
        /// <returns>Year since 1900</returns>
        [Obsolete]
        public virtual int getYear()
        {
            java.util.Calendar c = new java.util.GregorianCalendar();
            c.setTimeInMillis(this.milliseconds);
            return c.get(java.util.Calendar.YEAR) - 1900;
        }

        /// <summary>
        /// The month, based on zero for January
        /// </summary>
        /// <returns>Month</returns>
        [Obsolete]
        public virtual int getMonth()
        {
            java.util.Calendar c = new java.util.GregorianCalendar();
            c.setTimeInMillis(this.milliseconds);
            return c.get(java.util.Calendar.MONTH);
        }

        /// <summary>
        /// The hour
        /// </summary>
        /// <returns>Hour</returns>
        [Obsolete]
        public virtual int getHours()
        {
            java.util.Calendar c = new java.util.GregorianCalendar();
            c.setTimeInMillis(this.milliseconds);
            return c.get(java.util.Calendar.HOUR);
        }

        /// <summary>
        /// The minute
        /// </summary>
        /// <returns>Minute</returns>
        [Obsolete]
        public virtual int getMinutes()
        {
            java.util.Calendar c = new java.util.GregorianCalendar();
            c.setTimeInMillis(this.milliseconds);
            return c.get(java.util.Calendar.MINUTE);
        }

        /// <summary>
        /// The second
        /// </summary>
        /// <returns>Second</returns>
        [Obsolete]
        public virtual int getSeconds()
        {
            java.util.Calendar c = new java.util.GregorianCalendar();
            c.setTimeInMillis(this.milliseconds);
            return c.get(java.util.Calendar.SECOND);
        }

        /// <summary>
        /// The day of month.
        /// </summary>
        /// <returns>Day of month</returns>
        [Obsolete]
        public virtual int getDate()
        {
            java.util.Calendar c = new java.util.GregorianCalendar();
            c.setTimeInMillis(this.milliseconds);
            return c.get(java.util.Calendar.DAY_OF_MONTH);
        }

        public Date()
        {
            this.milliseconds = java.lang.SystemJ.currentTimeMillis();
        }
        public Date (long timeInMillisSince_1970_01_01) {
            this.milliseconds = timeInMillisSince_1970_01_01;
        }
        public virtual bool after (Date date) {
            return this.milliseconds > date.milliseconds;
        }
        public virtual bool before(Date date)
        {
            return this.milliseconds < date.milliseconds;
        }
        public virtual int compareTo(Date date)
        {
            if (this.after(date)) return 1;
            if (this.before(date)) return -1;
            return 0;
        }
        public override bool Equals(object obj)
        {
            return (this == obj) || ((obj is Date) && (this.milliseconds == ((Date)obj).milliseconds));
        }
        public virtual long getTime()
        {
            return this.milliseconds;
        }
        public virtual void setTime(long timeInMillisSince_1970_01_01)
        {
            this.milliseconds = timeInMillisSince_1970_01_01;
        }
        private String toTwoDigits(int digit)
        {
            if (10 > digit)
            {
                return "0" + digit;
            }
            else {
                return ""+digit;
            }
        }
        private static int zone(String text)
        {
            if (text.equals("EST"))
            { 
                return -5;
            }
            if (text.equals("EDT"))
            { 
                return -4;
            }
            if (text.equals("CST"))
            { 
                return -6;
            }
            if (text.equals("CDT"))
            { 
                return -5;
            }
            if (text.equals("MST"))
            { 
                return -7;
            }
            if (text.equals("MDT"))
            { 
                return -6;
            }
            if (text.equals("PST"))
            { 
                return -8;
            }
            if (text.equals("PDT"))
            { 
                return -7;
            }
            return 0;
        }

        public Object clone()
        {
            Date clone = new Date(this.milliseconds);
            return clone;
        }

        public override int GetHashCode()
        {
            return (int)this.milliseconds;
        }
    }
}
