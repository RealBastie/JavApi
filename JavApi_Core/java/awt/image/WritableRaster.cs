using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt.image
{
    /**
     * @author Igor V. Stolyarov
     */

    public class WritableRaster : Raster
    {

        protected WritableRaster(SampleModel sampleModel, DataBuffer dataBuffer,
                Rectangle aRegion, Point sampleModelTranslate,
                WritableRaster parent) :
            base(sampleModel, dataBuffer, aRegion, sampleModelTranslate, parent)
        {
        }

        protected WritableRaster(SampleModel sampleModel, DataBuffer dataBuffer,
                Point origin) :
            this(sampleModel, dataBuffer, new Rectangle(origin.x, origin.y,
                    sampleModel.width, sampleModel.height), origin, null)
        {
        }

        protected WritableRaster(SampleModel sampleModel, Point origin) :
            this(sampleModel, sampleModel.createDataBuffer(), new Rectangle(
                    origin.x, origin.y, sampleModel.width, sampleModel.height),
                    origin, null)
        {
            ;
        }

        public virtual void setDataElements(int x, int y, Object inData)
        {
            sampleModel.setDataElements(x - sampleModelTranslateX,
                    y - sampleModelTranslateY, inData, dataBuffer);
        }

        public virtual void setDataElements(int x, int y, int w, int h, Object inData)
        {
            sampleModel.setDataElements(x - sampleModelTranslateX,
                    y - sampleModelTranslateY, w, h, inData, dataBuffer);
        }

        public virtual WritableRaster createWritableChild(int parentX, int parentY, int w,
                int h, int childMinX, int childMinY, int[] bandList)
        {
            if (w <= 0 || h <= 0)
            {
                // awt.244=Width or Height of child Raster is less than or equal to zero
                throw new RasterFormatException("Width or Height of child Raster is less than or equal to zero"); //$NON-NLS-1$
            }

            if (parentX < this.minX || parentX + w > this.minX + this.width)
            {
                // awt.245=parentX disposes outside Raster
                throw new RasterFormatException("parentX disposes outside Raster"); //$NON-NLS-1$
            }

            if (parentY < this.minY || parentY + h > this.minY + this.height)
            {
                // awt.246=parentY disposes outside Raster
                throw new RasterFormatException("parentY disposes outside Raster"); //$NON-NLS-1$
            }

            if ((long)parentX + w > java.lang.Integer.MAX_VALUE)
            {
                // awt.247=parentX + w results in integer overflow
                throw new RasterFormatException("parentX + w results in integer overflow"); //$NON-NLS-1$
            }

            if ((long)parentY + h > java.lang.Integer.MAX_VALUE)
            {
                // awt.248=parentY + h results in integer overflow
                throw new RasterFormatException("parentY + h results in integer overflow"); //$NON-NLS-1$
            }

            if ((long)childMinX + w > java.lang.Integer.MAX_VALUE)
            {
                // awt.249=childMinX + w results in integer overflow
                throw new RasterFormatException("childMinX + w results in integer overflow"); //$NON-NLS-1$
            }

            if ((long)childMinY + h > java.lang.Integer.MAX_VALUE)
            {
                // awt.24A=childMinY + h results in integer overflow
                throw new RasterFormatException("childMinY + h results in integer overflow"); //$NON-NLS-1$
            }

            SampleModel childModel;

            if (bandList == null)
            {
                childModel = sampleModel;
            }
            else
            {
                childModel = sampleModel.createSubsetSampleModel(bandList);
            }

            int childTranslateX = childMinX - parentX;
            int childTranslateY = childMinY - parentY;

            return new WritableRaster(childModel, dataBuffer,
                    new Rectangle(childMinX, childMinY, w, h),
                    new Point(childTranslateX + sampleModelTranslateX,
                            childTranslateY + sampleModelTranslateY),
                    this);
        }

        public virtual WritableRaster createWritableTranslatedChild(int childMinX,
                int childMinY)
        {
            return createWritableChild(minX, minY, width, height, childMinX,
                    childMinY, null);
        }

        public virtual WritableRaster getWritableParent()
        {
            return (WritableRaster)parent;
        }

        public virtual void setRect(Raster srcRaster)
        {
            setRect(0, 0, srcRaster);
        }

        public virtual void setRect(int dx, int dy, Raster srcRaster)
        {
            int w = srcRaster.getWidth();
            int h = srcRaster.getHeight();

            int srcX = srcRaster.getMinX();
            int srcY = srcRaster.getMinY();

            int dstX = srcX + dx;
            int dstY = srcY + dy;

            if (dstX < this.minX)
            {
                int minOffX = this.minX - dstX;
                w -= minOffX;
                dstX = this.minX;
                srcX += minOffX;
            }

            if (dstY < this.minY)
            {
                int minOffY = this.minY - dstY;
                h -= minOffY;
                dstY = this.minY;
                srcY += minOffY;
            }

            if (dstX + w > this.minX + this.width)
            {
                int maxOffX = (dstX + w) - (this.minX + this.width);
                w -= maxOffX;
            }

            if (dstY + h > this.minY + this.height)
            {
                int maxOffY = (dstY + h) - (this.minY + this.height);
                h -= maxOffY;
            }

            if (w <= 0 || h <= 0)
            {
                return;
            }

            switch (sampleModel.getDataType())
            {
                case DataBuffer.TYPE_BYTE:
                case DataBuffer.TYPE_SHORT:
                case DataBuffer.TYPE_USHORT:
                case DataBuffer.TYPE_INT:
                    int[] iPixelsLine = null;
                    for (int i = 0; i < h; i++)
                    {
                        iPixelsLine = srcRaster.getPixels(srcX, srcY + i, w, 1,
                                iPixelsLine);
                        setPixels(dstX, dstY + i, w, 1, iPixelsLine);
                    }
                    break;

                case DataBuffer.TYPE_FLOAT:
                    float[] fPixelsLine = null;
                    for (int i = 0; i < h; i++)
                    {
                        fPixelsLine = srcRaster.getPixels(srcX, srcY + i, w, 1,
                                fPixelsLine);
                        setPixels(dstX, dstY + i, w, 1, fPixelsLine);
                    }
                    break;

                case DataBuffer.TYPE_DOUBLE:
                    double[] dPixelsLine = null;
                    for (int i = 0; i < h; i++)
                    {
                        dPixelsLine = srcRaster.getPixels(srcX, srcY + i, w, 1,
                                dPixelsLine);
                        setPixels(dstX, dstY + i, w, 1, dPixelsLine);
                    }
                    break;
            }
        }

        public virtual void setDataElements(int x, int y, Raster inRaster)
        {
            int dstX = x + inRaster.getMinX();
            int dstY = y + inRaster.getMinY();

            int w = inRaster.getWidth();
            int h = inRaster.getHeight();

            if (dstX < this.minX || dstX + w > this.minX + this.width ||
                    dstY < this.minY || dstY + h > this.minY + this.height)
            {
                // awt.63=Coordinates are not in bounds
                throw new java.lang.ArrayIndexOutOfBoundsException("Coordinates are not in bounds"); //$NON-NLS-1$
            }

            int srcX = inRaster.getMinX();
            int srcY = inRaster.getMinY();
            Object line = null;

            for (int i = 0; i < h; i++)
            {
                line = inRaster.getDataElements(srcX, srcY + i, w, 1, line);
                setDataElements(dstX, dstY + i, w, 1, line);
            }
        }

        public virtual void setPixel(int x, int y, int[] iArray)
        {
            sampleModel.setPixel(x - sampleModelTranslateX,
                    y - sampleModelTranslateY, iArray, dataBuffer);
        }

        public virtual void setPixel(int x, int y, float[] fArray)
        {
            sampleModel.setPixel(x - sampleModelTranslateX,
                    y - sampleModelTranslateY, fArray, dataBuffer);
        }

        public virtual void setPixel(int x, int y, double[] dArray)
        {
            sampleModel.setPixel(x - sampleModelTranslateX,
                    y - sampleModelTranslateY, dArray, dataBuffer);
        }

        public virtual void setPixels(int x, int y, int w, int h, int[] iArray)
        {
            sampleModel.setPixels(x - sampleModelTranslateX,
                    y - sampleModelTranslateY, w, h, iArray, dataBuffer);
        }

        public virtual void setPixels(int x, int y, int w, int h, float[] fArray)
        {
            sampleModel.setPixels(x - sampleModelTranslateX,
                    y - sampleModelTranslateY, w, h, fArray, dataBuffer);
        }

        public virtual void setPixels(int x, int y, int w, int h, double[] dArray)
        {
            sampleModel.setPixels(x - sampleModelTranslateX,
                    y - sampleModelTranslateY, w, h, dArray, dataBuffer);
        }

        public virtual void setSamples(int x, int y, int w, int h, int b, int[] iArray)
        {
            sampleModel.setSamples(x - sampleModelTranslateX,
                    y - sampleModelTranslateY, w, h, b, iArray, dataBuffer);
        }

        public virtual void setSamples(int x, int y, int w, int h, int b, float[] fArray)
        {
            sampleModel.setSamples(x - sampleModelTranslateX,
                    y - sampleModelTranslateY, w, h, b, fArray, dataBuffer);
        }

        public virtual void setSamples(int x, int y, int w, int h, int b, double[] dArray)
        {
            sampleModel.setSamples(x - sampleModelTranslateX,
                    y - sampleModelTranslateY, w, h, b, dArray, dataBuffer);
        }

        public virtual void setSample(int x, int y, int b, int s)
        {
            sampleModel.setSample(x - sampleModelTranslateX,
                    y - sampleModelTranslateY, b, s, dataBuffer);
        }

        public virtual void setSample(int x, int y, int b, float s)
        {
            sampleModel.setSample(x - sampleModelTranslateX,
                    y - sampleModelTranslateY, b, s, dataBuffer);
        }

        public virtual void setSample(int x, int y, int b, double s)
        {
            sampleModel.setSample(x - sampleModelTranslateX,
                    y - sampleModelTranslateY, b, s, dataBuffer);
        }

    }

}