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

/**
 * A specialized {@link InputStream} that reads bytes from a {@code String} in
 * a sequential manner.
 * 
 * @deprecated Use {@link StringReader}
 */
	[Obsolete]
	public class StringBufferInputStream : InputStream
	{
		/**
     * The source string containing the data to read.
     */
		protected String buffer;

		/**
     * The total number of characters in the source string.
     */
		protected int count;

		/**
     * The current position within the source string.
     */
		protected int pos;

		/**
     * Construct a new {@code StringBufferInputStream} with {@code str} as
     * source. The size of the stream is set to the {@code length()} of the
     * string.
     * 
     * @param str
     *            the source string for this stream.
     * @throws NullPointerException
     *             if {@code str} is {@code null}.
     */
		public StringBufferInputStream (String str)
		{
			if (str == null) {
				throw new java.lang.NullPointerException ();
			}
			buffer = str;
			count = str.length ();
		}

		private Object lockJ = new Object ();
		/**
     * Returns the number of bytes that are available before this stream will
     * block.
     * 
     * @return the number of bytes available before blocking.
     */
		public override int available ()
		{
			lock (lockJ) {
				return count - pos;
			}
		}

		/**
     * Reads a single byte from the source string and returns it as an integer
     * in the range from 0 to 255. Returns -1 if the end of the source string
     * has been reached.
     * 
     * @return the byte read or -1 if the end of the source string has been
     *         reached.
     */
    
		public override int read ()
		{
			lock (lockJ) {
				return pos < count ? buffer.charAt (pos++) & 0xFF : -1;
			}
		}

		/**
     * Reads at most {@code length} bytes from the source string and stores them
     * in the byte array {@code b} starting at {@code offset}.
     * 
     * @param b
     *            the byte array in which to store the bytes read.
     * @param offset
     *            the initial position in {@code b} to store the bytes read from
     *            this stream.
     * @param length
     *            the maximum number of bytes to store in {@code b}.
     * @return the number of bytes actually read or -1 if the end of the source
     *         string has been reached.
     * @throws IndexOutOfBoundsException
     *             if {@code offset < 0} or {@code length < 0}, or if
     *             {@code offset + length} is greater than the length of
     *             {@code b}.
     * @throws NullPointerException
     *             if {@code b} is {@code null}.
     */
    
		public override int read (byte []b, int offset, int length)
		{
			lock (lockJ) {
				// According to 22.7.6 should return -1 before checking other
				// parameters.
				if (pos >= count) {
					return -1;
				}
				if (b == null) {
					// luni.11=buffer is null
					throw new java.lang.NullPointerException ("buffer is null"); //$NON-NLS-1$
				}
				// avoid int overflow
				if (offset < 0 || offset > b.Length) {
					// luni.12=Offset out of bounds \: {0}
					throw new java.lang.ArrayIndexOutOfBoundsException ("Offset out of bounds : " + offset); //$NON-NLS-1$
				}
				if (length < 0 || length > b.Length - offset) {
					// luni.18=Length out of bounds \: {0}
					throw new java.lang.ArrayIndexOutOfBoundsException ("Length out of bounds : " + length); //$NON-NLS-1$
				}

				if (length == 0) {
					return 0;
				}

				int copylen = count - pos < length ? count - pos : length;
				for (int i = 0; i < copylen; i++) {
					b [offset + i] = (byte)buffer.charAt (pos + i);
				}
				pos += copylen;
				return copylen;
			}
		}

		/**
     * Resets this stream to the beginning of the source string.
     */
    
		public override void reset ()
		{
			lock (lockJ) {
				pos = 0;
			}
		}

		/**
     * Skips {@code n} characters in the source string. It does nothing and
     * returns 0 if {@code n} is negative. Less than {@code n} characters are
     * skipped if the end of the source string is reached before the operation
     * completes.
     * 
     * @param n
     *            the number of characters to skip.
     * @return the number of characters actually skipped.
     */
    
		public override long skip (long n)
		{
			lock (lockJ) {
				if (n <= 0) {
					return 0;
				}

				int numskipped;
				if (this.count - pos < n) {
					numskipped = this.count - pos;
					pos = this.count;
				} else {
					numskipped = (int)n;
					pos += (int)n;
				}
				return numskipped;
			}
		}
	}
}
