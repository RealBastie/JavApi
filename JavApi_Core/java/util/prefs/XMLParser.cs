/* Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using java = biz.ritter.javapi;
using javax = biz.ritter.javapix;

/*import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.FactoryConfigurationError;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.TransformerException;

import org.apache.harmony.prefs.internal.nls.Messages;
import org.apache.xpath.XPathAPI;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;
import org.xml.sax.EntityResolver;
import org.xml.sax.ErrorHandler;
import org.xml.sax.InputSource;
import org.xml.sax.SAXException;
import org.xml.sax.SAXParseException;
*/
namespace biz.ritter.javapi.util.prefs
{
	/**
 * Utility class for the Preferences import/export from XML file.
 */
	internal class XMLParser
	{
		/*
     * Constant - the specified DTD URL
     */
		const String PREFS_DTD_NAME = "http://java.sun.com/dtd/preferences.dtd";
		//$NON-NLS-1$
		/*
     * Constant - the DTD string
     */
		const String PREFS_DTD = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
		                           + "    <!ELEMENT preferences (root)>"
		                           + "    <!ATTLIST preferences EXTERNAL_XML_VERSION CDATA \"0.0\" >"
		                           + "    <!ELEMENT root (map, node*) >"
		                           + "    <!ATTLIST root type (system|user) #REQUIRED >"
		                           + "    <!ELEMENT node (map, node*) >"
		                           + "    <!ATTLIST node name CDATA #REQUIRED >"
		                           + "    <!ELEMENT map (entry*) >"
		                           + "    <!ELEMENT entry EMPTY >"
		                           + "    <!ATTLIST entry key   CDATA #REQUIRED value CDATA #REQUIRED >";
		/*
     * Constant - the specified header
     */
		const String HEADER = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
		//$NON-NLS-1$
		/*
     * Constant - the specified DOCTYPE
     */
		const String DOCTYPE = "<!DOCTYPE preferences SYSTEM";
		//$NON-NLS-1$
		/*
     * empty string array constant
     */
		private static readonly String[] EMPTY_SARRAY = new String[0];
		/*
     * Constant - used by FilePreferencesImpl, which is default implementation
     * of Linux platform
     */
		private const String FILE_PREFS = "<!DOCTYPE map SYSTEM 'http://java.sun.com/dtd/preferences.dtd'>";
		//$NON-NLS-1$
		/*
     * Constant - specify the DTD version
     */
		private const float XML_VERSION = 1.0f;
		/*
     * DOM builder
     */
		private static readonly javax.xml.parsers.DocumentBuilder builder;
		/*
     * specify the indent level
     */
		private static int indent = -1;
		/*
     * init DOM builder
     */
		static XMLParser ()
		{
			javax.xml.parsers.DocumentBuilderFactory factory = javax.xml.parsers.DocumentBuilderFactory.newInstance ();
			factory.setValidating (true);
			try {
				builder = factory.newDocumentBuilder ();
			} catch (javax.xml.parsers.ParserConfigurationException e) {
				throw new java.lang.Error (e);
			}
			builder.setEntityResolver (new IAC_EntityResolver ());
			builder.setErrorHandler (new IAC_ErrorHandler ());
		}

		class IAC_ErrorHandler : org.xml.sax.ErrorHandler
		{
			public void warning (org.xml.sax.SAXParseException e)
			{// throws SAXException {
				throw e;
			}

			public void error (org.xml.sax.SAXParseException e)
			{//throws SAXException {
				throw e;
			}

			public void fatalError (org.xml.sax.SAXParseException e)
			{//throws SAXException {
				throw e;
			}
		}

		class IAC_EntityResolver : org.xml.sax.EntityResolver
		{
			public org.xml.sax.InputSource resolveEntity (String publicId, String systemId)
			{//	throws SAXException, IOException {
				if (systemId.equals (PREFS_DTD_NAME)) {
					org.xml.sax.InputSource result = new org.xml.sax.InputSource (new java.io.StringReader (
						                                 PREFS_DTD));
					result.setSystemId (PREFS_DTD_NAME);
					return result;
				}
				// prefs.1=Invalid DOCTYPE declaration: {0}
				throw new org.xml.sax.SAXException ("Invalid DOCTYPE declaration: " + systemId); //$NON-NLS-1$
			}
		}

