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

    /**
     * @author Oleg V. Khaschansky
     */
    [Serializable]
    public class Color : Paint, java.io.Serializable
    {
    /*
     * The values of the following colors are based on 1.5 release behavior which
     * can be revealed using the following or similar code:
     *   Color c = Color.white;
     *   System.out.println(c);
     */

    public static readonly Color white = new Color(255, 255, 255);

    public static readonly Color WHITE = white;

    public static readonly Color lightGray = new Color(192, 192, 192);

    public static readonly Color LIGHT_GRAY = lightGray;

    public static readonly Color gray = new Color(128, 128, 128);

    public static readonly Color GRAY = gray;

    public static readonly Color darkGray = new Color(64, 64, 64);

    public static readonly Color DARK_GRAY = darkGray;

    public static readonly Color black = new Color(0, 0, 0);

    public static readonly Color BLACK = black;

    public static readonly Color red = new Color(255, 0, 0);

    public static readonly Color RED = red;

    public static readonly Color pink = new Color(255, 175, 175);

    public static readonly Color PINK = pink;

    public static readonly Color orange = new Color(255, 200, 0);

    public static readonly Color ORANGE = orange;

    public static readonly Color yellow = new Color(255, 255, 0);

    public static readonly Color YELLOW = yellow;

    public static readonly Color green = new Color(0, 255, 0);

    public static readonly Color GREEN = green;

    public static readonly Color magenta = new Color(255, 0, 255);

    public static readonly Color MAGENTA = magenta;

    public static readonly Color cyan = new Color(0, 255, 255);

    public static readonly Color CYAN = cyan;

    public static readonly Color blue = new Color(0, 0, 255);

    public static readonly Color BLUE = blue;
        /**
         * integer RGB value
         */
        int value;

        [NonSerialized]
        private PaintContext currentPaintContext;
        public int getTransparency()
        {
            switch (getAlpha())
            {
                case 0xff:
                    return TransparencyConstants.OPAQUE;
                case 0:
                    return TransparencyConstants.BITMASK;
                default:
                    return TransparencyConstants.TRANSLUCENT;
            }
        }
        public int getRed()
        {
            return (value >> 16) & 0xFF;
        }

        public int getRGB()
        {
            return value;
        }

        public int getGreen()
        {
            return (value >> 8) & 0xFF;
        }

        public int getBlue()
        {
            return value & 0xFF;
        }

        public int getAlpha()
        {
            return (value >> 24) & 0xFF;
        }

        public PaintContext createContext(
                java.awt.image.ColorModel cm,
                Rectangle r,
                java.awt.geom.Rectangle2D r2d,
                java.awt.geom.AffineTransform xform,
                RenderingHints rhs
        )
        {
            if (currentPaintContext != null)
            {
                return currentPaintContext;
            }
            currentPaintContext = new Color.ColorPaintContext(value);
            return currentPaintContext;
        }

        class ColorPaintContext : PaintContext
        {
            int rgbValue;
            java.awt.image.WritableRaster savedRaster;
            java.awt.image.ColorModel cm;

            protected internal ColorPaintContext(int rgb)
            {
                rgbValue = rgb;
                if ((rgb & 0xFF000000) == 0xFF000000)
                {
                    cm = new java.awt.image.DirectColorModel(24, 0xFF0000, 0xFF00, 0xFF);
                }
                else
                {
                    cm = java.awt.image.ColorModel.getRGBdefault();
                }
            }

            public void dispose()
            {
                savedRaster = null;
            }

            public java.awt.image.ColorModel getColorModel()
            {
                return cm;
            }

            public java.awt.image.Raster getRaster(int x, int y, int w, int h)
            {
                if (savedRaster == null ||
                        w != savedRaster.getWidth() ||
                        h != savedRaster.getHeight())
                {
                    savedRaster =
                            getColorModel().createCompatibleWritableRaster(w, h);

                    // Suppose we have here simple INT/RGB color/sample model
                    java.awt.image.DataBufferInt intBuffer =
                            (java.awt.image.DataBufferInt)savedRaster.getDataBuffer();
                    int[] rgbValues = intBuffer.getData();
                    int rgbFillValue = rgbValue;
                    java.util.Arrays<Object>.fill(rgbValues, rgbFillValue);
                }

                return savedRaster;
            }
        }
        public Color(int r, int g, int b)
        {
            if ((r & 0xFF) != r || (g & 0xFF) != g || (b & 0xFF) != b)
            {
                // awt.109=Color parameter outside of expected range.
                throw new java.lang.IllegalArgumentException("Color parameter outside of expected range."); //$NON-NLS-1$
            }
            // 0xFF for alpha channel
            value = (int) (b | (g << 8) | (r << 16) | 0xFF000000);
        }


    }
}