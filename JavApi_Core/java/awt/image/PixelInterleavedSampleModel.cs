using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt.image
{
    /**
     * @author Igor V. Stolyarov
     */
    public class PixelInterleavedSampleModel : ComponentSampleModel
    {
        public PixelInterleavedSampleModel(int dataType, int w, int h,
                int pixelStride, int scanlineStride, int[] bandOffsets) :

            base(dataType, w, h, pixelStride, scanlineStride, bandOffsets)
        {

            int maxOffset = bandOffsets[0];
            int minOffset = bandOffsets[0];
            for (int i = 1; i < bandOffsets.Length; i++)
            {
                if (bandOffsets[i] > maxOffset)
                {
                    maxOffset = bandOffsets[i];
                }
                if (bandOffsets[i] < minOffset)
                {
                    minOffset = bandOffsets[i];
                }
            }

            maxOffset -= minOffset;

            if (maxOffset > scanlineStride)
            {
                // awt.241=Any offset between bands is greater than the Scanline stride
                throw new java.lang.IllegalArgumentException("Any offset between bands is greater than the Scanline stride"); //$NON-NLS-1$
            }

            if (maxOffset > pixelStride)
            {
                // awt.242=Pixel stride is less than any offset between bands
                throw new java.lang.IllegalArgumentException("Pixel stride is less than any offset between bands"); //$NON-NLS-1$
            }

            if (pixelStride * w > scanlineStride)
            {
                // awt.243=Product of Pixel stride and w is greater than Scanline stride
                throw new java.lang.IllegalArgumentException("Product of Pixel stride and w is greater than Scanline stride"); //$NON-NLS-1$
            }

        }

    }
}