		private XMLParser ()
		{// empty constructor
		}
		/*
     * Utilities for Preferences export
     */
		internal static void exportPrefs (Preferences prefs, java.io.OutputStream stream,
		                                   bool withSubTree)
		{//throws IOException, BackingStoreException {
			indent = -1;
			java.io.BufferedWriter outJ = new java.io.BufferedWriter (new java.io.OutputStreamWriter (stream,
			                                                                                             "UTF-8"));
			outJ.write (HEADER);
			outJ.newLine ();
			outJ.newLine ();

			outJ.write (DOCTYPE);
			outJ.write (" '");
			outJ.write (PREFS_DTD_NAME);
			outJ.write ("'>");
			outJ.newLine ();
			outJ.newLine ();

			flushStartTag ("preferences", new String[] { "EXTERNAL_XML_VERSION" },
			                   new String[] { java.lang.StringJ.valueOf (XML_VERSION) }, outJ);
			flushStartTag ("root", new String[] { "type" }, new String[] { prefs
                .isUserNode () ? "user" : "system"
			}, outJ);
			flushEmptyElement ("map", outJ);

			StringTokenizer ancestors = new StringTokenizer (prefs.absolutePath (),
			                                                     "/");
			exportNode (ancestors, prefs, withSubTree, outJ);

			flushEndTag ("root", outJ);
			flushEndTag ("preferences", outJ);
			outJ.flush ();
			outJ = null;
		}

		private static void exportNode (StringTokenizer ancestors,
		                                 Preferences prefs, bool withSubTree, java.io.BufferedWriter outJ)
		{//throws IOException, BackingStoreException {
			if (ancestors.hasMoreTokens ()) {
				String name = ancestors.nextToken ();
				flushStartTag (
					"node", new String[] { "name" }, new String[] { name }, outJ); //$NON-NLS-1$ //$NON-NLS-2$
				if (ancestors.hasMoreTokens ()) {
					flushEmptyElement ("map", outJ); //$NON-NLS-1$
					exportNode (ancestors, prefs, withSubTree, outJ);
				} else {
					exportEntries (prefs, outJ);
					if (withSubTree) {
						exportSubTree (prefs, outJ);
					}
				}
				flushEndTag ("node", outJ); //$NON-NLS-1$
			}
		}

		private static void exportSubTree (Preferences prefs, java.io.BufferedWriter outJ)
		{//throws BackingStoreException, IOException {
			String[] names = prefs.childrenNames ();
			if (names.Length > 0) {
				for (int i = 0; i < names.Length; i++) {
					Preferences child = prefs.node (names [i]);
					flushStartTag (
						"node", new String[] { "name" }, new String[] { names [i] }, outJ); //$NON-NLS-1$ //$NON-NLS-2$
					exportEntries (child, outJ);
					exportSubTree (child, outJ);
					flushEndTag ("node", outJ); //$NON-NLS-1$
				}
			}
		}

		private static void exportEntries (Preferences prefs, java.io.BufferedWriter outJ)
		{//throws BackingStoreException, IOException {
			String[] keys = prefs.keys ();
			String[] values = new String[keys.Length];
			for (int i = 0; i < keys.Length; i++) {
				values [i] = prefs.get (keys [i], null);
			}
			exportEntries (keys, values, outJ);
		}

		private static void exportEntries (String[] keys, String[] values,
		                                    java.io.BufferedWriter outJ)
		{// throws IOException {
			if (keys.Length == 0) {
				flushEmptyElement ("map", outJ); //$NON-NLS-1$
				return;
			}
			flushStartTag ("map", outJ); //$NON-NLS-1$
			for (int i = 0; i < keys.Length; i++) {
				if (values [i] != null) {
					flushEmptyElement (
						"entry", new String[] { "key", "value" }, new String[] { keys [i], values [i] }, outJ); //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
				}
			}
			flushEndTag ("map", outJ); //$NON-NLS-1$
		}

		private static void flushEndTag (String tagName, java.io.BufferedWriter outJ)
		{//throws IOException {
			flushIndent (indent--, outJ);
			outJ.write ("</"); //$NON-NLS-1$
			outJ.write (tagName);
			outJ.write (">"); //$NON-NLS-1$
			outJ.newLine ();
		}

		private static void flushEmptyElement (String tagName, java.io.BufferedWriter outJ)
		{//throws IOException {
			flushIndent (++indent, outJ);
			outJ.write ("<"); //$NON-NLS-1$
			outJ.write (tagName);
			outJ.write (" />"); //$NON-NLS-1$
			outJ.newLine ();
			indent--;
		}

		private static void flushEmptyElement (String tagName, String[] attrKeys,
		                                        String[] attrValues, java.io.BufferedWriter outJ)
		{// throws IOException {
			flushIndent (++indent, outJ);
			outJ.write ("<"); //$NON-NLS-1$
			outJ.write (tagName);
			flushPairs (attrKeys, attrValues, outJ);
			outJ.write (" />"); //$NON-NLS-1$
			outJ.newLine ();
			indent--;
		}

