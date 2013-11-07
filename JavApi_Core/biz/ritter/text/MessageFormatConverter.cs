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
 *  Copyright © 2013 Sebastian Ritter
 */
using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi
{
	public class MessageFormatConverter
	{
		public MessageFormatConverter ()
		{
		}

		/// <summary>
		/// Convert formatted Java Message to formatted C# Message
		/// </summary>
		/// <para>
		/// <strong>Java:</strong> <code>%[argument_index$][flags][width][.precision]conversion</code><br />
		/// <strong>C#:</strong> <code>{index[,alignment][:formatString]}</code>
		/// </para>
		/// <para>
		/// <strong>Java:</strong> <code>"The %s costs $%.2f for %d months.%n", "studio", 499.0, 3</code><br />
		/// <strong>C#:</strong> <code>"The {0} costs {1:C} for {2} months.\n", "studio", 499.0, 3</code> 
		/// </para>
		/// <para>
		/// <strong>Java:</strong> <code>"Today is %tD%n", new java.util.Date()</code><br />
		/// <strong>C#:</strong> <code>"Today is " + DateTime.Now.ToShortDateString()</code>
		/// </para>
		/// <param name="msg">Message.</param>
		public static String convert (String msg) {
			// read chars. if %, read next charand convert to  C#
			char[] chars = msg.ToCharArray ();
			java.lang.StringBuilder result = new java.lang.StringBuilder ();
			int number = 0;
			for (int i = 0; i < chars.Length; i++) {
				switch (chars [i]) {
				case '%':
					i++;
					switch (chars[i]) {
					case '%':
						result.append ('%');
						break;
					case 'n':
						result.append ('\n');
						break;
					case '{':
						result.append ("{{");
						break;
					case '}':
						result.append ("}}");
					default:
						// gültige Zeichen: -0123456789bBhHsScCdoxXeEfgGaAtT
						// berücksichtigt:  -0123456789    s     xX
						result.append ('{'); // Beginn format
						result.append (number); // index of format parameter

						bool formattedPart = true;
						bool beginnAlignment = true;
						while (formattedPart) {
							switch (chars [i]) {
							case '-':
							case '0':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case '7':
							case '8':
							case '9':
								if (beginnAlignment) {
									result.append (',');
									beginnAlignment = false;
								}
								result.append (chars [i]);
								break;
							case 'x':
							case 'X':
								result.append (':');
								result.append (chars [i]);
								formattedPart = false;
								break;
							case 's':
								formattedPart = false;
								break;
							//case 'S': see http://stackoverflow.com/questions/1839649/c-sharp-string-format-flag-or-modifier-to-lowercase-param
							default :
								formattedPart = false;
								i--;
								break;
							}
							i++;
						}
						result.append ('}');
						number ++;
						break;
					}
					break;
				default:
					result.append (chars [i]);
					break;
				}
			}
			return result.toString ();;
		}

	}


}

