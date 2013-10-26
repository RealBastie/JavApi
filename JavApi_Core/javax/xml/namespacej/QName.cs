/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
// $Id: QName.java 446598 2006-09-15 12:55:40Z jeremias $

using System;
using java = biz.ritter.javapi;
using javax = biz.ritter.javapix;

namespace biz.ritter.javapix.xml.namespacej
{

    /** 
     * &lt;p&gt;&lt;code&gt;QName&lt;/code&gt; represents a &lt;strong&gt;qualified name&lt;/strong&gt;
     * as defined in the XML specifications: &lt;a
     * href="http://www.w3.org/TR/xmlschema-2/#QName"&gt;XML Schema Part2:
     * Datatypes specification&lt;/a&gt;, &lt;a
     * href="http://www.w3.org/TR/REC-xml-names/#ns-qualnames"&gt;Namespaces
     * in XML&lt;/a&gt;, &lt;a
     * href="http://www.w3.org/XML/xml-names-19990114-errata"&gt;Namespaces
     * in XML Errata&lt;/a&gt;.&lt;/p&gt;
     *
     * &lt;p&gt;The value of a &lt;code&gt;QName&lt;/code&gt; contains a &lt;strong&gt;Namespace
     * URI&lt;/strong&gt;, &lt;strong&gt;local part&lt;/strong&gt; and
     * &lt;strong&gt;prefix&lt;/strong&gt;.&lt;/p&gt;
     *
     * &lt;p&gt;The prefix is included in &lt;code&gt;QName&lt;/code&gt; to retain lexical
     * information &lt;strong&gt;&lt;em&gt;when present&lt;/em&gt;&lt;/strong&gt; in an {@link
     * javax.xml.transform.Source XML input source}. The prefix is
     * &lt;strong&gt;&lt;em&gt;NOT&lt;/em&gt;&lt;/strong&gt; used in {@link #equals(Object)
     * QName.equals(Object)} or to compute the {@link #hashCode()
     * QName.hashCode()}.  Equality and the hash code are defined using
     * &lt;strong&gt;&lt;em&gt;only&lt;/em&gt;&lt;/strong&gt; the Namespace URI and local part.&lt;/p&gt;
     *
     * &lt;p&gt;If not specified, the Namespace URI is set to {@link
     * javax.xml.XMLConstants#NULL_NS_URI XMLConstants.NULL_NS_URI}.
     * If not specified, the prefix is set to {@link
     * javax.xml.XMLConstants#DEFAULT_NS_PREFIX
     * XMLConstants.DEFAULT_NS_PREFIX}.&lt;/p&gt;
     *
     * &lt;p&gt;&lt;code&gt;QName&lt;/code&gt; is immutable.&lt;/p&gt;
     *
     * @author &lt;a href="mailto:Jeff.Suttor@Sun.com"&gt;Jeff Suttor&lt;/a&gt;
     * @version $Revision: 446598 $, $Date: 2006-09-15 08:55:40 -0400 (Fri, 15 Sep 2006) $
     * @see &lt;a href="http://www.w3.org/TR/xmlschema-2/#QName"&gt;XML Schema Part2: Datatypes specification&lt;/a&gt;
     * @see &lt;a href="http://www.w3.org/TR/REC-xml-names/#ns-qualnames"&gt;Namespaces in XML&lt;/a&gt;
     * @see &lt;a href="http://www.w3.org/XML/xml-names-19990114-errata"&gt;Namespaces in XML Errata&lt;/a&gt;
     * @since 1.5
     */
    [Serializable]
    public class QName : java.io.Serializable
    {

        /**
         * &lt;p&gt;Stream Unique Identifier.&lt;/p&gt;
         * 
         * &lt;p&gt;To enable the compatibility &lt;code&gt;serialVersionUID&lt;/code&gt;
         * set the System Property
         * &lt;code&gt;org.apache.xml.namespace.QName.useCompatibleSerialVersionUID&lt;/code&gt;
         * to a value of "1.0".&lt;/p&gt;
         */
        private static readonly long serialVersionUID;

        /**
         * &lt;p&gt;The original default Stream Unique Identifier.&lt;/p&gt;
         */
        private const long defaultSerialVersionUID = -9120448754896609940L;