		private static void flushPairs (String[] attrKeys, String[] attrValues,
		                                 java.io.BufferedWriter outJ)
		{// throws IOException {
			for (int i = 0; i < attrKeys.Length; i++) {
				outJ.write (" "); //$NON-NLS-1$
				outJ.write (attrKeys [i]);
				outJ.write ("=\""); //$NON-NLS-1$
				outJ.write (htmlEncode (attrValues [i]));
				outJ.write ("\""); //$NON-NLS-1$
			}
		}

		private static void flushIndent (int ind, java.io.BufferedWriter outJ)
		{//throws IOException {
			for (int i = 0; i < ind; i++) {
				outJ.write ("  "); //$NON-NLS-1$
			}
		}

		private static void flushStartTag (String tagName, String[] attrKeys,
		                                    String[] attrValues, java.io.BufferedWriter outJ)
		{// throws IOException {
			flushIndent (++indent, outJ);
			outJ.write ("<"); //$NON-NLS-1$
			outJ.write (tagName);
			flushPairs (attrKeys, attrValues, outJ);
			outJ.write (">"); //$NON-NLS-1$
			outJ.newLine ();
		}

		private static void flushStartTag (String tagName, java.io.BufferedWriter outJ)
		{//throws IOException {
			flushIndent (++indent, outJ);
			outJ.write ("<"); //$NON-NLS-1$
			outJ.write (tagName);
			outJ.write (">"); //$NON-NLS-1$
			outJ.newLine ();
		}

		private static String htmlEncode (String s)
		{
			java.lang.StringBuilder sb = new java.lang.StringBuilder ();
			char c;
			for (int i = 0; i < s.length(); i++) {
				c = s.charAt (i);
				switch (c) {
				case '<':
					sb.append ("&lt;"); //$NON-NLS-1$
					break;
				case '>':
					sb.append ("&gt;"); //$NON-NLS-1$
					break;
				case '&':
					sb.append ("&amp;"); //$NON-NLS-1$
					break;
				case '\\':
					sb.append ("&apos;"); //$NON-NLS-1$
					break;
				case '"':
					sb.append ("&quot;"); //$NON-NLS-1$
					break;
				default:
					sb.append (c);
					break;
				}
			}
			return sb.toString ();
		}
		/*
     * Utilities for Preferences import
     */
		internal static void importPrefs (java.io.InputStream inJ) //throws IOException,
		{//InvalidPreferencesFormatException {
			try {
				// load XML document
				org.w3c.dom.Document doc = builder.parse (new org.xml.sax.InputSource (inJ));

				// check preferences' export version
				org.w3c.dom.Element preferences;
				preferences = doc.getDocumentElement ();
				String version = preferences.getAttribute ("EXTERNAL_XML_VERSION"); //$NON-NLS-1$
				if (version != null && java.lang.Float.parseFloat (version) > XML_VERSION) {
					// prefs.2=This preferences exported version is not
					// supported:{0}
					throw new java.util.prefs.InvalidPreferencesFormatException ("his preferences exported version is not supported:" + version); //$NON-NLS-1$
				}

				// check preferences root's type
				org.w3c.dom.Element root = (org.w3c.dom.Element)preferences
                    .getElementsByTagName ("root").item (0); //$NON-NLS-1$
				Preferences prefsRoot = null;
				String type = root.getAttribute ("type"); //$NON-NLS-1$
				if (type.equals ("user")) { //$NON-NLS-1$
					prefsRoot = Preferences.userRoot ();
				} else {
					prefsRoot = Preferences.systemRoot ();
				}

				// load node
				loadNode (prefsRoot, root);
			} catch (javax.xml.parsers.FactoryConfigurationError e) {
				throw new InvalidPreferencesFormatException (e);
			} catch (org.xml.sax.SAXException e) {
				throw new InvalidPreferencesFormatException (e);
			} catch (javax.xml.transform.TransformerException e) {
				throw new InvalidPreferencesFormatException (e);
			}
		}

		private static void loadNode (Preferences prefs, org.w3c.dom.Element node)
		{//throws TransformerException {
			// load preferences
			org.w3c.dom.NodeList children = org.apache.xpath.XPathAPI.selectNodeList (node, "node"); //$NON-NLS-1$
			org.w3c.dom.NodeList entries = org.apache.xpath.XPathAPI.selectNodeList (node, "map/entry"); //$NON-NLS-1$
			int childNumber = children.getLength ();
			Preferences[] prefChildren = new Preferences[childNumber];
			int entryNumber = entries.getLength ();
			lock (((AbstractPreferences) prefs).lockJ) {
				if (((AbstractPreferences)prefs).isRemoved ()) {
					return;
				}
				for (int i = 0; i < entryNumber; i++) {
					org.w3c.dom.Element entry = (org.w3c.dom.Element)entries.item (i);
					String key = entry.getAttribute ("key"); //$NON-NLS-1$
					String value = entry.getAttribute ("value"); //$NON-NLS-1$
					prefs.put (key, value);
				}
				// get children preferences node
				for (int i = 0; i < childNumber; i++) {
					org.w3c.dom.Element child = (org.w3c.dom.Element)children.item (i);
					String name = child.getAttribute ("name"); //$NON-NLS-1$
					prefChildren [i] = prefs.node (name);
				}
			}

			// load children nodes after unlock
			for (int i = 0; i < childNumber; i++) {
				loadNode (prefChildren [i], (org.w3c.dom.Element)children.item (i));
			}
		}

