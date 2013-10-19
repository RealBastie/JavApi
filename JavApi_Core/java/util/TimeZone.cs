/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using java = biz.ritter.javapi;
using javax = biz.ritter.javapix;
 
namespace biz.ritter.javapi.util
{

    /**
     * {@code TimeZone} represents a time zone offset, taking into account
     * daylight savings.
     * <p/>
     * Typically, you get a {@code TimeZone} using {@code getDefault}
     * which creates a {@code TimeZone} based on the time zone where the
     * program is running. For example, for a program running in Japan,
     * {@code getDefault} creates a {@code TimeZone} object based on
     * Japanese Standard Time.
     * <p/>
     * You can also get a {@code TimeZone} using {@code getTimeZone}
     * along with a time zone ID. For instance, the time zone ID for the U.S.
     * Pacific Time zone is "America/Los_Angeles". So, you can get a U.S. Pacific
     * Time {@code TimeZone} object with the following: <blockquote>
     *
     * <pre>
     * TimeZone tz = TimeZone.getTimeZone(&quot;America/Los_Angeles&quot;);
     * </pre>
     *
     * </blockquote> You can use the {@code getAvailableIDs} method to iterate
     * through all the supported time zone IDs. You can then choose a supported ID
     * to get a {@code TimeZone}. If the time zone you want is not
     * represented by one of the supported IDs, then you can create a custom time
     * zone ID with the following syntax: <blockquote>
     *
     * <pre>
     * GMT[+|-]hh[[:]mm]
     * </pre>
     *
     * </blockquote> For example, you might specify GMT+14:00 as a custom time zone
     * ID. The {@code TimeZone} that is returned when you specify a custom
     * time zone ID does not include daylight savings time.
     * <p/>
     * For compatibility with JDK 1.1.x, some other three-letter time zone IDs (such
     * as "PST", "CTT", "AST") are also supported. However, <strong>their use is
     * deprecated</strong> because the same abbreviation is often used for multiple
     * time zones (for example, "CST" could be U.S. "Central Standard Time" and
     * "China Standard Time"), and the Java platform can then only recognize one of
     * them.
     * <p/>
     * Please note the type returned by factory methods, i.e. {@code getDefault()}
     * and {@code getTimeZone(String)}, is implementation dependent, so it may
     * introduce serialization incompatibility issues between different
     * implementations.
     *
     * @see GregorianCalendar
     * @see SimpleTimeZone
     */
    [Serializable]
    public abstract class TimeZone : java.io.Serializable, java.lang.Cloneable
    {
        private const long serialVersionUID = 3581463369166924961L;

        /**
         * The SHORT display name style.
         */
        public const int SHORT = 0;

        /**
         * The LONG display name style.
         */
        public const int LONG = 1;

        private static HashMap<String, TimeZone> AvailableZones;

        private static TimeZone Default;

        static TimeZone GMT = new SimpleTimeZone(0, "GMT"); // Greenwich Mean Time

        private String ID;

        private static void initializeAvailable()
        {
            TimeZone[] zones = TimeZones.getTimeZones();
            AvailableZones = new HashMap<String, TimeZone>(
                    (zones.Length + 1) * 4 / 3);
            AvailableZones.put(GMT.getID(), GMT);
            for (int i = 0; i < zones.Length; i++)
            {
                AvailableZones.put(zones[i].getID(), zones[i]);
            }
        }

        /**
         * Constructs a new instance of this class.
         */
        public TimeZone()
        {
        }

        /**
         * Returns a new {@code TimeZone} with the same ID, {@code rawOffset} and daylight savings
         * time rules as this {@code TimeZone}.
         *
         * @return a shallow copy of this {@code TimeZone}.
         * @see java.lang.Cloneable
         */
        public virtual Object clone()
        {
            try
            {
                TimeZone zone = (TimeZone)base.MemberwiseClone();
                return zone;
            }
            catch (java.lang.CloneNotSupportedException e)
            {
                return null;
            }
        }

        /**
         * Gets the ID of this {@code TimeZone}.
         * 
         * @return the time zone ID string.
         */
        public String getID()
        {
            return ID;
        }

        /**
         * Gets the daylight savings offset in milliseconds for this {@code TimeZone}.
         * <p/>
         * This implementation returns 3600000 (1 hour), or 0 if the time zone does
         * not observe daylight savings.
         * <p/>
         * Subclasses may override to return daylight savings values other than 1
         * hour.
         * <p/>
         * 
         * @return the daylight savings offset in milliseconds if this {@code TimeZone}
         *         observes daylight savings, zero otherwise.
         */
        public virtual int getDSTSavings()
        {
            if (useDaylightTime())
            {
                return 3600000;
            }
            return 0;
        }

