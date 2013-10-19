﻿// Attributes2Impl.java - extended AttributesImpl
// http://www.saxproject.org
// Public Domain: no warranty.
// $Id: org.xml.sax.ext.Attributes2Impl.cs 8775 2011-11-05 11:51:22Z unknown $
using System;
using java = biz.ritter.javapi;
using org.xml.sax.helpers;

namespace org.xml.sax.ext
{
    /**
     * SAX2 extension helper for additional Attributes information,
     * implementing the {@link Attributes2} interface.
     *
     * <blockquote>
     * <em>This module, both source code and documentation, is in the
     * Public Domain, and comes with <strong>NO WARRANTY</strong>.</em>
     * </blockquote>
     *
     * <p>This is not part of core-only SAX2 distributions.</p>
     *
     * <p>The <em>specified</em> flag for each attribute will always
     * be true, unless it has been set to false in the copy constructor
     * or using {@link #setSpecified}.
     * Similarly, the <em>declared</em> flag for each attribute will
     * always be false, except for defaulted attributes (<em>specified</em>
     * is false), non-CDATA attributes, or when it is set to true using
     * {@link #setDeclared}.
     * If you change an attribute's type by hand, you may need to modify
     * its <em>declared</em> flag to match. 
     * </p>
     *
     * @since SAX 2.0 (extensions 1.1 alpha)
     * @author David Brownell
     * @version TBS
     */
    public class Attributes2Impl : AttributesImpl, Attributes2
    {
        private bool[] declared;
        private bool[] specified;


        /**
         * Construct a new, empty Attributes2Impl object.
         */
        public Attributes2Impl() { }


        /**
         * Copy an existing Attributes or Attributes2 object.
         * If the object implements Attributes2, values of the
         * <em>specified</em> and <em>declared</em> flags for each
         * attribute are copied.
         * Otherwise the flag values are defaulted to assume no DTD was used,
         * unless there is evidence to the contrary (such as attributes with
         * type other than CDATA, which must have been <em>declared</em>).
         *
         * <p>This constructor is especially useful inside a
         * {@link org.xml.sax.ContentHandler#startElement startElement} event.</p>
         *
         * @param atts The existing Attributes object.
         */
        public Attributes2Impl(Attributes atts)
            : base(atts)
        {
        }


        ////////////////////////////////////////////////////////////////////
        // Implementation of Attributes2
        ////////////////////////////////////////////////////////////////////


        /**
         * Returns the current value of the attribute's "declared" flag.
         */
        // javadoc mostly from interface
        public bool isDeclared(int index)
        {
            if (index < 0 || index >= getLength())
                throw new java.lang.ArrayIndexOutOfBoundsException(
                "No attribute at index: " + index);
            return declared[index];
        }


        /**
         * Returns the current value of the attribute's "declared" flag.
         */
        // javadoc mostly from interface
        public bool isDeclared(String uri, String localName)
        {
            int index = getIndex(uri, localName);

            if (index < 0)
                throw new java.lang.IllegalArgumentException(
                "No such attribute: local=" + localName
                + ", namespace=" + uri);
            return declared[index];
        }


        /**
         * Returns the current value of the attribute's "declared" flag.
         */
        // javadoc mostly from interface
        public bool isDeclared(String qName)
        {
            int index = getIndex(qName);

            if (index < 0)
                throw new java.lang.IllegalArgumentException(
                "No such attribute: " + qName);
            return declared[index];
        }


        /**
         * Returns the current value of an attribute's "specified" flag.
         *
         * @param index The attribute index (zero-based).
         * @return current flag value
         * @exception java.lang.ArrayIndexOutOfBoundsException When the
         *            supplied index does not identify an attribute.
         */
        public bool isSpecified(int index)
        {
            if (index < 0 || index >= getLength())
                throw new java.lang.ArrayIndexOutOfBoundsException(
                "No attribute at index: " + index);
            return specified[index];
        }


        /**
         * Returns the current value of an attribute's "specified" flag.
         *
         * @param uri The Namespace URI, or the empty string if
         *        the name has no Namespace URI.
         * @param localName The attribute's local name.
         * @return current flag value
         * @exception java.lang.IllegalArgumentException When the
         *            supplied names do not identify an attribute.
         */
        public bool isSpecified(String uri, String localName)
        {
            int index = getIndex(uri, localName);

            if (index < 0)
                throw new java.lang.IllegalArgumentException(
                "No such attribute: local=" + localName
                + ", namespace=" + uri);
            return specified[index];
        }


