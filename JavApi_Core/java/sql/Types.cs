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
     * A class which defines constants used to identify generic SQL types, also
     * called JDBC types. The type constant values are equivalent to those defined
     * by X/OPEN.
     */
    public class Types
    {

        /*
         * Private constructor to prevent instantiation.
         */
        private Types()
            : base()
        {
        }

        /**
         * The type code that identifies the SQL type {@code ARRAY}.
         */
        public const int ARRAY = 2003;

        /**
         * The type code that identifies the SQL type {@code BIGINT}.
         */
        public const int BIGINT = -5;

        /**
         * The type code that identifies the SQL type {@code BINARY}.
         */
        public const int BINARY = -2;

        /**
         * The type code that identifies the SQL type {@code BIT}.
         */
        public const int BIT = -7;

        /**
         * The type code that identifies the SQL type {@code BLOB}.
         */
        public const int BLOB = 2004;

        /**
         * The type code that identifies the SQL type {@code BOOLEAN}.
         */
        public const int BOOLEAN = 16;

        /**
         * The type code that identifies the SQL type {@code CHAR}.
         */
        public const int CHAR = 1;

        /**
         * The type code that identifies the SQL type {@code CLOB}.
         */
        public const int CLOB = 2005;

        /**
         * The type code that identifies the SQL type {@code DATALINK}.
         */
        public const int DATALINK = 70;

        /**
         * The type code that identifies the SQL type {@code DATE}.
         */
        public const int DATE = 91;

        /**
         * The type code that identifies the SQL type {@code DECIMAL}.
         */
        public const int DECIMAL = 3;

        /**
         * The type code that identifies the SQL type {@code DISTINCT}.
         */
        public const int DISTINCT = 2001;

        /**
         * The type code that identifies the SQL type {@code DOUBLE}.
         */
        public const int DOUBLE = 8;

        /**
         * The type code that identifies the SQL type {@code FLOAT}.
         */
        public const int FLOAT = 6;

        /**
         * The type code that identifies the SQL type {@code INTEGER}.
         */
        public const int INTEGER = 4;

        /**
         * The type code that identifies the SQL type {@code JAVA_OBJECT}.
         */
        public const int JAVA_OBJECT = 2000;

        /**
         * The type code that identifies the SQL type {@code LONGVARBINARY}.
         */
        public const int LONGVARBINARY = -4;

        /**
         * The type code that identifies the SQL type {@code LONGVARCHAR}.
         */
        public const int LONGVARCHAR = -1;

        /**
         * The type code that identifies the SQL type {@code NULL}.
         */
        public const int NULL = 0;

        /**
         * The type code that identifies the SQL type {@code NUMERIC}.
         */
        public const int NUMERIC = 2;

        /**
         * The type code that identifies that the SQL type is database specific and
         * is mapped to a Java object, accessed via the methods
         * {@code getObject} and {@code setObject}.
         */
        public const int OTHER = 1111;

        /**
         * The type code that identifies the SQL type {@code REAL}.
         */
        public const int REAL = 7;

        /**
         * The type code that identifies the SQL type {@code REF}.
         */
        public const int REF = 2006;

        /**
         * The type code that identifies the SQL type {@code SMALLINT}.
         */
        public const int SMALLINT = 5;

        /**
         * The type code that identifies the SQL type {@code STRUCT}.
         */
        public const int STRUCT = 2002;

        /**
         * The type code that identifies the SQL type {@code TIME}.
         */
        public const int TIME = 92;

        /**
         * The type code that identifies the SQL type {@code TIMESTAMP}.
         */
        public const int TIMESTAMP = 93;

        /**
         * The type code that identifies the SQL type {@code TINYINT}.
         */
        public const int TINYINT = -6;

        /**
         * The type code that identifies the SQL type {@code VARBINARY}.
         */
        public const int VARBINARY = -3;

        /**
         * The type code that identifies the SQL type {@code VARCHAR}.
         */
        public const int VARCHAR = 12;

        /**
         * The type code that identifies the SQL type ROWID.
         */
        public const int ROWID = -8;

        /**
         * The type code that identifies the SQL type NCHAR.
         */
        public const int NCHAR = -15;

        /**
         * The type code that identifies the SQL type NVARCHAR.
         */
        public const int NVARCHAR = -9;

        /**
         * The type code that identifies the SQL type LONGNVARCHAR.
         */
        public const int LONGNVARCHAR = -16;

        /**
         * The type code that identifies the SQL type NCLOB.
         */
        public const int NCLOB = 2011;

        /**
         * The type code that identifies the SQL type SQLXML.
         */
        public const int SQLXML = 2009;
    }
}