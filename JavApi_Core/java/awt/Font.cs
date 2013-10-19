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

namespace biz.ritter.javapi.awt
{
    [Serializable]
    public class Font : java.io.Serializable
    {
        private static readonly long serialVersionUID = -4206021311591459213L;

        // Identity Transform attribute
        private static readonly java.awt.font.TransformAttribute IDENTITY_TRANSFORM = new java.awt.font.TransformAttribute(new java.awt.geom.AffineTransform());

        public static readonly int PLAIN = 0;

        public static readonly int BOLD = 1;

        public static readonly int ITALIC = 2;

        public static readonly int ROMAN_BASELINE = 0;

        public static readonly int CENTER_BASELINE = 1;

        public static readonly int HANGING_BASELINE = 2;

        public static readonly int TRUETYPE_FONT = 0;

        public static readonly int TYPE1_FONT = 1;

        public static readonly int LAYOUT_LEFT_TO_RIGHT = 0;

        public static readonly int LAYOUT_RIGHT_TO_LEFT = 1;

        public static readonly int LAYOUT_NO_START_CONTEXT = 2;

        public static readonly int LAYOUT_NO_LIMIT_CONTEXT = 4;

        static readonly Font DEFAULT_FONT = new Font("Dialog", Font.PLAIN, 12); //$NON-NLS-1$

        protected String name;

        protected int style;

        protected int size;

        protected float pointSize;
        // Set of font attributes 
        private java.util.Hashtable<java.text.AttributedCharacterIteratorNS.Attribute, Object> fRequestedAttributes;

        // Flag if the Font object transformed
        private bool transformed;

        public Font(String name, int style, int size)
        {
            this.name = (name != null) ? name : "Default"; //$NON-NLS-1$
            this.size = (size >= 0) ? size : 0;
            this.style = (style & ~0x03) == 0 ? style : Font.PLAIN;
            this.pointSize = this.size;

            fRequestedAttributes = new java.util.Hashtable<java.text.AttributedCharacterIteratorNS.Attribute, Object>();//5);

            fRequestedAttributes.put(java.awt.font.TextAttribute.TRANSFORM, IDENTITY_TRANSFORM);

            this.transformed = false;

            fRequestedAttributes.put(java.awt.font.TextAttribute.FAMILY, this.name);
            fRequestedAttributes.put(java.awt.font.TextAttribute.SIZE, new java.lang.Float(this.size));

            if ((this.style & Font.BOLD) != 0)
            {
                fRequestedAttributes.put(java.awt.font.TextAttribute.WEIGHT,
                        java.awt.font.TextAttribute.WEIGHT_BOLD);
            }
            else
            {
                fRequestedAttributes.put(java.awt.font.TextAttribute.WEIGHT,
                        java.awt.font.TextAttribute.WEIGHT_REGULAR);
            }
            if ((this.style & Font.ITALIC) != 0)
            {
                fRequestedAttributes.put(java.awt.font.TextAttribute.POSTURE,
                        java.awt.font.TextAttribute.POSTURE_OBLIQUE);
            }
            else
            {
                fRequestedAttributes.put(java.awt.font.TextAttribute.POSTURE,
                        java.awt.font.TextAttribute.POSTURE_REGULAR);
            }
        }
    }
}