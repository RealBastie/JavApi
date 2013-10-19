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
 *  Copyright © 2011,2012,2013 Sebastian Ritter
 */
using System;

namespace biz.ritter.javapi.lang
{
    public sealed class Math 
    {

        public static readonly double PI = System.Math.PI;
		public static readonly double E = System.Math.E;

        public static long min(long a, long b)
        {
            return System.Math.Min(a, b);
        }
        public static double min(double a, double b)
        {
            return System.Math.Min(a, b);
        }
        public static int min(int a, int b)
        {
            return System.Math.Min(a, b);
        }
        public static long max(long a, long b)
        {
            return System.Math.Max(a, b);
        }
        public static float min(float a, float b)
        {
            return System.Math.Min(a, b);
        }
        public static float max(float a, float b)
        {
            return System.Math.Max(a, b);
        }
        public static int max(int a, int b)
        {
            return System.Math.Max(a, b);
        }
        public static double max(double a, double b)
        {
            return System.Math.Max(a, b);
        }
        public static double log(double a)
        {
            return System.Math.Log(a);
        }
        public static double sqrt(double a)
        {
            return System.Math.Sqrt(a);
        }
        public static long abs(long a)
        {
            return System.Math.Abs(a);
        }
        public static int abs(int a)
        {
            return System.Math.Abs(a);
        }
        public static float abs(float a)
        {
            return System.Math.Abs(a);
        }
        public static double abs(double a)
        {
            return System.Math.Abs(a);
        }
        public static double log10(double a)
        {
            return System.Math.Log10(a);
        }
        public static double cos(double a)
        {
            return System.Math.Cos(a);
        }
        public static double acos(double a)
        {
            return System.Math.Acos(a);
        }
        public static double floor(double a)
        {
            return System.Math.Floor(a);
        }
        public static double ceil(double a)
        {
            return System.Math.Ceiling(a);
        }
        public static double round(double a)
        {
            return System.Math.Round(a);
        }
        public static double sin(double a)
        {
            return System.Math.Sin(a);
        }
        public static double pow(double a, double b)
        {
            return System.Math.Pow(a,b);
        }
		public static double cbrt(double d) {
			return radix(3d,d);

		}

		private static double radix (double r, double n) {
			return Math.pow (r, 1.0/n);
		}
    }
}
