/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements. See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership. The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the  "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
/*
 * $Id: XPathAPI.java 524807 2007-04-02 15:51:43Z zongaro $
 */

using System;
using java = biz.ritter.javapi;

// Basties Apache XALAN-J project
namespace org.apache.xpath {

	/*
import javax.xml.transform.TransformerException;

import org.apache.xml.utils.PrefixResolver;
import org.apache.xml.utils.PrefixResolverDefault;
import org.apache.xpath.objects.XObject;

import org.w3c.dom.Document;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.w3c.dom.traversal.NodeIterator;
*/


/**
 * The methods in this class are convenience methods into the
 * low-level XPath API.
 * These functions tend to be a little slow, since a number of objects must be
 * created for each evaluation.  A faster way is to precompile the
 * XPaths using the low-level API, and then just use the XPaths
 * over and over.
 *
 * NOTE: In particular, each call to this method will create a new
 * XPathContext, a new DTMManager... and thus a new DTM. That's very
 * safe, since it guarantees that you're always processing against a
 * fully up-to-date view of your document. But it's also portentially
 * very expensive, since you're rebuilding the DTM every time. You should
 * consider using an instance of CachedXPathAPI rather than these static
 * methods.
 *
 * @see <a href="http://www.w3.org/TR/xpath">XPath Specification</a> 
 * */
public class XPathAPI
{


  /**
   *  Use an XPath string to select a nodelist.
   *  XPath namespace prefixes are resolved from the contextNode.
   *
   *  @param contextNode The node to start searching from.
   *  @param str A valid XPath string.
   *  @return A NodeIterator, should never be null.
   *
   * @throws TransformerException
   */
  public static NodeList selectNodeList(Node contextNode, String str)
         // throws TransformerException
  {
    return selectNodeList(contextNode, str, contextNode);
  }

  /**
   *  Use an XPath string to select a nodelist.
   *  XPath namespace prefixes are resolved from the namespaceNode.
   *
   *  @param contextNode The node to start searching from.
   *  @param str A valid XPath string.
   *  @param namespaceNode The node from which prefixes in the XPath will be resolved to namespaces.
   *  @return A NodeIterator, should never be null.
   *
   * @throws TransformerException
   */
  public static NodeList selectNodeList(
          Node contextNode, String str, Node namespaceNode)
            //throws TransformerException
  {

    // Execute the XPath, and have it return the result
			throw new java.lang.UnsupportedOperationException ("XALAN-J not yet implemented");
    XObject list = eval(contextNode, str, namespaceNode);

    // Return a NodeList.
    return list.nodelist();
  }

}
}