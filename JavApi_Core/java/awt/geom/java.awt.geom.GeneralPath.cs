using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using java = biz.ritter.javapi;
using org.apache.harmony.awt.gl;

namespace biz.ritter.javapi.awt.geom
{
    /**
 * @author Denis M. Kishenko
 */

    public class GeneralPath : Shape, java.lang.Cloneable 
    {

    public const int WIND_EVEN_ODD = PathIteratorConstants.WIND_EVEN_ODD;
    public const int WIND_NON_ZERO = PathIteratorConstants.WIND_NON_ZERO;

    /**
     * The buffers size
     */
    private const int BUFFER_SIZE = 10;
    
    /**
     * The buffers capacity
     */
    private const int BUFFER_CAPACITY = 10;

    /**
     * The point's types buffer
     */
    byte[] types;
    
    /**
     * The points buffer
     */
    float[] points;
    
    /**
     * The point's type buffer size
     */
    int typeSize;
    
    /**
     * The points buffer size
     */
    int pointSize;
    
    /**
     * The path rule 
     */
    int rule;

    /**
     * The space amount in points buffer for different segmenet's types
     */
    static int [] pointShift = {
            2,  // MOVETO
            2,  // LINETO
            4,  // QUADTO
            6,  // CUBICTO
            0}; // CLOSE

    /*
     * GeneralPath path iterator 
     */
    class Iterator : PathIterator {

        /**
         * The current cursor position in types buffer
         */
        int typeIndex;
        
        /**
         * The current cursor position in points buffer
         */
        int pointIndex;
        
        /**
         * The source GeneralPath object
         */
        GeneralPath p;
        
        /**
         * The path iterator transformation
         */
        AffineTransform t;

        /**
         * Constructs a new GeneralPath.Iterator for given general path
         * @param path - the source GeneralPath object
         */
        Iterator(GeneralPath path) :this(path, null){
        }

        /**
         * Constructs a new GeneralPath.Iterator for given general path and transformation
         * @param path - the source GeneralPath object
         * @param at - the AffineTransform object to apply rectangle path
         */
        internal Iterator(GeneralPath path, AffineTransform at) {
            this.p = path;
            this.t = at;
        }

        public int getWindingRule() {
            return p.getWindingRule();
        }

        public bool isDone() {
            return typeIndex >= p.typeSize;
        }

        public void next() {
            typeIndex++;
        }

        public int currentSegment(double[] coords) {
            if (isDone()) {
                // awt.4B=Iterator out of bounds
                throw new java.util.NoSuchElementException("Iterator out of bounds"); //$NON-NLS-1$
            }
            int type = p.types[typeIndex];
            int count = GeneralPath.pointShift[type];
            for (int i = 0; i < count; i++) {
                coords[i] = p.points[pointIndex + i];
            }
            if (t != null) {
                t.transform(coords, 0, coords, 0, count / 2);
            }
            pointIndex += count;
            return type;
        }

        public int currentSegment(float[] coords) {
            if (isDone()) {
                // awt.4B=Iterator out of bounds
                throw new java.util.NoSuchElementException("Iterator out of bounds"); //$NON-NLS-1$
            }
            int type = p.types[typeIndex];
            int count = GeneralPath.pointShift[type];
            java.lang.SystemJ.arraycopy(p.points, pointIndex, coords, 0, count);
            if (t != null) {
                t.transform(coords, 0, coords, 0, count / 2);
            }
            pointIndex += count;
            return type;
        }

    }

    public GeneralPath() :
        this(WIND_NON_ZERO, BUFFER_SIZE){
    }

    public GeneralPath(int rule) :
        this(rule, BUFFER_SIZE){
    }

    public GeneralPath(int rule, int initialCapacity) {
        setWindingRule(rule);
        types = new byte[initialCapacity];
        points = new float[initialCapacity * 2];
    }

    public GeneralPath(Shape shape) :
        this(WIND_NON_ZERO, BUFFER_SIZE){
        PathIterator p = shape.getPathIterator(null);
        setWindingRule(p.getWindingRule());
        append(p, false);
    }

