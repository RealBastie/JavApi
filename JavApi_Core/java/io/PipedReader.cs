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
 * Receives information on a communications pipe. When two threads want to pass
 * data back and forth, one creates a piped writer and the other creates a piped
 * reader.
 *
 * @see PipedWriter
 */
public class PipedReader : Reader {

    private java.lang.Thread lastReader;

    private java.lang.Thread lastWriter;

    private bool isClosed;

    /**
     * The circular buffer through which data is passed.
     */
    private char [] data;

    /**
     * The index in {@code buffer} where the next character will be
     * written.
     */
    private int inJ = -1;

    /**
     * The index in {@code buffer} where the next character will be read.
     */
    private int outJ;

    /**
     * The size of the default pipe in characters
     */
    private const int PIPE_SIZE = 1024;

    /**
     * Indicates if this pipe is connected
     */
    private bool isConnected;

    /**
     * Constructs a new unconnected {@code PipedReader}. The resulting reader
     * must be connected to a {@code PipedWriter} before data may be read from
     * it.
     *
     * @see PipedWriter
     */
    public PipedReader() {
        data = new char[PIPE_SIZE];
    }

    /**
     * Constructs a new unconnected PipedReader and the buffer size is
     * specified. The resulting Reader must be connected to a PipedWriter before
     * data may be read from it.
     * 
     * @param size
     *            the size of the buffer.
     * @throws IllegalArgumentException
     *             if pipeSize is less than or equal to zero.
     * @since 1.6
     */
    public PipedReader(int size) {
        if (size <= 0) {
            throw new java.lang.IllegalArgumentException();
        }
        data = new char[size];
    }

    /**
     * Constructs a new PipedReader connected to the PipedWriter
     * <code>out</code> and the buffer size is specified. Any data written to the writer can be read from the
     * this reader.
     * 
     * @param out
     *            the PipedWriter to connect to.
     * @param pipeSize
     *            the size of the buffer.
     * @throws IOException
     *             if IO errors occur
     * @throws IllegalArgumentException
     *             if pipeSize is less than or equal to zero.
     * @since 1.6
     */
    public PipedReader(PipedWriter outJ, int pipeSize) ://throws IOException {
        this(pipeSize){
        connect(outJ);
    }

    /**
     * Constructs a new {@code PipedReader} connected to the {@link PipedWriter}
     * {@code out}. Any data written to the writer can be read from the this
     * reader.
     * 
     * @param out
     *            the {@code PipedWriter} to connect to.
     * @throws IOException
     *             if {@code out} is already connected.
     */
    public PipedReader(PipedWriter outJ) ://throws IOException {
        this(){
        connect(outJ);
    }

    /**
     * Closes this reader. This implementation releases the buffer used for
     * the pipe and notifies all threads waiting to read or write.
     * 
     * @throws IOException
     *             if an error occurs while closing this reader.
     */
    
    public override void close() {//throws IOException {
        lock (lockJ){
            /* No exception thrown if already closed */
            if (data != null) {
                /* Release buffer to indicate closed. */
                data = null;
            }
            isClosed = true;
        }
    }

    /**
     * Connects this {@code PipedReader} to a {@link PipedWriter}. Any data
     * written to the writer becomes readable in this reader.
     * 
     * @param src
     *            the writer to connect to.
     * @throws IOException
     *             if this reader is closed or already connected, or if {@code
     *             src} is already connected.
     */
    public void connect(PipedWriter src){// throws IOException {
        lock (lockJ){
            src.connect(this);
        }
    }

    /**
     * Establishes the connection to the PipedWriter.
     * 
     * @throws IOException
     *             If this Reader is already connected.
     */
		public void establishConnection(){// throws IOException { // public modifier, because called from PipedWriter
        lock (lockJ){
            if (data == null) {
					throw new IOException("Pipe is closed"); //$NON-NLS-1$
            }
            if (isConnected) {
					throw new IOException("Pipe already connected"); //$NON-NLS-1$
            }
            isConnected = true;
        }
    }

    /**
     * Reads a single character from this reader and returns it as an integer
     * with the two higher-order bytes set to 0. Returns -1 if the end of the
     * reader has been reached. If there is no data in the pipe, this method
     * blocks until data is available, the end of the reader is detected or an
     * exception is thrown.
     * <p>
     * Separate threads should be used to read from a {@code PipedReader} and to
     * write to the connected {@link PipedWriter}. If the same thread is used,
     * a deadlock may occur.
     *
     * @return the character read or -1 if the end of the reader has been
     *         reached.
     * @throws IOException
     *             if this reader is closed or some other I/O error occurs.
     */
    
