/*
 * Copyright (c) 1999 World Wide Web Consortium,
 * (Massachusetts Institute of Technology, Institut National de Recherche
 *  en Informatique et en Automatique, Keio University).
 * All Rights Reserved. http://www.w3.org/Consortium/Legal/
 */
namespace org.w3c.dom.views
{

    /**
     * A base interface that all views shall derive from.
     * @since DOM Level 2
     */
    public interface AbstractView
    {
        /**
         * The source <code>DocumentView</code> for which, this is an 
         * <code>AbstractView</code> of.
         */
        DocumentView getDocument();
    }

}