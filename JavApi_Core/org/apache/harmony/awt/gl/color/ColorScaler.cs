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

namespace org.apache.harmony.awt.gl.color
{
    internal class ColorScaler
    {
        private const float MAX_SHORT = 0xFFFF;
        private const float MAX_XYZ = 1f + (32767f / 32768f);

        // Cached values for scaling color data
        private float[] channelMinValues = null;
        private float[] channelMulipliers = null; // for scale
        private float[] invChannelMulipliers = null; // for unscale

        int nColorChannels = 0;

        // For scaling rasters, false if transfer type is double or float
        bool isTTypeIntegral = false;

        /**
         * Use this method only for double of float transfer types.
         * Extracts scaling data from the color space signature
         * and other tags, stored in the profile
         * @param pf - ICC profile
         */
        public void loadScalingData(java.awt.color.ICC_Profile pf)
        {
            // Supposing double or float transfer type
            isTTypeIntegral = false;

            nColorChannels = pf.getNumComponents();

            // Get min/max values directly from the profile
            // Very much like fillMinMaxValues in ICC_ColorSpace
            float[] maxValues = new float[nColorChannels];
            float[] minValues = new float[nColorChannels];

            switch (pf.getColorSpaceType())
            {
                case java.awt.color.ColorSpace.TYPE_XYZ:
                    minValues[0] = 0;
                    minValues[1] = 0;
                    minValues[2] = 0;
                    maxValues[0] = MAX_XYZ;
                    maxValues[1] = MAX_XYZ;
                    maxValues[2] = MAX_XYZ;
                    break;
                case java.awt.color.ColorSpace.TYPE_Lab:
                    minValues[0] = 0;
                    minValues[1] = -128;
                    minValues[2] = -128;
                    maxValues[0] = 100;
                    maxValues[1] = 127;
                    maxValues[2] = 127;
                    break;
                default:
                    for (int i = 0; i < nColorChannels; i++)
                    {
                        minValues[i] = 0;
                        maxValues[i] = 1;
                    }
                    break;
            }

            channelMinValues = minValues;
            channelMulipliers = new float[nColorChannels];
            invChannelMulipliers = new float[nColorChannels];

            for (int i = 0; i < nColorChannels; i++)
            {
                channelMulipliers[i] =
                    MAX_SHORT / (maxValues[i] - channelMinValues[i]);

                invChannelMulipliers[i] =
                    (maxValues[i] - channelMinValues[i]) / MAX_SHORT;
            }
        }
        /**
         * Extracts scaling data from the color space
         * @param cs - color space
         */
        public void loadScalingData(java.awt.color.ColorSpace cs)
        {
            nColorChannels = cs.getNumComponents();

            channelMinValues = new float[nColorChannels];
            channelMulipliers = new float[nColorChannels];
            invChannelMulipliers = new float[nColorChannels];

            for (int i = 0; i < nColorChannels; i++)
            {
                channelMinValues[i] = cs.getMinValue(i);

                channelMulipliers[i] =
                    MAX_SHORT / (cs.getMaxValue(i) - channelMinValues[i]);

                invChannelMulipliers[i] =
                    (cs.getMaxValue(i) - channelMinValues[i]) / MAX_SHORT;
            }
        }

        /**
        * Scales one pixel and puts obtained values to the chanData
        * @param pixelData - input pixel
        * @param chanData - output buffer
        * @param chanDataOffset - output buffer offset
        */
        public void scale(float[] pixelData, short[] chanData, int chanDataOffset)
        {
            for (int chan = 0; chan < nColorChannels; chan++)
            {
                chanData[chanDataOffset + chan] =
                        (short)((pixelData[chan] - channelMinValues[chan]) *
                            channelMulipliers[chan] + 0.5f);
            }
        }
        /**
         * Unscales one pixel and puts obtained values to the pixelData
         * @param pixelData - output pixel
         * @param chanData - input buffer
         * @param chanDataOffset - input buffer offset
         */
        public void unscale(float[] pixelData, short[] chanData, int chanDataOffset)
        {
            for (int chan = 0; chan < nColorChannels; chan++)
            {
                pixelData[chan] = (chanData[chanDataOffset + chan] & 0xFFFF)
                    * invChannelMulipliers[chan] + channelMinValues[chan];
            }
        }
    }
}