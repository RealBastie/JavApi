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

namespace org.apache.harmony.awt.gl
{

    internal class MultiRectArea : java.awt.Shape
    {

        /**
         * If CHECK is true validation check active
         */
        private const bool CHECK = false;

        internal bool sorted = true;

        /**
         * Rectangle buffer
         */
        public int[] rect;

        /**
         * Bounding box
         */
        java.awt.Rectangle bounds;

        /**
         * Result rectangle array
         */
        java.awt.Rectangle[] rectangles;

        /**
         * LineCash provides creating MultiRectArea line by line. Used in JavaShapeRasterizer.
         */
        public class LineCash : MultiRectArea
        {

            int lineY;
            int bottomCount;
            int[] bottom;

            public LineCash(int size)
                : base()
            {
                bottom = new int[size];
                bottomCount = 0;
            }

            public void setLine(int y)
            {
                lineY = y;
            }

            public void skipLine()
            {
                lineY++;
                bottomCount = 0;
            }

            public void addLine(int[] points, int pointCount)
            {
                int bottomIndex = 0;
                int pointIndex = 0;
                int rectIndex = 0;
                int pointX1 = 0;
                int pointX2 = 0;
                int bottomX1 = 0;
                int bottomX2 = 0;
                bool appendRect = false;
                bool deleteRect = false;
                int lastCount = bottomCount;

                while (bottomIndex < lastCount || pointIndex < pointCount)
                {

                    appendRect = false;
                    deleteRect = false;

                    if (bottomIndex < lastCount)
                    {
                        rectIndex = bottom[bottomIndex];
                        bottomX1 = rect[rectIndex];
                        bottomX2 = rect[rectIndex + 2];
                    }
                    else
                    {
                        appendRect = true;
                    }

                    if (pointIndex < pointCount)
                    {
                        pointX1 = points[pointIndex];
                        pointX2 = points[pointIndex + 1];
                    }
                    else
                    {
                        deleteRect = true;
                    }

                    if (!deleteRect && !appendRect)
                    {
                        if (pointX1 == bottomX1 && pointX2 == bottomX2)
                        {
                            rect[rectIndex + 3] = rect[rectIndex + 3] + 1;
                            pointIndex += 2;
                            bottomIndex++;
                            continue;
                        }
                        deleteRect = pointX2 >= bottomX1;
                        appendRect = pointX1 <= bottomX2;
                    }

                    if (deleteRect)
                    {
                        if (bottomIndex < bottomCount - 1)
                        {
                            java.lang.SystemJ.arraycopy(bottom, bottomIndex + 1, bottom, bottomIndex, bottomCount - bottomIndex - 1);
                            rectIndex -= 4;
                        }
                        bottomCount--;
                        lastCount--;
                    }

                    if (appendRect)
                    {
                        int i = rect[0];
                        bottom[bottomCount++] = i;
                        rect = MultiRectAreaOp.checkBufSize(rect, 4);
                        rect[i++] = pointX1;
                        rect[i++] = lineY;
                        rect[i++] = pointX2;
                        rect[i++] = lineY;
                        pointIndex += 2;
                    }
                }
                lineY++;

                invalidate();
            }

        }

        /**
         * RectCash provides simple creating MultiRectArea
         */
        internal class RectCash : MultiRectArea
        {

            int[] cash;

            public RectCash()
                : base()
            {
                cash = new int[MultiRectAreaOp.RECT_CAPACITY];
                cash[0] = 1;
            }

