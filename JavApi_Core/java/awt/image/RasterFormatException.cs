using System;
using java= biz.ritter.javapi;

namespace biz.ritter.javapi.awt.image
{
/**
 * @author Igor V. Stolyarov
 */


public class RasterFormatException : java.lang.RuntimeException {

    private static readonly long serialVersionUID = 96598996116164315L;

    public RasterFormatException(String s) :base(s){
    }

}

}
