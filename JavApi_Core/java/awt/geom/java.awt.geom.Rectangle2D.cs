using java = biz.ritter.javapi;
using System;

namespace biz.ritter.javapi.awt.geom
{
    public abstract class Rectangle2D : RectangularShape
    {

        public const int OUT_LEFT = 1;
        public const int OUT_TOP = 2;
        public const int OUT_RIGHT = 4;
        public const int OUT_BOTTOM = 8;

        public class Float : Rectangle2D
        {

            public float x;
            public float y;
            public float width;
            public float height;

            public Float()
            {
            }

            public Float(float x, float y, float width, float height)
            {
                setRect(x, y, width, height);
            }

            public override double getX()
            {
                return x;
            }

            public override double getY()
            {
                return y;
            }

            public override double getWidth()
            {
                return width;
            }

            public override double getHeight()
            {
                return height;
            }

            public override bool isEmpty()
            {
                return width <= 0.0f || height <= 0.0f;
            }

            public void setRect(float x, float y, float width, float height)
            {
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
            }

            public override void setRect(double x, double y, double width, double height)
            {
                this.x = (float)x;
                this.y = (float)y;
                this.width = (float)width;
                this.height = (float)height;
            }


            public override void setRect(Rectangle2D r)
            {
                this.x = (float)r.getX();
                this.y = (float)r.getY();
                this.width = (float)r.getWidth();
                this.height = (float)r.getHeight();
            }

            public override int outcode(double px, double py)
            {
                int code = 0;

                if (width <= 0.0f)
                {
                    code |= OUT_LEFT | OUT_RIGHT;
                }
                else
                    if (px < x)
                    {
                        code |= OUT_LEFT;
                    }
                    else
                        if (px > x + width)
                        {
                            code |= OUT_RIGHT;
                        }

                if (height <= 0.0f)
                {
                    code |= OUT_TOP | OUT_BOTTOM;
                }
                else
                    if (py < y)
                    {
                        code |= OUT_TOP;
                    }
                    else
                        if (py > y + height)
                        {
                            code |= OUT_BOTTOM;
                        }

                return code;
            }

            public override Rectangle2D getBounds2D()
            {
                return new Float(x, y, width, height);
            }

            public override Rectangle2D createIntersection(Rectangle2D r)
            {
                Rectangle2D dst;
                if (r is Double)
                {
                    dst = new Rectangle2D.Double();
                }
                else
                {
                    dst = new Rectangle2D.Float();
                }
                Rectangle2D.intersect(this, r, dst);
                return dst;
            }

            public override Rectangle2D createUnion(Rectangle2D r)
            {
                Rectangle2D dst;
                if (r is Double)
                {
                    dst = new Rectangle2D.Double();
                }
                else
                {
                    dst = new Rectangle2D.Float();
                }
                Rectangle2D.union(this, r, dst);
                return dst;
            }

            public override String ToString()
            {
                // The output format based on 1.5 release behaviour. It could be obtained in the following way
                // System.out.println(new Rectangle2D.Float().toString())
                return this.getClass().getName() + "[x=" + x + ",y=" + y + ",width=" + width + ",height=" + height + "]"; //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$ //$NON-NLS-4$ //$NON-NLS-5$
            }
        }

        public class Double : Rectangle2D
        {

            public double x;
            public double y;
            public double width;
            public double height;

            public Double()
            {
            }

            public Double(double x, double y, double width, double height)
            {
                setRect(x, y, width, height);
            }


            public override double getX()
            {
                return x;
            }


            public override double getY()
            {
                return y;
            }


            public override double getWidth()
            {
                return width;
            }


            public override double getHeight()
            {
                return height;
            }


            public override bool isEmpty()
            {
                return width <= 0.0 || height <= 0.0;
            }


            public override void setRect(double x, double y, double width, double height)
            {
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
            }


            public override void setRect(Rectangle2D r)
            {
                this.x = r.getX();
                this.y = r.getY();
                this.width = r.getWidth();
                this.height = r.getHeight();
            }


            public override int outcode(double px, double py)
            {
                int code = 0;

                if (width <= 0.0)
                {
                    code |= OUT_LEFT | OUT_RIGHT;
                }
                else
                    if (px < x)
                    {
                        code |= OUT_LEFT;
                    }
                    else
                        if (px > x + width)
                        {
                            code |= OUT_RIGHT;
                        }

                if (height <= 0.0)
                {
                    code |= OUT_TOP | OUT_BOTTOM;
                }
                else
                    if (py < y)
                    {
                        code |= OUT_TOP;
                    }
                    else
                        if (py > y + height)
                        {
                            code |= OUT_BOTTOM;
                        }

                return code;
            }