            public void addRectCashed(int x1, int y1, int x2, int y2)
            {
                addRect(x1, y1, x2, y2);
                invalidate();
                /*
                            // Exclude from cash unnecessary rectangles
                            int i = 1;
                            while(i < cash[0]) {
                                if (rect[cash[i] + 3] >= y1 - 1) {
                                    if (i > 1) {
                                        System.arraycopy(cash, i, cash, 1, cash[0] - i);
                                    }
                                    break;
                                }
                                i++;
                            }
                            cash[0] -= i - 1;

                            // Find in cash rectangle to concatinate
                            i = 1;
                            while(i < cash[0]) {
                                int index = cash[i];
                                if (rect[index + 3] != y1 - 1) {
                                    break;
                                }
                                if (rect[index] == x1 && rect[index + 2] == x2) {
                                    rect[index + 3] += y2 - y1 + 1;

                                    int pos = i + 1;
                                    while(pos < cash[0]) {
                                        if (rect[index + 3] <= rect[cash[i] + 3]) {
                                            System.arraycopy(cash, i + 1, cash, i, pos - i);
                                            break;
                                        }
                                        i++;
                                    }
                                    cash[pos - 1] = index;

                                    invalidate();
                                    return;
                                }
                                i++;
                            }

                            // Add rectangle to buffer
                            int index = rect[0];
                            rect = MultiRectAreaOp.checkBufSize(rect, 4);
                            rect[index + 0] = x1;
                            rect[index + 1] = y1;
                            rect[index + 2] = x2;
                            rect[index + 3] = y2;

                            // Add rectangle to cash
                            int length = cash[0];
                            cash = MultiRectAreaOp.checkBufSize(cash, 1);
                            while(i < length) {
                                if (y2 <= rect[cash[i] + 3]) {
                                    System.arraycopy(cash, i, cash, i + 1, length - i);
                                    break;
                                }
                                i++;
                            }
                            cash[i] = index;
                            invalidate();
                */
            }

            public void addRectCashed(int[] rect, int rectOff, int rectLength)
            {
                for (int i = rectOff; i < rectOff + rectLength; )
                {
                    addRect(rect[i++], rect[i++], rect[i++], rect[i++]);
                    //              addRectCashed(rect[i++], rect[i++], rect[i++], rect[i++]);
                }
            }

        }

        /**
         * MultiRectArea path iterator
         */
        internal class Iterator : java.awt.geom.PathIterator
        {

            int type;
            int index;
            int pos;

            int[] rect;
            java.awt.geom.AffineTransform t;

            internal Iterator(MultiRectArea mra, java.awt.geom.AffineTransform t)
            {
                rect = new int[mra.rect[0] - 1];
                java.lang.SystemJ.arraycopy(mra.rect, 1, rect, 0, rect.Length);
                this.t = t;
            }

            public int getWindingRule()
            {
                return java.awt.geom.PathIteratorConstants.WIND_NON_ZERO;
            }

            public bool isDone()
            {
                return pos >= rect.Length;
            }

            public void next()
            {
                if (index == 4)
                {
                    pos += 4;
                }
                index = (index + 1) % 5;
            }

