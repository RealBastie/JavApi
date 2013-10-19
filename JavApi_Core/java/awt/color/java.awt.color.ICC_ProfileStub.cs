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

namespace biz.ritter.javapi.awt.color
{
    /**
     * @author Oleg V. Khaschansky
     */

    [Serializable]
    internal sealed class ICC_ProfileStub : ICC_Profile
    {
        private static readonly long serialVersionUID = 501389760875253507L;
        [NonSerialized]
        int colorspace;

        public ICC_ProfileStub(int csSpecifier)
        {
            switch (csSpecifier)
            {
                case ColorSpace.CS_sRGB:
                case ColorSpace.CS_CIEXYZ:
                case ColorSpace.CS_LINEAR_RGB:
                case ColorSpace.CS_PYCC:
                case ColorSpace.CS_GRAY:
                    break;
                default:
                    // awt.15D=Invalid colorspace
                    throw new java.lang.IllegalArgumentException("Invalid colorspace"); //$NON-NLS-1$
            }
            colorspace = csSpecifier;
        }

        public void write(String fileName)
        {//throws IOException {
            throw new java.lang.UnsupportedOperationException("Stub cannot perform this operation"); //$NON-NLS-1$
        }

        /**
         * Serializable implementation
         *
         * @throws ObjectStreamException
         */
        private Object writeReplace()
        {//throws ObjectStreamException {
            return loadProfile();
        }

        public void write(java.io.OutputStream s)
        {//throws IOException {
            throw new java.lang.UnsupportedOperationException("Stub cannot perform this operation"); //$NON-NLS-1$
        }

        public void setData(int tagSignature, byte[] tagData)
        {
            throw new java.lang.UnsupportedOperationException("Stub cannot perform this operation"); //$NON-NLS-1$
        }

        public byte[] getData(int tagSignature)
        {
            throw new java.lang.UnsupportedOperationException("Stub cannot perform this operation"); //$NON-NLS-1$
        }


        public byte[] getData()
        {
            throw new java.lang.UnsupportedOperationException("Stub cannot perform this operation"); //$NON-NLS-1$
        }

        protected void finalize()
        {
        }

        public override int getProfileClass()
        {
            return CLASS_COLORSPACECONVERSION;
        }

        public int getPCSType()
        {
            throw new java.lang.UnsupportedOperationException("Stub cannot perform this operation"); //$NON-NLS-1$
        }

        public override int getNumComponents()
        {
            switch (colorspace)
            {
                case ColorSpace.CS_sRGB:
                case ColorSpace.CS_CIEXYZ:
                case ColorSpace.CS_LINEAR_RGB:
                case ColorSpace.CS_PYCC:
                    return 3;
                case ColorSpace.CS_GRAY:
                    return 1;
                default:
                    throw new java.lang.UnsupportedOperationException("Stub cannot perform this operation"); //$NON-NLS-1$
            }
        }

        public int getMinorVersion()
        {
            throw new java.lang.UnsupportedOperationException("Stub cannot perform this operation"); //$NON-NLS-1$
        }

        public int getMajorVersion()
        {
            throw new java.lang.UnsupportedOperationException("Stub cannot perform this operation"); //$NON-NLS-1$
        }

        public override int getColorSpaceType()
        {
            switch (colorspace)
            {
                case ColorSpace.CS_sRGB:
                case ColorSpace.CS_LINEAR_RGB:
                    return ColorSpace.TYPE_RGB;
                case ColorSpace.CS_CIEXYZ:
                    return ColorSpace.TYPE_XYZ;
                case ColorSpace.CS_PYCC:
                    return ColorSpace.TYPE_3CLR;
                case ColorSpace.CS_GRAY:
                    return ColorSpace.TYPE_GRAY;
                default:
                    throw new java.lang.UnsupportedOperationException("Stub cannot perform this operation"); //$NON-NLS-1$
            }
        }

        public static ICC_Profile getInstance(String fileName)
        {//throws IOException {
            throw new java.lang.UnsupportedOperationException("Stub cannot perform this operation"); //$NON-NLS-1$
        }

        public static ICC_Profile getInstance(java.io.InputStream s)
        {//throws IOException {
            throw new java.lang.UnsupportedOperationException("Stub cannot perform this operation"); //$NON-NLS-1$
        }

        public static ICC_Profile getInstance(byte[] data)
        {
            throw new java.lang.UnsupportedOperationException("Stub cannot perform this operation"); //$NON-NLS-1$
        }

        public static new ICC_Profile getInstance(int cspace)
        {
            throw new java.lang.UnsupportedOperationException("Stub cannot perform this operation"); //$NON-NLS-1$
        }

        public ICC_Profile loadProfile()
        {
            switch (colorspace)
            {
                case ColorSpace.CS_sRGB:
                    return ICC_Profile.getInstance(ColorSpace.CS_sRGB);
                case ColorSpace.CS_GRAY:
                    return ICC_Profile.getInstance(ColorSpace.CS_GRAY);
                case ColorSpace.CS_CIEXYZ:
                    return ICC_Profile.getInstance(ColorSpace.CS_CIEXYZ);
                case ColorSpace.CS_LINEAR_RGB:
                    return ICC_Profile.getInstance(ColorSpace.CS_LINEAR_RGB);
                case ColorSpace.CS_PYCC:
                    return ICC_Profile.getInstance(ColorSpace.CS_PYCC);
                default:
                    throw new java.lang.UnsupportedOperationException("Stub cannot perform this operation"); //$NON-NLS-1$
            }
        }
    }
}