        /**
         * &lt;p&gt;The compatibility Stream Unique Identifier that was introduced
         * with Java 5 SE SDK.&lt;/p&gt;
         */
        private const long compatabilitySerialVersionUID = 4418622981026545151L;

        static QName()
        {
            String compatPropValue = null;
            try
            {
                compatPropValue = java.lang.SystemJ.getProperty("org.apache.xml.namespace.QName.useCompatibleSerialVersionUID");
            }
            catch (Exception e) { }
            // If 1.0 use compatability serialVersionUID
            serialVersionUID = !"1.0".equals(compatPropValue) ? defaultSerialVersionUID : compatabilitySerialVersionUID;
        }

        /**
         * &lt;p&gt;Namespace URI of this &lt;code&gt;QName&lt;/code&gt;.&lt;/p&gt;
         */
        private readonly String namespaceURI;

        /**
         * &lt;p&gt;local part of this &lt;code&gt;QName&lt;/code&gt;.&lt;/p&gt;
         */
        private readonly String localPart;

        /**
         * &lt;p&gt;prefix of this &lt;code&gt;QName&lt;/code&gt;.&lt;/p&gt;
         */
        private String prefix;

        /**
         * &lt;p&gt;&lt;code&gt;String&lt;/code&gt; representation of this &lt;code&gt;QName&lt;/code&gt;.&lt;/p&gt;
         */
        [NonSerialized]
        private String qNameAsString;

        /** 
         * &lt;p&gt;&lt;code&gt;QName&lt;/code&gt; constructor specifying the Namespace URI
         * and local part.&lt;/p&gt;
         *
         * &lt;p&gt;If the Namespace URI is &lt;code&gt;null&lt;/code&gt;, it is set to
         * {@link javax.xml.XMLConstants#NULL_NS_URI
         * XMLConstants.NULL_NS_URI}.  This value represents no
         * explicitly defined Namespace as defined by the &lt;a
         * href="http://www.w3.org/TR/REC-xml-names/#ns-qualnames"&gt;Namespaces
         * in XML&lt;/a&gt; specification.  This action preserves compatible
         * behavior with QName 1.0.  Explicitly providing the {@link
         * javax.xml.XMLConstants#NULL_NS_URI
         * XMLConstants.NULL_NS_URI} value is the preferred coding
         * style.&lt;/p&gt;
         *
         * &lt;p&gt;If the local part is &lt;code&gt;null&lt;/code&gt; an
         * &lt;code&gt;IllegalArgumentException&lt;/code&gt; is thrown.
         * A local part of "" is allowed to preserve
         * compatible behavior with QName 1.0. &lt;/p&gt;
         *
         * &lt;p&gt;When using this constructor, the prefix is set to {@link
         * javax.xml.XMLConstants#DEFAULT_NS_PREFIX
         * XMLConstants.DEFAULT_NS_PREFIX}.&lt;/p&gt;
         *
         * &lt;p&gt;The Namespace URI is not validated as a
         * &lt;a href="http://www.ietf.org/rfc/rfc2396.txt"&gt;URI reference&lt;/a&gt;.
         * The local part is not validated as a
         * &lt;a href="http://www.w3.org/TR/REC-xml-names/#NT-NCName"&gt;NCName&lt;/a&gt;
         * as specified in &lt;a href="http://www.w3.org/TR/REC-xml-names/"&gt;Namespaces
         * in XML&lt;/a&gt;.&lt;/p&gt;
         *
         * @param namespaceURI Namespace URI of the &lt;code&gt;QName&lt;/code&gt;
         * @param localPart    local part of the &lt;code&gt;QName&lt;/code&gt;
         * 
         * @see #QName(String namespaceURI, String localPart, String
         * prefix) QName(String namespaceURI, String localPart, String
         * prefix)
         */
        public QName(String namespaceURI, String localPart) :
            this(namespaceURI, localPart, XMLConstants.DEFAULT_NS_PREFIX)
        {
        }

