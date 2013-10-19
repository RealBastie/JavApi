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

namespace biz.ritter.javapi.awt.image
{
    /**
     * @author Igor V. Stolyarov
     */
    public sealed class DataBufferInt : DataBuffer
    {

        int[][] data;

        //!FIXME: Better create a method in java.util.Arrays for copy 2-dim Arrays
        private int[][] copy(int[][] src)
        {
            int[][] result = new int[src.Length][];

            for (int i = 0; i < src.Length; i++)
            {
                int[] inner = new int[src[i].Length];
                java.lang.SystemJ.arraycopy(src[i], 0, inner, 0, inner.Length);
                result[i] = inner;
            }

            return result;
        }

        public DataBufferInt(int[][] dataArrays, int size, int[] offsets)
            : base(TYPE_INT, size, dataArrays.Length, offsets)
        {
            //data = dataArrays.clone();
            data = copy(dataArrays);
        }

        public DataBufferInt(int[][] dataArrays, int size)
            : base(TYPE_INT, size, dataArrays.Length)
        {
            //data = dataArrays.clone();
            data = copy(dataArrays);
        }

        public DataBufferInt(int[] dataArray, int size, int offset)
            : base(TYPE_INT, size, 1, offset)
        {
            data = new int[1][];
            data[0] = dataArray;
        }

        public DataBufferInt(int[] dataArray, int size)
            : base(TYPE_INT, size)
        {
            data = new int[1][];
            data[0] = dataArray;
        }

        public DataBufferInt(int size, int numBanks)
            : base(TYPE_INT, size, numBanks)
        {
            data = new int[numBanks][];
            int i = 0;
            while (i < numBanks)
            {
                data[i++] = new int[size];
            }
        }

        public DataBufferInt(int size)
            : base(TYPE_INT, size)
        {
            data = new int[1][];
            data[0] = new int[size];
        }

        public override void setElem(int bank, int i, int val)
        {
            data[bank][offsets[bank] + i] = val;
            notifyChanged();
        }

        public override void setElem(int i, int val)
        {
            data[0][offset + i] = val;
            notifyChanged();
        }

        public override int getElem(int bank, int i)
        {
            return data[bank][offsets[bank] + i];
        }

        public int[] getData(int bank)
        {
            notifyTaken();
            return data[bank];
        }

        public override int getElem(int i)
        {
            return data[0][offset + i];
        }

        public int[][] getBankData()
        {
            notifyTaken();
            //return data.clone();
            return copy(data);
        }

        public int[] getData()
        {
            notifyTaken();
            return data[0];
        }
    }

}
