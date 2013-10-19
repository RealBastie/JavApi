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
using org.apache.harmony.luni.util;

namespace biz.ritter.javapi.util.zip
{

    /**
     * This class provides an implementation of {@code FilterInputStream} that
     * uncompresses data from a <i>ZIP-archive</i> input stream.
     * <p>
     * A <i>ZIP-archive</i> is a collection of compressed (or uncompressed) files -
     * the so called ZIP entries. Therefore when reading from a {@code
     * ZipInputStream} first the entry's attributes will be retrieved with {@code
     * getNextEntry} before its data is read.
     * <p>
     * While {@code InflaterInputStream} can read a compressed <i>ZIP-archive</i>
     * entry, this extension can read uncompressed entries as well.
     * <p>
     * Use {@code ZipFile} if you can access the archive as a file directly.
     *
     * @see ZipEntry
     * @see ZipFile
     */
    public class ZipInputStream : InflaterInputStream {//implements ZipConstants {
        protected internal static readonly int DEFLATED = 8;

        protected internal static readonly int STORED = 0;

        protected internal static readonly int ZIPDataDescriptorFlag = 8;

        protected internal static readonly int ZIPLocalHeaderVersionNeeded = 20;

        private bool entriesEnd = false;

        private bool hasDD = false;

        private int entryIn = 0;

        private int inRead, lastRead = 0;

        protected internal ZipEntry currentEntry;

        private readonly byte[] hdrBuf = new byte[ZipFile.LOCHDR - ZipFile.LOCVER];

        private readonly CRC32 crc = new CRC32();

        private byte[] nameBuf = new byte[256];

        private char[] charBuf = new char[256];

        /**
         * Constructs a new {@code ZipInputStream} from the specified input stream.
         *
         * @param stream
         *            the input stream to representing a ZIP archive.
         */
        public ZipInputStream(java.io.InputStream stream) :base(new java.io.PushbackInputStream(stream, BUF_SIZE), new Inflater(true)) {
            if (stream == null) {
                throw new java.lang.NullPointerException();
            }
        }

        /**
         * Closes this {@code ZipInputStream}.
         *
         * @throws IOException
         *             if an {@code IOException} occurs.
         */
        
        public override void close(){// throws IOException {
            if (!closed) {
                closeEntry(); // Close the current entry
                base.close();
            }
        }

        /**
         * Closes the current ZIP entry and positions to read the next entry.
         *
         * @throws IOException
         *             if an {@code IOException} occurs.
         */
        public void closeEntry(){// throws IOException {
            if (closed) {
                throw new java.io.IOException("Stream is closed"); //$NON-NLS-1$
            }
            if (currentEntry == null) {
                return;
            }
            if (currentEntry is java.util.jar.JarEntry) {
                java.util.jar.Attributes temp = ((java.util.jar.JarEntry) currentEntry).getAttributes();
                if (temp != null && temp.containsKey("hidden")) { //$NON-NLS-1$
                    return;
                }
            }

            /*
             * The following code is careful to leave the ZipInputStream in a
             * consistent state, even when close() results in an exception. It does
             * so by:
             *  - pushing bytes back into the source stream
             *  - reading a data descriptor footer from the source stream
             *  - resetting fields that manage the entry being closed
             */

            // Ensure all entry bytes are read
            java.lang.Exception failure = null;
            try {
                skip(java.lang.Long.MAX_VALUE);
            } catch (java.lang.Exception e) {
                failure = e;
            }

            int inB, outJ;
            if (currentEntry.compressionMethod == DEFLATED) {
                inB = inf.getTotalIn();
                outJ = inf.getTotalOut();
            } else {
                inB = inRead;
                outJ = inRead;
            }
            int diff = entryIn - inB;
            // Pushback any required bytes
            if (diff != 0) {
                ((java.io.PushbackInputStream) inJ).unread(buf, len - diff, diff);
            }

            try {
                readAndVerifyDataDescriptor(inB, outJ);
            } catch (java.lang.Exception e) {
                if (failure == null) { // otherwise we're already going to throw
                    failure = e;
                }
            }

            inf.reset();
            lastRead = inRead = entryIn = len = 0;
            crc.reset();
            currentEntry = null;

            if (failure != null) {
                if (failure is java.io.IOException) {
                    throw (java.io.IOException) failure;
                } else if (failure is java.lang.RuntimeException) {
                    throw (java.lang.RuntimeException) failure;
                }
                java.lang.AssertionError error = new java.lang.AssertionError();
                error.initCause(failure);
                throw error;
            }
        }

