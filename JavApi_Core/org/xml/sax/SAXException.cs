// SAX exception class.
// http://www.saxproject.org
// No warranty; no copyright -- use this as you will.
// $Id$

using System;
using java = biz.ritter.javapi;

namespace org.xml.sax
{

    /**
     * Encapsulate a general SAX error or warning.
     *
     * <blockquote>
     * <em>This module, both source code and documentation, is in the
     * Public Domain, and comes with <strong>NO WARRANTY</strong>.</em>
     * See <a href='http://www.saxproject.org'>http://www.saxproject.org</a>
     * for further information.
     * </blockquote>
     *
     * <p/>This class can contain basic error or warning information from
     * either the XML parser or the application: a parser writer or
     * application writer can subclass it to provide additional
     * functionality.  SAX handlers may throw this exception or
     * any exception subclassed from it.
     *
     * <p/>If the application needs to pass through other types of
     * exceptions, it must wrap those exceptions in a SAXException
     * or an exception derived from a SAXException.
     *
     * <p/>If the parser or application needs to include information about a
     * specific location in an XML document, it should use the
     * {@link org.xml.sax.SAXParseException SAXParseException} subclass.
     *
     * @since SAX 1.0
     * @author David Megginson
     * @version 2.0.1 (sax2r2)
     * @see org.xml.sax.SAXParseException
     */
    public class SAXException : java.lang.Exception
    {


        /**
         * Create a new SAXException.
         */
        public SAXException()
            : base()
        {
            this.exception = null;
        }


        /**
         * Create a new SAXException.
         *
         * @param message The error or warning message.
         */
        public SAXException(String message)
            : base(message)
        {
            this.exception = null;
        }


        /**
         * Create a new SAXException wrapping an existing exception.
         *
         * <p/>The existing exception will be embedded in the new
         * one, and its message will become the default message for
         * the SAXException.
         *
         * @param e The exception to be wrapped in a SAXException.
         */
        public SAXException(Exception e)
            : base()
        {
            this.exception = e;
        }


        /**
         * Create a new SAXException from an existing exception.
         *
         * <p/>The existing exception will be embedded in the new
         * one, but the new exception will have its own message.
         *
         * @param message The detail message.
         * @param e The exception to be wrapped in a SAXException.
         */
        public SAXException(String message, Exception e)
            : base(message)
        {
            this.exception = e;
        }


        /**
         * Return a detail message for this exception.
         *
         * <p/>If there is an embedded exception, and if the SAXException
         * has no detail message of its own, this method will return
         * the detail message from the embedded exception.
         *
         * @return The error or warning message.
         */
        public override String getMessage()
        {
            String message = base.getMessage();

            if (message == null && exception != null)
            {
                return exception.getMessage();
            }
            else
            {
                return message;
            }
        }


        /**
         * Return the embedded exception, if any.
         *
         * @return The embedded exception, or null if there is none.
         */
        public Exception getException()
        {
            return exception;
        }


        /**
         * Override toString to pick up any embedded exception.
         *
         * @return A string representation of this exception.
         */
        public override String ToString()
        {
            if (exception != null)
            {
                return exception.ToString();
            }
            else
            {
                return base.ToString();
            }
        }



        //////////////////////////////////////////////////////////////////////
        // Internal state.
        //////////////////////////////////////////////////////////////////////


        /**
         * @serial The embedded exception if tunnelling, or null.
         */
        private Exception exception;

    }

    // end of SAXException.java
}