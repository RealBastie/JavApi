using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt.image
{
    /**
     * @author Igor V. Stolyarov
     */
    public sealed class DataBufferShort : DataBuffer
    {

        short[][] data;

        //!FIXME: Better create a method in java.util.Arrays for copy 2-dim Arrays
        private short[][] copy(short[][] src)
        {
            short[][] result = new short[src.Length][];

            for (int i = 0; i < src.Length; i++)
            {
                short[] inner = new short[src[i].Length];
                java.lang.SystemJ.arraycopy(src[i], 0, inner, 0, inner.Length);
                result[i] = inner;
            }

            return result;
        }

        public DataBufferShort(short[][] dataArrays, int size, int[] offsets)
            : base(TYPE_SHORT, size, dataArrays.Length, offsets)
        {
            data = copy(dataArrays);
        }

        public DataBufferShort(short[][] dataArrays, int size)
            : base(TYPE_SHORT, size, dataArrays.Length)
        {
            data = copy(dataArrays);
        }

        public DataBufferShort(short[] dataArray, int size, int offset)
            : base(TYPE_SHORT, size, 1, offset)
        {
            data = new short[1][];
            data[0] = dataArray;
        }

        public DataBufferShort(short[] dataArray, int size)
            : base(TYPE_SHORT, size)
        {
            data = new short[1][];
            data[0] = dataArray;
        }

        public DataBufferShort(int size, int numBanks)
            : base(TYPE_SHORT, size, numBanks)
        {
            data = new short[numBanks][];
            int i = 0;
            while (i < numBanks)
            {
                data[i++] = new short[size];
            }
        }

        public DataBufferShort(int size)
            : base(TYPE_SHORT, size)
        {
            data = new short[1][];
            data[0] = new short[size];
        }


        public override void setElem(int bank, int i, int val)
        {
            data[bank][offsets[bank] + i] = (short)val;
            notifyChanged();
        }


        public override void setElem(int i, int val)
        {
            data[0][offset + i] = (short)val;
            notifyChanged();
        }


        public override int getElem(int bank, int i)
        {
            return (data[bank][offsets[bank] + i]);
        }

        public short[] getData(int bank)
        {
            notifyTaken();
            return data[bank];
        }


        public override int getElem(int i)
        {
            return (data[0][offset + i]);
        }

        public short[][] getBankData()
        {
            notifyTaken();
            return copy(data);
        }

        public short[] getData()
        {
            notifyTaken();
            return data[0];
        }
    }

}