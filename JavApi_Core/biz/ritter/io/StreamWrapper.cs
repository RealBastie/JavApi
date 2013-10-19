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
 *  Copyright © 2011 Sebastian Ritter
 */
using System;
using java = biz.ritter.javapi;

namespace biz.ritter.io
{
    /// <summary>
    /// Wrap java.io.InputStream or java.io.OutputStream as System.IO.Stream
    /// </summary>
    public class StreamWrapper : System.IO.Stream
    {

        private java.io.InputStream input;
        private java.io.OutputStream output;

        public StreamWrapper(java.io.InputStream input, java.io.OutputStream output)
        {
            this.input = input;
            this.output = output;
        }

        public override bool CanRead
        {
            get { return input != null; }
        }

        public override bool CanSeek
        {
            get { return input != null;}
        }

        public override bool CanWrite
        {
            get { return output != null; }
        }

        public override void Flush()
        {
            this.output.flush();
        }

        public override long Length
        {
            get { return this.input.available(); }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.input.read(buffer, offset, count);
        }

        public override long Seek(long offset, System.IO.SeekOrigin origin)
        {
            if (origin.Equals(System.IO.SeekOrigin.Current))
            {
                return this.input.skip(offset);
            }
            else
            {
                throw new java.lang.IllegalArgumentException(new java.lang.UnsupportedOperationException("Only seek from current position yet implemented"));
            }
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.output.write(buffer, offset, count);
        }
    }
}