            public override Rectangle2D getBounds2D()
            {
                return new Double(x, y, width, height);
            }


            public override Rectangle2D createIntersection(Rectangle2D r)
            {
                Rectangle2D dst = new Rectangle2D.Double();
                Rectangle2D.intersect(this, r, dst);
                return dst;
            }


            public override Rectangle2D createUnion(Rectangle2D r)
            {
                Rectangle2D dest = new Rectangle2D.Double();
                Rectangle2D.union(this, r, dest);
                return dest;
            }


            public String toString()
            {
                // The output format based on 1.5 release behaviour. It could be obtained in the following way
                // System.out.println(new Rectangle2D.Double().toString())
                return this.getClass().getName() + "[x=" + x + ",y=" + y + ",width=" + width + ",height=" + height + "]"; //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$ //$NON-NLS-4$ //$NON-NLS-5$
            }
        }

        /*
         * Rectangle2D path iterator 
         */
        class Iterator : PathIterator
        {

            /**
             * The x coordinate of left-upper rectangle corner
             */
            double x;

            /**
             * The y coordinate of left-upper rectangle corner
             */
            double y;


            /**
             * The width of rectangle
             */
            double width;

            /**
             * The height of rectangle
             */
            double height;

            /**
             * The path iterator transformation
             */
            AffineTransform t;

            /**
             * The current segmenet index
             */
            int index;

            /**
             * Constructs a new Rectangle2D.Iterator for given rectangle and transformation
             * @param r - the source Rectangle2D object
             * @param at - the AffineTransform object to apply rectangle path 
             */
            internal Iterator(Rectangle2D r, AffineTransform at)
            {
                this.x = r.getX();
                this.y = r.getY();
                this.width = r.getWidth();
                this.height = r.getHeight();
                this.t = at;
                if (width < 0.0 || height < 0.0)
                {
                    index = 6;
                }
            }

            public int getWindingRule()
            {
                return PathIteratorConstants.WIND_NON_ZERO;
            }

            public bool isDone()
            {
                return index > 5;
            }

            public void next()
            {
                index++;
            }

            public int currentSegment(double[] coords)
            {
                if (isDone())
                {
                    throw new java.util.NoSuchElementException("Iterator out of bounds"); //$NON-NLS-1$
                }
                if (index == 5)
                {
                    return PathIteratorConstants.SEG_CLOSE;
                }
                int type;
                if (index == 0)
                {
                    type = PathIteratorConstants.SEG_MOVETO;
                    coords[0] = x;
                    coords[1] = y;
                }
                else
                {
                    type = PathIteratorConstants.SEG_LINETO;
                    switch (index)
                    {
                        case 1:
                            coords[0] = x + width;
                            coords[1] = y;
                            break;
                        case 2:
                            coords[0] = x + width;
                            coords[1] = y + height;
                            break;
                        case 3:
                            coords[0] = x;
                            coords[1] = y + height;
                            break;
                        case 4:
                            coords[0] = x;
                            coords[1] = y;
                            break;
                    }
                }
                if (t != null)
                {
                    t.transform(coords, 0, coords, 0, 1);
                }
                return type;
            }

            public int currentSegment(float[] coords)
            {
                if (isDone())
                {
                    throw new java.util.NoSuchElementException("Iterator out of bounds"); //$NON-NLS-1$
                }
                if (index == 5)
                {
                    return PathIteratorConstants.SEG_CLOSE;
                }
                int type;
                if (index == 0)
                {
                    coords[0] = (float)x;
                    coords[1] = (float)y;
                    type = PathIteratorConstants.SEG_MOVETO;
                }
                else
                {
                    type = PathIteratorConstants.SEG_LINETO;
                    switch (index)
                    {
                        case 1:
                            coords[0] = (float)(x + width);
                            coords[1] = (float)y;
                            break;
                        case 2:
                            coords[0] = (float)(x + width);
                            coords[1] = (float)(y + height);
                            break;
                        case 3:
                            coords[0] = (float)x;
                            coords[1] = (float)(y + height);
                            break;
                        case 4:
                            coords[0] = (float)x;
                            coords[1] = (float)y;
                            break;
                    }
                }
                if (t != null)
                {
                    t.transform(coords, 0, coords, 0, 1);
                }
                return type;
            }

        }

        protected Rectangle2D()
        {
        }

        public abstract void setRect(double x, double y, double width, double height);

        public abstract int outcode(double x, double y);

        public abstract Rectangle2D createIntersection(Rectangle2D r);

        public abstract Rectangle2D createUnion(Rectangle2D r);

        public virtual void setRect(Rectangle2D r)
        {
            setRect(r.getX(), r.getY(), r.getWidth(), r.getHeight());
        }


