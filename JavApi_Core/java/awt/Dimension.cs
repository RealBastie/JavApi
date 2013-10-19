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
using org.apache.harmony.misc;

namespace biz.ritter.javapi.awt
{
    /**
     * @author Denis M. Kishenko
     */
    [Serializable]
    public class Dimension : java.awt.geom.Dimension2D, java.io.Serializable
    {

        private static readonly long serialVersionUID = 4723952579491349524L;

        public int width;
        public int height;

        public Dimension(Dimension d) :
            this(d.width, d.height)
        {
        }

        public Dimension() :
            this(0, 0)
        {
        }

        public Dimension(int width, int height)
        {
            setSize(width, height);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.append(width);
            hash.append(height);
            return hash.hashCode();
        }

        public override bool Equals(Object obj)
        {
            if (obj == this)
            {
                return true;
            }
            if (obj is Dimension)
            {
                Dimension d = (Dimension)obj;
                return (d.width == width && d.height == height);
            }
            return false;
        }

        public override String ToString()
        {
            // The output format based on 1.5 release behaviour. It could be obtained in the following way
            // System.out.println(new Dimension().toString())
            return this.getClass().getName() + "[width=" + width + ",height=" + height + "]"; //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
        }

        public virtual void setSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public virtual void setSize(Dimension d)
        {
            setSize(d.width, d.height);
        }

        public override void setSize(double width, double height)
        {
            setSize((int)java.lang.Math.ceil(width), (int)java.lang.Math.ceil(height));
        }

        public virtual Dimension getSize()
        {
            return new Dimension(width, height);
        }


        public override double getHeight()
        {
            return height;
        }


        public override double getWidth()
        {
            return width;
        }

    }

}
