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
    [Serializable]
    public class GregorianCalendar : Calendar
    {
        /**
         * Value for the BC era.
         */
        public static readonly int BC = 0;

        /**
         * Value for the AD era.
         */
        public static readonly int AD = 1;

        public GregorianCalendar() : base() { }

        public GregorianCalendar (int year, int month, int day) : base()
        {
            this.delegateInstance = new DateTime (year,month,day);
        }
        public GregorianCalendar(int year, int month, int day, int hour, int minute, int second) : base()
        {
            this.delegateInstance = new DateTime(year, month, day, hour,minute,second);
        }
        public GregorianCalendar(int year, int month, int day, int hour, int minute) : base()
        {
            this.delegateInstance = new DateTime(year, month, day, hour, minute,0);
        }

        internal static byte[] DaysInMonth = new byte[] { 31, 28, 31, 30, 31, 30, 31, 31,
            30, 31, 30, 31 };


        [NonSerialized]
        private int changeYear = 1582;
        public bool isLeapYear(int year)
        {
            if (year > changeYear)
            {
                return 0 == year % 4 && (0 != year % 100 || 0 == year % 400);
            }
            else
            {
                return 0 == year % 4;
            }
        }
        public override int get(int field)
        {
            if (Calendar.ERA == field)
            {
                return this.delegateInstance.Year < 0 ? GregorianCalendar.BC : GregorianCalendar.AD;
            }
            else
            {
                return base.get(field);
            }
        }

    }

}
