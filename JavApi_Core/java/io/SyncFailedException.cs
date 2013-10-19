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
	/// Signals that the <see cref="biz.ritter.javapi.io.FileDescriptor#sync"/> method has failed to complete
	/// Sync failed exception.
	/// </summary>
	public class SyncFailedException : IOException
	{

		private const long serialVersionUID = -2353342684412443330L;

		/**
     * Constructs a new {@code SyncFailedException} with its stack trace and
     * detail message filled in.
     * 
     * @param detailMessage
     *            the detail message for this exception.
     */
		public SyncFailedException (String detailMessage) :base(detailMessage)
		{
        
		}
	}
}