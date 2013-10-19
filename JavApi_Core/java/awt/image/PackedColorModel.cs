using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt.image
{
    /**
     * @author Igor V. Stolyarov
     */
    public abstract class PackedColorModel : ColorModel
    {
        public PackedColorModel(java.awt.color.ColorSpace space, int bits, int rmask, int gmask,
                int bmask, int amask, bool isAlphaPremultiplied, int trans,
                int transferType)
            : base(bits, createBits(rmask, gmask, bmask, amask), space,
                (amask == 0 ? false : true), isAlphaPremultiplied, trans,
                validateTransferType(transferType))
        {
            //!++ TODO implement
            throw new java.lang.UnsupportedOperationException("Not yet implemented");
        }

        internal PackedColorModel(java.awt.color.ColorSpace space, int bits, int rmask, int gmask,
                int bmask, long amask, bool isAlphaPremultiplied, int trans,
                int transferType)
            : base(bits, createBits(rmask, gmask, bmask, amask), space,
                (amask == 0 ? false : true), isAlphaPremultiplied, trans,
                validateTransferType(transferType))
        {
            //!++ TODO implement
            throw new java.lang.UnsupportedOperationException("Not yet implemented");
        }
        
        private static int[] createBits(int[] colorMaskArray, int alphaMask)
        {
            int[] bits;
            int numComp;
            if (alphaMask == 0)
            {
                numComp = colorMaskArray.Length;
            }
            else
            {
                numComp = colorMaskArray.Length + 1;
            }

            bits = new int[numComp];
            int i = 0;
            for (; i < colorMaskArray.Length; i++)
            {
                bits[i] = countCompBits(colorMaskArray[i]);
                if (bits[i] < 0)
                {
                    // awt.23B=The mask of the {0} component is not contiguous
                    throw new java.lang.IllegalArgumentException("The mask of the " + i + " component is not contiguous"); //$NON-NLS-1$
                }
            }

            if (i < numComp)
            {
                bits[i] = countCompBits(alphaMask);

                if (bits[i] < 0)
                {
                    // awt.23C=The mask of the alpha component is not contiguous
                    throw new java.lang.IllegalArgumentException("The mask of the alpha component is not contiguous"); //$NON-NLS-1$
                }
            }

            return bits;
        }

        private static int[] createBits(int rmask, int gmask, int bmask,int amask)
        {

            int numComp;
            if (amask == 0)
            {
                numComp = 3;
            }
            else
            {
                numComp = 4;
            }
            int[] bits = new int[numComp];

            bits[0] = countCompBits(rmask);
            if (bits[0] < 0)
            {
                // awt.23D=The mask of the red component is not contiguous
                throw new java.lang.IllegalArgumentException("The mask of the red component is not contiguous"); //$NON-NLS-1$
            }

            bits[1] = countCompBits(gmask);
            if (bits[1] < 0)
            {
                // awt.23E=The mask of the green component is not contiguous
                throw new java.lang.IllegalArgumentException("The mask of the green component is not contiguous"); //$NON-NLS-1$
            }

            bits[2] = countCompBits(bmask);
            if (bits[2] < 0)
            {
                // awt.23F=The mask of the blue component is not contiguous
                throw new java.lang.IllegalArgumentException("The mask of the blue component is not contiguous"); //$NON-NLS-1$
            }

            if (amask != 0)
            {
                bits[3] = countCompBits(amask);
                if (bits[3] < 0)
                {
                    // awt.23C=The mask of the alpha component is not contiguous
                    throw new java.lang.IllegalArgumentException("The mask of the alpha component is not contiguous"); //$NON-NLS-1$
                }
            }

            return bits;
        }

        private static int[] createBits(int rmask, int gmask, int bmask, long amask)
        {

            int numComp;
            if (amask == 0)
            {
                numComp = 3;
            }
            else
            {
                numComp = 4;
            }
            int[] bits = new int[numComp];

            bits[0] = countCompBits(rmask);
            if (bits[0] < 0)
            {
                // awt.23D=The mask of the red component is not contiguous
                throw new java.lang.IllegalArgumentException("The mask of the red component is not contiguous"); //$NON-NLS-1$
            }

            bits[1] = countCompBits(gmask);
            if (bits[1] < 0)
            {
                // awt.23E=The mask of the green component is not contiguous
                throw new java.lang.IllegalArgumentException("The mask of the green component is not contiguous"); //$NON-NLS-1$
            }

            bits[2] = countCompBits(bmask);
            if (bits[2] < 0)
            {
                // awt.23F=The mask of the blue component is not contiguous
                throw new java.lang.IllegalArgumentException("The mask of the blue component is not contiguous"); //$NON-NLS-1$
            }

            if (amask != 0)
            {
                bits[3] = countCompBits(amask);
                if (bits[3] < 0)
                {
                    // awt.23C=The mask of the alpha component is not contiguous
                    throw new java.lang.IllegalArgumentException("The mask of the alpha component is not contiguous"); //$NON-NLS-1$
                }
            }

            return bits;
        }


        private static int countCompBits(int compMask)
        {
            int bits = 0;
            if (compMask != 0)
            {
                // Deleting final zeros
                while ((compMask & 1) == 0)
                {
                    compMask = java.dotnet.lang.Operator.shiftRightUnsignet(compMask, 1);//compMask >>>= 1;
                }
                // Counting component bits
                while ((compMask & 1) == 1)
                {
                    compMask = java.dotnet.lang.Operator.shiftRightUnsignet(compMask, 1); //compMask >>>= 1;
                    bits++;
                }
            }

            if (compMask != 0)
            {
                return -1;
            }

            return bits;
        }


        private static int countCompBits(long compMask)
        {
            int bits = 0;
            if (compMask != 0)
            {
                // Deleting final zeros
                while ((compMask & 1) == 0)
                {
                    compMask = java.dotnet.lang.Operator.shiftRightUnsignet(compMask, 1);//compMask >>>= 1;
                }
                // Counting component bits
                while ((compMask & 1) == 1)
                {
                    compMask = java.dotnet.lang.Operator.shiftRightUnsignet(compMask, 1); //compMask >>>= 1;
                    bits++;
                }
            }

            if (compMask != 0)
            {
                return -1;
            }

            return bits;
        }

        private static int validateTransferType(int transferType)
        {
            if (transferType != DataBuffer.TYPE_BYTE &&
                    transferType != DataBuffer.TYPE_USHORT &&
                    transferType != DataBuffer.TYPE_INT)
            {
                // awt.240=The transferType not is one of DataBuffer.TYPE_BYTE,
                //          DataBuffer.TYPE_USHORT or DataBuffer.TYPE_INT
                throw new java.lang.IllegalArgumentException("The transferType not is one of DataBuffer.TYPE_BYTE, DataBuffer.TYPE_USHORT or DataBuffer.TYPE_INT"); //$NON-NLS-1$
            }
            return transferType;
        }

    }
}