        /**
         * Returns the current value of an attribute's "specified" flag.
         *
         * @param qName The XML qualified (prefixed) name.
         * @return current flag value
         * @exception java.lang.IllegalArgumentException When the
         *            supplied name does not identify an attribute.
         */
        public bool isSpecified(String qName)
        {
            int index = getIndex(qName);

            if (index < 0)
                throw new java.lang.IllegalArgumentException(
                "No such attribute: " + qName);
            return specified[index];
        }


        ////////////////////////////////////////////////////////////////////
        // Manipulators
        ////////////////////////////////////////////////////////////////////


        /**
         * Copy an entire Attributes object.  The "specified" flags are
         * assigned as true, and "declared" flags as false (except when
         * an attribute's type is not CDATA),
         * unless the object is an Attributes2 object.
         * In that case those flag values are all copied.
         *
         * @see AttributesImpl#setAttributes
         */
        public void setAttributes(Attributes atts)
        {
            int length = atts.getLength();

            base.setAttributes(atts);
            declared = new bool[length];
            specified = new bool[length];

            if (atts is Attributes2)
            {
                Attributes2 a2 = (Attributes2)atts;
                for (int i = 0; i < length; i++)
                {
                    declared[i] = a2.isDeclared(i);
                    specified[i] = a2.isSpecified(i);
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    declared[i] = !"CDATA".equals(atts.getType(i));
                    specified[i] = true;
                }
            }
        }


        /**
         * Add an attribute to the end of the list, setting its
         * "specified" flag to true.  To set that flag's value
         * to false, use {@link #setSpecified}.
         *
         * <p>Unless the attribute <em>type</em> is CDATA, this attribute
         * is marked as being declared in the DTD.  To set that flag's value
         * to true for CDATA attributes, use {@link #setDeclared}.
         *
         * @see AttributesImpl#addAttribute
         */
        public void addAttribute(String uri, String localName, String qName,
                      String type, String value)
        {
            base.addAttribute(uri, localName, qName, type, value);

            int length = getLength();

            if (length < specified.Length)
            {
                bool[] newFlags;

                newFlags = new bool[length];
                java.lang.SystemJ.arraycopy(declared, 0, newFlags, 0, declared.Length);
                declared = newFlags;

                newFlags = new bool[length];
                java.lang.SystemJ.arraycopy(specified, 0, newFlags, 0, specified.Length);
                specified = newFlags;
            }

            specified[length - 1] = true;
            declared[length - 1] = !"CDATA".equals(type);
        }


        // javadoc entirely from superclass
        public void removeAttribute(int index)
        {
            int origMax = getLength() - 1;

            base.removeAttribute(index);
            if (index != origMax)
            {
                java.lang.SystemJ.arraycopy(declared, index + 1, declared, index,
                    origMax - index);
                java.lang.SystemJ.arraycopy(specified, index + 1, specified, index,
                    origMax - index);
            }
        }


        /**
         * Assign a value to the "declared" flag of a specific attribute.
         * This is normally needed only for attributes of type CDATA,
         * including attributes whose type is changed to or from CDATA.
         *
         * @param index The index of the attribute (zero-based).
         * @param value The desired flag value.
         * @exception java.lang.ArrayIndexOutOfBoundsException When the
         *            supplied index does not identify an attribute.
         * @see #setType
         */
        public void setDeclared(int index, bool value)
        {
            if (index < 0 || index >= getLength())
                throw new java.lang.ArrayIndexOutOfBoundsException(
                "No attribute at index: " + index);
            declared[index] = value;
        }


        /**
         * Assign a value to the "specified" flag of a specific attribute.
         * This is the only way this flag can be cleared, except clearing
         * by initialization with the copy constructor.
         *
         * @param index The index of the attribute (zero-based).
         * @param value The desired flag value.
         * @exception java.lang.ArrayIndexOutOfBoundsException When the
         *            supplied index does not identify an attribute.
         */
        public void setSpecified(int index, bool value)
        {
            if (index < 0 || index >= getLength())
                throw new java.lang.ArrayIndexOutOfBoundsException(
                "No attribute at index: " + index);
            specified[index] = value;
        }
    }
}