        public override void setFrame(double x, double y, double width, double height)
        {
            setRect(x, y, width, height);
        }

        public override Rectangle2D getBounds2D()
        {
            return (Rectangle2D)clone();
        }

        public bool intersectsLine(double x1, double y1, double x2, double y2)
        {
            double rx1 = getX();
            double ry1 = getY();
            double rx2 = rx1 + getWidth();
            double ry2 = ry1 + getHeight();
            return
                (rx1 <= x1 && x1 <= rx2 && ry1 <= y1 && y1 <= ry2) ||
                (rx1 <= x2 && x2 <= rx2 && ry1 <= y2 && y2 <= ry2) ||
                Line2D.linesIntersect(rx1, ry1, rx2, ry2, x1, y1, x2, y2) ||
                Line2D.linesIntersect(rx2, ry1, rx1, ry2, x1, y1, x2, y2);
        }

        public bool intersectsLine(Line2D l)
        {
            return intersectsLine(l.getX1(), l.getY1(), l.getX2(), l.getY2());
        }

        public int outcode(Point2D p)
        {
            return outcode(p.getX(), p.getY());
        }

        public override bool contains(double x, double y)
        {
            if (isEmpty())
            {
                return false;
            }

            double x1 = getX();
            double y1 = getY();
            double x2 = x1 + getWidth();
            double y2 = y1 + getHeight();

            return
                x1 <= x && x < x2 &&
                y1 <= y && y < y2;
        }

        public override bool intersects(double x, double y, double width, double height)
        {
            if (isEmpty() || width <= 0.0 || height <= 0.0)
            {
                return false;
            }

            double x1 = getX();
            double y1 = getY();
            double x2 = x1 + getWidth();
            double y2 = y1 + getHeight();

            return
                x + width > x1 && x < x2 &&
                y + height > y1 && y < y2;
        }

        public override bool contains(double x, double y, double width, double height)
        {
            if (isEmpty() || width <= 0.0 || height <= 0.0)
            {
                return false;
            }

            double x1 = getX();
            double y1 = getY();
            double x2 = x1 + getWidth();
            double y2 = y1 + getHeight();

            return
                x1 <= x && x + width <= x2 &&
                y1 <= y && y + height <= y2;
        }

        public static void intersect(Rectangle2D src1, Rectangle2D src2, Rectangle2D dst)
        {
            double x1 = java.lang.Math.max(src1.getMinX(), src2.getMinX());
            double y1 = java.lang.Math.max(src1.getMinY(), src2.getMinY());
            double x2 = java.lang.Math.min(src1.getMaxX(), src2.getMaxX());
            double y2 = java.lang.Math.min(src1.getMaxY(), src2.getMaxY());
            dst.setFrame(x1, y1, x2 - x1, y2 - y1);
        }

        public static void union(Rectangle2D src1, Rectangle2D src2, Rectangle2D dst)
        {
            double x1 = java.lang.Math.min(src1.getMinX(), src2.getMinX());
            double y1 = java.lang.Math.min(src1.getMinY(), src2.getMinY());
            double x2 = java.lang.Math.max(src1.getMaxX(), src2.getMaxX());
            double y2 = java.lang.Math.max(src1.getMaxY(), src2.getMaxY());
            dst.setFrame(x1, y1, x2 - x1, y2 - y1);
        }

        public void add(double x, double y)
        {
            double x1 = java.lang.Math.min(getMinX(), x);
            double y1 = java.lang.Math.min(getMinY(), y);
            double x2 = java.lang.Math.max(getMaxX(), x);
            double y2 = java.lang.Math.max(getMaxY(), y);
            setRect(x1, y1, x2 - x1, y2 - y1);
        }

        public void add(Point2D p)
        {
            add(p.getX(), p.getY());
        }

        public void add(Rectangle2D r)
        {
            union(this, r, this);
        }

        public override PathIterator getPathIterator(AffineTransform t)
        {
            return new Iterator(this, t);
        }


        public override PathIterator getPathIterator(AffineTransform t, double flatness)
        {
            return new Iterator(this, t);
        }


        public override int GetHashCode()
        {
            org.apache.harmony.misc.HashCode hash = new org.apache.harmony.misc.HashCode();
            hash.append(getX());
            hash.append(getY());
            hash.append(getWidth());
            hash.append(getHeight());
            return hash.hashCode();
        }


        public override bool Equals(Object obj)
        {
            if (obj == this)
            {
                return true;
            }
            if (obj is Rectangle2D)
            {
                Rectangle2D r = (Rectangle2D)obj;
                return
                    getX() == r.getX() &&
                    getY() == r.getY() &&
                    getWidth() == r.getWidth() &&
                    getHeight() == r.getHeight();
            }
            return false;
        }

    }

}