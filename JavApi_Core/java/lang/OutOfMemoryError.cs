using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace biz.ritter.javapi.lang
{
    /// <summary>
    /// Only for API compatibility - DO NOT USE THIS CLASS with C#
    /// <para>catch ever System.OutOfMemoryException</para>
    /// </summary>
    public class OutOfMemoryError : VirtualMachineError
    {
        /// <summary>
        /// Only for API compatibility - DO NOT USE THIS CLASS with C#
        /// <para>catch ever System.OutOfMemoryException</para>
        /// </summary>
        public OutOfMemoryError() : base() { }

        /// <summary>
        /// Only for API compatibility - DO NOT USE THIS CLASS with C#
        /// <para>catch ever System.OutOfMemoryException</para>
        /// </summary>
        public OutOfMemoryError(String msg) : base(msg) { }

    }
}
