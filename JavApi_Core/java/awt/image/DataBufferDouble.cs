using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt.image
{
/**
 * @author Igor V. Stolyarov
 */
public sealed class DataBufferDouble : DataBuffer {

    double [][]data;

    //!FIXME: Better create a method in java.util.Arrays for copy 2-dim Arrays
    private double[][] copy(double[][] src)
    {
        double[][] result = new double[src.Length][];

        for (int i = 0; i < src.Length; i++)
        {
            double[] inner = new double[src[i].Length];
            java.lang.SystemJ.arraycopy(src[i], 0, inner, 0, inner.Length);
            result[i] = inner;
        }

        return result;
    }

    public DataBufferDouble(double[][] dataArrays, int size, int[] offsets)
        : base(TYPE_DOUBLE, size, dataArrays.Length, offsets)
    {
        data = copy(dataArrays);
    }

    public DataBufferDouble(double [][]dataArrays, int size) : base(TYPE_DOUBLE, size, dataArrays.Length){
        data = copy (dataArrays);
    }

    public DataBufferDouble(double []dataArray, int size, int offset) :base(TYPE_DOUBLE, size, 1, offset){
        data = new double[1][];
        data[0] = dataArray;
    }

    public DataBufferDouble(double[] dataArray, int size) :base(TYPE_DOUBLE, size){
        data = new double[1][];
        data[0] = dataArray;
    }

    public DataBufferDouble(int size, int numBanks) : base(TYPE_DOUBLE, size, numBanks){
        data = new double[numBanks][];
        int i = 0;
        while (i < numBanks) {
            data[i++] = new double[size];
        }
    }

    public DataBufferDouble(int size) :base(TYPE_DOUBLE, size){
        data = new double[1][];
        data[0] = new double[size];
    }

    
    public override void setElem(int bank, int i, int val) {
        data[bank][offsets[bank] + i] = val;
        notifyChanged();
    }

    
    public override void setElemFloat(int bank, int i, float val) {
        data[bank][offsets[bank] + i] = val;
        notifyChanged();
    }

    
    public override void setElemDouble(int bank, int i, double val) {
        data[bank][offsets[bank] + i] = val;
        notifyChanged();
    }

    
    public override void setElem(int i, int val) {
        data[0][offset + i] = val;
        notifyChanged();
    }

    
    public override int getElem(int bank, int i) {
        return (int) (data[bank][offsets[bank] + i]);
    }

    
    public override float getElemFloat(int bank, int i) {
        return (float) (data[bank][offsets[bank] + i]);
    }

    
    public override double getElemDouble(int bank, int i) {
        return data[bank][offsets[bank] + i];
    }

    
    public override void setElemFloat(int i, float val) {
        data[0][offset + i] = val;
        notifyChanged();
    }

    
    public override void setElemDouble(int i, double val) {
        data[0][offset + i] = val;
        notifyChanged();
    }

    public double[] getData(int bank) {
        notifyTaken();
        return data[bank];
    }

    
    public override int getElem(int i) {
        return (int) (data[0][offset + i]);
    }

    
    public override float getElemFloat(int i) {
        return (float) (data[0][offset + i]);
    }

    
    public override double getElemDouble(int i) {
        return data[0][offset + i];
    }

    public double[][] getBankData() {
        notifyTaken();
        return copy(data);
    }

    public double[] getData() {
        notifyTaken();
        return data[0];
    }
}

}
