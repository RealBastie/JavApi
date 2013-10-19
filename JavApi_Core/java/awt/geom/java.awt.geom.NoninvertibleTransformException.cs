using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using java =biz.ritter.javapi;

namespace biz.ritter.javapi.awt.geom
{
    /**
     * @author Denis M. Kishenko
     */
    [Serializable]
    public class NoninvertibleTransformException : java.lang.Exception
    {

        private static readonly long serialVersionUID = 6137225240503990466L;

        public NoninvertibleTransformException(String s)
            : base()
        {
        }

    }
}