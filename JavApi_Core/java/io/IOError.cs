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

namespace biz.ritter.javapi.io
{
	/// <summary>
	/// This error is thrown when a severe I/O error has happened.
	/// </summary>
	[Serializable]
	public class IOError : java.lang.Error
	{
		private const long serialVersionUID = 67100927991680413L;

		/// <summary>
		/// Constructs a new instance of <see cref="biz.ritter.javapi.io.IOError"/> with its cause filled in.
		/// </summary>
		/// <param name="cause">The detail cause for the error.</param>
		public IOError (java.lang.Throwable cause) :base(cause)
		{
        
		}
	}
}