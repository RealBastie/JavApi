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

namespace biz.ritter.javapi.awt.font
{
    /**
     * @author Ilya S. Okomin
     */

    public class FontRenderContext
    {

        // Affine transform of this mode
        private java.awt.geom.AffineTransform transform;

        // Is the anti-aliased mode used
        private bool fAntiAliased;

        // Is the fractional metrics used
        private bool fFractionalMetrics;


        public FontRenderContext(java.awt.geom.AffineTransform trans, bool antiAliased,
                bool usesFractionalMetrics)
        {
            if (trans != null)
            {
                transform = new java.awt.geom.AffineTransform(trans);
            }
            fAntiAliased = antiAliased;
            fFractionalMetrics = usesFractionalMetrics;
        }

        protected FontRenderContext()
        {
        }

        public override bool Equals(Object obj)
        {
            if (obj == this)
            {
                return true;
            }

            if (obj != null)
            {
                try
                {
                    return equals((FontRenderContext)obj);
                }
                catch (java.lang.ClassCastException e)
                {
                    return false;
                }
            }
            return false;

        }

        public virtual java.awt.geom.AffineTransform getTransform()
        {
            if (transform != null)
            {
                return new java.awt.geom.AffineTransform(transform);
            }
            return new java.awt.geom.AffineTransform();
        }

        public virtual bool equals(FontRenderContext frc)
        {
            if (this == frc)
            {
                return true;
            }

            if (frc == null)
            {
                return false;
            }

            if (!frc.getTransform().equals(this.getTransform()) &&
                !frc.isAntiAliased() == this.fAntiAliased &&
                !frc.usesFractionalMetrics() == this.fFractionalMetrics)
            {
                return false;
            }
            return true;
        }

        public virtual bool usesFractionalMetrics()
        {
            return this.fFractionalMetrics;
        }

        public virtual bool isAntiAliased()
        {
            return this.fAntiAliased;
        }


        public override int GetHashCode()
        {
            return this.getTransform().GetHashCode() ^
                    new java.lang.Boolean(this.fFractionalMetrics).GetHashCode() ^
                    new java.lang.Boolean(this.fAntiAliased).GetHashCode();
        }

    }
}