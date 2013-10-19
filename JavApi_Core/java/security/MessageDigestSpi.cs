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

namespace biz.ritter.javapi.security
{

    /**
     * {@code MessageDigestSpi} is the Service Provider Interface (SPI) definition
     * for {@link MessageDigest}. Examples of digest algorithms are MD5 and SHA. A
     * digest is a secure one way hash function for a stream of bytes. It acts like
     * a fingerprint for a stream of bytes.
     * 
     * @see MessageDigest
     */
    public abstract class MessageDigestSpi
    {

        /**
         * Returns the engine digest length in bytes. If the implementation does not
         * implement this function {@code 0} is returned.
         * 
         * @return the digest length in bytes, or {@code 0}.
         */
        protected internal virtual int engineGetDigestLength()
        {
            return 0;
        }

        /**
         * Updates this {@code MessageDigestSpi} using the given {@code byte}.
         * 
         * @param input
         *            the {@code byte} to update this {@code MessageDigestSpi} with.
         * @see #engineReset()
         */
        protected internal abstract void engineUpdate(byte input);

        /**
         * Updates this {@code MessageDigestSpi} using the given {@code byte[]}.
         * 
         * @param input
         *            the {@code byte} array.
         * @param offset
         *            the index of the first byte in {@code input} to update from.
         * @param len
         *            the number of bytes in {@code input} to update from.
         * @throws IllegalArgumentException
         *             if {@code offset} or {@code len} are not valid in respect to
         *             {@code input}.
         */
        protected internal abstract void engineUpdate(byte[] input, int offset, int len);

        /**
         * Updates this {@code MessageDigestSpi} using the given {@code input}.
         * 
         * @param input
         *            the {@code ByteBuffer}.
         */
        protected internal virtual void engineUpdate(java.nio.ByteBuffer input)
        {
            if (!input.hasRemaining())
            {
                return;
            }
            byte[] tmp;
            if (input.hasArray())
            {
                tmp = (byte[])input.array();
                int offset = input.arrayOffset();
                int position = input.position();
                int limit = input.limit();
                engineUpdate(tmp, offset + position, limit - position);
                input.position(limit);
            }
            else
            {
                tmp = new byte[input.limit() - input.position()];
                input.get(tmp);
                engineUpdate(tmp, 0, tmp.Length);
            }
        }

        /**
         * Computes and returns the final hash value for this
         * {@link MessageDigestSpi}. After the digest is computed the receiver is
         * reset.
         * 
         * @return the computed one way hash value.
         * @see #engineReset()
         */
        protected internal abstract byte[] engineDigest();

        /**
         * Computes and stores the final hash value for this
         * {@link MessageDigestSpi}. After the digest is computed the receiver is
         * reset.
         * 
         * @param buf
         *            the buffer to store the result in.
         * @param offset
         *            the index of the first byte in {@code buf} to store in.
         * @param len
         *            the number of bytes allocated for the digest.
         * @return the number of bytes written to {@code buf}.
         * @throws DigestException
         *             if an error occures.
         * @throws IllegalArgumentException
         *             if {@code offset} or {@code len} are not valid in respect to
         *             {@code buf}.
         * @see #engineReset()
         */
        protected internal virtual int engineDigest(byte[] buf, int offset, int len)
        {//throws DigestException {
            if (len < engineGetDigestLength())
            {
                engineReset();
                throw new DigestException("The value of len parameter is less than the actual digest length.");  //$NON-NLS-1$
            }
            if (offset < 0)
            {
                engineReset();
                throw new DigestException("Invalid negative offset"); //$NON-NLS-1$
            }
            if (offset + len > buf.Length)
            {
                engineReset();
                throw new DigestException("Incorrect offset or len value"); //$NON-NLS-1$
            }
            byte[] tmp = engineDigest();
            if (len < tmp.Length)
            {
                throw new DigestException("The value of len parameter is less than the actual digest length.");  //$NON-NLS-1$
            }
            java.lang.SystemJ.arraycopy(tmp, 0, buf, offset, tmp.Length);
            return tmp.Length;
        }

        /**
         * Puts this {@code MessageDigestSpi} back in an initial state, such that it
         * is ready to compute a one way hash value.
         */
        protected internal abstract void engineReset();


        public virtual Object clone()
        {// throws CloneNotSupportedException {
            return base.MemberwiseClone();// super.clone();
        }
    }
}