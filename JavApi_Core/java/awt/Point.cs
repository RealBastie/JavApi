using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt
{
    /**
     * @author Denis M. Kishenko
     */
    [Serializable]
    public class Point : java.awt.geom.Point2D, java.io.Serializable
    {

        private static readonly long serialVersionUID = -5276940640259749850L;

        public int x;
        public int y;

        public Point()
        {
            setLocation(0, 0);
        }

        public Point(int x, int y)
        {
            setLocation(x, y);
        }

        public Point(Point p)
        {
            setLocation(p.x, p.y);
        }


        public override bool Equals(Object obj)
        {
            if (obj == this)
            {
                return true;
            }
            if (obj is Point)
            {
                Point p = (Point)obj;
                return x == p.x && y == p.y;
            }
            return false;
        }


        public override String ToString()
        {
            return this.getClass().getName() + "[x=" + x + ",y=" + y + "]"; //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
        }

        public override double getX()
        {
            return x;
        }

        public override double getY()
        {
            return y;
        }

        public Point getLocation()
        {
            return new Point(x, y);
        }

        public void setLocation(Point p)
        {
            setLocation(p.x, p.y);
        }

        public void setLocation(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override void setLocation(double x, double y)
        {
            x = x < java.lang.Integer.MIN_VALUE ? java.lang.Integer.MIN_VALUE : x > java.lang.Integer.MAX_VALUE ? java.lang.Integer.MAX_VALUE : x;
            y = y < java.lang.Integer.MIN_VALUE ? java.lang.Integer.MIN_VALUE : y > java.lang.Integer.MAX_VALUE ? java.lang.Integer.MAX_VALUE : y;
            setLocation((int)java.lang.Math.round(x), (int)java.lang.Math.round(y));
        }

        public void move(int x, int y)
        {
            setLocation(x, y);
        }

        public void translate(int dx, int dy)
        {
            x += dx;
            y += dy;
        }

    }

}