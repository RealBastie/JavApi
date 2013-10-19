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
using org.apache.harmony.awt.gl.image;

namespace biz.ritter.javapi.awt.image
{
/**
 * @author Igor V. Stolyarov
 */

public abstract class DataBuffer {

    public const int TYPE_BYTE = 0;

    public const int TYPE_USHORT = 1;

    public const int TYPE_SHORT = 2;

    public const int TYPE_INT = 3;

    public const int TYPE_FLOAT = 4;

    public const int TYPE_DOUBLE = 5;

    public const int TYPE_UNDEFINED = 32;

    protected int dataType;

    protected int banks;

    protected int offset;

    protected int size;

    protected int [] offsets;
    
    bool dataChanged = true;
    bool dataTaken = false;
    DataBufferListener listener;

    static DataBuffer() {
        AwtImageBackdoorAccessorImpl.init();
    }

    protected DataBuffer(int dataType, int size, int numBanks, int[] offsets) {
        this.dataType = dataType;
        this.size = size;
        this.banks = numBanks;
        // this.offsets = offsets.clone();
        this.offsets = new int[offsets.Length];
        java.lang.SystemJ.arraycopy(offsets, 0, this.offsets, 0, this.offsets.Length);
        this.offset = offsets[0];
    }

    protected DataBuffer(int dataType, int size, int numBanks, int offset) {
        this.dataType = dataType;
        this.size = size;
        this.banks = numBanks;
        this.offset = offset;
        this.offsets = new int[numBanks];
        int i = 0;
        while (i < numBanks) {
            offsets[i++] = offset;
        }
    }

    protected DataBuffer(int dataType, int size, int numBanks) {
        this.dataType = dataType;
        this.size = size;
        this.banks = numBanks;
        this.offset = 0;
        this.offsets = new int[numBanks];
    }

    protected DataBuffer(int dataType, int size) {
        this.dataType = dataType;
        this.size = size;
        this.banks = 1;
        this.offset = 0;
        this.offsets = new int[1];
    }

    public abstract void setElem(int bank, int i, int val);

    public virtual void setElemFloat(int bank, int i, float val) {
        setElem(bank, i, (int) val);
    }

    public virtual void setElemDouble(int bank, int i, double val) {
        setElem(bank, i, (int) val);
    }

    public virtual void setElem(int i, int val) {
        setElem(0, i, val);
    }

    public abstract int getElem(int bank, int i);

    public virtual float getElemFloat(int bank, int i) {
        return getElem(bank, i);
    }

    public virtual double getElemDouble(int bank, int i) {
        return getElem(bank, i);
    }

    public virtual void setElemFloat(int i, float val) {
        setElemFloat(0, i, val);
    }

    public virtual void setElemDouble(int i, double val) {
        setElemDouble(0, i, val);
    }

    public virtual int getElem(int i) {
        return getElem(0, i);
    }

    public virtual float getElemFloat(int i) {
        return getElem(0, i);
    }

    public virtual double getElemDouble(int i) {
        return getElem(i);
    }

    public int[] getOffsets() {
        return offsets;
    }

    public int getSize() {
        return size;
    }

    public int getOffset() {
        return offset;
    }

    public int getNumBanks() {
        return banks;
    }

    public int getDataType() {
        return this.dataType;
    }

    public static int getDataTypeSize(int type) {
        switch (type) {

        case TYPE_BYTE:
            return 8;

        case TYPE_USHORT:
        case TYPE_SHORT:
            return 16;

        case TYPE_INT:
        case TYPE_FLOAT:
            return 32;

        case TYPE_DOUBLE:
            return 64;

        default:
            // awt.22C=Unknown data type {0}
            throw new java.lang.IllegalArgumentException("Unknown data type "+ type); //$NON-NLS-1$
        }
    }
    
    protected void notifyChanged(){
        if(listener != null && !dataChanged){
            dataChanged = true;
            listener.dataChanged();
        }
    }
    
    protected void notifyTaken(){
        if(listener != null && !dataTaken){
            dataTaken = true;
            listener.dataTaken();
        }
    }
    
    void releaseData(){
        if(listener != null && dataTaken){
            dataTaken = false;
            listener.dataReleased();
        }
    }
    
    void addDataBufferListener(DataBufferListener listener){
        this.listener = listener;
    }
    
    void removeDataBufferListener(){
        listener = null;
    }
    
    void validate(){
        dataChanged = false;
    }
    
}

}
