using java = biz.ritter.javapi;
using System;

namespace biz.ritter.javapi.awt.geom
{
/**
 * @author Denis M. Kishenko
 */
    public abstract class Point2D : java.lang.Cloneable
    {

    public class Float : Point2D {

        public float x;
        public float y;

        public Float() {
        }

        public Float(float x, float y) {
            this.x = x;
            this.y = y;
        }

        public override double getX() {
            return x;
        }

        public override double getY() {
            return y;
        }

        public void setLocation(float x, float y) {
            this.x = x;
            this.y = y;
        }

        public override void setLocation(double x, double y) {
            this.x = (float)x;
            this.y = (float)y;
        }

        public override String ToString() {
            return this.getClass().getName() + "[x=" + x + ",y=" + y + "]"; //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
        }
    }

    public class Double : Point2D {

        public double x;
        public double y;

        public Double() {
        }

        public Double(double x, double y) {
            this.x = x;
            this.y = y;
        }

        public override double getX() {
            return x;
        }

        public override double getY() {
            return y;
        }

        public override void setLocation(double x, double y) {
            this.x = x;
            this.y = y;
        }

        public override String ToString() {
            return this.getClass().getName() + "[x=" + x + ",y=" + y + "]"; //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
        }
    }

    protected Point2D() {
    }

    public abstract double getX();

    public abstract double getY();

    public abstract void setLocation(double x, double y);

    public void setLocation(Point2D p) {
        setLocation(p.getX(), p.getY());
    }

    public static double distanceSq(double x1, double y1, double x2, double y2) {
        x2 -= x1;
        y2 -= y1;
        return x2 * x2 + y2 * y2;
    }

    public double distanceSq(double px, double py) {
        return Point2D.distanceSq(getX(), getY(), px, py);
    }

    public double distanceSq(Point2D p) {
        return Point2D.distanceSq(getX(), getY(), p.getX(), p.getY());
    }

    public static double distance(double x1, double y1, double x2, double y2) {
        return java.lang.Math.sqrt(distanceSq(x1, y1, x2, y2));
    }

    public double distance(double px, double py) {
        return java.lang.Math.sqrt(distanceSq(px, py));
    }

    public double distance(Point2D p) {
        return java.lang.Math.sqrt(distanceSq(p));
    }

    public Object clone() {
        try {
            return base.MemberwiseClone();
        } catch (java.lang.CloneNotSupportedException e) {
            throw new java.lang.InternalError();
        }
    }

    
    public override int GetHashCode() {
        org.apache.harmony.misc.HashCode hash = new org.apache.harmony.misc.HashCode();
        hash.append(getX());
        hash.append(getY());
        return hash.hashCode();
    }

    
    public override bool Equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (obj is Point2D) {
            Point2D p = (Point2D) obj;
            return getX() == p.getX() && getY() == p.getY();
        }
        return false;
    }
    }
}
