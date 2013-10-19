using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt.font
{
    [Serializable]
    public sealed class TransformAttribute : java.io.Serializable
    {
        private static readonly long serialVersionUID = 3356247357827709530L;

        // affine transform of this TransformAttribute instance
        private java.awt.geom.AffineTransform fTransform;

        public TransformAttribute(java.awt.geom.AffineTransform transform)
        {
            if (transform == null) {
                // awt.94=transform can not be null
                throw new java.lang.IllegalArgumentException("transform can not be null"); //$NON-NLS-1$
            }
            if (!transform.isIdentity()){
                this.fTransform = new java.awt.geom.AffineTransform(transform);
            }
        }

        public java.awt.geom.AffineTransform getTransform()
        {
            if (fTransform != null){
                return new java.awt.geom.AffineTransform(fTransform);
            }
            return new java.awt.geom.AffineTransform();
        }

        public bool isIdentity() {
            return (fTransform == null);
        }


    }
}
