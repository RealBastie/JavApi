﻿/*
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
 */

using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.io
{

    /**
     * Wraps an existing {@link Reader} and adds functionality to "push back"
     * characters that have been read, so that they can be read again. Parsers may
     * find this useful. The number of characters which may be pushed back can be
     * specified during construction. If the buffer of pushed back bytes is empty,
     * characters are read from the underlying reader.
     */
    public class PushbackReader : FilterReader {

        private const String MESSAGE_STREAM_IS_CLOSED = "Stream is closed";

        /**
         * The {@code char} array containing the chars to read.
         */
        char[] buf;

        /**
         * The current position within the char array {@code buf}. A value
         * equal to buf.length indicates no chars available. A value of 0 indicates
         * the buffer is full.
         */
        int pos;

        /**
         * Constructs a new {@code PushbackReader} with the specified reader as
         * source. The size of the pushback buffer is set to the default value of 1
         * character.
         * 
         * @param in
         *            the source reader.
         */
        public PushbackReader(Reader inJ) :base (inJ) {
            buf = new char[1];
            pos = 1;
        }

        /**
         * Constructs a new {@code PushbackReader} with {@code in} as source reader.
         * The size of the pushback buffer is set to {@code size}.
         * 
         * @param in
         *            the source reader.
         * @param size
         *            the size of the pushback buffer.
         * @throws IllegalArgumentException
         *             if {@code size} is negative.
         */
        public PushbackReader(Reader inJ, int size) : base (inJ) {
            if (size <= 0) {
                throw new java.lang.IllegalArgumentException("size <= 0"); //$NON-NLS-1$
            }
            buf = new char[size];
            pos = size;
        }

        /**
         * Closes this reader. This implementation closes the source reader
         * and releases the pushback buffer.
         * 
         * @throws IOException
         *             if an error occurs while closing this reader.
         */
        public override void close() //throws IOException 
        {
            lock (lockJ) {
                buf = null;
                inJ.close();
            }
        }

        /**
         * Marks the current position in this stream. Setting a mark is not
         * supported in this class; this implementation always throws an
         * {@code IOException}.
         * 
         * @param readAheadLimit
         *            the number of character that can be read from this reader
         *            before the mark is invalidated; this parameter is ignored.
         * @throws IOException
         *             if this method is called.
         */
        public override void mark(int readAheadLimit) //throws IOException 
        {
            throw new IOException(); //$NON-NLS-1$ Messages.getString("luni.D4")
        }

        /**
         * Indicates whether this reader supports the {@code mark(int)} and
         * {@code reset()} methods. {@code PushbackReader} does not support them, so
         * it returns {@code false}.
         * 
         * @return always {@code false}.
         * @see #mark(int)
         * @see #reset()
         */
        public override bool markSupported() {
            return false;
        }

        /**
         * Reads a single character from this reader and returns it as an integer
         * with the two higher-order bytes set to 0. Returns -1 if the end of the
         * reader has been reached. If the pushback buffer does not contain any
         * available characters then a character from the source reader is returned.
         * Blocks until one character has been read, the end of the source reader is
         * detected or an exception is thrown.
         *
         * @return the character read or -1 if the end of the source reader has been
         *         reached.
         * @throws IOException
         *             if this reader is closed or an I/O error occurs while reading
         *             from this reader.
         */
        public override int read() // throws IOException 
        {
            lock (lockJ) {
                if (buf == null) {
                    throw new IOException(MESSAGE_STREAM_IS_CLOSED); //$NON-NLS-1$
                }
                /* Is there a pushback character available? */
                if (pos < buf.Length) {
                    return buf[pos++];
                }
                /*
                 * Assume read() in the InputStream will return 2 lowest-order bytes
                 * or -1 if end of stream.
                 */
                return inJ.read();
            }
        }

        /**
         * Reads at most {@code length} bytes from this reader and stores them in
         * byte array {@code buffer} starting at {@code offset}. Characters are
         * read from the pushback buffer first, then from the source reader if more
         * bytes are required. Blocks until {@code count} characters have been read,
         * the end of the source reader is detected or an exception is thrown.
         * 
         * @param buffer
         *            the array in which to store the characters read from this
         *            reader.
         * @param offset
         *            the initial position in {@code buffer} to store the characters
         *            read from this reader.
         * @param count
         *            the maximum number of bytes to store in {@code buffer}.
         * @return the number of bytes read or -1 if the end of the source reader
         *         has been reached.
         * @throws IndexOutOfBoundsException
         *             if {@code offset &lt; 0} or {@code count &lt; 0}, or if
         *             {@code offset + count} is greater than the length of
         *             {@code buffer}.
         * @throws IOException
         *             if this reader is closed or another I/O error occurs while
         *             reading from this reader.
         */
        public override int read(char[] buffer, int offset, int count) //throws IOException 
        {
            lock (lockJ) {
                if (null == buf) {
                    throw new IOException(MESSAGE_STREAM_IS_CLOSED); //$NON-NLS-1$
                }
                // avoid int overflow
                if (offset < 0 || count < 0 || offset > buffer.Length - count) {
                    throw new java.lang.IndexOutOfBoundsException();
                }

                int copiedChars = 0;
                int copyLength = 0;
                int newOffset = offset;
                /* Are there pushback chars available? */
                if (pos < buf.Length) {
                    copyLength = (buf.Length - pos >= count) ? count : buf.Length - pos;
                    java.lang.SystemJ.arraycopy(buf, pos, buffer, newOffset, copyLength);
                    newOffset += copyLength;
                    copiedChars += copyLength;
                    /* Use up the chars in the local buffer */
                    pos += copyLength;
                }
                /* Have we copied enough? */
                if (copyLength == count) {
                    return count;
                }
                int inCopied = inJ.read(buffer, newOffset, count - copiedChars);
                if (inCopied > 0) {
                    return inCopied + copiedChars;
                }
                if (copiedChars == 0) {
                    return inCopied;
                }
                return copiedChars;
            }
        }

        /**
         * Indicates whether this reader is ready to be read without blocking.
         * Returns {@code true} if this reader will not block when {@code read} is
         * called, {@code false} if unknown or blocking will occur.
         * 
         * @return {@code true} if the receiver will not block when
         *         {@code read()} is called, {@code false} if unknown
         *         or blocking will occur.
         * @throws IOException
         *             if this reader is closed or some other I/O error occurs.
         * @see #read()
         * @see #read(char[], int, int)
         */
        public override bool ready() //throws IOException 
        {
            lock (lockJ) {
                if (buf == null) {
                    throw new IOException(MESSAGE_STREAM_IS_CLOSED); //$NON-NLS-1$
                }
                return (buf.Length - pos > 0 || inJ.ready());
            }
        }

        /**
         * Resets this reader to the last marked position. Resetting the reader is
         * not supported in this class; this implementation always throws an
         * {@code IOException}.
         * 
         * @throws IOException
         *             if this method is called.
         */
        public override void reset() //throws IOException 
        {
            throw new IOException(); //$NON-NLS-1$
        }

        /**
         * Pushes all the characters in {@code buffer} back to this reader. The
         * characters are pushed back in such a way that the next character read
         * from this reader is buffer[0], then buffer[1] and so on.
         * <p>
         * If this reader's internal pushback buffer cannot store the entire
         * contents of {@code buffer}, an {@code IOException} is thrown. Parts of
         * {@code buffer} may have already been copied to the pushback buffer when
         * the exception is thrown.
         *</p>
         * @param buffer
         *            the buffer containing the characters to push back to this
         *            reader.
         * @throws IOException
         *             if this reader is closed or the free space in the internal
         *             pushback buffer is not sufficient to store the contents of
         *             {@code buffer}.
         */
        public virtual void unread(char[] buffer)// throws IOException 
        {
            unread(buffer, 0, buffer.Length);
        }

        /**
         * Pushes a subset of the characters in {@code buffer} back to this reader.
         * The subset is defined by the start position {@code offset} within
         * {@code buffer} and the number of characters specified by {@code length}.
         * The bytes are pushed back in such a way that the next byte read from this
         * stream is {@code buffer[offset]}, then {@code buffer[1]} and so on.
         * <p>
         * If this stream's internal pushback buffer cannot store the selected
         * subset of {@code buffer}, an {@code IOException} is thrown. Parts of
         * {@code buffer} may have already been copied to the pushback buffer when
         * the exception is thrown.
         *</p>
         * @param buffer
         *            the buffer containing the characters to push back to this
         *            reader.
         * @param offset
         *            the index of the first byte in {@code buffer} to push back.
         * @param length
         *            the number of bytes to push back.
         * @throws IndexOutOfBoundsException
         *             if {@code offset &lt; 0} or {@code count &lt; 0}, or if
         *             {@code offset + count} is greater than the length of
         *             {@code buffer}.
         * @throws IOException
         *             if this reader is closed or the free space in the internal
         *             pushback buffer is not sufficient to store the selected
         *             contents of {@code buffer}.
         * @throws NullPointerException
         *             if {@code buffer} is {@code null}.
         */
        public virtual void unread(char[] buffer, int offset, int length) //throws IOException 
        {
            lock (lockJ) {
                if (buf == null) {
                    // luni.24=Stream is closed
                    throw new IOException(MESSAGE_STREAM_IS_CLOSED); //$NON-NLS-1$
                }
                if (length > pos) {
                    // luni.D3=Pushback buffer full
                    throw new IOException("Pushback buffer full"); //$NON-NLS-1$
                }
                // Force buffer null check first!
                if (offset > buffer.Length - length || offset < 0) {
                    // luni.12=Offset out of bounds \: {0}
                    throw new java.lang.ArrayIndexOutOfBoundsException("Offset out of bounds : "+ offset); //$NON-NLS-1$
                }
                if (length < 0) {
                    // luni.18=Length out of bounds \: {0}
                    throw new java.lang.ArrayIndexOutOfBoundsException("Length out of bounds : "+ length); //$NON-NLS-1$
                }

                for (int i = offset + length - 1; i >= offset; i--) {
                    unread(buffer[i]);
                }
            }
        }

        /**
         * Pushes the specified character {@code oneChar} back to this reader. This
         * is done in such a way that the next character read from this reader is
         * {@code (char) oneChar}.
         * <p>
         * If this reader's internal pushback buffer cannot store the character, an
         * {@code IOException} is thrown.
         *</p>
         * @param oneChar
         *            the character to push back to this stream.
         * @throws IOException
         *             if this reader is closed or the internal pushback buffer is
         *             full.
         */
        public virtual void unread(int oneChar) //throws IOException
        {
            lock (lockJ) {
                if (buf == null) {
                    throw new IOException(MESSAGE_STREAM_IS_CLOSED); //$NON-NLS-1$
                }
                if (pos == 0) {
                    throw new IOException("stream position is zero"); //$NON-NLS-1$
                }
                buf[--pos] = (char) oneChar;
            }
        }

        /**
         * Skips {@code count} characters in this reader. This implementation skips
         * characters in the pushback buffer first and then in the source reader if
         * necessary.
         * 
         * @param count
         *            the number of characters to skip.
         * @return the number of characters actually skipped.
         * @throws IllegalArgumentException if {@code count &lt; 0}.
         * @throws IOException
         *             if this reader is closed or another I/O error occurs.
         */
        public override long skip(long count) //throws IOException 
        {
            if (count < 0) {
                throw new java.lang.IllegalArgumentException();
            }
            lock (lockJ) {
                if (buf == null) {
                    throw new IOException(MESSAGE_STREAM_IS_CLOSED); //$NON-NLS-1$
                }
                if (count == 0) {
                    return 0;
                }
                long inSkipped;
                int availableFromBuffer = buf.Length - pos;
                if (availableFromBuffer > 0) {
                    long requiredFromIn = count - availableFromBuffer;
                    if (requiredFromIn <= 0) {
                        pos += (int)count;
                        return count;
                    }
                    pos += availableFromBuffer;
                    inSkipped = inJ.skip(requiredFromIn);
                } else {
                    inSkipped = inJ.skip(count);
                }
                return inSkipped + availableFromBuffer;
            }
        }
    }
}
