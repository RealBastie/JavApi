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
     * Wraps either an existing {@link OutputStream} or an existing {@link Writer}
     * and provides convenience methods for printing common data types in a human
     * readable format. No {@code IOException} is thrown by this class. Instead,
     * callers should use {@link #checkError()} to see if a problem has occurred in
     * this writer.
     */
    public class PrintWriter : Writer {
        /**
         * The writer to print data to.
         */
        protected Writer outJ;

        /**
         * Indicates whether this PrintWriter is in an error state.
         */
        private bool ioError;

        /**
         * Indicates whether or not this PrintWriter should flush its contents after
         * printing a new line.
         */
        private bool autoflush;

        private readonly String lineSeparator = java.lang.SystemJ.getProperty("line.separator");
            //AccessController.doPrivileged(new PriviAction<String>("line.separator")); //$NON-NLS-1$

        /**
         * Constructs a new {@code PrintWriter} with {@code out} as its target
         * stream. By default, the new print writer does not automatically flush its
         * contents to the target stream when a newline is encountered.
         * 
         * @param out
         *            the target output stream.
         * @throws NullPointerException
         *             if {@code out} is {@code null}.
         */
        public PrintWriter(OutputStream outJJ) :
            this(new OutputStreamWriter(outJJ), false){
        }

        /**
         * Constructs a new {@code PrintWriter} with {@code out} as its target
         * stream. The parameter {@code autoflush} determines if the print writer
         * automatically flushes its contents to the target stream when a newline is
         * encountered.
         * 
         * @param out
         *            the target output stream.
         * @param autoflush
         *            indicates whether contents are flushed upon encountering a
         *            newline sequence.
         * @throws NullPointerException
         *             if {@code out} is {@code null}.
         */
        public PrintWriter(OutputStream outJJ, bool autoflush) :
            this(new OutputStreamWriter(outJJ), autoflush){
        }

        /**
         * Constructs a new {@code PrintWriter} with {@code wr} as its target
         * writer. By default, the new print writer does not automatically flush its
         * contents to the target writer when a newline is encountered.
         * 
         * @param wr
         *            the target writer.
         * @throws NullPointerException
         *             if {@code wr} is {@code null}.
         */
        public PrintWriter(Writer wr) :
            this(wr, false){
        }

        /**
         * Constructs a new {@code PrintWriter} with {@code out} as its target
         * writer. The parameter {@code autoflush} determines if the print writer
         * automatically flushes its contents to the target writer when a newline is
         * encountered.
         * 
         * @param wr
         *            the target writer.
         * @param autoflush
         *            indicates whether to flush contents upon encountering a
         *            newline sequence.
         * @throws NullPointerException
         *             if {@code out} is {@code null}.
         */
        public PrintWriter(Writer wr, bool autoflush) :base (wr){
            this.autoflush = autoflush;
            outJ = wr;
        }

        /**
         * Constructs a new {@code PrintWriter} with {@code file} as its target. The
         * virtual machine's default character set is used for character encoding.
         * The print writer does not automatically flush its contents to the target
         * file when a newline is encountered. The output to the file is buffered.
         * 
         * @param file
         *            the target file. If the file already exists, its contents are
         *            removed, otherwise a new file is created.
         * @throws FileNotFoundException
         *             if an error occurs while opening or creating the target file.
         * @throws SecurityException
         *             if a security manager exists and it denies writing to the
         *             target file.
         */
        public PrintWriter(File file) ://throws FileNotFoundException {
            this(new OutputStreamWriter(new BufferedOutputStream(
                    new FileOutputStream(file))), false){
        }

        /**
         * Constructs a new {@code PrintWriter} with {@code file} as its target. The
         * character set named {@code csn} is used for character encoding.
         * The print writer does not automatically flush its contents to the target
         * file when a newline is encountered. The output to the file is buffered.
         * 
         * @param file
         *            the target file. If the file already exists, its contents are
         *            removed, otherwise a new file is created.
         * @param csn
         *            the name of the character set used for character encoding.
         * @throws FileNotFoundException
         *             if an error occurs while opening or creating the target file.
         * @throws NullPointerException
         *             if {@code csn} is {@code null}.
         * @throws SecurityException
         *             if a security manager exists and it denies writing to the
         *             target file.
         * @throws UnsupportedEncodingException
         *             if the encoding specified by {@code csn} is not supported.
         */
        public PrintWriter(File file, String csn) //throws FileNotFoundException,
        ://UnsupportedEncodingException {
            this(new OutputStreamWriter(
                    new BufferedOutputStream(new FileOutputStream(file)), csn),
                    false){
        }

        /**
         * Constructs a new {@code PrintWriter} with the file identified by {@code
         * fileName} as its target. The virtual machine's default character set is
         * used for character encoding. The print writer does not automatically
         * flush its contents to the target file when a newline is encountered. The
         * output to the file is buffered.
         * 
         * @param fileName
         *            the target file's name. If the file already exists, its
         *            contents are removed, otherwise a new file is created.
         * @throws FileNotFoundException
         *             if an error occurs while opening or creating the target file.
         * @throws SecurityException
         *             if a security manager exists and it denies writing to the
         *             target file.
         */
        public PrintWriter(String fileName) ://throws FileNotFoundException {
            this(new OutputStreamWriter(new BufferedOutputStream(
                    new FileOutputStream(fileName))), false){
        }

         /**
         * Constructs a new {@code PrintWriter} with the file identified by {@code
         * fileName} as its target. The character set named {@code csn} is used for
         * character encoding. The print writer does not automatically flush its
         * contents to the target file when a newline is encountered. The output to
         * the file is buffered.
         * 
         * @param fileName
         *            the target file's name. If the file already exists, its
         *            contents are removed, otherwise a new file is created.
         * @param csn
         *            the name of the character set used for character encoding.
         * @throws FileNotFoundException
         *             if an error occurs while opening or creating the target file.
         * @throws NullPointerException
         *             if {@code csn} is {@code null}.
         * @throws SecurityException
         *             if a security manager exists and it denies writing to the
         *             target file.
         * @throws UnsupportedEncodingException
         *             if the encoding specified by {@code csn} is not supported.
         */
        public PrintWriter(String fileName, String csn)
               :// throws FileNotFoundException, UnsupportedEncodingException {
            this(new OutputStreamWriter(new BufferedOutputStream(
                    new FileOutputStream(fileName)), csn), false){
        }

        /**
         * Flushes this writer and returns the value of the error flag.
         * 
         * @return {@code true} if either an {@code IOException} has been thrown
         *         previously or if {@code setError()} has been called;
         *         {@code false} otherwise.
         * @see #setError()
         */
        public override bool checkError() {
            Writer delegateJ = outJ;
            if (delegateJ == null) {
                return ioError;
            }

            flush();
            return ioError || delegateJ.checkError();
        }
    
        /**
         * Sets the error state of the stream to false.
         * 
         * @since 1.6
         */
        protected void clearError() {
            lock (lockJ) {
                ioError = false;
            }
        }

        /**
         * Closes this print writer. Flushes this writer and then closes the target.
         * If an I/O error occurs, this writer's error flag is set to {@code true}.
         */
        
        public override void close() {
            lock (lockJ) {
                if (outJ != null) {
                    try {
                        outJ.close();
                    } catch (IOException e) {
                        setError();
                    }
                    outJ = null;
                }
            }
        }

        /**
         * Ensures that all pending data is sent out to the target. It also
         * flushes the target. If an I/O error occurs, this writer's error
         * state is set to {@code true}.
         */
        
        public override void flush() {
            lock (lockJ) {
                if (outJ != null) {
                    try {
                        outJ.flush();
                    } catch (IOException e) {
                        setError();
                    }
                } else {
                    setError();
                }
            }
        }

        /**
         * Writes a string formatted by an intermediate {@code Formatter} to the
         * target using the specified format string and arguments. For the locale,
         * the default value of the current virtual machine instance is used. If
         * automatic flushing is enabled then the buffer is flushed as well.
         * 
         * @param format
         *            the format string used for {@link java.util.Formatter#format}.
         * @param args
         *            the list of arguments passed to the formatter. If there are
         *            more arguments than required by the {@code format} string,
         *            then the additional arguments are ignored.
         * @return this writer.
         * @throws IllegalFormatException
         *             if the format string is illegal or incompatible with the
         *             arguments, if there are not enough arguments or if any other
         *             error regarding the format string or arguments is detected.
         * @throws NullPointerException
         *             if {@code format} is {@code null}.
         */
        public virtual PrintWriter format(String formatS, params Object[] args) {
            return format(java.util.Locale.getDefault(), formatS, args);
        }

        /**
         * Writes a string formatted by an intermediate {@code Formatter} to the
         * target using the specified locale, format string and arguments. If
         * automatic flushing is enabled then this writer is flushed.
         * 
         * @param l
         *            the locale used in the method. No localization will be applied
         *            if {@code l} is {@code null}.
         * @param format
         *            the format string used for {@link java.util.Formatter#format}.
         * @param args
         *            the list of arguments passed to the formatter. If there are
         *            more arguments than required by the {@code format} string,
         *            then the additional arguments are ignored.
         * @return this writer.
         * @throws IllegalFormatException
         *             if the format string is illegal or incompatible with the
         *             arguments, if there are not enough arguments or if any other
         *             error regarding the format string or arguments is detected.
         * @throws NullPointerException
         *             if {@code format} is {@code null}.
         */
        public virtual PrintWriter format(java.util.Locale l, String formatS, params Object[] args) {
            if (formatS == null) {
                throw new java.lang.NullPointerException("format is null"); //$NON-NLS-1$
            }
            throw new java.lang.UnsupportedOperationException("java.util.Formatter is not yet implemented");
            /*
            new java.util.Formatter(this, l).format(formatS, args);
            if (autoflush) {
                flush();
            }
            return this;
             */
        }

        /**
         * Prints a formatted string. The behavior of this method is the same as
         * this writer's {@code #format(String, Object...)} method. For the locale,
         * the default value of the current virtual machine instance is used.
         * 
         * @param format
         *            the format string used for {@link java.util.Formatter#format}.
         * @param args
         *            the list of arguments passed to the formatter. If there are
         *            more arguments than required by the {@code format} string,
         *            then the additional arguments are ignored.
         * @return this writer.
         * @throws IllegalFormatException
         *             if the format string is illegal or incompatible with the
         *             arguments, if there are not enough arguments or if any other
         *             error regarding the format string or arguments is detected.
         * @throws NullPointerException
         *             if {@code format} is {@code null}.
         */
        public virtual PrintWriter printf(String formatS, params Object[] args) {
            return format(formatS, args);
        }

        /**
         * Prints a formatted string. The behavior of this method is the same as
         * this writer's {@code #format(Locale, String, Object...)} method.
         * 
         * @param l
         *            the locale used in the method. No localization will be applied
         *            if {@code l} is {@code null}.
         * @param format
         *            the format string used for {@link java.util.Formatter#format}.
         * @param args
         *            the list of arguments passed to the formatter. If there are
         *            more arguments than required by the {@code format} string,
         *            then the additional arguments are ignored.
         * @return this writer.
         * @throws IllegalFormatException
         *             if the format string is illegal or incompatible with the
         *             arguments, if there are not enough arguments or if any other
         *             error regarding the format string or arguments is detected.
         * @throws NullPointerException
         *             if {@code format} is {@code null}.
         */
        public virtual PrintWriter printf(java.util.Locale l, String formatS, params Object[] args) {
            return format(l, formatS, args);
        }

        /**
         * Prints the string representation of the specified character array
         * to the target.
         * 
         * @param charArray
         *            the character array to print to the target.
         * @see #print(String)
         */
        public virtual void print(char[] charArray) {
            print(new String(charArray, 0, charArray.Length));
        }

        /**
         * Prints the string representation of the specified character to the
         * target.
         * 
         * @param ch
         *            the character to print to the target.
         * @see #print(String)
         */
        public virtual void print(char ch) {
            print(""+ch);
        }

        /**
         * Prints the string representation of the specified double to the target.
         * 
         * @param dnum
         *            the double value to print to the target.
         * @see #print(String)
         */
        public virtual void print(double dnum) {
            print(java.lang.StringJ.valueOf(dnum));
        }

        /**
         * Prints the string representation of the specified float to the target.
         * 
         * @param fnum
         *            the float value to print to the target.
         * @see #print(String)
         */
        public virtual void print(float fnum) {
            print(java.lang.StringJ.valueOf(fnum));
        }

        /**
         * Prints the string representation of the specified integer to the target.
         * 
         * @param inum
         *            the integer value to print to the target.
         * @see #print(String)
         */
        public void print(int inum) {
            print(java.lang.StringJ.valueOf(inum));
        }

        /**
         * Prints the string representation of the specified long to the target.
         * 
         * @param lnum
         *            the long value to print to the target.
         * @see #print(String)
         */
        public void print(long lnum) {
            print(java.lang.StringJ.valueOf(lnum));
        }

        /**
         * Prints the string representation of the specified object to the target.
         * 
         * @param obj
         *            the object to print to the target.
         * @see #print(String)
         */
        public void print(Object obj) {
            print(java.lang.StringJ.valueOf(obj));
        }

        /// <summary>
        /// Prints a string to the target. The string is converted to an array of 
        ///  bytes using the encoding chosen during the construction of this writer.
        ///  The bytes are then written to the target with {@code write(int)}.
        ///  <para>
        ///  If an I/O error occurs, this writer's error flag is set to <code>true</code>.
        ///  </para>
        /// </summary>
        /// <param name="str">the string to print to the target.</param>
        /// <see cref="write(int)"/>
        public void print(String str) {
            write(str != null ? str : java.lang.StringJ.valueOf((Object) null));
        }

        /**
         * Prints the string representation of the specified boolean to the target.
         * 
         * @param bool
         *            the boolean value to print the target.
         * @see #print(String)
         */
        public void print(bool boolean) {
            print(java.lang.StringJ.valueOf(boolean));
        }

        /**
         * Prints the string representation of the system property {@code
         * "line.separator"} to the target. Flushes this writer if the autoflush
         * flag is set to {@code true}.
         */
        public void println() {
            lock (lockJ) {
                print(lineSeparator);
                if (autoflush) {
                    flush();
                }
            }
        }

        /**
         * Prints the string representation of the specified character array
         * followed by the system property {@code "line.separator"} to the target.
         * Flushes this writer if the autoflush flag is set to {@code true}.
         * 
         * @param charArray
         *            the character array to print to the target.
         * @see #print(String)
         */
        public void println(char[] charArray) {
            println(new String(charArray, 0, charArray.Length));
        }

        /**
         * Prints the string representation of the specified character followed by
         * the system property {@code "line.separator"} to the target. Flushes this
         * writer if the autoflush flag is set to {@code true}.
         * 
         * @param ch
         *            the character to print to the target.
         * @see #print(String)
         */
        public void println(char ch) {
            println(java.lang.StringJ.valueOf(ch));
        }

        /**
         * Prints the string representation of the specified double followed by the
         * system property {@code "line.separator"} to the target. Flushes this
         * writer if the autoflush flag is set to {@code true}.
         * 
         * @param dnum
         *            the double value to print to the target.
         * @see #print(String)
         */
        public void println(double dnum) {
            println(java.lang.StringJ.valueOf(dnum));
        }

        /**
         * Prints the string representation of the specified float followed by the
         * system property {@code "line.separator"} to the target. Flushes this
         * writer if the autoflush flag is set to {@code true}.
         * 
         * @param fnum
         *            the float value to print to the target.
         * @see #print(String)
         */
        public void println(float fnum) {
            println(java.lang.StringJ.valueOf(fnum));
        }

        /**
         * Prints the string representation of the specified integer followed by the
         * system property {@code "line.separator"} to the target. Flushes this
         * writer if the autoflush flag is set to {@code true}.
         * 
         * @param inum
         *            the integer value to print to the target.
         * @see #print(String)
         */
        public void println(int inum) {
            println(java.lang.StringJ.valueOf(inum));
        }

        /**
         * Prints the string representation of the specified long followed by the
         * system property {@code "line.separator"} to the target. Flushes this
         * writer if the autoflush flag is set to {@code true}.
         * 
         * @param lnum
         *            the long value to print to the target.
         * @see #print(String)
         */
        public void println(long lnum) {
            println(java.lang.StringJ.valueOf(lnum));
        }

        /**
         * Prints the string representation of the specified object followed by the
         * system property {@code "line.separator"} to the target. Flushes this
         * writer if the autoflush flag is set to {@code true}.
         * 
         * @param obj
         *            the object to print to the target.
         * @see #print(String)
         */
        public void println(Object obj) {
            println(java.lang.StringJ.valueOf(obj));
        }

        /**
         * Prints a string followed by the system property {@code "line.separator"}
         * to the target. The string is converted to an array of bytes using the
         * encoding chosen during the construction of this writer. The bytes are
         * then written to the target with {@code write(int)}. Finally, this writer
         * is flushed if the autoflush flag is set to {@code true}.
         * <p>
         * If an I/O error occurs, this writer's error flag is set to {@code true}.
         *</p>
         * @param str
         *            the string to print to the target.
         * @see #write(int)
         */
        public void println(String str) {
            lock (lockJ) {
                print(str);
                println();
            }
        }

        /**
         * Prints the string representation of the specified boolean followed by the
         * system property {@code "line.separator"} to the target. Flushes this
         * writer if the autoflush flag is set to {@code true}.
         * 
         * @param bool
         *            the boolean value to print to the target.
         * @see #print(String)
         */
        public void println(bool boolean) {
            println(java.lang.StringJ.valueOf(boolean));
        }

        /**
         * Sets the error flag of this writer to {@code true}.
         */
        protected void setError() {
            lock (lockJ) {
                ioError = true;
            }
        }

        /**
         * Writes the character buffer {@code buf} to the target.
         * 
         * @param buf
         *            the non-null array containing characters to write.
         */
        public override void write(char[] buf) {
            write(buf, 0, buf.Length);
        }

        /**
         * Writes {@code count} characters from {@code buffer} starting at {@code
         * offset} to the target.
         * <p>
         * This writer's error flag is set to {@code true} if this writer is closed
         * or an I/O error occurs.
         *</p>
         * @param buf
         *            the buffer to write to the target.
         * @param offset
         *            the index of the first character in {@code buffer} to write.
         * @param count
         *            the number of characters in {@code buffer} to write.
         * @throws IndexOutOfBoundsException
         *             if {@code offset &lt; 0} or {@code count &lt; 0}, or if {@code
         *             offset + count} is greater than the length of {@code buf}.
         */

        public override void write(char[] buf, int offset, int count) {
            doWrite(buf, offset, count);
        }

        /**
         * Writes one character to the target. Only the two least significant bytes
         * of the integer {@code oneChar} are written.
         * <p>
         * This writer's error flag is set to {@code true} if this writer is closed
         * or an I/O error occurs.
         * </p>
         * @param oneChar
         *            the character to write to the target.
         */
        
        public override void write(int oneChar) {
            doWrite(new char[] { (char) oneChar }, 0, 1);
        }

        private  void doWrite(char[] buf, int offset, int count) {
            lock (lockJ) {
                if (outJ != null) {
                    try {
                        outJ.write(buf, offset, count);
                    } catch (IOException e) {
                        setError();
                    }
                } else {
                    setError();
                }
            }
        }

        /**
         * Writes the characters from the specified string to the target.
         * 
         * @param str
         *            the non-null string containing the characters to write.
         */
        
        public override void write(String str) {
            write(str.toCharArray());
        }

        /**
         * Writes {@code count} characters from {@code str} starting at {@code
         * offset} to the target.
         * 
         * @param str
         *            the non-null string containing the characters to write.
         * @param offset
         *            the index of the first character in {@code str} to write.
         * @param count
         *            the number of characters from {@code str} to write.
         * @throws IndexOutOfBoundsException
         *             if {@code offset &lt; 0} or {@code count &lt; 0}, or if {@code
         *             offset + count} is greater than the length of {@code str}.
         */

        public override void write(String str, int offset, int count) {
            write(str.substring(offset, offset + count).toCharArray());
        }

        /**
         * Appends the character {@code c} to the target.
         * 
         * @param c
         *            the character to append to the target.
         * @return this writer.
         */
        
        public new PrintWriter append(char c) {
            write(c);
            return this;
        }

        /**
         * Appends the character sequence {@code csq} to the target. This
         * method works the same way as {@code PrintWriter.print(csq.toString())}.
         * If {@code csq} is {@code null}, then the string "null" is written
         * to the target.
         * 
         * @param csq
         *            the character sequence appended to the target.
         * @return this writer.
         */
        
        public new PrintWriter append(java.lang.CharSequence csq) {
            if (null == csq) {
                append(new java.lang.StringJ(TOKEN_NULL), 0, TOKEN_NULL.length());
            } else {
                append(csq, 0, csq.length());
            }
            return this;
        }

        /**
         * Appends a subsequence of the character sequence {@code csq} to the
         * target. This method works the same way as {@code
         * PrintWriter.print(csq.subsequence(start, end).toString())}. If {@code
         * csq} is {@code null}, then the specified subsequence of the string "null"
         * will be written to the target.
         * 
         * @param csq
         *            the character sequence appended to the target.
         * @param start
         *            the index of the first char in the character sequence appended
         *            to the target.
         * @param end
         *            the index of the character following the last character of the
         *            subsequence appended to the target.
         * @return this writer.
         * @throws StringIndexOutOfBoundsException
         *             if {@code start > end}, {@code start &lt; 0}, {@code end &lt; 0} or
         *             either {@code start} or {@code end} are greater or equal than
         *             the length of {@code csq}.
         */
        
        public new PrintWriter append(java.lang.CharSequence csq, int start, int end) {
            if (null == csq) {
                csq = new java.lang.StringJ(TOKEN_NULL);
            }
            String output = csq.subSequence(start, end).toString();
            write(output, 0, output.length());
            return this;
        }
    }
}