        /** 
         * &lt;p&gt;&lt;code&gt;QName&lt;/code&gt; constructor specifying the Namespace URI,
         * local part and prefix.&lt;/p&gt;
         *
         * &lt;p&gt;If the Namespace URI is &lt;code&gt;null&lt;/code&gt;, it is set to
         * {@link javax.xml.XMLConstants#NULL_NS_URI
         * XMLConstants.NULL_NS_URI}.  This value represents no
         * explicitly defined Namespace as defined by the &lt;a
         * href="http://www.w3.org/TR/REC-xml-names/#ns-qualnames"&gt;Namespaces
         * in XML&lt;/a&gt; specification.  This action preserves compatible
         * behavior with QName 1.0.  Explicitly providing the {@link
         * javax.xml.XMLConstants#NULL_NS_URI
         * XMLConstants.NULL_NS_URI} value is the preferred coding
         * style.&lt;/p&gt;
         * 
         * &lt;p&gt;If the local part is &lt;code&gt;null&lt;/code&gt; an
         * &lt;code&gt;IllegalArgumentException&lt;/code&gt; is thrown.
         * A local part of "" is allowed to preserve
         * compatible behavior with QName 1.0. &lt;/p&gt;
         * 
         * &lt;p&gt;If the prefix is &lt;code&gt;null&lt;/code&gt;, an
         * &lt;code&gt;IllegalArgumentException&lt;/code&gt; is thrown.  Use {@link
         * javax.xml.XMLConstants#DEFAULT_NS_PREFIX
         * XMLConstants.DEFAULT_NS_PREFIX} to explicitly indicate that no
         * prefix is present or the prefix is not relevant.&lt;/p&gt;
         *
         * &lt;p&gt;The Namespace URI is not validated as a
         * &lt;a href="http://www.ietf.org/rfc/rfc2396.txt"&gt;URI reference&lt;/a&gt;.
         * The local part and prefix are not validated as a
         * &lt;a href="http://www.w3.org/TR/REC-xml-names/#NT-NCName"&gt;NCName&lt;/a&gt;
         * as specified in &lt;a href="http://www.w3.org/TR/REC-xml-names/"&gt;Namespaces
         * in XML&lt;/a&gt;.&lt;/p&gt;
         *
         * @param namespaceURI Namespace URI of the &lt;code&gt;QName&lt;/code&gt;
         * @param localPart    local part of the &lt;code&gt;QName&lt;/code&gt;
         * @param prefix       prefix of the &lt;code&gt;QName&lt;/code&gt;
         */
        public QName(String namespaceURI, String localPart, String prefix)
        {

            // map null Namespace URI to default to preserve compatibility with QName 1.0
            if (namespaceURI == null)
            {
                this.namespaceURI = XMLConstants.NULL_NS_URI;
            }
            else
            {
                this.namespaceURI = namespaceURI;
            }

            // local part is required.  "" is allowed to preserve compatibility with QName 1.0        
            if (localPart == null)
            {
                throw new java.lang.IllegalArgumentException("local part cannot be \"null\" when creating a QName");
            }
            this.localPart = localPart;

            // prefix is required        
            if (prefix == null)
            {
                throw new java.lang.IllegalArgumentException("prefix cannot be \"null\" when creating a QName");
            }
            this.prefix = prefix;
        }

        /** 
         * &lt;p&gt;&lt;code&gt;QName&lt;/code&gt; constructor specifying the local part.&lt;/p&gt;
         *
         * &lt;p&gt;If the local part is &lt;code&gt;null&lt;/code&gt; an
         * &lt;code&gt;IllegalArgumentException&lt;/code&gt; is thrown.
         * A local part of "" is allowed to preserve
         * compatible behavior with QName 1.0. &lt;/p&gt;
         *
         * &lt;p&gt;When using this constructor, the Namespace URI is set to
         * {@link javax.xml.XMLConstants#NULL_NS_URI
         * XMLConstants.NULL_NS_URI} and the prefix is set to {@link
         * javax.xml.XMLConstants#DEFAULT_NS_PREFIX
         * XMLConstants.DEFAULT_NS_PREFIX}.&lt;/p&gt;
         *
         * &lt;p&gt;&lt;em&gt;In an XML context, all Element and Attribute names exist
         * in the context of a Namespace.  Making this explicit during the
         * construction of a &lt;code&gt;QName&lt;/code&gt; helps prevent hard to
         * diagnosis XML validity errors.  The constructors {@link
         * #QName(String namespaceURI, String localPart) QName(String
         * namespaceURI, String localPart)} and
         * {@link #QName(String namespaceURI, String localPart, String prefix)} 
         * are preferred.&lt;/em&gt;&lt;/p&gt;
         * 
         * &lt;p&gt;The local part is not validated as a
         * &lt;a href="http://www.w3.org/TR/REC-xml-names/#NT-NCName"&gt;NCName&lt;/a&gt;
         * as specified in &lt;a href="http://www.w3.org/TR/REC-xml-names/"&gt;Namespaces
         * in XML&lt;/a&gt;.&lt;/p&gt;
         *
         * @param localPart local part of the &lt;code&gt;QName&lt;/code&gt;
         * @see #QName(String namespaceURI, String localPart) QName(String
         * namespaceURI, String localPart)
         * @see #QName(String namespaceURI, String localPart, String
         * prefix) QName(String namespaceURI, String localPart, String
         * prefix)
         */
        public QName(String localPart) :
            this(
                XMLConstants.NULL_NS_URI,
                localPart,
                XMLConstants.DEFAULT_NS_PREFIX)
        {
        }