    public void setWindingRule(int rule) {
        if (rule != WIND_EVEN_ODD && rule != WIND_NON_ZERO) {
            // awt.209=Invalid winding rule value
            throw new java.lang.IllegalArgumentException("Invalid winding rule value"); //$NON-NLS-1$
        }
        this.rule = rule;
    }

    public int getWindingRule() {
        return rule;
    }

    /**
     * Checks points and types buffer size to add pointCount points. If necessary realloc buffers to enlarge size.   
     * @param pointCount - the point count to be added in buffer
     */
    void checkBuf(int pointCount, bool checkMove) {
        if (checkMove && typeSize == 0) {
            // awt.20A=First segment should be SEG_MOVETO type
            throw new IllegalPathStateException("First segment should be SEG_MOVETO type"); //$NON-NLS-1$
        }
        if (typeSize == types.Length) {
            byte[] tmp = new byte[typeSize + BUFFER_CAPACITY];
            java.lang.SystemJ.arraycopy(types, 0, tmp, 0, typeSize);
            types = tmp;
        }
        if (pointSize + pointCount > points.Length) {
            float[] tmp = new float[pointSize + java.lang.Math.max(BUFFER_CAPACITY * 2, pointCount)];
            java.lang.SystemJ.arraycopy(points, 0, tmp, 0, pointSize);
            points = tmp;
        }
    }

    public void moveTo(float x, float y) {
        if (typeSize > 0 && types[typeSize - 1] == PathIteratorConstants.SEG_MOVETO) {
            points[pointSize - 2] = x;
            points[pointSize - 1] = y;
        } else {
            checkBuf(2, false);
            types[typeSize++] = PathIteratorConstants.SEG_MOVETO;
            points[pointSize++] = x;
            points[pointSize++] = y;
        }
    }

    public void lineTo(float x, float y) {
        checkBuf(2, true);
        types[typeSize++] = PathIteratorConstants.SEG_LINETO;
        points[pointSize++] = x;
        points[pointSize++] = y;
    }

    public void quadTo(float x1, float y1, float x2, float y2) {
        checkBuf(4, true);
        types[typeSize++] = PathIteratorConstants.SEG_QUADTO;
        points[pointSize++] = x1;
        points[pointSize++] = y1;
        points[pointSize++] = x2;
        points[pointSize++] = y2;
    }

    public void curveTo(float x1, float y1, float x2, float y2, float x3, float y3) {
        checkBuf(6, true);
        types[typeSize++] = PathIteratorConstants.SEG_CUBICTO;
        points[pointSize++] = x1;
        points[pointSize++] = y1;
        points[pointSize++] = x2;
        points[pointSize++] = y2;
        points[pointSize++] = x3;
        points[pointSize++] = y3;
    }

    public void closePath() {
        if (typeSize == 0 || types[typeSize - 1] != PathIteratorConstants.SEG_CLOSE) {
            checkBuf(0, true);
            types[typeSize++] = PathIteratorConstants.SEG_CLOSE;
        }
    }

    public void append(Shape shape, bool connect) {
        PathIterator p = shape.getPathIterator(null);
        append(p, connect);
    }

    public void append(PathIterator path, bool connect) {
        while (!path.isDone()) {
            float[] coords = new float[6];
            switch (path.currentSegment(coords)) {
            case PathIteratorConstants.SEG_MOVETO:
                if (!connect || typeSize == 0) {
                    moveTo(coords[0], coords[1]);
                    break;
                }
                if (types[typeSize - 1] != PathIteratorConstants.SEG_CLOSE &&
                    points[pointSize - 2] == coords[0] &&
                    points[pointSize - 1] == coords[1])
                {
                    break;
                }
            // NO BREAK;
                lineTo(coords[0], coords[1]);
                break;
            case PathIteratorConstants.SEG_LINETO:
                lineTo(coords[0], coords[1]);
                break;
            case PathIteratorConstants.SEG_QUADTO:
                quadTo(coords[0], coords[1], coords[2], coords[3]);
                break;
            case PathIteratorConstants.SEG_CUBICTO:
                curveTo(coords[0], coords[1], coords[2], coords[3], coords[4], coords[5]);
                break;
            case PathIteratorConstants.SEG_CLOSE:
                closePath();
                break;
            }
            path.next();
            connect = false;
        }
    }

