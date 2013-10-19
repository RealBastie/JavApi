using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt.image
{
/**
 * @author Igor V. Stolyarov
 */

public sealed class DataBufferUShort : DataBuffer {

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

    public DataBufferUShort(short [][]dataArrays, int size, int []offsets) :
        base(TYPE_USHORT, size, dataArrays.Length, offsets){
        for(int i = 0; i < dataArrays.Length; i++){
            if(dataArrays[i].Length < offsets[i] + size){
                // awt.28d=Length of dataArray[{0}] is less than size + offset[{1}]
                throw new java.lang.IllegalArgumentException("Length of dataArray[{"+i+"] is less than size + offset["+i+"]");  //$NON-NLS-1$
            }
        }
        data = copy(dataArrays);
    }

    public DataBufferUShort(short [][]dataArrays, int size) :
        base(TYPE_USHORT, size, dataArrays.Length){
        data = copy (dataArrays);
    }

    public DataBufferUShort(short []dataArray, int size, int offset) :
        base(TYPE_USHORT, size, 1, offset){
        if(dataArray.Length < size + offset){
            // awt.28E=Length of dataArray is less than size + offset
            throw new java.lang.IllegalArgumentException("Length of dataArray is less than size + offset"); //$NON-NLS-1$
        }
        data = new short[1][];
        data[0] = dataArray;
    }

    public DataBufferUShort(short []dataArray, int size) :
        base(TYPE_USHORT, size){
        data = new short[1][];
        data[0] = dataArray;
    }

    public DataBufferUShort(int size, int numBanks) :
        base(TYPE_USHORT, size, numBanks){
        data = new short[numBanks][];
        int i= 0;
        while( i < numBanks) {
            data[i++] = new short[size];
        }
    }

    public DataBufferUShort(int size) :
        base(TYPE_USHORT, size){
        data = new short[1][];
        data[0] = new short[size];
    }

    
    public override void setElem(int bank, int i, int val) {
        data[bank][offsets[bank] + i] = (short)val;
        notifyChanged();
    }

    
    public override void setElem(int i, int val) {
        data[0][offset + i] = (short)val;
        notifyChanged();
    }

    
    public override int getElem(int bank, int i) {
        return (data[bank][offsets[bank] + i]) & 0xffff;
    }

    public short[] getData(int bank) {
        notifyTaken();
        return data[bank];
    }

    
    public override int getElem(int i) {
        return (data[0][offset + i]) & 0xffff;
    }

    public short[][] getBankData() {
        notifyTaken();
        return copy(data);
    }

    public short[] getData() {
        notifyTaken();
        return data[0];
    }
}

}
