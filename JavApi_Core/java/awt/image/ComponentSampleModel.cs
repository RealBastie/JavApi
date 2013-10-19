using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt.image
{
    /**
     * @author Igor V. Stolyarov
     */
    public class ComponentSampleModel : SampleModel
    {

        protected int[] bandOffsets;

        protected int[] bankIndices;

        //! Implementation bug in Harmony, because super class known same variable with same access
        /*protected int numBands;*/

        protected int numBanks;

        protected int scanlineStride;

        protected int pixelStride;

        public ComponentSampleModel(int dataType, int w, int h, int pixelStride,
                int scanlineStride, int[] bankIndices, int[] bandOffsets) :

            base(dataType, w, h, bandOffsets.Length)
        {

            if (pixelStride < 0)
            {
                // awt.24B=Pixel stride must be >= 0
                throw new java.lang.IllegalArgumentException("Pixel stride must be >= 0"); //$NON-NLS-1$
            }

            if (scanlineStride < 0)
            {
                // awt.24C=Scanline stride must be >= 0
                throw new java.lang.IllegalArgumentException("Scanline stride must be >= 0"); //$NON-NLS-1$
            }

            if (bankIndices.Length != bandOffsets.Length)
            {
                // awt.24D=Bank Indices length must be equal Bank Offsets length
                throw new java.lang.IllegalArgumentException("Bank Indices length must be equal Bank Offsets length"); //$NON-NLS-1$
            }

            this.pixelStride = pixelStride;
            this.scanlineStride = scanlineStride;
            this.bandOffsets = java.util.Arrays<Object>.copy(bandOffsets);
            this.bankIndices = java.util.Arrays<Object>.copy(bankIndices);
            this.numBands = bandOffsets.Length;

            int maxBank = 0;
            for (int i = 0; i < bankIndices.Length; i++)
            {
                if (bankIndices[i] < 0)
                {
                    // awt.24E=Index of {0} bank must be >= 0
                    throw new java.lang.IllegalArgumentException("Index of "+i+" bank must be >= 0"); //$NON-NLS-1$
                }
                if (bankIndices[i] > maxBank)
                {
                    maxBank = bankIndices[i];
                }
            }
            this.numBanks = maxBank + 1;

        }

        public ComponentSampleModel(int dataType, int w, int h, int pixelStride,
                int scanlineStride, int[] bandOffsets) :

            base(dataType, w, h, bandOffsets.Length)
        {
            if (pixelStride < 0)
            {
                // awt.24B=Pixel stride must be >= 0
                throw new java.lang.IllegalArgumentException("Pixel stride must be >= 0"); //$NON-NLS-1$
            }

            if (scanlineStride < 0)
            {
                // awt.24C=Scanline stride must be >= 0
                throw new java.lang.IllegalArgumentException("Scanline stride must be >= 0"); //$NON-NLS-1$
            }

            this.pixelStride = pixelStride;
            this.scanlineStride = scanlineStride;
            this.bandOffsets = java.util.Arrays<Object>.copy(bandOffsets);
            this.numBands = bandOffsets.Length;
            this.numBanks = 1;

            this.bankIndices = new int[numBands];
            for (int i = 0; i < numBands; i++)
            {
                bankIndices[i] = 0;
            }
        }


        public override Object getDataElements(int x, int y, Object obj, DataBuffer data) {
        if (x < 0 || y < 0 || x >= this.width || y >= this.height) {
            // awt.63=Coordinates are not in bounds
            throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
        }
        switch (dataType) {
        case DataBuffer.TYPE_BYTE:
            byte[] bdata;
            if (obj == null) {
                bdata = new byte[numBands];
            } else {
                bdata = (byte[]) obj;
            }

            for (int i = 0; i < numBands; i++) {
                bdata[i] = (byte) getSample(x, y, i, data);
            }

            obj = bdata;
            break;

        case DataBuffer.TYPE_SHORT:
        case DataBuffer.TYPE_USHORT:
            short[] sdata;
            if (obj == null) {
                sdata = new short[numBands];
            } else {
                sdata = (short[]) obj;
            }

            for (int i = 0; i < numBands; i++) {
                sdata[i] = (short) getSample(x, y, i, data);
            }

            obj = sdata;
            break;

        case DataBuffer.TYPE_INT:
            int[] idata;
            if (obj == null) {
                idata = new int[numBands];
            } else {
                idata = (int[]) obj;
            }

            for (int i = 0; i < numBands; i++) {
                idata[i] = getSample(x, y, i, data);
            }

            obj = idata;
            break;

        case DataBuffer.TYPE_FLOAT:
            float[] fdata;
            if (obj == null) {
                fdata = new float[numBands];
            } else {
                fdata = (float[]) obj;
            }

            for (int i = 0; i < numBands; i++) {
                fdata[i] = getSampleFloat(x, y, i, data);
            }

            obj = fdata;
            break;

        case DataBuffer.TYPE_DOUBLE:
            double[] ddata;
            if (obj == null) {
                ddata = new double[numBands];
            } else {
                ddata = (double[]) obj;
            }

            for (int i = 0; i < numBands; i++) {
                ddata[i] = getSampleDouble(x, y, i, data);
            }

            obj = ddata;
            break;
        }

        return obj;
    }


        public override void setDataElements(int x, int y, Object obj, DataBuffer data)
        {
            if (x < 0 || y < 0 || x >= this.width || y >= this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }
            switch (dataType)
            {
                case DataBuffer.TYPE_BYTE:
                    byte[] barr = (byte[])obj;
                    for (int i = 0; i < numBands; i++)
                    {
                        setSample(x, y, i, barr[i] & 0xff, data);
                    }
                    break;

                case DataBuffer.TYPE_SHORT:
                case DataBuffer.TYPE_USHORT:
                    short[] sarr = (short[])obj;
                    for (int i = 0; i < numBands; i++)
                    {
                        setSample(x, y, i, sarr[i] & 0xffff, data);
                    }
                    break;

                case DataBuffer.TYPE_INT:
                    int[] iarr = (int[])obj;
                    for (int i = 0; i < numBands; i++)
                    {
                        setSample(x, y, i, iarr[i], data);
                    }
                    break;

                case DataBuffer.TYPE_FLOAT:
                    float[] farr = (float[])obj;
                    for (int i = 0; i < numBands; i++)
                    {
                        setSample(x, y, i, farr[i], data);
                    }
                    break;

                case DataBuffer.TYPE_DOUBLE:
                    double[] darr = (double[])obj;
                    for (int i = 0; i < numBands; i++)
                    {
                        setSample(x, y, i, darr[i], data);
                    }
                    break;
            }
        }


        public override bool Equals(Object o)
        {
            if ((o == null) || !(o is ComponentSampleModel))
            {
                return false;
            }
            ComponentSampleModel model = (ComponentSampleModel)o;
            return this.width == model.width && this.height == model.height &&
                   this.numBands == model.numBands &&
                   this.dataType == model.dataType &&
                   java.util.Arrays<Object>.equals(this.bandOffsets, model.bandOffsets) &&
                   java.util.Arrays<Object>.equals(this.bankIndices, model.bankIndices) &&
                   this.numBands == model.numBands &&
                   this.numBanks == model.numBanks &&
                   this.scanlineStride == model.scanlineStride &&
                   this.pixelStride == model.pixelStride;
        }


        public override SampleModel createSubsetSampleModel(int[] bands)
        {
            if (bands.Length > this.numBands)
            {
                // awt.64=The number of the bands in the subset is greater than the number of bands in the sample model
                throw new RasterFormatException("The number of the bands in the subset is greater than the number of bands in the sample model"); //$NON-NLS-1$
            }

            int[] indices = new int[bands.Length];
            int[] offsets = new int[bands.Length];

            for (int i = 0; i < bands.Length; i++)
            {
                indices[i] = bankIndices[bands[i]];
                offsets[i] = bandOffsets[bands[i]];
            }

            return new ComponentSampleModel(dataType, width, height, pixelStride,
                    scanlineStride, indices, offsets);

        }


        public override SampleModel createCompatibleSampleModel(int w, int h)
        {
            return new ComponentSampleModel(dataType, w, h, pixelStride,
                    pixelStride * w, bankIndices, bandOffsets);
        }


        public override int[] getPixel(int x, int y, int[] iArray, DataBuffer data)
        {
            int[] pixel;

            if (iArray == null)
            {
                pixel = new int[numBands];
            }
            else
            {
                pixel = iArray;
            }

            for (int i = 0; i < numBands; i++)
            {
                pixel[i] = getSample(x, y, i, data);
            }

            return pixel;
        }


        public override void setPixel(int x, int y, int[] iArray, DataBuffer data)
        {
            for (int i = 0; i < numBands; i++)
            {
                setSample(x, y, i, iArray[i], data);
            }
        }


        public override int getSample(int x, int y, int b, DataBuffer data)
        {
            if (x < 0 || y < 0 || x >= this.width || y >= this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }

            return data.getElem(bankIndices[b], y * scanlineStride +
                    x * pixelStride + bandOffsets[b]);
        }


        public override float getSampleFloat(int x, int y, int b, DataBuffer data)
        {
            if (x < 0 || y < 0 || x >= this.width || y >= this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }

            return data.getElemFloat(bankIndices[b], y * scanlineStride +
                    x * pixelStride + bandOffsets[b]);
        }


        public override double getSampleDouble(int x, int y, int b, DataBuffer data)
        {
            if (x < 0 || y < 0 || x >= this.width || y >= this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }

            return data.getElemDouble(bankIndices[b], y * scanlineStride +
                    x * pixelStride + bandOffsets[b]);
        }


        public override int[] getPixels(int x, int y, int w, int h, int[] iArray,
                DataBuffer data)
        {
            if (x < 0 || y < 0 || x > this.width || x + w > this.width
                    || y > this.height || y + h > this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }
            int[] pixels = null;
            int idx = 0;

            if (iArray == null)
            {
                pixels = new int[w * h * numBands];
            }
            else
            {
                pixels = iArray;
            }

            for (int i = y; i < y + h; i++)
            {
                for (int j = x; j < x + w; j++)
                {
                    for (int n = 0; n < numBands; n++)
                    {
                        pixels[idx++] = getSample(j, i, n, data);
                    }
                }
            }

            return pixels;
        }


        public override void setPixels(int x, int y, int w, int h, int[] iArray,
                DataBuffer data)
        {
            if (x < 0 || y < 0 || x + w > this.width || y + h > this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }
            int idx = 0;
            for (int i = y; i < y + h; i++)
            {
                for (int j = x; j < x + w; j++)
                {
                    for (int n = 0; n < numBands; n++)
                    {
                        setSample(j, i, n, iArray[idx++], data);
                    }
                }
            }
        }


        public override void setSample(int x, int y, int b, int s, DataBuffer data)
        {
            if (x < 0 || y < 0 || x >= this.width || y >= this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }

            data.setElem(bankIndices[b], y * scanlineStride + x * pixelStride
                    + bandOffsets[b], s);
        }


        public override int[] getSamples(int x, int y, int w, int h, int b, int[] iArray,
                DataBuffer data)
        {
            if (x < 0 || y < 0 || x + w > this.width || y + h > this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }
            int[] samples;
            int idx = 0;

            if (iArray == null)
            {
                samples = new int[w * h];
            }
            else
            {
                samples = iArray;
            }

            if (data == null)
            {
                // awt.295=data is null
                throw new java.lang.NullPointerException("data is null"); //$NON-NLS-1$
            }

            for (int i = y; i < y + h; i++)
            {
                for (int j = x; j < x + w; j++)
                {
                    samples[idx++] = getSample(j, i, b, data);
                }
            }

            return samples;
        }


        public override void setSamples(int x, int y, int w, int h, int b, int[] iArray,
                DataBuffer data)
        {
            if (x < 0 || y < 0 || x + w > this.width || y + h > this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }
            int idx = 0;
            for (int i = y; i < y + h; i++)
            {
                for (int j = x; j < x + w; j++)
                {
                    setSample(j, i, b, iArray[idx++], data);
                }
            }
        }


        public override void setSample(int x, int y, int b, float s, DataBuffer data)
        {
            if (x < 0 || y < 0 || x >= this.width || y >= this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }

            data.setElemFloat(bankIndices[b], y * scanlineStride +
                    x * pixelStride + bandOffsets[b], s);
        }


        public override void setSample(int x, int y, int b, double s, DataBuffer data)
        {
            if (x < 0 || y < 0 || x >= this.width || y >= this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }

            data.setElemDouble(bankIndices[b], y * scanlineStride +
                    x * pixelStride + bandOffsets[b], s);
        }


        public override DataBuffer createDataBuffer()
        {
            DataBuffer data = null;

            int maxOffset = bandOffsets[0];
            for (int i = 1; i < bandOffsets.Length; i++)
            {
                if (bandOffsets[i] > maxOffset)
                {
                    maxOffset = bandOffsets[i];
                }
            }
            int size = (height - 1) * scanlineStride +
            (width - 1) * pixelStride + maxOffset + 1;

            switch (dataType)
            {
                case DataBuffer.TYPE_BYTE:
                    data = new DataBufferByte(size, numBanks);
                    break;
                case DataBuffer.TYPE_SHORT:
                    data = new DataBufferShort(size, numBanks);
                    break;
                case DataBuffer.TYPE_USHORT:
                    data = new DataBufferUShort(size, numBanks);
                    break;
                case DataBuffer.TYPE_INT:
                    data = new DataBufferInt(size, numBanks);
                    break;
                case DataBuffer.TYPE_FLOAT:
                    data = new DataBufferFloat(size, numBanks);
                    break;
                case DataBuffer.TYPE_DOUBLE:
                    data = new DataBufferDouble(size, numBanks);
                    break;
            }

            return data;

        }

        public int getOffset(int x, int y, int b)
        {
            return y * scanlineStride + x * pixelStride + bandOffsets[b];
        }

        public int getOffset(int x, int y)
        {
            return y * scanlineStride + x * pixelStride + bandOffsets[0];
        }


        public override int getSampleSize(int band)
        {
            return DataBuffer.getDataTypeSize(dataType);
        }


        public override int[] getSampleSize()
        {
            int[] sampleSizes = new int[numBands];
            int size = DataBuffer.getDataTypeSize(dataType);

            for (int i = 0; i < numBands; i++)
            {
                sampleSizes[i] = size;
            }
            return sampleSizes;
        }

        public int[] getBankIndices()
        {
            return java.util.Arrays<Object>.copy(bankIndices);
        }

        public int[] getBandOffsets()
        {
            return java.util.Arrays<Object>.copy(bandOffsets);
        }


        public int hashCode() {
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
        foreach (int element in bandOffsets) {
            hash ^= element;
            tmp = java.dotnet.lang.Operator.shiftRightUnsignet(hash, 24);
            hash <<= 8;
            hash |= tmp;
        }
        foreach (int element in bankIndices) {
            hash ^= element;
            tmp = java.dotnet.lang.Operator.shiftRightUnsignet(hash, 24);
            hash <<= 8;
            hash |= tmp;
        }
        hash ^= pixelStride;
        tmp = java.dotnet.lang.Operator.shiftRightUnsignet(hash, 24);
        hash <<= 8;
        hash |= tmp;

        hash ^= scanlineStride;
        return hash;
    }

        public int getScanlineStride()
        {
            return scanlineStride;
        }

        public int getPixelStride()
        {
            return pixelStride;
        }


        public override int getNumDataElements()
        {
            return numBands;
        }

    }
}