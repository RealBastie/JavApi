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
 */
using System;
using java = biz.ritter.javapi;

namespace org.apache.harmony.luni.util
{

    /**
     * The class contains static {@link java.io.InputStream} utilities.
     */
    internal class InputStreamHelper {

        /* Basties Note: Implementation using dotnet visible modifier 
         *               protected internal and so we do not use reflection.
         */

        /**
         * Reads all bytes from {@link java.io.ByteArrayInputStream} using its
         * underlying buffer directly.
         * 
         * @return an underlying buffer, if a current position is at the buffer
         *         beginning, and an end position is at the buffer end, or a copy of
         *         the underlying buffer part.
         */
        private static byte[] expose(java.io.ByteArrayInputStream bais) {
            byte[] buffer, buf;
            int pos;
            lock (bais) {
                int available = bais.available();
                try {
                    buf = bais.buf;
                    pos = bais.pos;
                } catch (java.lang.IllegalAccessException iae) {
                    throw new java.lang.InternalError(iae.getLocalizedMessage());
                }
                if (pos == 0 && available == buf.Length) {
                    buffer = buf;
                } else {
                    buffer = new byte[available];
                    java.lang.SystemJ.arraycopy(buf, pos, buffer, 0, available);
                }
                bais.skip(available);
            }
            return buffer;
        }

        /**
         * The utility method for reading the whole input stream into a snapshot
         * buffer. To speed up the access it works with an underlying buffer for a
         * given {@link java.io.ByteArrayInputStream}.
         * 
         * @param is
         *            the stream to be read.
         * @return the snapshot wrapping the buffer where the bytes are read to.
         * @throws UnsupportedOperationException
         *             if the input stream data cannot be exposed
         */
        public static byte[] expose(java.io.InputStream isJ)// throws IOException,
        {//UnsupportedOperationException {
            if (isJ is ExposedByteArrayInputStream) {
                return ((ExposedByteArrayInputStream) isJ).expose();
            }

            if (isJ.GetType().Equals(typeof (java.io.ByteArrayInputStream))){
                return expose((java.io.ByteArrayInputStream) isJ);
            }

            // We don't know how to do this
            throw new java.lang.UnsupportedOperationException();
        }

        /**
         * Reads all the bytes from the given input stream.
         * 
         * Calls read multiple times on the given input stream until it receives an
         * end of file marker. Returns the combined results as a byte array. Note
         * that this method may block if the underlying stream read blocks.
         * 
         * @param is
         *            the input stream to be read.
         * @return the content of the stream as a byte array.
         * @throws IOException
         *             if a read error occurs.
         */
        public static byte[] readFullyAndClose(java.io.InputStream isJ){// throws IOException {

            try {
                // Initial read
                byte[] buffer = new byte[1024];
                int count = isJ.read(buffer);
                int nextByte = isJ.read();

                // Did we get it all in one read?
                if (nextByte == -1) {
                    byte[] dest = new byte[count];
                    java.lang.SystemJ.arraycopy(buffer, 0, dest, 0, count);
                    return dest;
                }

                // Requires additional reads
                java.io.ByteArrayOutputStream baos = new java.io.ByteArrayOutputStream(count * 2);
                baos.write(buffer, 0, count);
                baos.write(nextByte);
                while (true) {
                    count = isJ.read(buffer);
                    if (count == -1) {
                        return baos.toByteArray();
                    }
                    baos.write(buffer, 0, count);
                }
            } finally {
                isJ.close();
            }
        }
    }
#region ExposedByteArrayinputStream
    /**
        * The extension of <code>ByteArrayInputStream</code> which exposes an
        * underlying buffer.
        */
    internal class ExposedByteArrayInputStream : java.io.ByteArrayInputStream {

        /**
            * @see java.io.ByteArrayInputStream(byte[])
            */
        public ExposedByteArrayInputStream(byte[] buf) :base(buf){
        }

        /**
            * @see java.io.ByteArrayInputStream(byte[], int, int)
            */
        public ExposedByteArrayInputStream(byte[] buf, int offset, int length) :base(buf, offset, length){
        }

        /**
            * Reads the whole stream and returns the stream snapshot.
            */
        public virtual byte[] expose() {
            lock (this) {
            if (pos == 0 && countJ == buf.Length) {
                skip(countJ);
                return buf;
            }

            int availableJ = available();
            byte[] buffer = new byte[availableJ];
            java.lang.SystemJ.arraycopy(buf, pos, buffer, 0, availableJ);
            skip(availableJ);
            return buffer;
            }
        }
    }
#endregion
}
