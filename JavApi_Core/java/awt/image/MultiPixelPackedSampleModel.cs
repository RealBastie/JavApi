using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt.image
{
    public class MultiPixelPackedSampleModel : SampleModel
    {
        private int pixelBitStride;

        private int scanlineStride;

        private int dataBitOffset;

        private int bitMask;

        private int dataElementSize;

        private int pixelsPerDataElement;

        public MultiPixelPackedSampleModel(int dataType, int w, int h,int numberOfBits, int scanlineStride, int dataBitOffset)
        : base (dataType, w, h, 1){
            if (dataType != DataBuffer.TYPE_BYTE &&
                   dataType != DataBuffer.TYPE_USHORT &&
                   dataType != DataBuffer.TYPE_INT)
            {
                // awt.61=Unsupported data type: {0}
                throw new java.lang.IllegalArgumentException("Unsupported data type: "+dataType);
            }

            this.scanlineStride = scanlineStride;
            if (numberOfBits == 0)
            {
                // awt.20C=Number of Bits equals to zero
                throw new RasterFormatException("Number of Bits equals to zero"); //$NON-NLS-1$
            }
            this.pixelBitStride = numberOfBits;
            this.dataElementSize = DataBuffer.getDataTypeSize(dataType);
            if (dataElementSize % pixelBitStride != 0)
            {
                // awt.20D=The number of bits per pixel is not a power of 2 or pixels span data element boundaries
                throw new RasterFormatException("The number of bits per pixel is not a power of 2 or pixels span data element boundaries"); //$NON-NLS-1$
            }

            if (dataBitOffset % numberOfBits != 0)
            {
                // awt.20E=Data Bit offset is not a multiple of pixel bit stride
                throw new RasterFormatException("Data Bit offset is not a multiple of pixel bit stride"); //$NON-NLS-1$
            }
            this.dataBitOffset = dataBitOffset;

            this.pixelsPerDataElement = dataElementSize / pixelBitStride;
            this.bitMask = (1 << numberOfBits) - 1;
        }

        public MultiPixelPackedSampleModel(int dataType, int w, int h,int numberOfBits)
        :

            this(dataType, w, h, numberOfBits, (numberOfBits * w +
                   DataBuffer.getDataTypeSize(dataType) - 1) /
                   DataBuffer.getDataTypeSize(dataType), 0){
        }

        public int getDataBitOffset()
        {
            return dataBitOffset;
        }

        public int getScanlineStride()
        {
            return scanlineStride;
        }

        public override int getNumDataElements()
        {
            return 1;
        }

        public override int getSampleSize(int band)
        {
            return pixelBitStride;
        }

        public override int[] getSampleSize()
        {
            int[] sampleSizes = { pixelBitStride };
            return sampleSizes;
        }

        public override int GetHashCode()
        {
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
            hash ^= scanlineStride;
            tmp = java.dotnet.lang.Operator.shiftRightUnsignet(hash, 24);
            hash <<= 8;
            hash |= tmp;
            hash ^= pixelBitStride;
            tmp = java.dotnet.lang.Operator.shiftRightUnsignet(hash, 24);
            hash <<= 8;
            hash |= tmp;
            hash ^= dataBitOffset;
            tmp = java.dotnet.lang.Operator.shiftRightUnsignet(hash, 24);
            hash <<= 8;
            hash |= tmp;
            hash ^= bitMask;
            tmp = java.dotnet.lang.Operator.shiftRightUnsignet(hash, 24);
            hash <<= 8;
            hash |= tmp;
            hash ^= dataElementSize;
            tmp = java.dotnet.lang.Operator.shiftRightUnsignet(hash, 24);
            hash <<= 8;
            hash |= tmp;
            hash ^= pixelsPerDataElement;
            return hash;
        }


        public override void setSample(int x, int y, int b, int s, DataBuffer data)
        {
            if (b != 0)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }

            setSample(x, y, null, data, 3, s);
        }


        public override DataBuffer createDataBuffer()
        {
            DataBuffer dataBuffer = null;
            int size = scanlineStride * height;

            switch (dataType)
            {
                case DataBuffer.TYPE_BYTE:
                    dataBuffer = new DataBufferByte(size + (dataBitOffset + 7) / 8);
                    break;
                case DataBuffer.TYPE_USHORT:
                    dataBuffer = new DataBufferUShort(size + (dataBitOffset + 15) / 16);
                    break;
                case DataBuffer.TYPE_INT:
                    dataBuffer = new DataBufferInt(size + (dataBitOffset + 31) / 32);
                    break;
            }
            return dataBuffer;
        }

        public override int getTransferType()
        {
            if (pixelBitStride > 16)
            {
                return DataBuffer.TYPE_INT;
            }
            else if (pixelBitStride > 8)
            {
                return DataBuffer.TYPE_USHORT;
            }
            else
            {
                return DataBuffer.TYPE_BYTE;
            }
        }

        /**
         * This method is used by other methods of this class. The behaviour of
         * this method depends on the method which has been invoke this one. The
         * argument methodId is used to choose valid behaviour in a particular case.
         * If methodId is equal to 1 it means that this method has been invoked by
         * the setDataElements() method, 2 - means setPixel(), and setSample() in
         * any other cases.
         */
        private void setSample(int x, int y, Object obj,
                 DataBuffer data, int methodId, int s)
        {
            if ((x < 0) || (y < 0) || (x >= this.width) || (y >= this.height))
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }

            int bitnum = dataBitOffset + x * pixelBitStride;
            int idx = y * scanlineStride + bitnum / dataElementSize;
            int shift = dataElementSize - (bitnum & (dataElementSize - 1))
                    - pixelBitStride;
            int mask = ~(bitMask << shift);
            int elem = data.getElem(idx);

            switch (methodId)
            {
                case 1:
                    {                        // Invoked from setDataElements()
                        switch (getTransferType())
                        {
                            case DataBuffer.TYPE_BYTE:
                                s = ((byte[])obj)[0] & 0xff;
                                break;
                            case DataBuffer.TYPE_USHORT:
                                s = ((short[])obj)[0] & 0xffff;
                                break;
                            case DataBuffer.TYPE_INT:
                                s = ((int[])obj)[0];
                                break;
                        }
                        break;
                    }
                case 2:
                    {                        // Invoked from setPixel()
                        s = ((int[])obj)[0];
                        break;
                    }
            }

            elem &= mask;
            elem |= (s & bitMask) << shift;
            data.setElem(idx, elem);
        }
        public override int getSample(int x, int y, int b, DataBuffer data)
        {
            if (x < 0 || y < 0 || x >= this.width || y >= this.height || b != 0)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }

            int bitnum = dataBitOffset + x * pixelBitStride;
            int elem = data.getElem(y * scanlineStride + bitnum / dataElementSize);
            int shift = dataElementSize - (bitnum & (dataElementSize - 1)) -
                    pixelBitStride;

            return (elem >> shift) & bitMask;
        }

        public override SampleModel createSubsetSampleModel(int[] bands)
        {
            if (bands != null && bands.Length != 1)
            {
                // awt.20F=Number of bands must be only 1
                throw new RasterFormatException("Number of bands must be only 1"); //$NON-NLS-1$
            }
            return createCompatibleSampleModel(width, height);
        }

        public override SampleModel createCompatibleSampleModel(int w, int h)
        {
            return new MultiPixelPackedSampleModel(dataType, w, h, pixelBitStride);
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
                pixel = new int[numBands];
            }
            else
            {
                pixel = iArray;
            }

            pixel[0] = getSample(x, y, 0, data);
            return pixel;
        }

        public override void setPixel(int x, int y, int[] iArray, DataBuffer data)
        {
            setSample(x, y, iArray, data, 2, 0);
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
                    bdata[0] = (byte)getSample(x, y, 0, data);
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
                    sdata[0] = (short)getSample(x, y, 0, data);
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
                    idata[0] = getSample(x, y, 0, data);
                    obj = idata;
                    break;
            }

            return obj;
        }


        public override void setDataElements(int x, int y, Object obj, DataBuffer data)
        {
            setSample(x, y, obj, data, 1, 0);
        }


        public override bool Equals(Object o)
        {
            if ((o == null) || !(o is MultiPixelPackedSampleModel))
            {
                return false;
            }

            MultiPixelPackedSampleModel model = (MultiPixelPackedSampleModel)o;
            return this.width == model.width &&
                   this.height == model.height &&
                   this.numBands == model.numBands &&
                   this.dataType == model.dataType &&
                   this.pixelBitStride == model.pixelBitStride &&
                   this.bitMask == model.bitMask &&
                   this.pixelsPerDataElement == model.pixelsPerDataElement &&
                   this.dataElementSize == model.dataElementSize &&
                   this.dataBitOffset == model.dataBitOffset &&
                   this.scanlineStride == model.scanlineStride;
        }

    }
}