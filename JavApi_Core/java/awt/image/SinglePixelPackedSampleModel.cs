using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt.image
{
    /**
     * @author Igor V. Stolyarov
     */
    public class SinglePixelPackedSampleModel : SampleModel
    {

        private int[] bitMasks;

        private int[] bitOffsets;

        private int[] bitSizes;

        private int scanlineStride;

        private int maxBitSize;

        public SinglePixelPackedSampleModel(int dataType, int w, int h,
                int scanlineStride, int[] bitMasks) :

            base(dataType, w, h, bitMasks.Length)
        {

            if (dataType != DataBuffer.TYPE_BYTE &&
                    dataType != DataBuffer.TYPE_USHORT &&
                    dataType != DataBuffer.TYPE_INT)
            {
                // awt.61=Unsupported data type: {0}
                throw new java.lang.IllegalArgumentException("Unsupported data type: " + dataType);
            }

            this.scanlineStride = scanlineStride;
            this.bitMasks = java.util.Arrays<Object>.copy(bitMasks);
            this.bitOffsets = new int[this.numBands];
            this.bitSizes = new int[this.numBands];

            this.maxBitSize = 0;

            for (int i = 0; i < this.numBands; i++)
            {
                int offset = 0;
                int size = 0;
                int mask = bitMasks[i];

                if (mask != 0)
                {
                    while ((mask & 1) == 0)
                    {
                        mask = java.dotnet.lang.Operator.shiftRightUnsignet(mask, 1);
                        offset++;
                    }

                    while ((mask & 1) == 1)
                    {
                        mask = java.dotnet.lang.Operator.shiftRightUnsignet(mask, 1);
                        size++;
                    }

                    if (mask != 0)
                    {
                        // awt.62=Wrong mask : {0}
                        throw new java.lang.IllegalArgumentException("Wrong mask : " + bitMasks[i]); //$NON-NLS-1$
                    }
                }

                this.bitOffsets[i] = offset;
                this.bitSizes[i] = size;

                if (this.maxBitSize < size)
                {
                    this.maxBitSize = size;
                }

            }

        }

        public override DataBuffer createDataBuffer()
        {
            DataBuffer data = null;
            int size = (this.height - 1) * scanlineStride + width;

            switch (this.dataType)
            {
                case DataBuffer.TYPE_BYTE:
                    data = new DataBufferByte(size);
                    break;
                case DataBuffer.TYPE_USHORT:
                    data = new DataBufferUShort(size);
                    break;
                case DataBuffer.TYPE_INT:
                    data = new DataBufferInt(size);
                    break;
            }
            return data;
        }

        public override void setSample(int x, int y, int b, int s, DataBuffer data)
        {
            if (x < 0 || y < 0 || x >= this.width || y >= this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }
            int tmp = data.getElem(y * scanlineStride + x);
            tmp &= ~this.bitMasks[b];
            tmp |= (s << this.bitOffsets[b]) & this.bitMasks[b];
            data.setElem(y * scanlineStride + x, tmp);
        }

        public override SampleModel createSubsetSampleModel(int[] bands)
        {
            if (bands.Length > this.numBands)
            {
                // awt.64=The number of the bands in the subset is greater than the number of bands in the sample model
                throw new RasterFormatException("The number of the bands in the subset is greater than the number of bands in the sample model"); //$NON-NLS-1$
            }

            int[] masks = new int[bands.Length];
            for (int i = 0; i < bands.Length; i++)
            {
                masks[i] = this.bitMasks[bands[i]];
            }
            return new SinglePixelPackedSampleModel(this.dataType, this.width,
                    this.height, this.scanlineStride, masks);
        }




        public override int getSampleSize(int band)
        {
            return bitSizes[band];
        }


        public override int[] getSampleSize()
        {
            return java.util.Arrays<Object>.copy(this.bitSizes); //bitSizes.clone();
        }
        public SinglePixelPackedSampleModel(int dataType, int w, int h,
                int[] bitMasks) :
            this(dataType, w, h, w, bitMasks)
        {
        }

        public override SampleModel createCompatibleSampleModel(int w, int h)
        {
            return new SinglePixelPackedSampleModel(this.dataType, w, h,
                    this.bitMasks);
        }

        public override int[] getPixel(int x, int y, int[] iArray, DataBuffer data)
        {
            if (x < 0 || y < 0 || x >= this.width || y >= this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }
            int[] pixel;
            if (iArray == null)
            {
                pixel = new int[this.numBands];
            }
            else
            {
                pixel = iArray;
            }

            for (int i = 0; i < this.numBands; i++)
            {
                pixel[i] = getSample(x, y, i, data);
            }

            return pixel;
        }

        public override void setPixel(int x, int y, int[] iArray, DataBuffer data)
        {
            if (x < 0 || y < 0 || x >= this.width || y >= this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }
            for (int i = 0; i < this.numBands; i++)
            {
                setSample(x, y, i, iArray[i], data);
            }
        }

        public int getScanlineStride()
        {
            return this.scanlineStride;
        }

        public override int getNumDataElements()
        {
            return 1;
        }

        public override int getSample(int x, int y, int b, DataBuffer data)
        {
            if (x < 0 || y < 0 || x >= this.width || y >= this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }
            int sample = data.getElem(y * scanlineStride + x);
            return java.dotnet.lang.Operator.shiftRightUnsignet((sample & this.bitMasks[b]), this.bitOffsets[b]);
        }

        public override void setDataElements(int x, int y, Object obj, DataBuffer data)
        {
            if (x < 0 || y < 0 || x >= this.width || y >= this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }
            switch (getTransferType())
            {
                case DataBuffer.TYPE_BYTE:
                    data.setElem(y * scanlineStride + x, ((byte[])obj)[0] & 0xff);
                    break;
                case DataBuffer.TYPE_USHORT:
                    data.setElem(y * scanlineStride + x, ((short[])obj)[0] & 0xffff);
                    break;
                case DataBuffer.TYPE_INT:
                    data.setElem(y * scanlineStride + x, ((int[])obj)[0]);
                    break;
            }
        }
        public override Object getDataElements(int x, int y, Object obj, DataBuffer data)
        {
            if (x < 0 || y < 0 || x >= this.width || y >= this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }
            switch (getTransferType())
            {
                case DataBuffer.TYPE_BYTE:
                    byte[] bdata;
                    if (obj == null)
                    {
                        bdata = new byte[1];
                    }
                    else
                    {
                        bdata = (byte[])obj;
                    }

                    bdata[0] = (byte)data.getElem(y * scanlineStride + x);
                    obj = bdata;
                    break;
                case DataBuffer.TYPE_USHORT:
                    short[] sdata;
                    if (obj == null)
                    {
                        sdata = new short[1];
                    }
                    else
                    {
                        sdata = (short[])obj;
                    }

                    sdata[0] = (short)data.getElem(y * scanlineStride + x);
                    obj = sdata;
                    break;
                case DataBuffer.TYPE_INT:
                    int[] idata;
                    if (obj == null)
                    {
                        idata = new int[1];
                    }
                    else
                    {
                        idata = (int[])obj;
                    }

                    idata[0] = data.getElem(y * scanlineStride + x);
                    obj = idata;
                    break;
            }
            return obj;
        }
    public override bool Equals(Object o) {
        if ((o == null) || !(o is SinglePixelPackedSampleModel)) {
            return false;
        }

        SinglePixelPackedSampleModel model = (SinglePixelPackedSampleModel) o;
        return this.width == model.width &&
                this.height == model.height &&
                this.numBands == model.numBands &&
                this.dataType == model.dataType &&
                java.util.Arrays<Object>.equals(this.bitMasks, model.bitMasks) &&
                java.util.Arrays<Object>.equals(this.bitOffsets, model.bitOffsets) &&
                java.util.Arrays<Object>.equals(this.bitSizes, model.bitSizes) &&
                this.scanlineStride == model.scanlineStride;
    }


    public override int GetHashCode() {
        int hash = 0;
        int tmp = 0;

        hash = width;
        tmp = java.dotnet.lang.Operator.shiftRightUnsignet(hash, 24);
        hash <<= 8;
        hash |= tmp;
        hash ^= height;
        tmp = java.dotnet.lang.Operator.shiftRightUnsignet(hash, 24);
        hash <<= 8;
        hash |= tmp;
        hash ^= numBands;
        tmp = java.dotnet.lang.Operator.shiftRightUnsignet(hash, 24);
        hash <<= 8;
        hash |= tmp;
        hash ^= dataType;
        tmp = java.dotnet.lang.Operator.shiftRightUnsignet(hash, 24);
        hash <<= 8;
        hash |= tmp;
        foreach (int element in bitMasks) {
            hash ^= element;
            tmp = java.dotnet.lang.Operator.shiftRightUnsignet(hash, 24);
            hash <<= 8;
            hash |= tmp;
        }
        foreach (int element in bitOffsets) {
            hash ^= element;
            tmp = java.dotnet.lang.Operator.shiftRightUnsignet(hash, 24);
            hash <<= 8;
            hash |= tmp;
        }
        foreach (int element in bitSizes) {
            hash ^= element;
            tmp = java.dotnet.lang.Operator.shiftRightUnsignet(hash, 24);
            hash <<= 8;
            hash |= tmp;
        }
        hash ^= scanlineStride;
        return hash;
    }

    }

}