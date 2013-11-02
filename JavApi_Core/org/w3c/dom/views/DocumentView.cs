/*
 * Copyright (c) 1999 World Wide Web Consortium,
 * (Massachusetts Institute of Technology, Institut National de Recherche
 *  en Informatique et en Automatique, Keio University).
 * All Rights Reserved. http://www.w3.org/Consortium/Legal/
 */

namespace org.w3c.dom.views
{

    /**
     * The <code>DocumentView</code> interface is implemented by 
     * <code>Document</code> objects in DOM implementations supporting DOM Views. 
     * It provides an attribute to retrieve the default view of a document.
     * @since DOM Level 2
     */
    public interface DocumentView
    {
        /**
         * The default <code>AbstractView</code> for this <code>Document</code>, or 
         * <code>null</code> if none available.
         */
         AbstractView getDefaultView();
    }

}