        private void readAndVerifyDataDescriptor(int inB, int outJ) {//throws IOException {
            if (hasDD) {
                inJ.read(hdrBuf, 0, ZipFile.EXTHDR);
                if (getLong(hdrBuf, 0) != ZipFile.EXTSIG) {
                    throw new ZipException("Unknown format"); //$NON-NLS-1$
                }
                currentEntry.crc = getLong(hdrBuf, ZipFile.EXTCRC);
                currentEntry.compressedSize = getLong(hdrBuf, ZipFile.EXTSIZ);
                currentEntry.size = getLong(hdrBuf, ZipFile.EXTLEN);
            }
            if (currentEntry.crc != crc.getValue()) {
                throw new ZipException("Crc mismatch"); //$NON-NLS-1$
            }
            if (currentEntry.compressedSize != inB || currentEntry.size != outJ) {
                throw new ZipException("Size mismatch"); //$NON-NLS-1$
            }
        }

        /**
         * Reads the next entry from this {@code ZipInputStream} or {@code null} if
         * no more entries are present.
         *
         * @return the next {@code ZipEntry} contained in the input stream.
         * @throws IOException
         *             if an {@code IOException} occurs.
         * @see ZipEntry
         */
        public ZipEntry getNextEntry() {//throws IOException {
            closeEntry();
            if (entriesEnd) {
                return null;
            }

            int x = 0, count = 0;
            while (count != 4) {
                count += x = inJ.read(hdrBuf, count, 4 - count);
                if (x == -1) {
                    return null;
                }
            }
            long hdr = getLong(hdrBuf, 0);
            if (hdr == ZipFile.CENSIG) {
                entriesEnd = true;
                return null;
            }
            if (hdr != ZipFile.LOCSIG) {
                return null;
            }

            // Read the local header
            count = 0;
            while (count != (ZipFile.LOCHDR - ZipFile.LOCVER)) {
                count += x = inJ.read(hdrBuf, count, (ZipFile.LOCHDR - ZipFile.LOCVER) - count);
                if (x == -1) {
                    throw new java.io.EOFException();
                }
            }
            int version = getShort(hdrBuf, 0) & 0xff;
            if (version > ZIPLocalHeaderVersionNeeded) {
                throw new ZipException("Cannot read version"); //$NON-NLS-1$
            }
            int flags = getShort(hdrBuf, ZipFile.LOCFLG - ZipFile.LOCVER);
            hasDD = ((flags & ZIPDataDescriptorFlag) == ZIPDataDescriptorFlag);
            int cetime = getShort(hdrBuf, ZipFile.LOCTIM - ZipFile.LOCVER);
            int cemodDate = getShort(hdrBuf, ZipFile.LOCTIM - ZipFile.LOCVER + 2);
            int cecompressionMethod = getShort(hdrBuf, ZipFile.LOCHOW - ZipFile.LOCVER);
            long cecrc = 0, cecompressedSize = 0, cesize = -1;
            if (!hasDD) {
                cecrc = getLong(hdrBuf, ZipFile.LOCCRC - ZipFile.LOCVER);
                cecompressedSize = getLong(hdrBuf, ZipFile.LOCSIZ - ZipFile.LOCVER);
                cesize = getLong(hdrBuf, ZipFile.LOCLEN - ZipFile.LOCVER);
            }
            int flen = getShort(hdrBuf,ZipFile.LOCNAM - ZipFile.LOCVER);
            if (flen == 0) {
                throw new ZipException("Entry is not named"); //$NON-NLS-1$
            }
            int elen = getShort(hdrBuf, ZipFile.LOCEXT - ZipFile.LOCVER);

            count = 0;
            if (flen > nameBuf.Length) {
                nameBuf = new byte[flen];
                charBuf = new char[flen];
            }
            while (count != flen) {
                count += x = inJ.read(nameBuf, count, flen - count);
                if (x == -1) {
                    throw new java.io.EOFException();
                }
            }
            currentEntry = createZipEntry(org.apache.harmony.luni.util.Util.convertUTF8WithBuf(nameBuf, charBuf,
                    0, flen));
            currentEntry.time = cetime;
            currentEntry.modDate = cemodDate;
            currentEntry.setMethod(cecompressionMethod);
            if (cesize != -1) {
                currentEntry.setCrc(cecrc);
                currentEntry.setSize(cesize);
                currentEntry.setCompressedSize(cecompressedSize);
            }
            if (elen > 0) {
                count = 0;
                byte[] e = new byte[elen];
                while (count != elen) {
                    count += x = inJ.read(e, count, elen - count);
                    if (x == -1) {
                        throw new java.io.EOFException();
                    }
                }
                currentEntry.setExtra(e);
            }
            return currentEntry;
        }