        /** 
         * &lt;p&gt;Get the Namespace URI of this &lt;code&gt;QName&lt;/code&gt;.&lt;/p&gt;
         *
         * @return Namespace URI of this &lt;code&gt;QName&lt;/code&gt;
         */
        public virtual String getNamespaceURI()
        {
            return namespaceURI;
        }

        /**
         * &lt;p&gt;Get the local part of this &lt;code&gt;QName&lt;/code&gt;.&lt;/p&gt;
         *
         *  @return local part of this &lt;code&gt;QName&lt;/code&gt;
         */
        public virtual String getLocalPart()
        {
            return localPart;
        }

        /** 
         * &lt;p&gt;Get the prefix of this &lt;code&gt;QName&lt;/code&gt;.&lt;/p&gt;
         *
         * &lt;p&gt;The prefix assigned to a &lt;code&gt;QName&lt;/code&gt; might
         * &lt;strong&gt;&lt;em&gt;NOT&lt;/em&gt;&lt;/strong&gt; be valid in a different
         * context. For example, a &lt;code&gt;QName&lt;/code&gt; may be assigned a
         * prefix in the context of parsing a document but that prefix may
         * be invalid in the context of a different document.&lt;/p&gt;
         *
         *  @return prefix of this &lt;code&gt;QName&lt;/code&gt;
         */
        public virtual String getPrefix()
        {
            return prefix;
        }

        /**
         * &lt;p&gt;Test this &lt;code&gt;QName&lt;/code&gt; for equality with another
         * &lt;code&gt;Object&lt;/code&gt;.&lt;/p&gt;
         *
         * &lt;p&gt;If the &lt;code&gt;Object&lt;/code&gt; to be tested is not a
         * &lt;code&gt;QName&lt;/code&gt; or is &lt;code&gt;null&lt;/code&gt;, then this method
         * returns &lt;code&gt;false&lt;/code&gt;.&lt;/p&gt;
         *
         * &lt;p&gt;Two &lt;code&gt;QName&lt;/code&gt;s are considered equal if and only if
         * both the Namespace URI and local part are equal. This method
         * uses &lt;code&gt;String.equals()&lt;/code&gt; to check equality of the
         * Namespace URI and local part. The prefix is
         * &lt;strong&gt;&lt;em&gt;NOT&lt;/em&gt;&lt;/strong&gt; used to determine equality.&lt;/p&gt;
         *
         * &lt;p&gt;This method satisfies the general contract of {@link
         * java.lang.Object#equals(Object) Object.equals(Object)}&lt;/p&gt;
         *
         * @param objectToTest the &lt;code&gt;Object&lt;/code&gt; to test for
         * equality with this &lt;code&gt;QName&lt;/code&gt;
         * @return &lt;code&gt;true&lt;/code&gt; if the given &lt;code&gt;Object&lt;/code&gt; is
         * equal to this &lt;code&gt;QName&lt;/code&gt; else &lt;code&gt;false&lt;/code&gt;
         */
        public override bool Equals(Object objectToTest)
        {
            // Is this the same object?
            if (objectToTest == this)
            {
                return true;
            }
            // Is this a QName?
            if (objectToTest is QName)
            {
                QName qName = (QName)objectToTest;
                return localPart.equals(qName.localPart) && namespaceURI.equals(qName.namespaceURI);
            }
            return false;
        }

