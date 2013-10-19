using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt.geom
{
/**
 * @author Denis M. Kishenko
 */
public abstract class RectangularShape : Shape, java.lang.Cloneable {

    protected RectangularShape() {
    }

    public abstract double getX();

    public abstract double getY();

    public abstract double getWidth();

    public abstract double getHeight();

    public abstract bool isEmpty();

    public abstract void setFrame(double x, double y, double w, double h);

    public double getMinX() {
        return getX();
    }

    public double getMinY() {
        return getY();
    }

    public double getMaxX() {
        return getX() + getWidth();
    }

    public double getMaxY() {
        return getY() + getHeight();
    }

    public double getCenterX() {
        return getX() + getWidth() / 2.0;
    }

    public double getCenterY() {
        return getY() + getHeight() / 2.0;
    }

    public Rectangle2D getFrame() {
        return new Rectangle2D.Double(getX(), getY(), getWidth(), getHeight());
    }

    public void setFrame(Point2D loc, Dimension2D size) {
        setFrame(loc.getX(), loc.getY(), size.getWidth(), size.getHeight());
    }

    public void setFrame(Rectangle2D r) {
        setFrame(r.getX(), r.getY(), r.getWidth(), r.getHeight());
    }

    public void setFrameFromDiagonal(double x1, double y1, double x2, double y2) {
        double rx, ry, rw, rh;
        if (x1 < x2) {
            rx = x1;
            rw = x2 - x1;
        } else {
            rx = x2;
            rw = x1 - x2;
        }
        if (y1 < y2) {
            ry = y1;
            rh = y2 - y1;
        } else {
            ry = y2;
            rh = y1 - y2;
        }
        setFrame(rx, ry, rw, rh);
    }

    public void setFrameFromDiagonal(Point2D p1, Point2D p2) {
        setFrameFromDiagonal(p1.getX(), p1.getY(), p2.getX(), p2.getY());
    }

    public void setFrameFromCenter(double centerX, double centerY, double cornerX, double cornerY) {
        double width = java.lang.Math.abs(cornerX - centerX);
        double height = java.lang.Math.abs(cornerY - centerY);
        setFrame(centerX - width, centerY - height, width * 2.0, height * 2.0);
    }

    public void setFrameFromCenter(Point2D center, Point2D corner) {
        setFrameFromCenter(center.getX(), center.getY(), corner.getX(), corner.getY());
    }

    public bool contains(Point2D point) {
        return contains(point.getX(), point.getY());
    }

    public bool intersects(Rectangle2D rect) {
        return intersects(rect.getX(), rect.getY(), rect.getWidth(), rect.getHeight());
    }

    public bool contains(Rectangle2D rect) {
        return contains(rect.getX(), rect.getY(), rect.getWidth(), rect.getHeight());
    }

    public virtual Rectangle getBounds() {
        int x1 = (int)java.lang.Math.floor(getMinX());
        int y1 = (int)java.lang.Math.floor(getMinY());
        int x2 = (int)java.lang.Math.ceil(getMaxX());
        int y2 = (int)java.lang.Math.ceil(getMaxY());
        return new Rectangle(x1, y1, x2 - x1, y2 - y1);
    }

    public virtual PathIterator getPathIterator(AffineTransform t, double flatness) {
        return new FlatteningPathIterator(getPathIterator(t), flatness);
    }

    public abstract bool contains(double x, double y);
    public abstract bool contains(double x, double y, double w, double h);
    public abstract bool intersects(double x, double y, double w, double h);
    public abstract java.awt.geom.PathIterator getPathIterator(java.awt.geom.AffineTransform at);
    public abstract java.awt.geom.Rectangle2D getBounds2D();

    public Object clone()
    {
        try {
            return base.MemberwiseClone();
        } catch (java.lang.CloneNotSupportedException e) {
            throw new java.lang.InternalError();
        }
    }

}

}
