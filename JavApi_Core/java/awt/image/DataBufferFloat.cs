using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt.image
{
    /**
     * @author Igor V. Stolyarov
     */
    public sealed class DataBufferFloat : DataBuffer
    {

        float[][] data;

        //!FIXME: Better create a method in java.util.Arrays for copy 2-dim Arrays
        private float[][] copy(float[][] src)
        {
            float[][] result = new float[src.Length][];

            for (int i = 0; i < src.Length; i++)
            {
                float[] inner = new float[src[i].Length];
                java.lang.SystemJ.arraycopy(src[i], 0, inner, 0, inner.Length);
                result[i] = inner;
            }

            return result;
        }

        public DataBufferFloat(float[][] dataArrays, int size, int[] offsets)
            : base(TYPE_FLOAT, size, dataArrays.Length, offsets)
        {
            data = copy(dataArrays);
        }

        public DataBufferFloat(float[][] dataArrays, int size)
            : base(TYPE_FLOAT, size, dataArrays.Length)
        {
            data = copy(dataArrays);
        }

        public DataBufferFloat(float[] dataArray, int size, int offset)
            : base(TYPE_FLOAT, size, 1, offset)
        {
            data = new float[1][];
            data[0] = dataArray;
        }

        public DataBufferFloat(float[] dataArray, int size)
            : base(TYPE_FLOAT, size)
        {
            data = new float[1][];
            data[0] = dataArray;
        }

        public DataBufferFloat(int size, int numBanks)
            : base(TYPE_FLOAT, size, numBanks)
        {
            data = new float[numBanks][];
            int i = 0;
            while (i < numBanks)
            {
                data[i++] = new float[size];
            }
        }

        public DataBufferFloat(int size)
            : base(TYPE_FLOAT, size)
        {
            data = new float[1][];
            data[0] = new float[size];
        }


        public override void setElem(int bank, int i, int val)
        {
            data[bank][offsets[bank] + i] = val;
            notifyChanged();
        }


        public override void setElemFloat(int bank, int i, float val)
        {
            data[bank][offsets[bank] + i] = val;
            notifyChanged();
        }


        public override void setElemDouble(int bank, int i, double val)
        {
            data[bank][offsets[bank] + i] = (float)val;
            notifyChanged();
        }


        public override void setElem(int i, int val)
        {
            data[0][offset + i] = val;
            notifyChanged();
        }


        public override int getElem(int bank, int i)
        {
            return (int)(data[bank][offsets[bank] + i]);
        }


        public override float getElemFloat(int bank, int i)
        {
            return data[bank][offsets[bank] + i];
        }


        public override double getElemDouble(int bank, int i)
        {
            return data[bank][offsets[bank] + i];
        }


        public override void setElemFloat(int i, float val)
        {
            data[0][offset + i] = val;
            notifyChanged();
        }


        public override void setElemDouble(int i, double val)
        {
            data[0][offset + i] = (float)val;
            notifyChanged();
        }

        public float[] getData(int bank)
        {
            notifyTaken();
            return data[bank];
        }


        public override int getElem(int i)
        {
            return (int)(data[0][offset + i]);
        }


        public override float getElemFloat(int i)
        {
            return data[0][offset + i];
        }


        public override double getElemDouble(int i)
        {
            return data[0][offset + i];
        }

        public float[][] getBankData()
        {
            notifyTaken();
            return copy(data);
        }

        public float[] getData()
        {
            notifyTaken();
            return data[0];
        }
    }
}