        /* Read 4 bytes from the buffer and store it as an int */

        /**
         * Reads up to the specified number of uncompressed bytes into the buffer
         * starting at the offset.
         *
         * @param buffer
         *            a byte array
         * @param start
         *            the starting offset into the buffer
         * @param length
         *            the number of bytes to read
         * @return the number of bytes read
         */
        
        public override int read(byte[] buffer, int start, int length) {//throws IOException {
            if (closed) {
                throw new java.io.IOException("Stream is closed"); //$NON-NLS-1$
            }
            if (inf.finished() || currentEntry == null) {
                return -1;
            }
            // avoid int overflow, check null buffer
            if (start > buffer.Length || length < 0 || start < 0
                    || buffer.Length - start < length) {
                throw new java.lang.ArrayIndexOutOfBoundsException();
            }

            if (currentEntry.compressionMethod == STORED) {
                int csize = (int) currentEntry.size;
                if (inRead >= csize) {
                    return -1;
                }
                if (lastRead >= len) {
                    lastRead = 0;
                    if ((len = inJ.read(buf)) == -1) {
                        eof = true;
                        return -1;
                    }
                    entryIn += len;
                }
                int toRead = length > (len - lastRead) ? len - lastRead : length;
                if ((csize - inRead) < toRead) {
                    toRead = csize - inRead;
                }
                java.lang.SystemJ.arraycopy(buf, lastRead, buffer, start, toRead);
                lastRead += toRead;
                inRead += toRead;
                crc.update(buffer, start, toRead);
                return toRead;
            }
            if (inf.needsInput()) {
                fill();
                if (len > 0) {
                    entryIn += len;
                }
            }
            int read;
            try {
                read = inf.inflate(buffer, start, length);
            } catch (DataFormatException e) {
                throw new ZipException(e.getMessage());
            }
            if (read == 0 && inf.finished()) {
                return -1;
            }
            crc.update(buffer, start, read);
            return read;
        }

        /**
         * Skips up to the specified number of bytes in the current ZIP entry.
         *
         * @param value
         *            the number of bytes to skip.
         * @return the number of bytes skipped.
         * @throws IOException
         *             if an {@code IOException} occurs.
         */
        
        public override long skip(long value) {//throws IOException {
            if (value < 0) {
                throw new java.lang.IllegalArgumentException();
            }

            long skipped = 0;
            byte[] b = new byte[(int)java.lang.Math.min(value, 2048L)];
            while (skipped != value) {
                long rem = value - skipped;
                int x = read(b, 0, (int) (b.Length > rem ? rem : b.Length));
                if (x == -1) {
                    return skipped;
                }
                skipped += x;
            }
            return skipped;
        }

        /**
         * Returns 0 if the {@code EOF} has been reached, otherwise returns 1.
         *
         * @return 0 after {@code EOF} of current entry, 1 otherwise.
         * @throws IOException
         *             if an IOException occurs.
         */
        
        public override int available() {//throws IOException {
            if (closed) {
                throw new java.io.IOException("Stream is closed"); //$NON-NLS-1$
            }
            return (currentEntry == null || inRead < currentEntry.size) ? 1 : 0;
        }

        /**
         * creates a {@link ZipEntry } with the given name.
         *
         * @param name
         *            the name of the entry.
         * @return the created {@code ZipEntry}.
         */
        protected ZipEntry createZipEntry(String name) {
            return new ZipEntry(name);
        }

        private int getShort(byte[] buffer, int off) {
            return (buffer[off] & 0xFF) | ((buffer[off + 1] & 0xFF) << 8);
        }

        private long getLong(byte[] buffer, int off) {
            long l = 0;
            l |= (buffer[off] & 0xFF);
            l |= (buffer[off + 1] & 0xFF) << 8;
            l |= (buffer[off + 2] & 0xFF) << 16;
            l |= ((long) (buffer[off + 3] & 0xFF)) << 24;
            return l;
        }
    }
}