    public Point2D getCurrentPoint() {
        if (typeSize == 0) {
            return null;
        }
        int j = pointSize - 2;
        if (types[typeSize - 1] == PathIteratorConstants.SEG_CLOSE) {

            for (int i = typeSize - 2; i > 0; i--) {
                int type = types[i];
                if (type == PathIteratorConstants.SEG_MOVETO) {
                    break;
                }
                j -= pointShift[type];
            }
        }
        return new Point2D.Float(points[j], points[j + 1]);
    }

    public void reset() {
        typeSize = 0;
        pointSize = 0;
    }

    public void transform(AffineTransform t) {
        t.transform(points, 0, points, 0, pointSize / 2);
    }

    public Shape createTransformedShape(AffineTransform t) {
        GeneralPath p = (GeneralPath)clone();
        if (t != null) {
            p.transform(t);
        }
        return p;
    }

    public Rectangle2D getBounds2D() {
        float rx1, ry1, rx2, ry2;
        if (pointSize == 0) {
            rx1 = ry1 = rx2 = ry2 = 0.0f;
        } else {
            int i = pointSize - 1;
            ry1 = ry2 = points[i--];
            rx1 = rx2 = points[i--];
            while (i > 0) {
                float y = points[i--];
                float x = points[i--];
                if (x < rx1) {
                    rx1 = x;
                } else
                    if (x > rx2) {
                        rx2 = x;
                    }
                if (y < ry1) {
                    ry1 = y;
                } else
                    if (y > ry2) {
                        ry2 = y;
                    }
            }
        }
        return new Rectangle2D.Float(rx1, ry1, rx2 - rx1, ry2 - ry1);
    }

    public Rectangle getBounds() {
        return getBounds2D().getBounds();
    }

    /**
     * Checks cross count according to path rule to define is it point inside shape or not. 
     * @param cross - the point cross count
     * @return true if point is inside path, or false otherwise 
     */
    bool isInside(int cross) {
        if (rule == WIND_NON_ZERO) {
            return Crossing.isInsideNonZero(cross);
        }
        return Crossing.isInsideEvenOdd(cross);
    }

    public bool contains(double px, double py) {
        return isInside(Crossing.crossShape(this, px, py));
    }

    public bool contains(double rx, double ry, double rw, double rh) {
        int cross = Crossing.intersectShape(this, rx, ry, rw, rh);
        return cross != Crossing.CROSSING && isInside(cross);
    }

    public bool intersects(double rx, double ry, double rw, double rh) {
        int cross = Crossing.intersectShape(this, rx, ry, rw, rh);
        return cross == Crossing.CROSSING || isInside(cross);
    }

    public bool contains(Point2D p) {
        return contains(p.getX(), p.getY());
    }

    public bool contains(Rectangle2D r) {
        return contains(r.getX(), r.getY(), r.getWidth(), r.getHeight());
    }

    public bool intersects(Rectangle2D r) {
        return intersects(r.getX(), r.getY(), r.getWidth(), r.getHeight());
    }

    public PathIterator getPathIterator(AffineTransform t) {
        return new Iterator(this, t);
    }

    public PathIterator getPathIterator(AffineTransform t, double flatness) {
        return new FlatteningPathIterator(getPathIterator(t), flatness);
    }

    public Object clone() {
        try {
            GeneralPath p = (GeneralPath) base.MemberwiseClone();
            byte [] newTypes = new byte[this.types.Length];
            java.lang.SystemJ.arraycopy(this.types,0,newTypes,0,newTypes.Length);
            //p.types = types.clone();
            float [] newPoints = new float[this.points.Length];
            java.lang.SystemJ.arraycopy(this.points,0,newTypes,0,newPoints.Length);
            //p.points = points.clone();
            return p;
        } catch (java.lang.CloneNotSupportedException e) {
            throw new java.lang.InternalError();
        }
    }
    }
}