        /**
         * Gets the offset from GMT of this {@code TimeZone} for the specified date. The
         * offset includes daylight savings time if the specified date is within the
         * daylight savings time period.
         * 
         * @param time
         *            the date in milliseconds since January 1, 1970 00:00:00 GMT
         * @return the offset from GMT in milliseconds.
         */
        public int getOffset(long time)
        {
            if (inDaylightTime(new Date(time)))
            {
                return getRawOffset() + getDSTSavings();
            }
            return getRawOffset();
        }

        /**
         * Gets the offset from GMT of this {@code TimeZone} for the specified date and
         * time. The offset includes daylight savings time if the specified date and
         * time are within the daylight savings time period.
         * 
         * @param era
         *            the {@code GregorianCalendar} era, either {@code GregorianCalendar.BC} or
         *            {@code GregorianCalendar.AD}.
         * @param year
         *            the year.
         * @param month
         *            the {@code Calendar} month.
         * @param day
         *            the day of the month.
         * @param dayOfWeek
         *            the {@code Calendar} day of the week.
         * @param time
         *            the time of day in milliseconds.
         * @return the offset from GMT in milliseconds.
         */
        abstract public int getOffset(int era, int year, int month, int day,
                int dayOfWeek, int time);

        /**
         * Gets the offset for standard time from GMT for this {@code TimeZone}.
         * 
         * @return the offset from GMT in milliseconds.
         */
        abstract public int getRawOffset();

        private static String formatTimeZoneName(String name, int offset)
        {
            java.lang.StringBuilder buf = new java.lang.StringBuilder();
            int index = offset, length = name.length();
            buf.append(name.substring(0, offset));

            while (index < length)
            {
                if (java.lang.Character.digit(name.charAt(index), 10) != -1)
                {
                    buf.append(name.charAt(index));
                    if ((length - (index + 1)) == 2)
                    {
                        buf.append(':');
                    }
                }
                else if (name.charAt(index) == ':')
                {
                    buf.append(':');
                }
                index++;
            }

            if (buf.toString().indexOf(":") == -1)
            {
                buf.append(':');
                buf.append("00");
            }

            if (buf.toString().indexOf(":") == 5)
            {
                buf.insert(4, '0');
            }

            return buf.toString();
        }

        /**
         * Returns whether the specified {@code TimeZone} has the same raw offset as this
         * {@code TimeZone}.
         * 
         * @param zone
         *            a {@code TimeZone}.
         * @return {@code true} when the {@code TimeZone} have the same raw offset, {@code false}
         *         otherwise.
         */
        public virtual bool hasSameRules(TimeZone zone)
        {
            if (zone == null)
            {
                return false;
            }
            return getRawOffset() == zone.getRawOffset();
        }

        /**
         * Returns whether the specified {@code Date} is in the daylight savings time period for
         * this {@code TimeZone}.
         * 
         * @param time
         *            a {@code Date}.
         * @return {@code true} when the {@code Date} is in the daylight savings time period, {@code false}
         *         otherwise.
         */
        abstract public bool inDaylightTime(Date time);

        private static int parseNumber(String s, int offset, int[] position)
        {
            int index = offset, length = s.length(), digit, result = 0;
            while (index < length
                    && (digit = java.lang.Character.digit(s.charAt(index), 10)) != -1)
            {
                index++;
                result = result * 10 + digit;
            }
            position[0] = index == offset ? -1 : index;
            return result;
        }

        /**
         * Sets the ID of this {@code TimeZone}.
         * 
         * @param name
         *            a string which is the time zone ID.
         */
        public void setID(String name)
        {
            if (name == null)
            {
                throw new java.lang.NullPointerException();
            }
            ID = name;
        }

        /**
         * Sets the offset for standard time from GMT for this {@code TimeZone}.
         * 
         * @param offset
         *            the offset from GMT in milliseconds.
         */
        abstract public void setRawOffset(int offset);

        /**
         * Returns whether this {@code TimeZone} has a daylight savings time period.
         * 
         * @return {@code true} if this {@code TimeZone} has a daylight savings time period, {@code false}
         *         otherwise.
         */
        abstract public bool useDaylightTime();

    }
}