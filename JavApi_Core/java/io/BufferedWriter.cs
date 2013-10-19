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
using System.Text;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.io
{

    /**
     * Wraps an existing {@link Writer} and <em>buffers</em> the output. Expensive
     * interaction with the underlying reader is minimized, since most (smaller)
     * requests can be satisfied by accessing the buffer alone. The drawback is that
     * some extra space is required to hold the buffer and that copying takes place
     * when filling that buffer, but this is usually outweighed by the performance
     * benefits.
     * 
     * <p/>A typical application pattern for the class looks like this:<p/>
     *
     * <pre>
     * BufferedWriter buf = new BufferedWriter(new FileWriter(&quot;file.java&quot;));
     * </pre>
     *
     * @see BufferedReader
     */
    public class BufferedWriter : Writer
    {

        private Writer outj;

        private char[] buf;

        private int pos;

        private String lineSeparator = java.lang.SystemJ.getProperty("line.separator"); //$NON-NLS-1$

        /**
         * Constructs a new {@code BufferedWriter} with {@code outj} as the writer
         * for which to buffer write operations. The buffer size is set to the
         * default value of 8 KB.
         * 
         * @param outj
         *            the writer for which character writing is buffered.
         */
        public BufferedWriter(Writer outj) :
            base(outj)
        {
            this.outj = outj;
            buf = new char[8192];
        }

        /**
         * Constructs a new {@code BufferedWriter} with {@code outj} as the writer
         * for which to buffer write operations. The buffer size is set to {@code
         * size}.
         * 
         * @param outj
         *            the writer for which character writing is buffered.
         * @param size
         *            the size of the buffer in bytes.
         * @throws IllegalArgumentException
         *             if {@code size <= 0}.
         */
        public BufferedWriter(Writer outj, int size) :
            base(outj)
        {
            if (size <= 0)
            {
                throw new java.lang.IllegalArgumentException("size must be > 0"); //$NON-NLS-1$
            }
            this.outj = outj;
            this.buf = new char[size];
        }

        /**
         * Closes this writer. The contents of the buffer are flushed, the target
         * writer is closed, and the buffer is released. Only the first invocation
         * of close has any effect.
         * 
         * @throws IOException
         *             if an error occurs while closing this writer.
         */

        public override void close()
        {//throws IOException {
            lock (lockJ)
            {
                if (isClosed())
                {
                    return;
                }

                java.lang.Throwable thrown = null;
                try
                {
                    flushInternal();
                }
                catch (java.lang.Throwable e)
                {
                    thrown = e;
                }
                buf = null;

                try
                {
                    outj.close();
                }
                catch (java.lang.Throwable e)
                {
                    if (thrown == null)
                    {
                        thrown = e;
                    }
                }
                outj = null;

                if (thrown != null)
                {
                    throw thrown;
                }
            }
        }

        /**
         * Flushes this writer. The contents of the buffer are committed to the
         * target writer and it is then flushed.
         * 
         * @throws IOException
         *             if an error occurs while flushing this writer.
         */

        public override void flush()
        {// throws IOException {
            lock (lockJ)
            {
                if (isClosed())
                {
                    throw new IOException("Writer is closed."); //$NON-NLS-1$
                }
                flushInternal();
                outj.flush();
            }
        }

        /**
         * Flushes the internal buffer.
         */
        private void flushInternal()
        {//throws IOException {
            if (pos > 0)
            {
                outj.write(buf, 0, pos);
            }
            pos = 0;
        }

        /**
         * Indicates whether this writer is closed.
         * 
         * @return {@code true} if this writer is closed, {@code false} otherwise.
         */
        private bool isClosed()
        {
            return outj == null;
        }

        /**
         * Writes a newline to this writer. A newline is determined by the System
         * property "line.separator". The target writer may or may not be flushed
         * when a newline is written.
         * 
         * @throws IOException
         *             if an error occurs attempting to write to this writer.
         */
        public void newLine()
        {//throws IOException {
            write(lineSeparator, 0, lineSeparator.length());
        }

        /**
         * Writes {@code count} characters starting at {@code offset} in
         * {@code cbuf} to this writer. If {@code count} is greater than this
         * writer's buffer, then the buffer is flushed and the characters are
         * written directly to the target writer.
         * 
         * @param cbuf
         *            the array containing characters to write.
         * @param offset
         *            the start position in {@code cbuf} for retrieving characters.
         * @param count
         *            the maximum number of characters to write.
         * @throws IndexOutOfBoundsException
         *             if {@code offset < 0} or {@code count < 0}, or if
         *             {@code offset + count} is greater than the size of
         *             {@code cbuf}.
         * @throws IOException
         *             if this writer is closed or another I/O error occurs.
         */

        public override void write(char[] cbuf, int offset, int count)
        {//throws IOException {
            lock (lockJ)
            {
                if (isClosed())
                {
                    throw new IOException("Writer is closed."); //$NON-NLS-1$
                }
                if (offset < 0 || offset > cbuf.Length - count || count < 0)
                {
                    throw new java.lang.IndexOutOfBoundsException();
                }
                if (pos == 0 && count >= this.buf.Length)
                {
                    outj.write(cbuf, offset, count);
                    return;
                }
                int available = this.buf.Length - pos;
                if (count < available)
                {
                    available = count;
                }
                if (available > 0)
                {
                    java.lang.SystemJ.arraycopy(cbuf, offset, this.buf, pos, available);
                    pos += available;
                }
                if (pos == this.buf.Length)
                {
                    outj.write(this.buf, 0, this.buf.Length);
                    pos = 0;
                    if (count > available)
                    {
                        offset += available;
                        available = count - available;
                        if (available >= this.buf.Length)
                        {
                            outj.write(cbuf, offset, available);
                            return;
                        }

                        java.lang.SystemJ.arraycopy(cbuf, offset, this.buf, pos, available);
                        pos += available;
                    }
                }
            }
        }

        /**
         * Writes the character {@code oneChar} to this writer. If the buffer
         * gets full by writing this character, this writer is flushed. Only the
         * lower two bytes of the integer {@code oneChar} are written.
         * 
         * @param oneChar
         *            the character to write.
         * @throws IOException
         *             if this writer is closed or another I/O error occurs.
         */
        public override void write(int oneChar)
        {//throws IOException {
            lock (lockJ)
            {
                if (isClosed())
                {
                    throw new IOException("Writer is closed."); //$NON-NLS-1$
                }
                if (pos >= buf.Length)
                {
                    outj.write(buf, 0, buf.Length);
                    pos = 0;
                }
                buf[pos++] = (char)oneChar;
            }
        }

        /**
         * Writes {@code count} characters starting at {@code offset} in {@code str}
         * to this writer. If {@code count} is greater than this writer's buffer,
         * then this writer is flushed and the remaining characters are written
         * directly to the target writer. If count is negative no characters are
         * written to the buffer. This differs from the behavior of the superclass.
         * 
         * @param str
         *            the non-null String containing characters to write.
         * @param offset
         *            the start position in {@code str} for retrieving characters.
         * @param count
         *            maximum number of characters to write.
         * @throws IOException
         *             if this writer has already been closed or another I/O error
         *             occurs.
         * @throws IndexOutOfBoundsException
         *             if {@code offset < 0} or {@code offset + count} is greater
         *             than the length of {@code str}.
         */
        public override void write(String str, int offset, int count)
        {//throws IOException {
            lock (lockJ)
            {
                if (isClosed())
                {
                    throw new IOException("Writer is closed."); //$NON-NLS-1$
                }
                if (count <= 0)
                {
                    return;
                }
                if (offset > str.length() - count || offset < 0)
                {
                    throw new java.lang.StringIndexOutOfBoundsException();
                }
                if (pos == 0 && count >= buf.Length)
                {
                    char[] chars = new char[count];
                    str.getChars(offset, offset + count, chars, 0);
                    outj.write(chars, 0, count);
                    return;
                }
                int available = buf.Length - pos;
                if (count < available)
                {
                    available = count;
                }
                if (available > 0)
                {
                    str.getChars(offset, offset + available, buf, pos);
                    pos += available;
                }
                if (pos == buf.Length)
                {
                    outj.write(this.buf, 0, this.buf.Length);
                    pos = 0;
                    if (count > available)
                    {
                        offset += available;
                        available = count - available;
                        if (available >= buf.Length)
                        {
                            char[] chars = new char[count];
                            str.getChars(offset, offset + available, chars, 0);
                            outj.write(chars, 0, available);
                            return;
                        }
                        str.getChars(offset, offset + available, buf, pos);
                        pos += available;
                    }
                }
            }
        }
    }

}