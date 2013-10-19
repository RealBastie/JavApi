using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using java = biz.ritter.javapi;

namespace org.apache.harmony.awt.gl.image
{

    internal class OrdinaryWritableRaster : java.awt.image.WritableRaster
    {

        public OrdinaryWritableRaster(java.awt.image.SampleModel sampleModel,
                java.awt.image.DataBuffer dataBuffer, java.awt.Rectangle aRegion,
                java.awt.Point sampleModelTranslate, java.awt.image.WritableRaster parent)
            :
                base(sampleModel, dataBuffer, aRegion, sampleModelTranslate, parent)
        {
        }

        public OrdinaryWritableRaster(java.awt.image.SampleModel sampleModel,
                java.awt.image.DataBuffer dataBuffer, java.awt.Point origin)
            :
                base(sampleModel, dataBuffer, origin)
        {
        }

        public OrdinaryWritableRaster(java.awt.image.SampleModel sampleModel, java.awt.Point origin)
            :
                base(sampleModel, origin)
        {
        }

/* Basties Note: Remove all "I do what same methods in super class do"-methods (think it isn't OO-like)
        public override void setDataElements(int x, int y, Object inData)
        {
            base.setDataElements(x, y, inData);
        }


        public override void setDataElements(int x, int y, int w, int h, Object inData)
        {
            base.setDataElements(x, y, w, h, inData);
        }


        public override java.awt.image.WritableRaster createWritableChild(int parentX, int parentY, int w,
                int h, int childMinX, int childMinY, int[] bandList)
        {
            return base.createWritableChild(parentX, parentY, w, h, childMinX,
                    childMinY, bandList);
        }


        public override java.awt.image.WritableRaster createWritableTranslatedChild(int childMinX,
                int childMinY)
        {
            return base.createWritableTranslatedChild(childMinX, childMinY);
        }


        public override java.awt.image.WritableRaster getWritableParent()
        {
            return base.getWritableParent();
        }


        public override void setRect(java.awt.image.Raster srcRaster)
        {
            base.setRect(srcRaster);
        }


        public override void setRect(int dx, int dy, java.awt.image.Raster srcRaster)
        {
            base.setRect(dx, dy, srcRaster);
        }


        public override void setDataElements(int x, int y, java.awt.image.Raster inRaster)
        {
            base.setDataElements(x, y, inRaster);
        }


        public override void setPixel(int x, int y, int[] iArray)
        {
            base.setPixel(x, y, iArray);
        }


        public override void setPixel(int x, int y, float[] fArray)
        {
            base.setPixel(x, y, fArray);
        }


        public override void setPixel(int x, int y, double[] dArray)
        {
            base.setPixel(x, y, dArray);
        }


        public override void setPixels(int x, int y, int w, int h, int[] iArray)
        {
            base.setPixels(x, y, w, h, iArray);
        }


        public override void setPixels(int x, int y, int w, int h, float[] fArray)
        {
            base.setPixels(x, y, w, h, fArray);
        }


        public override void setPixels(int x, int y, int w, int h, double[] dArray)
        {
            base.setPixels(x, y, w, h, dArray);
        }


        public override void setSamples(int x, int y, int w, int h, int b, int[] iArray)
        {
            base.setSamples(x, y, w, h, b, iArray);
        }


        public override void setSamples(int x, int y, int w, int h, int b, float[] fArray)
        {
            base.setSamples(x, y, w, h, b, fArray);
        }


        public override void setSamples(int x, int y, int w, int h, int b, double[] dArray)
        {
            base.setSamples(x, y, w, h, b, dArray);
        }


        public override void setSample(int x, int y, int b, int s)
        {
            base.setSample(x, y, b, s);
        }


        public override void setSample(int x, int y, int b, float s)
        {
            base.setSample(x, y, b, s);
        }


        public override void setSample(int x, int y, int b, double s)
        {
            base.setSample(x, y, b, s);
        }
 */
    }
}
