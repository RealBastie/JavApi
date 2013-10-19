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

namespace org.apache.harmony.awt.gl
{



    /**
     * This class give an opportunity to get access to private data of 
     * some java.awt.image classes 
     * Implementation of this class placed in java.awt.image package
     * @author Igor V. Stolyarov
     * Created on 23.11.2005
     */

    internal abstract class AwtImageBackdoorAccessor
    {

        static protected AwtImageBackdoorAccessor inst;

        public static AwtImageBackdoorAccessor getInstance()
        {
            // First we need to run the static initializer in the DataBuffer class to resolve inst.
            new java.awt.image.DataBufferInt(0);
            return inst;
        }

        public abstract Surface getImageSurface(java.awt.Image image);
        public abstract bool isGrayPallete(java.awt.image.IndexColorModel icm);

        public abstract Object getData(java.awt.image.DataBuffer db);
        public abstract int[] getDataInt(java.awt.image.DataBuffer db);
        public abstract byte[] getDataByte(java.awt.image.DataBuffer db);
        public abstract short[] getDataShort(java.awt.image.DataBuffer db);
        public abstract short[] getDataUShort(java.awt.image.DataBuffer db);
        public abstract double[] getDataDouble(java.awt.image.DataBuffer db);
        public abstract float[] getDataFloat(java.awt.image.DataBuffer db);
        public abstract void releaseData(java.awt.image.DataBuffer db);

        public abstract void addDataBufferListener(java.awt.image.DataBuffer db, DataBufferListener listener);
        public abstract void removeDataBufferListener(java.awt.image.DataBuffer db);
        public abstract void validate(java.awt.image.DataBuffer db);
    }
}