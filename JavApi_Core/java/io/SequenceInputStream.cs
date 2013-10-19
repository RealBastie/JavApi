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
 * Concatenates two or more existing {@link InputStream}s. Reads are taken from
 * the first stream until it ends, then the next stream is used, until the last
 * stream returns end of file.
 */
	public class SequenceInputStream : InputStream
	{
		/**
     * An enumeration which will return types of InputStream.
     */
		private java.util.Enumeration<InputStream> e;

		/**
     * The current input stream.
     */
		private InputStream inJ;

		/**
     * Constructs a new {@code SequenceInputStream} using the two streams
     * {@code s1} and {@code s2} as the sequence of streams to read from.
     * 
     * @param s1
     *            the first stream to get bytes from.
     * @param s2
     *            the second stream to get bytes from.
     * @throws NullPointerException
     *             if {@code s1} is {@code null}.
     */
		public SequenceInputStream (InputStream s1, InputStream s2)
		{
			if (s1 == null) {
				throw new java.lang.NullPointerException ();
			}
			java.util.Vector<InputStream> inVector = new java.util.Vector<InputStream> (1);
			inVector.addElement (s2);
			e = inVector.elements ();
			inJ = s1;
		}

		/**
     * Constructs a new SequenceInputStream using the elements returned from
     * Enumeration {@code e} as the stream sequence. The instances returned by
     * {@code e.nextElement()} must be of type {@link InputStream}.
     * 
     * @param e
     *            the enumeration of {@code InputStreams} to get bytes from.
     * @throws NullPointerException
     *             if any of the elements in {@code e} is {@code null}.
     */
		public SequenceInputStream (java.util.Enumeration<InputStream> e)
		{
			this.e = e;
			if (e.hasMoreElements ()) {
				inJ = e.nextElement ();
				if (inJ == null) {
					throw new java.lang.NullPointerException ();
				}
			}
		}

		/**
     * Returns the number of bytes that are available before the current input stream will
     * block.
     * 
     * @return the number of bytes available in the current input stream before blocking.
     * @throws IOException
     *             if an I/O error occurs in the current input stream.
     */
    
		public override int available ()
		{//throws IOException {
			if (e != null && inJ != null) {
				return inJ.available ();
			}
			return 0;
		}

		/**
     * Closes all streams in this sequence of input stream.
     * 
     * @throws IOException
     *             if an error occurs while closing any of the input streams.
     */
    
		public override void close ()
		{//throws IOException {
			while (inJ != null) {
				nextStream ();
			}
			e = null;
		}

		/**
     * Sets up the next InputStream or leaves it alone if there are none left.
     * 
     * @throws IOException
     */
		private void nextStream ()
		{// throws IOException {
			if (inJ != null) {
				inJ.close ();
			}
			if (e.hasMoreElements ()) {
				inJ = e.nextElement ();
				if (inJ == null) {
					throw new java.lang.NullPointerException ();
				}
			} else {
				inJ = null;
			}
		}

		/**
     * Reads a single byte from this sequence of input streams and returns it as
     * an integer in the range from 0 to 255. It tries to read from the current
     * stream first; if the end of this stream has been reached, it reads from
     * the next one. Blocks until one byte has been read, the end of the last
     * input stream in the sequence has been reached, or an exception is thrown.
     * 
     * @return the byte read or -1 if either the end of the last stream in the
     *         sequence has been reached or this input stream sequence is
     *         closed.
     * @throws IOException
     *             if an error occurs while reading the current source input
     *             stream.
     */
    
		public override int read ()
		{// throws IOException {
			while (inJ != null) {
				int result = inJ.read ();
				if (result >= 0) {
					return result;
				}
				nextStream ();
			}
			return -1;
		}

		/**
     * Reads at most {@code count} bytes from this sequence of input streams and
     * stores them in the byte array {@code buffer} starting at {@code offset}.
     * Blocks only until at least 1 byte has been read, the end of the stream
     * has been reached, or an exception is thrown.
     * <p>
     * This SequenceInputStream shows the same behavior as other InputStreams.
     * To do this it will read only as many bytes as a call to read on the
     * current substream returns. If that call does not return as many bytes as
     * requested by {@code count}, it will not retry to read more on its own
     * because subsequent reads might block. This would violate the rule that
     * it will only block until at least one byte has been read.
     * <p>
     * If a substream has already reached the end when this call is made, it
     * will close that substream and start with the next one. If there are no
     * more substreams it will return -1.
     * 
     * @param buffer
     *            the array in which to store the bytes read.
     * @param offset
     *            the initial position in {@code buffer} to store the bytes read
     *            from this stream.
     * @param count
     *            the maximum number of bytes to store in {@code buffer}.
     * @return the number of bytes actually read; -1 if this sequence of streams
     *         is closed or if the end of the last stream in the sequence has
     *         been reached.
     * @throws IndexOutOfBoundsException
     *             if {@code offset < 0} or {@code count < 0}, or if {@code
     *             offset + count} is greater than the size of {@code buffer}.
     * @throws IOException
     *             if an I/O error occurs.
     * @throws NullPointerException
     *             if {@code buffer} is {@code null}.
     */
    
		public override int read (byte[] buffer, int offset, int count)
		{//throws IOException {
			if (inJ == null) {
				return -1;
			}
			if (buffer == null) {
				throw new java.lang.NullPointerException ();
			}
			// avoid int overflow
			if (offset < 0 || offset > buffer.Length - count || count < 0) {
				throw new java.lang.IndexOutOfBoundsException ();
			}
			while (inJ != null) {
				int result = inJ.read (buffer, offset, count);
				if (result >= 0) {
					return result;
				}
				nextStream ();
			}
			return -1;
		}
	}
}
