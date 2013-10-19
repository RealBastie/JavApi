using System;

using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt.image
{
    /**
     * @author Igor V. Stolyarov
     */

    public sealed class DataBufferByte : DataBuffer
    {

        byte[][] data;

        //!FIXME: Better create a method in java.util.Arrays for copy 2-dim Arrays
        private byte[][] copy(byte[][] src)
        {
            byte[][] result = new byte[src.Length][];

            for (int i = 0; i < src.Length; i++)
            {
                byte[] inner = new byte[src[i].Length];
                java.lang.SystemJ.arraycopy(src[i], 0, inner, 0, inner.Length);
                result[i] = inner;
            }

            return result;
        }

        public DataBufferByte(byte[][] dataArrays, int size, int[] offsets) :
            base(TYPE_BYTE, size, dataArrays.Length, offsets)
        {
            data = this.copy(dataArrays);
        }

        public DataBufferByte(byte[][] dataArrays, int size) :
            base(TYPE_BYTE, size, dataArrays.Length)
        {
            data = this.copy(dataArrays);
        }

        public DataBufferByte(byte[] dataArray, int size, int offset) :
            base(TYPE_BYTE, size, 1, offset)
        {
            data = new byte[1][];
            data[0] = dataArray;
        }

        public DataBufferByte(byte[] dataArray, int size) :
            base(TYPE_BYTE, size)
        {
            data = new byte[1][];
            data[0] = dataArray;
        }

        public DataBufferByte(int size, int numBanks) :
            base(TYPE_BYTE, size, numBanks)
        {
            data = new byte[numBanks][];
            int i = 0;
            while (i < numBanks)
            {
                data[i++] = new byte[size];
            }
        }

        public DataBufferByte(int size) :
            base(TYPE_BYTE, size)
        {
            data = new byte[1][];
            data[0] = new byte[size];
        }


        public override void setElem(int bank, int i, int val)
        {
            data[bank][offsets[bank] + i] = (byte)val;
            notifyChanged();
        }


        public override void setElem(int i, int val)
        {
            data[0][offset + i] = (byte)val;
            notifyChanged();
        }


        public override int getElem(int bank, int i)
        {
            return (data[bank][offsets[bank] + i]) & 0xff;
        }

        public byte[] getData(int bank)
        {
            notifyTaken();
            return data[bank];
        }


        public override int getElem(int i)
        {
            return (data[0][offset + i]) & 0xff;
        }

        public byte[][] getBankData()
        {
            notifyTaken();
            return this.copy(data);
        }

        public byte[] getData()
        {
            notifyTaken();
            return data[0];
        }

    }

}