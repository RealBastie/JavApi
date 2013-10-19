using System;
using System.Text;

using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt.geom
{
    /**
     * @author Denis M. Kishenko
     */
    public abstract class Dimension2D : java.lang.Cloneable
    {

        protected Dimension2D()
        {
        }

        public abstract double getWidth();

        public abstract double getHeight();

        public abstract void setSize(double width, double height);

        public void setSize(Dimension2D d)
        {
            setSize(d.getWidth(), d.getHeight());
        }

        public Object clone()
        {
            try
            {
                return base.MemberwiseClone();
            }
            catch (java.lang.CloneNotSupportedException e)
            {
                throw new java.lang.InternalError();
            }
        }
    }

}