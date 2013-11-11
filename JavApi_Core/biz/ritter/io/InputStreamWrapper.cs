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
 *  Copyright © 2012, 2013 Sebastian Ritter
 */
using System;
using java = biz.ritter.javapi;

namespace biz.ritter.io
{

    /// <summary>
    /// Wrap .net System.IO.Stream instance as java.io.InputStream
    /// </summary>
    public class InputStreamWrapper : java.io.InputStream
    {
        private System.IO.Stream delegateInstance;
		private System.IO.TextReader otherDelegateInstance;

        public InputStreamWrapper(System.IO.Stream input)
        {
            this.delegateInstance = input;
        }

        public override int read()
        {
            return this.delegateInstance.ReadByte();
        }
        /// Optimized reading for files
        public override int read(byte[] buffer, int beginOffset, int length)
        {
            return this.delegateInstance.Read(buffer, beginOffset, length);
        }

        public override void close()
        {
            this.delegateInstance.Close();
        }

        public override int available()
        {
            return (int)(this.delegateInstance.Length - this.delegateInstance.Position);
        }
    }
	/// <summary>
	/// Wrap .net System.IO.TextReader instance as java.io.InputStream, because
	/// java.lang.Process need InputStream not Reader
	/// </summary>
	public class InputStreamWrapper4Reader : InputStreamWrapper
	{

		public InputStreamWrapper4Reader(System.IO.StreamReader input) : base (input.BaseStream){
		}
	}
}