    public override int read(){// throws IOException {
        char[] carray = new char[1];
        int result = read(carray, 0, 1);
        return result != -1 ? carray[0] : result;
    }

    /**
     * Reads at most {@code count} characters from this reader and stores them
     * in the character array {@code buffer} starting at {@code offset}. If
     * there is no data in the pipe, this method blocks until at least one byte
     * has been read, the end of the reader is detected or an exception is
     * thrown.
     * <p>
     * Separate threads should be used to read from a {@code PipedReader} and to
     * write to the connected {@link PipedWriter}. If the same thread is used, a
     * deadlock may occur.
     * 
     * @param buffer
     *            the character array in which to store the characters read.
     * @param offset
     *            the initial position in {@code bytes} to store the characters
     *            read from this reader.
     * @param count
     *            the maximum number of characters to store in {@code buffer}.
     * @return the number of characters read or -1 if the end of the reader has
     *         been reached.
     * @throws IndexOutOfBoundsException
     *             if {@code offset < 0} or {@code count < 0}, or if {@code
     *             offset + count} is greater than the size of {@code buffer}.
     * @throws InterruptedIOException
     *             if the thread reading from this reader is interrupted.
     * @throws IOException
     *             if this reader is closed or not connected to a writer, or if
     *             the thread writing to the connected writer is no longer
     *             alive.
     */
    
    public override int read(char[] buffer, int offset, int count){// throws IOException {
        lock (lockJ){
            if (!isConnected) {
					throw new IOException("Pipe Not Connected"); //$NON-NLS-1$
            }
            if (data == null) {
					throw new IOException("Pipe is closed"); //$NON-NLS-1$
            }
            // avoid int overflow
            if (offset < 0 || count > buffer.Length - offset || count < 0) {
                throw new java.lang.IndexOutOfBoundsException();
            }
            if (count == 0) {
                return 0;
            }
            /**
             * Set the last thread to be reading on this PipedReader. If
             * lastReader dies while someone is waiting to write an IOException
             * of "Pipe broken" will be thrown in receive()
             */
            lastReader = java.lang.Thread.currentThread();
            try {
                bool first = true;
                while (inJ == -1) {
                    // Are we at end of stream?
                    if (isClosed) {
                        return -1;
                    }
                    if (!first && lastWriter != null && !lastWriter.isAlive()) {
							throw new IOException("Pipe broken"); //$NON-NLS-1$
                    }
                    first = false;
                    // Notify callers of receive()
                    lockJ.notifyAll();
                    lockJ.wait(1000);
                }
            } catch (java.lang.InterruptedException e) {
                throw new InterruptedIOException();
            }
			catch (System.Threading.ThreadInterruptedException e) {
					throw new InterruptedIOException ();
					}

            int copyLength = 0;
            /* Copy chars from out to end of buffer first */
            if (outJ >= inJ) {
                copyLength = count > data.Length - outJ ? data.Length - outJ
                        : count;
                java.lang.SystemJ.arraycopy(data, outJ, buffer, offset, copyLength);
                outJ += copyLength;
                if (outJ == data.Length) {
                    outJ = 0;
                }
                if (outJ == inJ) {
                    // empty buffer
                    inJ = -1;
                    outJ = 0;
                }
            }

            /*
             * Did the read fully succeed in the previous copy or is the buffer
             * empty?
             */
            if (copyLength == count || inJ == -1) {
                return copyLength;
            }

            int charsCopied = copyLength;
            /* Copy bytes from 0 to the number of available bytes */
            copyLength = inJ - outJ > count - copyLength ? count - copyLength
                    : inJ - outJ;
            java.lang.SystemJ.arraycopy(data, outJ, buffer, offset + charsCopied,
                    copyLength);
            outJ += copyLength;
            if (outJ == inJ) {
                // empty buffer
                inJ = -1;
                outJ = 0;
            }
            return charsCopied + copyLength;
        }
    }

    /**
     * Indicates whether this reader is ready to be read without blocking.
     * Returns {@code true} if this reader will not block when {@code read} is
     * called, {@code false} if unknown or blocking will occur. This
     * implementation returns {@code true} if the internal buffer contains
     * characters that can be read.
     * 
     * @return always {@code false}.
     * @throws IOException
     *             if this reader is closed or not connected, or if some other
     *             I/O error occurs.
     * @see #read()
     * @see #read(char[], int, int)
     */
    
    public override bool ready() {//throws IOException {
        lock (lockJ){
            if (!isConnected) {
					throw new IOException("Pipe Not Connected"); //$NON-NLS-1$
            }
            if (data == null) {
					throw new IOException("Pipe is closed"); //$NON-NLS-1$
            }
            return inJ != -1;
        }
    }

