using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt.image
{


    /**
     * @author Igor V. Stolyarov
     */
    public class DirectColorModel : PackedColorModel
    {
        public DirectColorModel(int bits, int rmask, int gmask, int bmask,
                int amask)
            : base(java.awt.color.ColorSpace.getInstance(java.awt.color.ColorSpace.CS_sRGB), bits, rmask, gmask,
                bmask, amask, false,
                (amask == 0 ? TransparencyConstants.OPAQUE : TransparencyConstants.TRANSLUCENT),
                ColorModel.getTransferType(bits))
        {

            initLUTs();
        }
        internal DirectColorModel(int bits, int rmask, int gmask, int bmask, long amask)
            : base(java.awt.color.ColorSpace.getInstance(java.awt.color.ColorSpace.CS_sRGB), bits, rmask, gmask,
                bmask, amask, false,
                (amask == 0 ? TransparencyConstants.OPAQUE : TransparencyConstants.TRANSLUCENT),
                ColorModel.getTransferType(bits))
        {

            initLUTs();
        }

        public DirectColorModel(int bits, int rmask, int gmask, int bmask) :
            this(bits, rmask, gmask, bmask, 0)
        {
        }

        /**
         * Initialization of Lookup tables
         */
        private void initLUTs()
        {
            //!++ TODO implement
            throw new java.lang.UnsupportedOperationException("Not yet implemented");
        }

    public override int getRed(Object inData) {
        int pixel = 0;
        switch (transferType) {
        case DataBuffer.TYPE_BYTE:
            byte[] ba = (byte[]) inData;
            pixel = ba[0] & 0xff;
            break;

        case DataBuffer.TYPE_USHORT:
            short[] sa = (short[]) inData;
            pixel = sa[0] & 0xffff;
            break;

        case DataBuffer.TYPE_INT:
            int [] ia = (int[]) inData;
            pixel = ia[0];
            break;

        default:
            // awt.214=This Color Model doesn't support this transferType
            throw new java.lang.UnsupportedOperationException("This Color Model doesn't support this transferType"); //$NON-NLS-1$
        }
        return getRed(pixel);
    }


        /// <summary>
        /// ColorModel has sRGB ColorSpace
        /// </summary>
    private bool is_sRGB;            
        /// <summary>
        /// Color Model has Linear RGB Color Space
        /// </summary>
    private bool is_LINEAR_RGB;     

       public override int getRed(int pixel)
    {
           //!++ TODO implement
        throw new java.lang.UnsupportedOperationException("Not yet implemented");
    }

       public override int getGreen(int pixel)
       {
           //!++ TODO implement
           throw new java.lang.UnsupportedOperationException("Not yet implemented");
       }

       public override int getBlue(int pixel)
       {
           //!++ TODO implement
           throw new java.lang.UnsupportedOperationException("Not yet implemented");
       }
       public override int getAlpha(int pixel)
       {
           //!++ TODO implement
           throw new java.lang.UnsupportedOperationException("Not yet implemented");
       }

    }
}