            public int currentSegment(double[] coords)
            {
                if (isDone())
                {
                    // awt.4B=Iiterator out of bounds
                    throw new java.util.NoSuchElementException("Iterator out of bounds"); //$NON-NLS-1$
                }
                int type = 0;

                switch (index)
                {
                    case 0:
                        type = java.awt.geom.PathIteratorConstants.SEG_MOVETO;
                        coords[0] = rect[pos + 0];
                        coords[1] = rect[pos + 1];
                        break;
                    case 1:
                        type = java.awt.geom.PathIteratorConstants.SEG_LINETO;
                        coords[0] = rect[pos + 2];
                        coords[1] = rect[pos + 1];
                        break;
                    case 2:
                        type = java.awt.geom.PathIteratorConstants.SEG_LINETO;
                        coords[0] = rect[pos + 2];
                        coords[1] = rect[pos + 3];
                        break;
                    case 3:
                        type = java.awt.geom.PathIteratorConstants.SEG_LINETO;
                        coords[0] = rect[pos + 0];
                        coords[1] = rect[pos + 3];
                        break;
                    case 4:
                        type = java.awt.geom.PathIteratorConstants.SEG_CLOSE;
                        break;
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
                    // awt.4B=Iiterator out of bounds
                    throw new java.util.NoSuchElementException("Iterator out of bounds"); //$NON-NLS-1$
                }
                int type = 0;

                switch (index)
                {
                    case 0:
                        type = java.awt.geom.PathIteratorConstants.SEG_MOVETO;
                        coords[0] = rect[pos + 0];
                        coords[1] = rect[pos + 1];
                        break;
                    case 1:
                        type = java.awt.geom.PathIteratorConstants.SEG_LINETO;
                        coords[0] = rect[pos + 2];
                        coords[1] = rect[pos + 1];
                        break;
                    case 2:
                        type = java.awt.geom.PathIteratorConstants.SEG_LINETO;
                        coords[0] = rect[pos + 2];
                        coords[1] = rect[pos + 3];
                        break;
                    case 3:
                        type = java.awt.geom.PathIteratorConstants.SEG_LINETO;
                        coords[0] = rect[pos + 0];
                        coords[1] = rect[pos + 3];
                        break;
                    case 4:
                        type = java.awt.geom.PathIteratorConstants.SEG_CLOSE;
                        break;
                }

                if (t != null)
                {
                    t.transform(coords, 0, coords, 0, 1);
                }
                return type;
            }

        }

        /**
         * Constructs a new empty MultiRectArea 
         */
        public MultiRectArea()
        {
            rect = MultiRectAreaOp.createBuf(0);
        }

        public MultiRectArea(bool sorted)
            : this()
        {
            this.sorted = sorted;
        }

        /**
         * Constructs a new MultiRectArea as a copy of another one 
         */
        public MultiRectArea(MultiRectArea mra)
        {
            if (mra == null)
            {
                rect = MultiRectAreaOp.createBuf(0);
            }
            else
            {
                rect = new int[mra.rect.Length];
                java.lang.SystemJ.arraycopy(mra.rect, 0, rect, 0, mra.rect.Length);
                check(this, "MultiRectArea(MRA)"); //$NON-NLS-1$
            }
        }

        /**
         * Constructs a new MultiRectArea consists of single rectangle 
         */
        public MultiRectArea(java.awt.Rectangle r)
        {
            rect = MultiRectAreaOp.createBuf(0);
            if (r != null && !r.isEmpty())
            {
                rect[0] = 5;
                rect[1] = r.x;
                rect[2] = r.y;
                rect[3] = r.x + r.width - 1;
                rect[4] = r.y + r.height - 1;
            }
            check(this, "MultiRectArea(Rectangle)"); //$NON-NLS-1$
        }

        /**
         * Constructs a new MultiRectArea consists of single rectangle
         */
        public MultiRectArea(int x0, int y0, int x1, int y1)
        {
            rect = MultiRectAreaOp.createBuf(0);
            if (x1 >= x0 && y1 >= y0)
            {
                rect[0] = 5;
                rect[1] = x0;
                rect[2] = y0;
                rect[3] = x1;
                rect[4] = y1;
            }
            check(this, "MultiRectArea(Rectangle)"); //$NON-NLS-1$
        }

        /**
         * Constructs a new MultiRectArea and append rectangle from buffer
         */
        public MultiRectArea(java.awt.Rectangle[] buf)
            : this()
        {
            foreach (java.awt.Rectangle element in buf)
            {
                add(element);
            }
        }

        /**
         * Constructs a new MultiRectArea and append rectangle from array
         */
        public MultiRectArea(java.util.ArrayList<java.awt.Rectangle> buf)
            : this()
        {
            for (int i = 0; i < buf.size(); i++)
            { //Basties note: also foreach available...
                add(buf.get(i));
            }
        }