    /**
     * Receives a char and stores it into the PipedReader. This called by
     * PipedWriter.write() when writes occur.
     * <P>
     * If the buffer is full and the thread sending #receive is interrupted, the
     * InterruptedIOException will be thrown.
     * 
     * @param oneChar
     *            the char to store into the pipe.
     * 
     * @throws IOException
     *             If the stream is already closed or another IOException
     *             occurs.
     */
		public void receive(char oneChar) {//throws IOException {// public modifier, because called from PipedWriter
        lock (lockJ){
            if (data == null) {
					throw new IOException("Pipe is closed"); //$NON-NLS-1$
            }
            if (lastReader != null && !lastReader.isAlive()) {
					throw new IOException("Pipe broken"); //$NON-NLS-1$
            }
            /*
             * Set the last thread to be writing on this PipedWriter. If
             * lastWriter dies while someone is waiting to read an IOException
             * of "Pipe broken" will be thrown in read()
             */
            lastWriter = java.lang.Thread.currentThread();
            try {
                while (data != null && outJ == inJ) {
                    lockJ.notifyAll();
                    this.wait(1000);
                    if (lastReader != null && !lastReader.isAlive()) {
							throw new IOException("Pipe broken"); //$NON-NLS-1$
                    }
                }
            } catch (java.lang.InterruptedException e) {
                throw new InterruptedIOException();
            }
				catch (System.Threading.ThreadInterruptedException e) {
					throw new InterruptedIOException ();
				}
				if (data != null) {
                if (inJ == -1) {
                    inJ = 0;
                }
                data[inJ++] = oneChar;
                if (inJ == data.Length) {
                    inJ = 0;
                }
                return;
            }
        }
    }

    /**
     * Receives a char array and stores it into the PipedReader. This called by
     * PipedWriter.write() when writes occur.
     * <P>
     * If the buffer is full and the thread sending #receive is interrupted, the
     * InterruptedIOException will be thrown.
     * 
     * @param chars
     *            the char array to store into the pipe.
     * @param offset
     *            offset to start reading from
     * @param count
     *            total characters to read
     * 
     * @throws IOException
     *             If the stream is already closed or another IOException
     *             occurs.
     */
		public void receive(char[] chars, int offset, int count) {//throws IOException { // public modifier, because called from PipedWriter
        lock (lockJ){
            if (data == null) {
					throw new IOException("Pipe is closed"); //$NON-NLS-1$
            }
            if (lastReader != null && !lastReader.isAlive()) {
					throw new IOException("Pipe broken"); //$NON-NLS-1$
            }
            /**
             * Set the last thread to be writing on this PipedWriter. If
             * lastWriter dies while someone is waiting to read an IOException
             * of "Pipe broken" will be thrown in read()
             */
            lastWriter = java.lang.Thread.currentThread();
            while (count > 0) {
                try {
                    while (data != null && outJ == inJ) {
                        lockJ.notifyAll();
                        this.wait(1000);
                        if (lastReader != null && !lastReader.isAlive()) {
								throw new IOException("Pipe broken"); //$NON-NLS-1$
                        }
                    }
                } catch (java.lang.InterruptedException e) {
                    throw new InterruptedIOException();
                }
					catch (System.Threading.ThreadInterruptedException e) {
						throw new InterruptedIOException ();
					}
					if (data == null) {
                    break;
                }
                if (inJ == -1) {
                    inJ = 0;
                }
                if (inJ >= outJ) {
                    int length = data.Length - inJ;
                    if (count < length) {
                        length = count;
                    }
                    java.lang.SystemJ.arraycopy(chars, offset, data, inJ, length);
                    offset += length;
                    count -= length;
                    inJ += length;
                    if (inJ == data.Length) {
                        inJ = 0;
                    }
                }
                if (count > 0 && inJ != outJ) {
                    int length = outJ - inJ;
                    if (count < length) {
                        length = count;
                    }
                    java.lang.SystemJ.arraycopy(chars, offset, data, inJ, length);
                    offset += length;
                    count -= length;
                    inJ += length;
                }
            }
            if (count == 0) {
                return;
            }
        }
    }

    public void done() { // public modifier, because called from PipedWriter
        lock (lockJ){
            isClosed = true;
            lockJ.notifyAll();
        }
    }

		public void flush() {//throws IOException {// public modifier, because called from PipedWriter
        lock (lockJ){
            if (isClosed) {
					throw new IOException("Pipe is closed"); //$NON-NLS-1$
            }
            lockJ.notifyAll();
        }
    }
}
}