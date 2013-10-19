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
     * {@code SimpleTimeZone} is a concrete subclass of {@code TimeZone}
     * that represents a time zone for use with a Gregorian calendar. This class
     * does not handle historical changes.
     * <p>
     * Use a negative value for {@code dayOfWeekInMonth} to indicate that
     * {@code SimpleTimeZone} should count from the end of the month
     * backwards. For example, Daylight Savings Time ends at the last
     * (dayOfWeekInMonth = -1) Sunday in October, at 2 AM in standard time.
     *
     * @see Calendar
     * @see GregorianCalendar
     * @see TimeZone
     */
    [Serializable]
    public class SimpleTimeZone : TimeZone
    {

        private const long serialVersionUID = -403250971215465050L;

        private int rawOffset;

        private int startYear, startMonth, startDay, startDayOfWeek, startTime;

        private int endMonth, endDay, endDayOfWeek, endTime;

        private int startMode, endMode;

        private const int DOM_MODE = 1, DOW_IN_MONTH_MODE = 2,
                DOW_GE_DOM_MODE = 3, DOW_LE_DOM_MODE = 4;

        /**
         * The constant for representing a start or end time in GMT time mode.
         */
        public const int UTC_TIME = 2;

        /**
         * The constant for representing a start or end time in standard local time mode,
         * based on timezone's raw offset from GMT; does not include Daylight
         * savings.
         */
        public const int STANDARD_TIME = 1;

        /**
         * The constant for representing a start or end time in local wall clock time
         * mode, based on timezone's adjusted offset from GMT; includes
         * Daylight savings.
         */
        public const int WALL_TIME = 0;

        private bool useDaylight;

        private GregorianCalendar daylightSavings;

        private int dstSavings = 3600000;

        [NonSerialized]
        private readonly bool isSimple;


        /**
         * Returns a new {@code SimpleTimeZone} with the same ID, {@code rawOffset} and daylight
         * savings time rules as this SimpleTimeZone.
         * 
         * @return a shallow copy of this {@code SimpleTimeZone}.
         * @see java.lang.Cloneable
         */
        public override Object clone()
        {
            SimpleTimeZone zone = (SimpleTimeZone)base.MemberwiseClone();
            if (daylightSavings != null)
            {
                zone.daylightSavings = (GregorianCalendar)daylightSavings.clone();
            }
            return zone;
        }

        /**
         * Compares the specified object to this {@code SimpleTimeZone} and returns whether they
         * are equal. The object must be an instance of {@code SimpleTimeZone} and have the
         * same internal data.
         * 
         * @param object
         *            the object to compare with this object.
         * @return {@code true} if the specified object is equal to this
         *         {@code SimpleTimeZone}, {@code false} otherwise.
         * @see #hashCode
         */
        public override bool Equals(Object obj)
        {
            if (!(obj is SimpleTimeZone))
            {
                return false;
            }
            SimpleTimeZone tz = (SimpleTimeZone)obj;
            return getID().equals(tz.getID())
                    && rawOffset == tz.rawOffset
                    && useDaylight == tz.useDaylight
                    && (!useDaylight || (startYear == tz.startYear
                            && startMonth == tz.startMonth
                            && startDay == tz.startDay && startMode == tz.startMode
                            && startDayOfWeek == tz.startDayOfWeek
                            && startTime == tz.startTime && endMonth == tz.endMonth
                            && endDay == tz.endDay
                            && endDayOfWeek == tz.endDayOfWeek
                            && endTime == tz.endTime && endMode == tz.endMode && dstSavings == tz.dstSavings));
        }

        public override int getDSTSavings()
        {
            if (!useDaylight)
            {
                return 0;
            }
            return dstSavings;
        }

        public override int getRawOffset()
        {
            return rawOffset;
        }

        /**
         * Returns an integer hash code for the receiver. Objects which are equal
         * return the same value for this method.
         * 
         * @return the receiver's hash.
         * @see #equals
         */
        public override int GetHashCode()
        {
            lock (this)
            {
                int hashCode = getID().GetHashCode() + rawOffset;
                if (useDaylight)
                {
                    hashCode += startYear + startMonth + startDay + startDayOfWeek
                            + startTime + startMode + endMonth + endDay + endDayOfWeek
                            + endTime + endMode + dstSavings;
                }
                return hashCode;
            }
        }

        public override bool hasSameRules(TimeZone zone)
        {
            if (!(zone is SimpleTimeZone))
            {
                return false;
            }
            SimpleTimeZone tz = (SimpleTimeZone)zone;
            if (useDaylight != tz.useDaylight)
            {
                return false;
            }
            if (!useDaylight)
            {
                return rawOffset == tz.rawOffset;
            }
            return rawOffset == tz.rawOffset && dstSavings == tz.dstSavings
                    && startYear == tz.startYear && startMonth == tz.startMonth
                    && startDay == tz.startDay && startMode == tz.startMode
                    && startDayOfWeek == tz.startDayOfWeek
                    && startTime == tz.startTime && endMonth == tz.endMonth
                    && endDay == tz.endDay && endDayOfWeek == tz.endDayOfWeek
                    && endTime == tz.endTime && endMode == tz.endMode;
        }

        private bool isLeapYear(int year)
        {
            if (year > 1582)
            {
                return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
            }
            return year % 4 == 0;
        }

        /**
         * Sets the daylight savings offset in milliseconds for this {@code SimpleTimeZone}.
         * 
         * @param milliseconds
         *            the daylight savings offset in milliseconds.
         */
        public void setDSTSavings(int milliseconds)
        {
            if (milliseconds > 0)
            {
                dstSavings = milliseconds;
            }
            else
            {
                throw new java.lang.IllegalArgumentException();
            }
        }

        private void checkRange(int month, int dayOfWeek, int time)
        {
            if (month < Calendar.JANUARY || month > Calendar.DECEMBER)
            {
                throw new java.lang.IllegalArgumentException("month: " + month); //$NON-NLS-1$
            }
            if (dayOfWeek < Calendar.SUNDAY || dayOfWeek > Calendar.SATURDAY)
            {
                throw new java.lang.IllegalArgumentException("day of week:" + dayOfWeek); //$NON-NLS-1$
            }
            if (time < 0 || time >= 24 * 3600000)
            {
                throw new java.lang.IllegalArgumentException("time: " + time); //$NON-NLS-1$
            }
        }

        private void checkDay(int month, int day)
        {
            if (day <= 0 || day > GregorianCalendar.DaysInMonth[month])
            {
                throw new java.lang.IllegalArgumentException("day of month:" + day); //$NON-NLS-1$
            }
        }

        private void setEndMode()
        {
            if (endDayOfWeek == 0)
            {
                endMode = DOM_MODE;
            }
            else if (endDayOfWeek < 0)
            {
                endDayOfWeek = -endDayOfWeek;
                if (endDay < 0)
                {
                    endDay = -endDay;
                    endMode = DOW_LE_DOM_MODE;
                }
                else
                {
                    endMode = DOW_GE_DOM_MODE;
                }
            }
            else
            {
                endMode = DOW_IN_MONTH_MODE;
            }
            useDaylight = startDay != 0 && endDay != 0;
            if (endDay != 0)
            {
                checkRange(endMonth, endMode == DOM_MODE ? 1 : endDayOfWeek,
                        endTime);
                if (endMode != DOW_IN_MONTH_MODE)
                {
                    checkDay(endMonth, endDay);
                }
                else
                {
                    if (endDay < -5 || endDay > 5)
                    {
                        throw new java.lang.IllegalArgumentException("day of week in month: " + endDay); //$NON-NLS-1$
                    }
                }
            }
            if (endMode != DOM_MODE)
            {
                endDayOfWeek--;
            }
        }


        private void setStartMode()
        {
            if (startDayOfWeek == 0)
            {
                startMode = DOM_MODE;
            }
            else if (startDayOfWeek < 0)
            {
                startDayOfWeek = -startDayOfWeek;
                if (startDay < 0)
                {
                    startDay = -startDay;
                    startMode = DOW_LE_DOM_MODE;
                }
                else
                {
                    startMode = DOW_GE_DOM_MODE;
                }
            }
            else
            {
                startMode = DOW_IN_MONTH_MODE;
            }
            useDaylight = startDay != 0 && endDay != 0;
            if (startDay != 0)
            {
                checkRange(startMonth, startMode == DOM_MODE ? 1 : startDayOfWeek,
                        startTime);
                if (startMode != DOW_IN_MONTH_MODE)
                {
                    checkDay(startMonth, startDay);
                }
                else
                {
                    if (startDay < -5 || startDay > 5)
                    {
                        throw new java.lang.IllegalArgumentException("day of week in month: " + startDay); //$NON-NLS-1$
                    }
                }
            }
            if (startMode != DOM_MODE)
            {
                startDayOfWeek--;
            }
        }

        /**
         * Sets the starting year for daylight savings time in this {@code SimpleTimeZone}.
         * Years before this start year will always be in standard time.
         * 
         * @param year
         *            the starting year.
         */
        public void setStartYear(int year)
        {
            startYear = year;
            useDaylight = true;
        }

        /**
         * Returns the string representation of this {@code SimpleTimeZone}.
         * 
         * @return the string representation of this {@code SimpleTimeZone}.
         */

        public override String ToString()
        {
            return this.getClass().getName()
                    + "[id=" //$NON-NLS-1$
                    + getID()
                    + ",offset=" //$NON-NLS-1$
                    + rawOffset
                    + ",dstSavings=" //$NON-NLS-1$
                    + dstSavings
                    + ",useDaylight=" //$NON-NLS-1$
                    + useDaylight
                    + ",startYear=" //$NON-NLS-1$
                    + startYear
                    + ",startMode=" //$NON-NLS-1$
                    + startMode
                    + ",startMonth=" //$NON-NLS-1$
                    + startMonth
                    + ",startDay=" //$NON-NLS-1$
                    + startDay
                    + ",startDayOfWeek=" //$NON-NLS-1$
                    + (useDaylight && (startMode != DOM_MODE) ? startDayOfWeek + 1
                            : 0) + ",startTime=" + startTime + ",endMode=" //$NON-NLS-1$ //$NON-NLS-2$
                    + endMode + ",endMonth=" + endMonth + ",endDay=" + endDay //$NON-NLS-1$ //$NON-NLS-2$
                    + ",endDayOfWeek=" //$NON-NLS-1$
                    + (useDaylight && (endMode != DOM_MODE) ? endDayOfWeek + 1 : 0)
                    + ",endTime=" + endTime + "]"; //$NON-NLS-1$//$NON-NLS-2$
        }


        public override bool useDaylightTime()
        {
            return useDaylight;
        }


        /**
         * Constructs a {@code SimpleTimeZone} with the given base time zone offset from GMT
         * and time zone ID. Timezone IDs can be obtained from
         * {@code TimeZone.getAvailableIDs}. Normally you should use {@code TimeZone.getDefault} to
         * construct a {@code TimeZone}.
         * 
         * @param offset
         *            the given base time zone offset to GMT.
         * @param name
         *            the time zone ID which is obtained from
         *            {@code TimeZone.getAvailableIDs}.
         */
        public SimpleTimeZone(int offset, String name)
        {
            setID(name);
            rawOffset = offset;
            isSimple = false;
            /*
            icuTZ = getICUTimeZone(name); 
            if (icuTZ instanceof com.ibm.icu.util.SimpleTimeZone) {
                isSimple = true;
                icuTZ.setRawOffset(offset);
            } else {
                isSimple = false;
            }
            useDaylight = icuTZ.useDaylightTime();
             */
        }


        /**
         * Constructs a {@code SimpleTimeZone} with the given base time zone offset from GMT,
         * time zone ID, and times to start and end the daylight savings time. Timezone IDs can
         * be obtained from {@code TimeZone.getAvailableIDs}. Normally you should use
         * {@code TimeZone.getDefault} to create a {@code TimeZone}. For a time zone that does not
         * use daylight saving time, do not use this constructor; instead you should
         * use {@code SimpleTimeZone(rawOffset, ID)}.
         * <p>
         * By default, this constructor specifies day-of-week-in-month rules. That
         * is, if the {@code startDay} is 1, and the {@code startDayOfWeek} is {@code SUNDAY}, then this
         * indicates the first Sunday in the {@code startMonth}. A {@code startDay} of -1 likewise
         * indicates the last Sunday. However, by using negative or zero values for
         * certain parameters, other types of rules can be specified.
         * <p>
         * Day of month: To specify an exact day of the month, such as March 1, set
         * {@code startDayOfWeek} to zero.
         * <p>
         * Day of week after day of month: To specify the first day of the week
         * occurring on or after an exact day of the month, make the day of the week
         * negative. For example, if {@code startDay} is 5 and {@code startDayOfWeek} is {@code -MONDAY},
         * this indicates the first Monday on or after the 5th day of the
         * {@code startMonth}.
         * <p>
         * Day of week before day of month: To specify the last day of the week
         * occurring on or before an exact day of the month, make the day of the
         * week and the day of the month negative. For example, if {@code startDay} is {@code -21}
         * and {@code startDayOfWeek} is {@code -WEDNESDAY}, this indicates the last Wednesday on or
         * before the 21st of the {@code startMonth}.
         * <p>
         * The above examples refer to the {@code startMonth}, {@code startDay}, and {@code startDayOfWeek};
         * the same applies for the {@code endMonth}, {@code endDay}, and {@code endDayOfWeek}.
         * <p>
         * The daylight savings time difference is set to the default value: one hour.
         *
         * @param offset
         *            the given base time zone offset to GMT.
         * @param name
         *            the time zone ID which is obtained from
         *            {@code TimeZone.getAvailableIDs}.
         * @param startMonth
         *            the daylight savings starting month. The month indexing is 0-based. eg, 0
         *            for January.
         * @param startDay
         *            the daylight savings starting day-of-week-in-month. Please see
         *            the member description for an example.
         * @param startDayOfWeek
         *            the daylight savings starting day-of-week. Please see the
         *            member description for an example.
         * @param startTime
         *            the daylight savings starting time in local wall time, which
         *            is standard time in this case. Please see the member
         *            description for an example.
         * @param endMonth
         *            the daylight savings ending month. The month indexing is 0-based. eg, 0 for
         *            January.
         * @param endDay
         *            the daylight savings ending day-of-week-in-month. Please see
         *            the member description for an example.
         * @param endDayOfWeek
         *            the daylight savings ending day-of-week. Please see the member
         *            description for an example.
         * @param endTime
         *            the daylight savings ending time in local wall time, which is
         *            daylight time in this case. Please see the member description
         *            for an example.
         * @throws IllegalArgumentException
         *             if the month, day, dayOfWeek, or time parameters are out of
         *             range for the start or end rule.
         */
        public SimpleTimeZone(int offset, String name, int startMonth,
                int startDay, int startDayOfWeek, int startTime, int endMonth,
                int endDay, int endDayOfWeek, int endTime)
            :
                this(offset, name, startMonth, startDay, startDayOfWeek, startTime,
                        endMonth, endDay, endDayOfWeek, endTime, 3600000)
        {
        }

        /**
         * Constructs a {@code SimpleTimeZone} with the given base time zone offset from GMT,
         * time zone ID, times to start and end the daylight savings time, and
         * the daylight savings time difference in milliseconds.
         *
         * @param offset
         *            the given base time zone offset to GMT.
         * @param name
         *            the time zone ID which is obtained from
         *            {@code TimeZone.getAvailableIDs}.
         * @param startMonth
         *            the daylight savings starting month. Month is 0-based. eg, 0
         *            for January.
         * @param startDay
         *            the daylight savings starting day-of-week-in-month. Please see
         *            the description of {@link #SimpleTimeZone(int, String, int, int, int, int, int, int, int, int)} for an example.
         * @param startDayOfWeek
         *            the daylight savings starting day-of-week. Please see the
         *            description of {@link #SimpleTimeZone(int, String, int, int, int, int, int, int, int, int)} for an example.
         * @param startTime
         *            The daylight savings starting time in local wall time, which
         *            is standard time in this case. Please see the description of
         *            {@link #SimpleTimeZone(int, String, int, int, int, int, int, int, int, int)} for an example.
         * @param endMonth
         *            the daylight savings ending month. Month is 0-based. eg, 0 for
         *            January.
         * @param endDay
         *            the daylight savings ending day-of-week-in-month. Please see
         *            the description of {@link #SimpleTimeZone(int, String, int, int, int, int, int, int, int, int)} for an example.
         * @param endDayOfWeek
         *            the daylight savings ending day-of-week. Please see the description of
         *            {@link #SimpleTimeZone(int, String, int, int, int, int, int, int, int, int)} for an example.
         * @param endTime
         *            the daylight savings ending time in local wall time, which is
         *            daylight time in this case. Please see the description of {@link #SimpleTimeZone(int, String, int, int, int, int, int, int, int, int)}
         *            for an example.
         * @param daylightSavings
         *            the daylight savings time difference in milliseconds.
         * @throws IllegalArgumentException
         *                if the month, day, dayOfWeek, or time parameters are out of
         *                range for the start or end rule.
         */
        public SimpleTimeZone(int offset, String name, int startMonth,
                int startDay, int startDayOfWeek, int startTime, int endMonth,
                int endDay, int endDayOfWeek, int endTime, int daylightSavings)
        {
            /*
        icuTZ = getICUTimeZone(name);
        if (icuTZ instanceof com.ibm.icu.util.SimpleTimeZone) {
            isSimple = true;
            com.ibm.icu.util.SimpleTimeZone tz = (com.ibm.icu.util.SimpleTimeZone)icuTZ;
            tz.setRawOffset(offset);
            tz.setStartRule(startMonth, startDay, startDayOfWeek, startTime);
            tz.setEndRule(endMonth, endDay, endDayOfWeek, endTime);
            tz.setDSTSavings(daylightSavings);
        } else {
            isSimple = false;
        }*/
            isSimple = false;
            setID(name);
            rawOffset = offset;
            if (daylightSavings <= 0)
            {
                throw new java.lang.IllegalArgumentException("DST offset: " + daylightSavings); //$NON-NLS-1$
            }
            dstSavings = daylightSavings;
            /*
        setStartRule(startMonth, startDay, startDayOfWeek, startTime);
        setEndRule(endMonth, endDay, endDayOfWeek, endTime);
        
        useDaylight = daylightSavings > 0 || icuTZ.useDaylightTime();
             */
        }

        /**
         * Construct a {@code SimpleTimeZone} with the given base time zone offset from GMT,
         * time zone ID, times to start and end the daylight savings time including a
         * mode specifier, the daylight savings time difference in milliseconds.
         * The mode specifies either {@link #WALL_TIME}, {@link #STANDARD_TIME}, or
         * {@link #UTC_TIME}.
         *
         * @param offset
         *            the given base time zone offset to GMT.
         * @param name
         *            the time zone ID which is obtained from
         *            {@code TimeZone.getAvailableIDs}.
         * @param startMonth
         *            the daylight savings starting month. The month indexing is 0-based. eg, 0
         *            for January.
         * @param startDay
         *            the daylight savings starting day-of-week-in-month. Please see
         *            the description of {@link #SimpleTimeZone(int, String, int, int, int, int, int, int, int, int)} for an example.
         * @param startDayOfWeek
         *            the daylight savings starting day-of-week. Please see the
         *            description of {@link #SimpleTimeZone(int, String, int, int, int, int, int, int, int, int)} for an example.
         * @param startTime
         *            the time of day in milliseconds on which daylight savings
         *            time starts, based on the {@code startTimeMode}.
         * @param startTimeMode
         *            the mode (UTC, standard, or wall time) of the start time
         *            value.
         * @param endDay
         *            the day of the week on which daylight savings time ends.
         * @param endMonth
         *            the daylight savings ending month. The month indexing is 0-based. eg, 0 for
         *            January.
         * @param endDayOfWeek
         *            the daylight savings ending day-of-week. Please see the description of
         *            {@link #SimpleTimeZone(int, String, int, int, int, int, int, int, int, int)} for an example.
         * @param endTime
         *            the time of day in milliseconds on which daylight savings
         *            time ends, based on the {@code endTimeMode}.
         * @param endTimeMode
         *            the mode (UTC, standard, or wall time) of the end time value.
         * @param daylightSavings
         *            the daylight savings time difference in milliseconds.
         * @throws IllegalArgumentException
         *             if the month, day, dayOfWeek, or time parameters are out of
         *             range for the start or end rule.
         */
        public SimpleTimeZone(int offset, String name, int startMonth,
                int startDay, int startDayOfWeek, int startTime, int startTimeMode,
                int endMonth, int endDay, int endDayOfWeek, int endTime,
                int endTimeMode, int daylightSavings)
            :

                this(offset, name, startMonth, startDay, startDayOfWeek, startTime,
                        endMonth, endDay, endDayOfWeek, endTime, daylightSavings)
        {
            startMode = startTimeMode;
            endMode = endTimeMode;
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
        public override int getOffset(int era, int year, int month, int day,
                int dayOfWeek, int time)
        {
            throw new java.lang.UnsupportedOperationException("Not yet implemented");
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
        public override bool inDaylightTime(Date time)
        {
            throw new java.lang.UnsupportedOperationException("Not yet implemented");
        }

        /**
         * Sets the offset for standard time from GMT for this {@code TimeZone}.
         * 
         * @param offset
         *            the offset from GMT in milliseconds.
         */
        public override void setRawOffset(int offset)
        {
            throw new java.lang.UnsupportedOperationException("Not yet implemented");
        }

    }
}