		/**
     * Load preferences from file, if cannot load, create a new one.
     * 
     * @param file
     *            the XML file to be read
     * @return Properties instance which indicates the preferences key-value
     *         pairs
     */
		static java.util.Properties loadFilePrefs (java.io.File file)
		{
			//FIXME: need lock or not?
			return loadFilePrefsImpl (file);
                    
		}

		static java.util.Properties loadFilePrefsImpl (java.io.File file)
		{
			Properties result = new Properties ();
			if (!file.exists ()) {
				file.getParentFile ().mkdirs ();
				return result;
			}

			if (file.canRead ()) {
				java.io.InputStream inJ = null;
				java.nio.channels.FileLock lockJ = null;
				try {
					java.io.FileInputStream istream = new java.io.FileInputStream (file);
					inJ = new java.io.BufferedInputStream (istream);
					java.nio.channels.FileChannel channel = istream.getChannel ();
					lockJ = channel.lockJ (0L, java.lang.Long.MAX_VALUE, true);
					org.w3c.dom.Document doc = builder.parse (inJ);
					org.w3c.dom.NodeList entries = org.apache.xpath.XPathAPI.selectNodeList (doc
                        .getDocumentElement (), "entry"); //$NON-NLS-1$
					int length = entries.getLength ();
					for (int i = 0; i < length; i++) {
						org.w3c.dom.Element node = (org.w3c.dom.Element)entries.item (i);
						String key = node.getAttribute ("key"); //$NON-NLS-1$
						String value = node.getAttribute ("value"); //$NON-NLS-1$
						result.setProperty (key, value);
					}
					return result;
				} catch (java.io.IOException e) {
				} catch (org.xml.sax.SAXException e) {
				} catch (javax.xml.transform.TransformerException e) {
					// transform shouldn't fail for xpath call
					throw new java.lang.AssertionError (e);
				} finally {
					releaseQuietly (lockJ);
					closeQuietly (inJ);
				}
			} else {
				file.delete ();
			}
			return result;
		}

		static void flushFilePrefs (java.io.File file, java.util.Properties prefs)
		{//throws PrivilegedActionException {
			flushFilePrefsImpl (file, prefs);
		}

		static void flushFilePrefsImpl (java.io.File file, java.util.Properties prefs)
		{//throws IOException {
			java.io.BufferedWriter outJ = null;
			java.nio.channels.FileLock lockJ = null;
			try {
				java.io.FileOutputStream ostream = new java.io.FileOutputStream (file);
				outJ = new java.io.BufferedWriter (new java.io.OutputStreamWriter (ostream, "UTF-8")); //$NON-NLS-1$
				java.nio.channels.FileChannel channel = ostream.getChannel ();
				lockJ = channel.lockJ ();
				outJ.write (HEADER);
				outJ.newLine ();
				outJ.write (FILE_PREFS);
				outJ.newLine ();
				if (prefs.size () == 0) {
					exportEntries (EMPTY_SARRAY, EMPTY_SARRAY, outJ);
				} else {
					String[] keys = prefs.keySet ()
                        .toArray (new String[prefs.size ()]);
					int length = keys.Length;
					String[] values = new String[length];
					for (int i = 0; i < length; i++) {
						values [i] = prefs.getProperty (keys [i]);
					}
					exportEntries (keys, values, outJ);
				}
				outJ.flush ();
			} finally {
				releaseQuietly (lockJ);
				closeQuietly (outJ);
			}
		}

		private static void releaseQuietly (java.nio.channels.FileLock lockJ)
		{
			if (lockJ == null) {
				return;
			}
			try {
				lockJ.release ();
			} catch (java.io.IOException e) {
			}
		}

		private static void closeQuietly (java.io.Writer outJ)
		{
			if (outJ == null) {
				return;
			}
			try {
				outJ.close ();
			} catch (java.io.IOException e) {
			}
		}

		private static void closeQuietly (java.io.InputStream inJ)
		{
			if (inJ == null) {
				return;
			}
			try {
				inJ.close ();
			} catch (java.io.IOException e) {
			}
		}
	}
}