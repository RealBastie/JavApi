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
// $Id: DatatypeConstants.java 446598 2006-09-15 12:55:40Z jeremias $

using System;
using java = biz.ritter.javapi;
using javax = biz.ritter.javapix;

namespace biz.ritter.javapix.xml.datatype
{

    /**
     * <p>Utility class to contain basic Datatype values as constants.</p>
     *
     * @author <a href="mailto:Jeff.Suttor@Sun.com">Jeff Suttor</a>
     * @version $Revision: 446598 $, $Date: 2006-09-15 08:55:40 -0400 (Fri, 15 Sep 2006) $
     * @since 1.5
     */

    public sealed class DatatypeConstants
    {

        /**
         * <p>Private constructor to prevent instantiation.</p>
         */
        private DatatypeConstants()
        {
        }

        /**
         * Value for first month of year.
         */
        public const int JANUARY = 1;

        /**
         * Value for second month of year.
         */
        public const int FEBRUARY = 2;

        /**
         * Value for third month of year.
         */
        public const int MARCH = 3;

        /**
         * Value for fourth month of year.
         */
        public const int APRIL = 4;

        /**
         * Value for fifth month of year.
         */
        public const int MAY = 5;

        /**
         * Value for sixth month of year.
         */
        public const int JUNE = 6;

        /**
         * Value for seventh month of year.
         */
        public const int JULY = 7;

        /**
         * Value for eighth month of year.
         */
        public const int AUGUST = 8;

        /**
         * Value for ninth month of year.
         */
        public const int SEPTEMBER = 9;

        /**
         * Value for tenth month of year.
         */
        public const int OCTOBER = 10;

        /**
         * Value for eleven month of year.
         */
        public const int NOVEMBER = 11;

        /**
         * Value for twelve month of year.
         */
        public const int DECEMBER = 12;

        /**
         * <p>Comparison result.</p>
         */
        public const int LESSER = -1;

        /**
         * <p>Comparison result.</p>
         */
        public const int EQUAL = 0;

        /**
         * <p>Comparison result.</p>
         */
        public const int GREATER = 1;

        /**
         * <p>Comparison result.</p>
         */
        public const int INDETERMINATE = 2;

        /**
         * Designation that an "int" field is not set.
         */
        public const int FIELD_UNDEFINED = java.lang.Integer.MIN_VALUE;

        /**
         * <p>A constant that represents the years field.</p>
         */
        public static readonly Field YEARS = new Field("YEARS", 0);

        /**
         * <p>A constant that represents the months field.</p>
         */
        public static readonly Field MONTHS = new Field("MONTHS", 1);

        /**
         * <p>A constant that represents the days field.</p>
         */
        public static readonly Field DAYS = new Field("DAYS", 2);

        /**
         * <p>A constant that represents the hours field.</p>
         */
        public static readonly Field HOURS = new Field("HOURS", 3);

        /**
         * <p>A constant that represents the minutes field.</p>
         */
        public static readonly Field MINUTES = new Field("MINUTES", 4);

        /**
         * <p>A constant that represents the seconds field.</p>
         */
        public static readonly Field SECONDS = new Field("SECONDS", 5);

        /**
         * Type-safe enum class that represents six fields
         * of the {@link Duration} class.
         */
        public sealed class Field
        {

            /**
             * <p><code>String</code> representation of <ode>Field</code>.</p>
             */
            private readonly String str;
            /**
             * <p>Unique id of the field.</p>
             * 
             * <p>This value allows the {@link Duration} class to use switch
             * statements to process fields.</p>  
             */
            private readonly int id;

            /**
             * <p>Construct a <code>Field</code> with specified values.</p>
             * @param str <code>String</code> representation of <code>Field</code>
             * @param id  <code>int</code> representation of <code>Field</code>
             */
            internal Field(String str, int id)
            {
                this.str = str;
                this.id = id;
            }
            /**
             * Returns a field name in English. This method 
             * is intended to be used for debugging/diagnosis
             * and not for display to end-users.
             * 
             * @return
             *      a non-null valid String constant.
             */
            public String toString() { return str; }

