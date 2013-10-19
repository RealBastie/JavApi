using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt.geom
{
    /**
     * @author Denis M. Kishenko
     */

    public class IllegalPathStateException : java.lang.RuntimeException
    {

        private static readonly long serialVersionUID = -5158084205220481094L;

        public IllegalPathStateException()
        {
        }

        public IllegalPathStateException(String s)
            : base(s)
        {
        }

    }

}