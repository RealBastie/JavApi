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

namespace biz.ritter.javapi.nio.channels
{

	public class FileChannel
	{
		/// <summary>
		/// <para>
		/// Creating FileChannel instance over input stream returning an unique FileChannel object
		/// associated with this FileInputStream. The position in this Channel qill be equal to the
		/// number of bytes readed from file. Reading bytes from FileInputStream will also
		/// increment the position in this Channel. Also changing position in Channel changing
		/// position fin FileInputStream.
		/// </para>
		/// <para>
		/// Working over same delegate System.IO.FileStream object is the way to make this true.
		/// </para>
		/// </summary>
		private System.IO.FileStream delegateInstance;
		internal FileChannel(System.IO.FileStream delegateInputStream) {
			this.delegateInstance = delegateInputStream;
		}

		private FileLock fileLock;

		protected FileChannel ()
		{
		}

		public FileLock tryLock() {
			// TODO implement it
			throw new java.lang.UnsupportedOperationException ("Not yet implemented");
		}

		public FileLock lockJ() {
			return this.lockJ (0,java.lang.Long.MAX_VALUE, false); // Works it?
		}


		public FileLock lockJ (long position, long size, bool shared) {
			try {
				delegateInstance.Lock (position, size); // Works it?
				this.fileLock = new FileLock(this,position, size, shared);
				return this.fileLock;
			}
			catch (System.Exception ex) {
				throw new java.io.IOException (ex.getMessage ());
			}
		}

		/// <summary>
		/// Internal method calling from FileLock instance to release lock.
		/// </summary>
		/// <param name="pos">Position.</param>
		/// <param name="length">Length.</param>
		internal void releaseFileLock (long pos, long length) {
			this.delegateInstance.Unlock (pos, length);
		}

		public void close () {
			this.delegateInstance.Close ();
		}
	}
}