            /**
             * <p>Get id of this Field.</p>
             * 
             * @return Id of field.
             */
            public int getId()
            {
                return id;
            }
        }

        /**
         * <p>Fully qualified name for W3C XML Schema 1.0 datatype <code>dateTime</code>.</p> 
         */
        public static readonly javax.xml.namespacej.QName DATETIME = new javax.xml.namespacej.QName(XMLConstants.W3C_XML_SCHEMA_NS_URI, "dateTime");

        /**
         * <p>Fully qualified name for W3C XML Schema 1.0 datatype <code>time</code>.</p> 
         */
        public static readonly javax.xml.namespacej.QName TIME = new javax.xml.namespacej.QName(XMLConstants.W3C_XML_SCHEMA_NS_URI, "time");

        /**

         * <p>Fully qualified name for W3C XML Schema 1.0 datatype <code>date</code>.</p> 
         */
        public static readonly javax.xml.namespacej.QName DATE = new javax.xml.namespacej.QName(XMLConstants.W3C_XML_SCHEMA_NS_URI, "date");

        /**
         * <p>Fully qualified name for W3C XML Schema 1.0 datatype <code>gYearMonth</code>.</p> 
         */
        public static readonly javax.xml.namespacej.QName GYEARMONTH = new javax.xml.namespacej.QName(XMLConstants.W3C_XML_SCHEMA_NS_URI, "gYearMonth");

        /**
         * <p>Fully qualified name for W3C XML Schema 1.0 datatype <code>gMonthDay</code>.</p> 
         */
        public static readonly javax.xml.namespacej.QName GMONTHDAY = new javax.xml.namespacej.QName(XMLConstants.W3C_XML_SCHEMA_NS_URI, "gMonthDay");

        /**
         * <p>Fully qualified name for W3C XML Schema 1.0 datatype <code>gYear</code>.</p> 
         */
        public static readonly javax.xml.namespacej.QName GYEAR = new javax.xml.namespacej.QName(XMLConstants.W3C_XML_SCHEMA_NS_URI, "gYear");

        /**
         * <p>Fully qualified name for W3C XML Schema 1.0 datatype <code>gMonth</code>.</p> 
         */
        public static readonly javax.xml.namespacej.QName GMONTH = new javax.xml.namespacej.QName(XMLConstants.W3C_XML_SCHEMA_NS_URI, "gMonth");

        /**
         * <p>Fully qualified name for W3C XML Schema 1.0 datatype <code>gDay</code>.</p> 
         */
        public static readonly javax.xml.namespacej.QName GDAY = new javax.xml.namespacej.QName(XMLConstants.W3C_XML_SCHEMA_NS_URI, "gDay");

        /**
         * <p>Fully qualified name for W3C XML Schema datatype <code>duration</code>.</p>
         */
        public static readonly javax.xml.namespacej.QName DURATION = new javax.xml.namespacej.QName(XMLConstants.W3C_XML_SCHEMA_NS_URI, "duration");

        /**
         * <p>Fully qualified name for XQuery 1.0 and XPath 2.0 datatype <code>dayTimeDuration</code>.</p>
         */
        public static readonly javax.xml.namespacej.QName DURATION_DAYTIME = new javax.xml.namespacej.QName(XMLConstants.W3C_XPATH_DATATYPE_NS_URI, "dayTimeDuration");

        /**
         * <p>Fully qualified name for XQuery 1.0 and XPath 2.0 datatype <code>yearMonthDuration</code>.</p>
         */
        public static readonly javax.xml.namespacej.QName DURATION_YEARMONTH = new javax.xml.namespacej.QName(XMLConstants.W3C_XPATH_DATATYPE_NS_URI, "yearMonthDuration");

        /**
         * W3C XML Schema max timezone offset is -14:00. Zone offset is in minutes.
         */
        public const int MAX_TIMEZONE_OFFSET = -14 * 60;

        /**
         * W3C XML Schema min timezone offset is +14:00. Zone offset is in minutes.
         */
        public const int MIN_TIMEZONE_OFFSET = 14 * 60;

    }
}