        /**
         * Sort rectangle buffer
         */
        internal void resort()
        {
            int[] buf = new int[4];
            for (int i = 1; i < rect[0]; i += 4)
            {
                int k = i;
                int x1 = rect[k];
                int y1 = rect[k + 1];
                for (int j = i + 4; j < rect[0]; j += 4)
                {
                    int x2 = rect[j];
                    int y2 = rect[j + 1];
                    if (y1 > y2 || (y1 == y2 && x1 > x2))
                    {
                        x1 = x2;
                        y1 = y2;
                        k = j;
                    }
                }
                if (k != i)
                {
                    java.lang.SystemJ.arraycopy(rect, i, buf, 0, 4);
                    java.lang.SystemJ.arraycopy(rect, k, rect, i, 4);
                    java.lang.SystemJ.arraycopy(buf, 0, rect, k, 4);
                }
            }
            invalidate();
        }

        /**
         * Tests equals with another object
         */

        public override bool Equals(Object obj)
        {
            if (obj == this)
            {
                return true;
            }
            if (obj is MultiRectArea)
            {
                MultiRectArea mra = (MultiRectArea)obj;
                for (int i = 0; i < rect[0]; i++)
                {
                    if (rect[i] != mra.rect[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        /**
         * Checks validation of MultiRectArea object
         */
        static MultiRectArea check(MultiRectArea mra, String msg)
        {
            if (CHECK && mra != null)
            {
                if (MultiRectArea.checkValidation(mra.getRectangles(), mra.sorted) != -1)
                {
                    // awt.4C=Invalid MultiRectArea in method {0}
                    new java.lang.RuntimeException("Invalid MultiRectArea in method " + msg); //$NON-NLS-1$
                }
            }
            return mra;
        }

        /**
         * Checks validation of MultiRectArea object
         */
        public static int checkValidation(java.awt.Rectangle[] r, bool sorted)
        {

            // Check width and height
            for (int i = 0; i < r.Length; i++)
            {
                if (r[i].width <= 0 || r[i].height <= 0)
                {
                    return i;
                }
            }

            // Check order
            if (sorted)
            {
                for (int i = 1; i < r.Length; i++)
                {
                    if (r[i - 1].y > r[i].y)
                    {
                        return i;
                    }
                    if (r[i - 1].y == r[i].y)
                    {
                        if (r[i - 1].x > r[i].x)
                        {
                            return i;
                        }
                    }
                }
            }

            // Check override
            for (int i = 0; i < r.Length; i++)
            {
                for (int j = i + 1; j < r.Length; j++)
                {
                    if (r[i].intersects(r[j]))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        /**
         * Assigns rectangle from another buffer
         */
        protected internal void setRect(int[] buf, bool copy)
        {
            if (copy)
            {
                rect = new int[buf.Length];
                java.lang.SystemJ.arraycopy(buf, 0, rect, 0, buf.Length);
            }
            else
            {
                rect = buf;
            }
            invalidate();
        }

        /**
         * Union with another MultiRectArea object
         */
        public void add(MultiRectArea mra)
        {
            setRect(union(this, mra).rect, false);
            invalidate();
        }

        /**
         * Intersect with another MultiRectArea object
         */
        public void intersect(MultiRectArea mra)
        {
            setRect(intersect(this, mra).rect, false);
            invalidate();
        }

        /**
         * Subtract another MultiRectArea object
         */
        public void substract(MultiRectArea mra)
        {
            setRect(subtract(this, mra).rect, false);
            invalidate();
        }

        /**
         * Union with Rectangle object
         */
        public void add(java.awt.Rectangle rect)
        {
            setRect(union(this, new MultiRectArea(rect)).rect, false);
            invalidate();
        }

        /**
         * Intersect with Rectangle object
         */
        public void intersect(java.awt.Rectangle rect)
        {
            setRect(intersect(this, new MultiRectArea(rect)).rect, false);
            invalidate();
        }

        /**
         * Subtract rectangle object
         */
        public void substract(java.awt.Rectangle rect)
        {
            setRect(subtract(this, new MultiRectArea(rect)).rect, false);
        }

        /**
         * Union two MutliRectareArea objects
         */
        public static MultiRectArea intersect(MultiRectArea src1, MultiRectArea src2)
        {
            MultiRectArea res = check(MultiRectAreaOp.Intersection.getResult(src1, src2), "intersect(MRA,MRA)"); //$NON-NLS-1$
            return res;
        }

        /**
         * Intersect two MultiRectArea objects
         */
        public static MultiRectArea union(MultiRectArea src1, MultiRectArea src2)
        {
            MultiRectArea res = check(new MultiRectAreaOp.Union().getResult(src1, src2), "union(MRA,MRA)"); //$NON-NLS-1$
            return res;
        }

        /**
         * Subtract two MultiRectArea objects
         */
        public static MultiRectArea subtract(MultiRectArea src1, MultiRectArea src2)
        {
            MultiRectArea res = check(MultiRectAreaOp.Subtraction.getResult(src1, src2), "subtract(MRA,MRA)"); //$NON-NLS-1$
            return res;
        }

        /**
         * Print MultiRectArea object to output stream
         */
        public static void print(MultiRectArea mra, String msg)
        {
            if (mra == null)
            {
                java.lang.SystemJ.outJ.println(msg + "=null"); //$NON-NLS-1$
            }
            else
            {
                java.awt.Rectangle[] rects = mra.getRectangles();
                java.lang.SystemJ.outJ.println(msg + "(" + rects.Length + ")"); //$NON-NLS-1$ //$NON-NLS-2$
                foreach (java.awt.Rectangle element in rects)
                {
                    java.lang.SystemJ.outJ.println(
                            element.x + "," + //$NON-NLS-1$
                            element.y + "," + //$NON-NLS-1$
                            (element.x + element.width - 1) + "," + //$NON-NLS-1$
                            (element.y + element.height - 1));
                }
            }
        }

        /**
         * Translate MultiRectArea object by (x, y)
         */
        public void translate(int x, int y)
        {
            for (int i = 1; i < rect[0]; )
            {
                rect[i++] += x;
                rect[i++] += y;
                rect[i++] += x;
                rect[i++] += y;
            }

            if (bounds != null && !bounds.isEmpty())
            {
                bounds.translate(x, y);
            }

            if (rectangles != null)
            {
                foreach (java.awt.Rectangle element in rectangles)
                {
                    element.translate(x, y);
                }
            }
        }

        /**
         * Add rectangle to the buffer without any checking
         */
        public void addRect(int x1, int y1, int x2, int y2)
        {
            int i = rect[0];
            rect = MultiRectAreaOp.checkBufSize(rect, 4);
            rect[i++] = x1;
            rect[i++] = y1;
            rect[i++] = x2;
            rect[i++] = y2;
        }

        /**
         * Tests is MultiRectArea empty 
         */
        public bool isEmpty()
        {
            return rect[0] == 1;
        }

        void invalidate()
        {
            bounds = null;
            rectangles = null;
        }

        /**
         * Returns bounds of MultiRectArea object
         */
        public java.awt.Rectangle getBounds()
        {
            if (bounds != null)
            {
                return bounds;
            }

            if (isEmpty())
            {
                return bounds = new java.awt.Rectangle();
            }

            int x1 = rect[1];
            int y1 = rect[2];
            int x2 = rect[3];
            int y2 = rect[4];

            for (int i = 5; i < rect[0]; i += 4)
            {
                int rx1 = rect[i + 0];
                int ry1 = rect[i + 1];
                int rx2 = rect[i + 2];
                int ry2 = rect[i + 3];
                if (rx1 < x1)
                {
                    x1 = rx1;
                }
                if (rx2 > x2)
                {
                    x2 = rx2;
                }
                if (ry1 < y1)
                {
                    y1 = ry1;
                }
                if (ry2 > y2)
                {
                    y2 = ry2;
                }
            }

            return bounds = new java.awt.Rectangle(x1, y1, x2 - x1 + 1, y2 - y1 + 1);
        }

        /**
         * Return rectangle count in the buffer
         */
        public int getRectCount()
        {
            return (rect[0] - 1) / 4;
        }

        /**
         * Returns Rectangle array 
         */
        public java.awt.Rectangle[] getRectangles()
        {
            if (rectangles != null)
            {
                return rectangles;
            }

            rectangles = new java.awt.Rectangle[(rect[0] - 1) / 4];
            int j = 0;
            for (int i = 1; i < rect[0]; i += 4)
            {
                rectangles[j++] = new java.awt.Rectangle(
                        rect[i],
                        rect[i + 1],
                        rect[i + 2] - rect[i] + 1,
                        rect[i + 3] - rect[i + 1] + 1);
            }
            return rectangles;
        }

        /**
         * Returns Bounds2D
         */
        public java.awt.geom.Rectangle2D getBounds2D()
        {
            return getBounds();
        }

        /**
         * Tests does point lie inside MultiRectArea object
         */
        public bool contains(double x, double y)
        {
            for (int i = 1; i < rect[0]; i += 4)
            {
                if (rect[i] <= x && x <= rect[i + 2] && rect[i + 1] <= y && y <= rect[i + 3])
                {
                    return true;
                }
            }
            return false;
        }

        /**
         * Tests does Point2D lie inside MultiRectArea object
         */
        public bool contains(java.awt.geom.Point2D p)
        {
            return contains(p.getX(), p.getY());
        }

        /**
         * Tests does rectangle lie inside MultiRectArea object
         */
        public bool contains(double x, double y, double w, double h)
        {
            throw new java.lang.RuntimeException("Not implemented"); //$NON-NLS-1$
        }

        /**
         * Tests does Rectangle2D lie inside MultiRectArea object
         */
        public bool contains(java.awt.geom.Rectangle2D r)
        {
            return this.contains(r.getX(), r.getY(), r.getWidth(), r.getHeight()); // Basties note: easy to implement...
        }

        /**
         * Tests does rectangle intersect MultiRectArea object
         */
        public bool intersects(double x, double y, double w, double h)
        {
            java.awt.Rectangle r = new java.awt.Rectangle();
            r.setRect(x, y, w, h);
            return intersects(r);
        }

        /**
         * Tests does Rectangle2D intersect MultiRectArea object
         */
        public bool intersects(java.awt.geom.Rectangle2D r)
        {
            if (r == null || r.isEmpty())
            {
                return false;
            }
            for (int i = 1; i < rect[0]; i += 4)
            {
                if (r.intersects(rect[i], rect[i + 1], rect[i + 2] - rect[i] + 1, rect[i + 3] - rect[i + 1] + 1))
                {
                    return true;
                }
            }
            return false;
        }

        /**
         * Returns path iterator
         */
        public java.awt.geom.PathIterator getPathIterator(java.awt.geom.AffineTransform t, double flatness)
        {
            return new Iterator(this, t);
        }

        /**
         * Returns path iterator
         */
        public java.awt.geom.PathIterator getPathIterator(java.awt.geom.AffineTransform t)
        {
            return new Iterator(this, t);
        }

        /**
         * Returns MultiRectArea object converted to string 
         */

        public override String ToString()
        {
            int cnt = getRectCount();
            java.lang.StringBuilder sb = new java.lang.StringBuilder((cnt << 5) + 128);
            sb.append(this.getClass().getName()).append(" ["); //$NON-NLS-1$
            for (int i = 1; i < rect[0]; i += 4)
            {
                sb.append(i > 1 ? ", [" : "[").append(rect[i]).append(", ").append(rect[i + 1]). //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
                append(", ").append(rect[i + 2] - rect[i] + 1).append(", "). //$NON-NLS-1$ //$NON-NLS-2$
                append(rect[i + 3] - rect[i + 1] + 1).append("]"); //$NON-NLS-1$
            }
            return sb.append("]").toString(); //$NON-NLS-1$
        }

    }

}