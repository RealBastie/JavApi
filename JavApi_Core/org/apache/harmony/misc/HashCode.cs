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
 */

using System;
using java = biz.ritter.javapi;

namespace org.apache.harmony.misc
{
/**
 * This class is a convenience method to sequentially calculate hash code of the
 * object based on the field values. The result depends on the order of elements
 * appended. The exact formula is the same as for
 * <code>java.util.List.hashCode</code>.
 * 
 * If you need order independent hash code just summate, multiply or XOR all
 * elements.
 * 
 * <p>
 * Suppose we have class:
 * 
 * <pre><code>
 * class Thing {
 *     long id;
 *     String name;
 *     float weight;
 * }
 * </code></pre>
 * 
 * The hash code calculation can be expressed in 2 forms.
 * 
 * <p>
 * For maximum performance:
 * 
 * <pre><code>
 * public int hashCode() {
 *     int hashCode = HashCode.EMPTY_HASH_CODE;
 *     hashCode = HashCode.combine(hashCode, id);
 *     hashCode = HashCode.combine(hashCode, name);
 *     hashCode = HashCode.combine(hashCode, weight);
 *     return hashCode;
 * }
 * </code></pre>
 * 
 * <p>
 * For convenience: <code><pre>
 * public int hashCode() {
 *     return new HashCode().append(id).append(name).append(weight).hashCode();
 * }
 * </code></pre>
 * 
 * @see java.util.List#hashCode()
 */
internal sealed class HashCode {
    /**
     * The hashCode value before any data is appended, equals to 1. 
     * @see java.util.List#hashCode()
     */
    public static readonly int EMPTY_HASH_CODE = 1;
    
    private int hashCodeJ = EMPTY_HASH_CODE;
    
    /**
     * Returns accumulated hashCode
     */
    public int hashCode() {
        return hashCodeJ;
    }
    
    /**
     * Combines hashCode of previous elements sequence and value's hashCode.
     * @param hashCode previous hashCode value
     * @param value new element
     * @return combined hashCode
     */
    public static int combine(int hashCode, bool value) {    
        int v = value ? 1231 : 1237;
        return combine(hashCode, v);
    }
    
    /**
     * Combines hashCode of previous elements sequence and value's hashCode.
     * @param hashCode previous hashCode value
     * @param value new element
     * @return combined hashCode
     */
    public static int combine(int hashCode, long value) {    
        int v = (int) (value ^ java.dotnet.lang.Operator.shiftRightUnsignet(value , 32));
        return combine(hashCode, v);
    }

    /**
     * Combines hashCode of previous elements sequence and value's hashCode.
     * @param hashCode previous hashCode value
     * @param value new element
     * @return combined hashCode
     */
    public static int combine(int hashCode, float value) {    
        int v = java.lang.Float.floatToIntBits(value);
        return combine(hashCode, v);
    }
    
    /**
     * Combines hashCode of previous elements sequence and value's hashCode.
     * @param hashCode previous hashCode value
     * @param value new element
     * @return combined hashCode
     */
    public static int combine(int hashCode, double value) {    
        long v = java.lang.Double.doubleToLongBits(value);
        return combine(hashCode, v);
    }

    /**
     * Combines hashCode of previous elements sequence and value's hashCode.
     * @param hashCode previous hashCode value
     * @param value new element
     * @return combined hashCode
     */
    public static int combine(int hashCode, Object value) {
        return combine(hashCode, value.GetHashCode());
    }
    
    /**
     * Combines hashCode of previous elements sequence and value's hashCode.
     * @param hashCode previous hashCode value
     * @param value new element
     * @return combined hashCode
     */
    public static int combine(int hashCode, int value) {
        return 31 * hashCode + value;
    }
    
    /**
     * Appends value's hashCode to the current hashCode.
     * @param value new element
     * @return this
     */
    public HashCode append(int value) {
        hashCodeJ = combine(hashCodeJ, value);
        return this;
    }
    
    /**
     * Appends value's hashCode to the current hashCode.
     * @param value new element
     * @return this
     */
    public HashCode append(long value) {
        hashCodeJ = combine(hashCodeJ, value);
        return this;
    }
    
    /**
     * Appends value's hashCode to the current hashCode.
     * @param value new element
     * @return this
     */
    public HashCode append(float value) {
        hashCodeJ = combine(hashCodeJ, value);
        return this;
    }
    
    /**
     * Appends value's hashCode to the current hashCode.
     * @param value new element
     * @return this
     */
    public HashCode append(double value) {
        hashCodeJ = combine(hashCodeJ, value);
        return this;
    }
    
    /**
     * Appends value's hashCode to the current hashCode.
     * @param value new element
     * @return this
     */
    public HashCode append(bool value) {
        hashCodeJ = combine(hashCodeJ, value);
        return this;
    }
    
    /**
     * Appends value's hashCode to the current hashCode.
     * @param value new element
     * @return this
     */
    public HashCode append(Object value) {
        hashCodeJ = combine(hashCodeJ, value);
        return this;
    }
}
}
