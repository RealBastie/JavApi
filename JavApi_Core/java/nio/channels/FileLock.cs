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

	public class FileLock
	{

		private readonly FileChannel fileChannel;
		private long positionJ;
		private long sizeJ;
		private bool shared;
		protected internal FileLock (FileChannel sourceChannel, long pos, long length, bool isShared)
		{
			if (pos < 0 || length < 0)
				throw new java.lang.IllegalArgumentException ("Lock pos=" + pos + ", length=" + length + " need to be greater than null.");
			this.fileChannel = sourceChannel;
			this.positionJ = pos;
			this.sizeJ = length;
			this.shared = isShared;
		}

		public FileChannel channel () {
			return this.fileChannel;
		}

		public void release () {
			this.fileChannel.releaseFileLock (position (), size ());
		}

		public long position () {
			return positionJ;
		}
		public long size () {
			return sizeJ;
		}
	}
}