        /**
         * &lt;p&gt;Generate the hash code for this &lt;code&gt;QName&lt;/code&gt;.&lt;/p&gt;
         *
         * &lt;p&gt;The hash code is calculated using both the Namespace URI and
         * the local part of the &lt;code&gt;QName&lt;/code&gt;.  The prefix is
         * &lt;strong&gt;&lt;em&gt;NOT&lt;/em&gt;&lt;/strong&gt; used to calculate the hash
         * code.&lt;/p&gt;
         *
         * &lt;p&gt;This method satisfies the general contract of {@link
         * java.lang.Object#hashCode() Object.hashCode()}.&lt;/p&gt;
         *
         * @return hash code for this &lt;code&gt;QName&lt;/code&gt; &lt;code&gt;Object&lt;/code&gt;
         */
        public override int GetHashCode()
        {
            return namespaceURI.GetHashCode() ^ localPart.GetHashCode();
        }

        /** 
         * &lt;p&gt;&lt;code&gt;String&lt;/code&gt; representation of this
         * &lt;code&gt;QName&lt;/code&gt;.&lt;/p&gt;
         *
         * &lt;p&gt;The commonly accepted way of representing a &lt;code&gt;QName&lt;/code&gt;
         * as a &lt;code&gt;String&lt;/code&gt; was &lt;a href="http://jclark.com/xml/xmlns.htm"&gt;defined&lt;/a&gt;
         * by James Clark.  Although this is not a &lt;em&gt;standard&lt;/em&gt;
         * specification, it is in common use,  e.g. {@link javax.xml.transform.Transformer#setParameter(String name, Object value)}.
         * This implementation represents a &lt;code&gt;QName&lt;/code&gt; as:
         * "{" + Namespace URI + "}" + local part.  If the Namespace URI
         * &lt;code&gt;.equals(XMLConstants.NULL_NS_URI)&lt;/code&gt;, only the
         * local part is returned.  An appropriate use of this method is
         * for debugging or logging for human consumption.&lt;/p&gt;
         *
         * &lt;p&gt;Note the prefix value is &lt;strong&gt;&lt;em&gt;NOT&lt;/em&gt;&lt;/strong&gt;
         * returned as part of the &lt;code&gt;String&lt;/code&gt; representation.&lt;/p&gt;
         *  
         * &lt;p&gt;This method satisfies the general contract of {@link
         * java.lang.Object#toString() Object.toString()}.&lt;/p&gt;
         *
         *  @return &lt;code&gt;String&lt;/code&gt; representation of this &lt;code&gt;QName&lt;/code&gt;
         */
        public override String ToString()
        {
            String _qNameAsString = qNameAsString;
            if (_qNameAsString == null)
            {
                int nsLength = namespaceURI.length();
                if (nsLength == 0)
                {
                    _qNameAsString = localPart;
                }
                else
                {
                    java.lang.StringBuffer buffer = new java.lang.StringBuffer(nsLength + localPart.length() + 2);
                    buffer.append('{');
                    buffer.append(namespaceURI);
                    buffer.append('}');
                    buffer.append(localPart);
                    _qNameAsString = buffer.toString();
                }
                qNameAsString = _qNameAsString;
            }
            return _qNameAsString;
        }

