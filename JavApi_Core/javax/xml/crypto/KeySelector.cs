/*
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at 
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 *  
 */
/*
 * Copyright 2005 Sun Microsystems, Inc. All rights reserved.
 */

using System;
using java = biz.ritter.javapi;
using javax = biz.ritter.javapix;

namespace biz.ritter.javapix.xml.crypto
{

//import javax.xml.crypto.dsig.keyinfo.KeyInfo;
//import javax.xml.crypto.dsig.keyinfo.RetrievalMethod;

/**
 * A selector that finds and returns a key using the data contained in a
 * {@link KeyInfo} object. An example of an implementation of
 * this class is one that searchs a {@link java.security.KeyStore} for 
 * trusted keys that match information contained in a <code>KeyInfo</code>.
 *
 * <p>Whether or not the returned key is trusted and the mechanisms 
 * used to determine that is implementation-specific.
 *
 * @author Sean Mullan
 * @author JSR 105 Expert Group
 */
public abstract class KeySelector {

    /**
     * The purpose of the key that is to be selected.
     */
    public class Purpose {

        private readonly String name;

        private Purpose(String name) 	{ this.name = name; }

        /**
         * Returns a string representation of this purpose ("sign",
         * "verify", "encrypt", or "decrypt").
         *
         * @return a string representation of this purpose
         */
        public String toString()	{ return name; } 	

        /**
         * A key for signing.
         */
        public static readonly Purpose SIGN = new Purpose("sign");
        /**
         * A key for verifying.
         */
        public static readonly Purpose VERIFY = new Purpose("verify");
        /**
         * A key for encrypting.
         */
        public static readonly Purpose ENCRYPT = new Purpose("encrypt");
        /**
         * A key for decrypting.
         */
        public static readonly Purpose DECRYPT = new Purpose("decrypt");
    }

    /**
     * Default no-args constructor; intended for invocation by subclasses only.
     */
    protected KeySelector() {}

    /**
     * Attempts to find a key that satisfies the specified constraints.
     *
     * @param keyInfo a <code>KeyInfo</code> (may be <code>null</code>)
     * @param purpose the key's purpose ({@link Purpose#SIGN}, 
     *    {@link Purpose#VERIFY}, {@link Purpose#ENCRYPT}, or 
     *    {@link Purpose#DECRYPT})
     * @param method the algorithm method that this key is to be used for.
     *    Only keys that are compatible with the algorithm and meet the 
     *    constraints of the specified algorithm should be returned.
     * @param context an <code>XMLCryptoContext</code> that may contain
     *    useful information for finding an appropriate key. If this key 
     *    selector supports resolving {@link RetrievalMethod} types, the 
     *    context's <code>baseURI</code> and <code>dereferencer</code> 
     *    parameters (if specified) should be used by the selector to 
     *    resolve and dereference the URI.
     * @return the result of the key selector
     * @throws KeySelectorException if an exceptional condition occurs while 
     *    attempting to find a key. Note that an inability to find a key is not 
     *    considered an exception (<code>null</code> should be
     *    returned in that case). However, an error condition (ex: network 
     *    communications failure) that prevented the <code>KeySelector</code>
     *    from finding a potential key should be considered an exception.
     * @throws ClassCastException if the data type of <code>method</code> 
     *    is not supported by this key selector
     */
    public abstract KeySelectorResult select(javax.xml.crypto.dsig.keyinfo.KeyInfo keyInfo, Purpose purpose, 
        AlgorithmMethod method, XMLCryptoContext context) 
       ;// throws KeySelectorException;

    /**
     * Returns a <code>KeySelector</code> that always selects the specified
     * key, regardless of the <code>KeyInfo</code> passed to it.
     *
     * @param key the sole key to be stored in the key selector
     * @return a key selector that always selects the specified key
     * @throws NullPointerException if <code>key</code> is <code>null</code>
     */
    public static KeySelector singletonKeySelector(java.security.Key key) {
        return new SingletonKeySelector(key);
    }

    internal class SingletonKeySelector : KeySelector {
        internal readonly java.security.Key key;

        internal SingletonKeySelector(java.security.Key key) {
            if (key == null) {
                throw new java.lang.NullPointerException();
            }
            this.key = key;
        }

		internal class IAC_KeySelectorResult : KeySelectorResult {
		   private readonly SingletonKeySelector sks;
		   internal IAC_KeySelectorResult (SingletonKeySelector sks) {
		     this.sks = sks;
		   }
		   public java.security.Key getKey() {
		     return this.sks.key;
		   }
		}
        public override KeySelectorResult select(javax.xml.crypto.dsig.keyinfo.KeyInfo keyInfo, 
        	Purpose purpose,
            AlgorithmMethod method, 
            XMLCryptoContext context) 
            {//throws KeySelectorException {
             	return new IAC_KeySelectorResult(this);
				
            }
        }
    }
}
