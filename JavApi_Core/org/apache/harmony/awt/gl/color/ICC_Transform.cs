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
    /**
     * @author Oleg V. Khaschansky
     */

    /**
     * This class encapsulates native ICC transform object, is responsible for its
     * creation, destruction and passing its handle to the native CMM.
     */
    internal class ICC_Transform
    {
        private long transformHandle;
        private int numInputChannels;
        private int numOutputChannels;
        private java.awt.color.ICC_Profile src;
        private java.awt.color.ICC_Profile dst;


        /**
         * @return Returns the number of input channels.
         */
        public int getNumInputChannels()
        {
            return numInputChannels;
        }

        /**
         * @return Returns the number of output channels.
         */
        public int getNumOutputChannels()
        {
            return numOutputChannels;
        }

        /**
         * @return Returns the dst.
         */
        public java.awt.color.ICC_Profile getDst()
        {
            return dst;
        }

        /**
         * @return Returns the src.
         */
        public java.awt.color.ICC_Profile getSrc()
        {
            return src;
        }

        /**
         * Constructs a multiprofile ICC transform
         * @param profiles - list of ICC profiles
         * @param renderIntents - only hints for CMM
         */
        public ICC_Transform(java.awt.color.ICC_Profile[] profiles, int[] renderIntents)
        {
            throw new java.lang.UnsupportedOperationException("Not yet implemented");
        }

        /**
         * This constructor is able to set intents by default
         * @param profiles - list of ICC profiles
         */
        public ICC_Transform(java.awt.color.ICC_Profile[] profiles)
        {
            throw new java.lang.UnsupportedOperationException("Not yet implemented");
        }
    }
}