        /** 
         * &lt;p&gt;&lt;code&gt;QName&lt;/code&gt; derived from parsing the formatted
         * &lt;code&gt;String&lt;/code&gt;.&lt;/p&gt;
         *
         * &lt;p&gt;If the &lt;code&gt;String&lt;/code&gt; is &lt;code&gt;null&lt;/code&gt; or does not conform to
         * {@link #toString() QName.toString()} formatting, an
         * &lt;code&gt;IllegalArgumentException&lt;/code&gt; is thrown.&lt;/p&gt;
         *  
         * &lt;p&gt;&lt;em&gt;The &lt;code&gt;String&lt;/code&gt; &lt;strong&gt;MUST&lt;/strong&gt; be in the
         * form returned by {@link #toString() QName.toString()}.&lt;/em&gt;&lt;/p&gt;

         * &lt;p&gt;The commonly accepted way of representing a &lt;code&gt;QName&lt;/code&gt;
         * as a &lt;code&gt;String&lt;/code&gt; was &lt;a href="http://jclark.com/xml/xmlns.htm"&gt;defined&lt;/a&gt;
         * by James Clark.  Although this is not a &lt;em&gt;standard&lt;/em&gt;
         * specification, it is in common use,  e.g. {@link javax.xml.transform.Transformer#setParameter(String name, Object value)}.
         * This implementation parses a &lt;code&gt;String&lt;/code&gt; formatted
         * as: "{" + Namespace URI + "}" + local part.  If the Namespace
         * URI &lt;code&gt;.equals(XMLConstants.NULL_NS_URI)&lt;/code&gt;, only the
         * local part should be provided.&lt;/p&gt;
         *
         * &lt;p&gt;The prefix value &lt;strong&gt;&lt;em&gt;CANNOT&lt;/em&gt;&lt;/strong&gt; be
         * represented in the &lt;code&gt;String&lt;/code&gt; and will be set to
         * {@link javax.xml.XMLConstants#DEFAULT_NS_PREFIX
         * XMLConstants.DEFAULT_NS_PREFIX}.&lt;/p&gt;
         *
         * &lt;p&gt;This method does not do full validation of the resulting
         * &lt;code&gt;QName&lt;/code&gt;. 
         * &lt;p&gt;The Namespace URI is not validated as a
         * &lt;a href="http://www.ietf.org/rfc/rfc2396.txt"&gt;URI reference&lt;/a&gt;.
         * The local part is not validated as a
         * &lt;a href="http://www.w3.org/TR/REC-xml-names/#NT-NCName"&gt;NCName&lt;/a&gt;
         * as specified in
         * &lt;a href="http://www.w3.org/TR/REC-xml-names/"&gt;Namespaces in XML&lt;/a&gt;.&lt;/p&gt;
         *
         * @param qNameAsString &lt;code&gt;String&lt;/code&gt; representation
         * of the &lt;code&gt;QName&lt;/code&gt;
         * @return &lt;code&gt;QName&lt;/code&gt; corresponding to the given &lt;code&gt;String&lt;/code&gt;
         * @see #toString() QName.toString()
         */
        public static QName valueOf(String qNameAsString)
        {

            // null is not valid
            if (qNameAsString == null)
            {
                throw new java.lang.IllegalArgumentException("cannot create QName from \"null\" or \"\" String");
            }

            // "" local part is valid to preserve compatible behavior with QName 1.0
            if (qNameAsString.length() == 0)
            {
                return new QName(
                    XMLConstants.NULL_NS_URI,
                    qNameAsString,
                    XMLConstants.DEFAULT_NS_PREFIX);
            }

            // local part only?
            if (qNameAsString.charAt(0) != '{')
            {
                return new QName(
                    XMLConstants.NULL_NS_URI,
                    qNameAsString,
                    XMLConstants.DEFAULT_NS_PREFIX);
            }

            // Namespace URI improperly specified?
            if (qNameAsString.startsWith("{" + XMLConstants.NULL_NS_URI + "}"))
            {
                throw new java.lang.IllegalArgumentException(
                    "Namespace URI .equals(XMLConstants.NULL_NS_URI), "
                    + ".equals(\"" + XMLConstants.NULL_NS_URI + "\"), "
                    + "only the local part, "
                    + "\"" + qNameAsString.substring(2 + XMLConstants.NULL_NS_URI.length()) + "\", "
                    + "should be provided.");
            }

            // Namespace URI and local part specified
            int endOfNamespaceURI = qNameAsString.indexOf('}');
            if (endOfNamespaceURI == -1)
            {
                throw new java.lang.IllegalArgumentException(
                    "cannot create QName from \""
                        + qNameAsString
                        + "\", missing closing \"}\"");
            }
            return new QName(
                qNameAsString.substring(1, endOfNamespaceURI),
                qNameAsString.substring(endOfNamespaceURI + 1),
                XMLConstants.DEFAULT_NS_PREFIX);
        }

        /*
         * For old versions of QName which didn't have a prefix field,
         * &lt;code&gt;ObjectInputStream.defaultReadObject()&lt;/code&gt; will initialize
         * the prefix to &lt;code&gt;null&lt;/code&gt; instead of the empty string. This
         * method fixes up the prefix field if it didn't exist in the serialized
         * object.
         */
        private void readObject(java.io.ObjectInputStream inJ)
        {//throws IOException, ClassNotFoundException {
            inJ.defaultReadObject();
            if (prefix == null)
            {
                prefix = XMLConstants.DEFAULT_NS_PREFIX;
            }
        }
    }
}