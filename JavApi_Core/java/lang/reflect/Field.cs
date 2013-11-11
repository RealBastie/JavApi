/* 
 * Licensed to the Apache Software Foundation (ASF) under one or more
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

namespace biz.ritter.javapi.lang.reflect{

/**
 * This class represents a field. Information about the field can be accessed,
 * and the field's value can be accessed dynamically.
 */
public sealed class Field : AccessibleObject, Member {

		private String name;


    /*
     * This class must be implemented by the VM vendor.
     */

    /**
     * Prevent this class from being instantiated
     */
    private Field(){
        //do nothing
    }
		internal Field (String name){
			this.name = name;
		}

    /*
     * Returns the field's signature in non-printable form. This is called
     * (only) from IO native code and needed for deriving the serialVersionUID
     * of the class
     *
     * @return the field's signature.
     */
    //native String getSignature();

    /**
     * Indicates whether or not this field is synthetic.
     *
     * @return {@code true} if this field is synthetic, {@code false} otherwise
     */
    public bool isSynthetic() {
        return false;
    }

    /**
     * Returns the string representation of this field, including the field's
     * generic type.
     *
     * @return the string representation of this field
     * @since 1.5
     */
    public String toGenericString() {
        return null;
    }

    /**
     * Indicates whether or not this field is an enumeration constant.
     *
     * @return {@code true} if this field is an enumeration constant, {@code
     *         false} otherwise
     * @since 1.5
     */
    public bool isEnumConstant() {
        return false;
    }

    /**
     * Returns the generic type of this field.
     *
     * @return the generic type
     * @throws GenericSignatureFormatError
     *             if the generic field signature is invalid
     * @throws TypeNotPresentException
     *             if the generic type points to a missing type
     * @throws MalformedParameterizedTypeException
     *             if the generic type points to a type that cannot be
     *             instantiated for some reason
     * @since 1.5
     */
    public Type getGenericType() {
        return null;
    }

    /**
     * Indicates whether or not the specified {@code object} is equal to this
     * field. To be equal, the specified object must be an instance of
     * {@code Field} with the same declaring class, type and name as this field.
     *
     * @param object
     *            the object to compare
     * @return {@code true} if the specified object is equal to this method,
     *         {@code false} otherwise
     * @see #hashCode
     */
    public override bool Equals(Object objectJ) {
		return false;
	}

    /**
     * Returns the value of the field in the specified object. This reproduces
     * the effect of {@code object.fieldName}
     * <p/>
     * If the type of this field is a primitive type, the field value is
     * automatically wrapped.
     * <p/>
     * If this field is static, the object argument is ignored.
     * Otherwise, if the object is null, a NullPointerException is thrown. If
     * the object is not an instance of the declaring class of the method, an
     * IllegalArgumentException is thrown.
     * <p/>
     * If this Field object is enforcing access control (see AccessibleObject)
     * and this field is not accessible from the current context, an
     * IllegalAccessException is thrown.
     *
     * @param object
     *            the object to access
     * @return the field value, possibly wrapped
     * @throws NullPointerException
     *             if the object is {@code null} and the field is non-static
     * @throws IllegalArgumentException
     *             if the object is not compatible with the declaring class
     * @throws IllegalAccessException
     *             if this field is not accessible
     */
		public Object get(Object objectJ) {
			Type type = objectJ.GetType ();
			System.Reflection.FieldInfo f = type.GetField (this.name);
			Object resultObj = f.GetValue (objectJ);
			return resultObj;
		}

    /**
     * Returns the value of the field in the specified object as a {@code
     * boolean}. This reproduces the effect of {@code object.fieldName}
     * <p>
     * If this field is static, the object argument is ignored.
     * Otherwise, if the object is {@code null}, a NullPointerException is
     * thrown. If the object is not an instance of the declaring class of the
     * method, an IllegalArgumentException is thrown.
     * <p>
     * If this Field object is enforcing access control (see AccessibleObject)
     * and this field is not accessible from the current context, an
     * IllegalAccessException is thrown.
     *
     * @param object
     *            the object to access
     * @return the field value
     * @throws NullPointerException
     *             if the object is {@code null} and the field is non-static
     * @throws IllegalArgumentException
     *             if the object is not compatible with the declaring class
     * @throws IllegalAccessException
     *             if this field is not accessible
     */
	public bool getBoolean(Object objectJ) {
			Type type = objectJ.GetType ();
			System.Reflection.FieldInfo f = type.GetField (this.name);
			Object resultObj = f.GetValue (objectJ);

			if (resultObj is Boolean) return ((Boolean)resultObj);
			if (resultObj is java.lang.Boolean)
				return (((java.lang.Boolean)resultObj).booleanValue());
			throw new IllegalArgumentException ("Attempt to get int field " + objectJ.getClass().getName()+"."+this.getName() + " with illegal data type conversion to " + "boolean");
		}

    /**
     * Returns the value of the field in the specified object as a {@code byte}.
     * This reproduces the effect of {@code object.fieldName}
     * <p/>
     * If this field is static, the object argument is ignored.
     * Otherwise, if the object is {@code null}, a NullPointerException is
     * thrown. If the object is not an instance of the declaring class of the
     * method, an IllegalArgumentException is thrown.
     * <p/>
     * If this Field object is enforcing access control (see AccessibleObject)
     * and this field is not accessible from the current context, an
     * IllegalAccessException is thrown.
     *
     * @param object
     *            the object to access
     * @return the field value
     * @throws NullPointerException
     *             if the object is {@code null} and the field is non-static
     * @throws IllegalArgumentException
     *             if the object is not compatible with the declaring class
     * @throws IllegalAccessException
     *             if this field is not accessible
     */
		public byte getByte(Object objectJ) {
			Type type = objectJ.GetType ();
			System.Reflection.FieldInfo f = type.GetField (this.name);
			Object resultObj = f.GetValue (objectJ);

			if (resultObj is Byte) return ((Byte)resultObj);
			if (resultObj is java.lang.Byte)
				return (((java.lang.Byte)resultObj).byteValue());
			throw new IllegalArgumentException ("Attempt to get int field " + objectJ.getClass().getName()+"."+this.getName() + " with illegal data type conversion to " + "byte");
		}

    /**
     * Returns the value of the field in the specified object as a {@code char}.
     * This reproduces the effect of {@code object.fieldName}
     * <p/>
     * If this field is static, the object argument is ignored.
     * Otherwise, if the object is {@code null}, a NullPointerException is
     * thrown. If the object is not an instance of the declaring class of the
     * method, an IllegalArgumentException is thrown.
     * <p/>
     * If this Field object is enforcing access control (see AccessibleObject)
     * and this field is not accessible from the current context, an
     * IllegalAccessException is thrown.
     *
     * @param object
     *            the object to access
     * @return the field value
     * @throws NullPointerException
     *             if the object is {@code null} and the field is non-static
     * @throws IllegalArgumentException
     *             if the object is not compatible with the declaring class
     * @throws IllegalAccessException
     *             if this field is not accessible
     */
		public char getChar(Object objectJ) {
			Type type = objectJ.GetType ();
			System.Reflection.FieldInfo f = type.GetField (this.name);
			Object resultObj = f.GetValue (objectJ);

			if (resultObj is Char) return ((Char)resultObj);
			if (resultObj is java.lang.Character)
				return (((java.lang.Character)resultObj).charValue());
			throw new IllegalArgumentException ("Attempt to get int field " + objectJ.getClass().getName()+"."+this.getName() + " with illegal data type conversion to " + "char");
		}

    /**
     * Returns the class that declares this field.
     *
     * @return the declaring class
     */
	public java.lang.Class getDeclaringClass() {
		return null;
	}

    /**
     * Returns the value of the field in the specified object as a {@code
     * double}. This reproduces the effect of {@code object.fieldName}
     * <p>
     * If this field is static, the object argument is ignored.
     * Otherwise, if the object is {@code null}, a NullPointerException is
     * thrown. If the object is not an instance of the declaring class of the
     * method, an IllegalArgumentException is thrown.
     * <p>
     * If this Field object is enforcing access control (see AccessibleObject)
     * and this field is not accessible from the current context, an
     * IllegalAccessException is thrown.
     *
     * @param object
     *            the object to access
     * @return the field value
     * @throws NullPointerException
     *             if the object is {@code null} and the field is non-static
     * @throws IllegalArgumentException
     *             if the object is not compatible with the declaring class
     * @throws IllegalAccessException
     *             if this field is not accessible
     */
		public double getDouble(Object objectJ){
			Type type = objectJ.GetType ();
			System.Reflection.FieldInfo f = type.GetField (this.name);
			Object resultObj = f.GetValue (objectJ);

			if (resultObj is Double) return ((Double)resultObj);
			if (resultObj is java.lang.Double)
				return (((java.lang.Double)resultObj).doubleValue());
			throw new IllegalArgumentException ("Attempt to get int field " + objectJ.getClass().getName()+"."+this.getName() + " with illegal data type conversion to " + "double");
		}
    /**
     * Returns the value of the field in the specified object as a {@code float}.
     * This reproduces the effect of {@code object.fieldName}
     * <p>
     * If this field is static, the object argument is ignored.
     * Otherwise, if the object is {@code null}, a NullPointerException is
     * thrown. If the object is not an instance of the declaring class of the
     * method, an IllegalArgumentException is thrown.
     * <p>
     * If this Field object is enforcing access control (see AccessibleObject)
     * and this field is not accessible from the current context, an
     * IllegalAccessException is thrown.
     *
     * @param object
     *            the object to access
     * @return the field value
     * @throws NullPointerException
     *             if the object is {@code null} and the field is non-static
     * @throws IllegalArgumentException
     *             if the object is not compatible with the declaring class
     * @throws IllegalAccessException
     *             if this field is not accessible
     */
		public float getFloat(Object objectJ) {
			Type type = objectJ.GetType ();
			System.Reflection.FieldInfo f = type.GetField (this.name);
			Object resultObj = f.GetValue (objectJ);

			if (resultObj is Float) return ((Float)resultObj);
			if (resultObj is java.lang.Float)
				return (((java.lang.Float)resultObj).floatValue());
			throw new IllegalArgumentException ("Attempt to get int field " + objectJ.getClass().getName()+"."+this.getName() + " with illegal data type conversion to " + "float");
		}

    /**
     * Returns the value of the field in the specified object as an {@code int}.
     * This reproduces the effect of {@code object.fieldName}
     * <p>
     * If this field is static, the object argument is ignored.
     * Otherwise, if the object is {@code null}, a NullPointerException is
     * thrown. If the object is not an instance of the declaring class of the
     * method, an IllegalArgumentException is thrown.
     * <p>
     * If this Field object is enforcing access control (see AccessibleObject)
     * and this field is not accessible from the current context, an
     * IllegalAccessException is thrown.
     *
     * @param object
     *            the object to access
     * @return the field value
     * @throws NullPointerException
     *             if the object is {@code null} and the field is non-static
     * @throws IllegalArgumentException
     *             if the object is not compatible with the declaring class
     * @throws IllegalAccessException
     *             if this field is not accessible
     */
		public int getInt(Object objectJ) {
			Type type = objectJ.GetType ();
			System.Reflection.FieldInfo f = type.GetField (this.name);
			Object resultObj = f.GetValue (objectJ);

			if (resultObj is int) return ((int)resultObj);
			if (resultObj is java.lang.Integer)
				return (((java.lang.Character)resultObj).charValue());
			throw new IllegalArgumentException ("Attempt to get int field " + objectJ.getClass().getName()+"."+this.getName() + " with illegal data type conversion to " + "int");
		}

    /**
     * Returns the value of the field in the specified object as a {@code long}.
     * This reproduces the effect of {@code object.fieldName}
     * <p>
     * If this field is static, the object argument is ignored.
     * Otherwise, if the object is {@code null}, a NullPointerException is
     * thrown. If the object is not an instance of the declaring class of the
     * method, an IllegalArgumentException is thrown.
     * <p>
     * If this Field object is enforcing access control (see AccessibleObject)
     * and this field is not accessible from the current context, an
     * IllegalAccessException is thrown.
     *
     * @param object
     *            the object to access
     * @return the field value
     * @throws NullPointerException
     *             if the object is {@code null} and the field is non-static
     * @throws IllegalArgumentException
     *             if the object is not compatible with the declaring class
     * @throws IllegalAccessException
     *             if this field is not accessible
     */
		public long getLong(Object objectJ) {
			Type type = objectJ.GetType ();
			System.Reflection.FieldInfo f = type.GetField (this.name);
			Object resultObj = f.GetValue (objectJ);

			if (resultObj is long) return ((long)resultObj);
			if (resultObj is java.lang.Long)
				return (((java.lang.Long)resultObj).longValue());
			throw new IllegalArgumentException ("Attempt to get int field " + objectJ.getClass().getName()+"."+this.getName() + " with illegal data type conversion to " + "long");
		}

    /**
     * Returns the modifiers for this field. The {@link Modifier} class should
     * be used to decode the result.
     *
     * @return the modifiers for this field
     * @see Modifier
     */
		public int getModifiers(){
			throw new UnsupportedOperationException ("Not yet implemented");
		}

    /**
     * Returns the name of this field.
     *
     * @return the name of this field
     */
	public String getName() {
		return this.name;
	}

    /**
     * Returns the value of the field in the specified object as a {@code short}
     * . This reproduces the effect of {@code object.fieldName}
     * <p>
     * If this field is static, the object argument is ignored.
     * Otherwise, if the object is {@code null}, a NullPointerException is
     * thrown. If the object is not an instance of the declaring class of the
     * method, an IllegalArgumentException is thrown.
     * <p>
     * If this Field object is enforcing access control (see AccessibleObject)
     * and this field is not accessible from the current context, an
     * IllegalAccessException is thrown.
     *
     * @param object
     *            the object to access
     * @return the field value
     * @throws NullPointerException
     *             if the object is {@code null} and the field is non-static
     * @throws IllegalArgumentException
     *             if the object is not compatible with the declaring class
     * @throws IllegalAccessException
     *             if this field is not accessible
     */
		public short getShort(Object objectJ) {
			Type type = objectJ.GetType ();
			System.Reflection.FieldInfo f = type.GetField (this.name);
			Object resultObj = f.GetValue (objectJ);

			if (resultObj is short) return ((short)resultObj);
			if (resultObj is java.lang.Short)
				return (((java.lang.Short)resultObj).shortValue());
			throw new IllegalArgumentException ("Attempt to get int field " + objectJ.getClass().getName()+"."+this.getName() + " with illegal data type conversion to " + "short");
		}

    /**
     * Return the {@link Class} associated with the type of this field.
     *
     * @return the type of this field
     */
	public java.lang.Class getType() {
		return null;
	}

    /**
     * Returns an integer hash code for this field. Objects which are equal
     * return the same value for this method.
     * <p>
     * The hash code for a Field is the exclusive-or combination of the hash
     * code of the field's name and the hash code of the name of its declaring
     * class.
     *
     * @return the hash code for this field
     * @see #equals
     */
	
    public override int GetHashCode() {
		return 0;
	}

    /**
     * Sets the value of the field in the specified object to the value. This
     * reproduces the effect of {@code object.fieldName = value}
     * <p>
     * If this field is static, the object argument is ignored.
     * Otherwise, if the object is {@code null}, a NullPointerException is
     * thrown. If the object is not an instance of the declaring class of the
     * method, an IllegalArgumentException is thrown.
     * <p>
     * If this Field object is enforcing access control (see AccessibleObject)
     * and this field is not accessible from the current context, an
     * IllegalAccessException is thrown.
     * <p>
     * If the field type is a primitive type, the value is automatically
     * unwrapped. If the unwrap fails, an IllegalArgumentException is thrown. If
     * the value cannot be converted to the field type via a widening
     * conversion, an IllegalArgumentException is thrown.
     *
     * @param object
     *            the object to access
     * @param value
     *            the new value
     * @throws NullPointerException
     *             if the object is {@code null} and the field is non-static
     * @throws IllegalArgumentException
     *             if the object is not compatible with the declaring class
     * @throws IllegalAccessException
     *             if this field is not accessible
     */
		public void set(Object objectJ, Object value){
			try {
				Type type = objectJ.GetType ();
				System.Reflection.FieldInfo f = type.GetField (this.name);
				f.SetValue(objectJ,value);
			}
			catch (ArgumentException ae) {
				throw new IllegalArgumentException(ae.getMessage());
			}
		}

    /**
     * Sets the value of the field in the specified object to the {@code
     * boolean} value. This reproduces the effect of {@code object.fieldName =
     * value}
     * <p>
     * If this field is static, the object argument is ignored.
     * Otherwise, if the object is {@code null}, a NullPointerException is
     * thrown. If the object is not an instance of the declaring class of the
     * method, an IllegalArgumentException is thrown.
     * <p>
     * If this Field object is enforcing access control (see AccessibleObject)
     * and this field is not accessible from the current context, an
     * IllegalAccessException is thrown.
     * <p>
     * If the value cannot be converted to the field type via a widening
     * conversion, an IllegalArgumentException is thrown.
     *
     * @param object
     *            the object to access
     * @param value
     *            the new value
     * @throws NullPointerException
     *             if the object is {@code null} and the field is non-static
     * @throws IllegalArgumentException
     *             if the object is not compatible with the declaring class
     * @throws IllegalAccessException
     *             if this field is not accessible
     */
		public void setBoolean(Object objectJ, bool value){
			try {
				Type type = objectJ.GetType ();
				System.Reflection.FieldInfo f = type.GetField (this.name);
				f.SetValue(objectJ,value);
			}
			catch (ArgumentException ae) {
				throw new IllegalArgumentException(ae.getMessage());
			}
		}

    /**
     * Sets the value of the field in the specified object to the {@code byte}
     * value. This reproduces the effect of {@code object.fieldName = value}
     * <p>
     * If this field is static, the object argument is ignored.
     * Otherwise, if the object is {@code null}, a NullPointerException is
     * thrown. If the object is not an instance of the declaring class of the
     * method, an IllegalArgumentException is thrown.
     * <p>
     * If this Field object is enforcing access control (see AccessibleObject)
     * and this field is not accessible from the current context, an
     * IllegalAccessException is thrown.
     * <p>
     * If the value cannot be converted to the field type via a widening
     * conversion, an IllegalArgumentException is thrown.
     *
     * @param object
     *            the object to access
     * @param value
     *            the new value
     * @throws NullPointerException
     *             if the object is {@code null} and the field is non-static
     * @throws IllegalArgumentException
     *             if the object is not compatible with the declaring class
     * @throws IllegalAccessException
     *             if this field is not accessible
     */
		public void setByte(Object objectJ, byte value){
			try {
				Type type = objectJ.GetType ();
				System.Reflection.FieldInfo f = type.GetField (this.name);
				f.SetValue(objectJ,value);
			}
			catch (ArgumentException ae) {
				throw new IllegalArgumentException(ae.getMessage());
			}
		}

    /**
     * Sets the value of the field in the specified object to the {@code char}
     * value. This reproduces the effect of {@code object.fieldName = value}
     * <p>
     * If this field is static, the object argument is ignored.
     * Otherwise, if the object is {@code null}, a NullPointerException is
     * thrown. If the object is not an instance of the declaring class of the
     * method, an IllegalArgumentException is thrown.
     * <p>
     * If this Field object is enforcing access control (see AccessibleObject)
     * and this field is not accessible from the current context, an
     * IllegalAccessException is thrown.
     * <p>
     * If the value cannot be converted to the field type via a widening
     * conversion, an IllegalArgumentException is thrown.
     *
     * @param object
     *            the object to access
     * @param value
     *            the new value
     * @throws NullPointerException
     *             if the object is {@code null} and the field is non-static
     * @throws IllegalArgumentException
     *             if the object is not compatible with the declaring class
     * @throws IllegalAccessException
     *             if this field is not accessible
     */
		public void setChar(Object objectJ, char value){
			try {
				Type type = objectJ.GetType ();
				System.Reflection.FieldInfo f = type.GetField (this.name);
				f.SetValue(objectJ,value);
			}
			catch (ArgumentException ae) {
				throw new IllegalArgumentException(ae.getMessage());
			}
		}

    /**
     * Sets the value of the field in the specified object to the {@code double}
     * value. This reproduces the effect of {@code object.fieldName = value}
     * <p>
     * If this field is static, the object argument is ignored.
     * Otherwise, if the object is {@code null}, a NullPointerException is
     * thrown. If the object is not an instance of the declaring class of the
     * method, an IllegalArgumentException is thrown.
     * <p>
     * If this Field object is enforcing access control (see AccessibleObject)
     * and this field is not accessible from the current context, an
     * IllegalAccessException is thrown.
     * <p>
     * If the value cannot be converted to the field type via a widening
     * conversion, an IllegalArgumentException is thrown.
     *
     * @param object
     *            the object to access
     * @param value
     *            the new value
     * @throws NullPointerException
     *             if the object is {@code null} and the field is non-static
     * @throws IllegalArgumentException
     *             if the object is not compatible with the declaring class
     * @throws IllegalAccessException
     *             if this field is not accessible
     */
		public void setDouble(Object objectJ, double value){
			try {
				Type type = objectJ.GetType ();
				System.Reflection.FieldInfo f = type.GetField (this.name);
				f.SetValue(objectJ,value);
			}
			catch (ArgumentException ae) {
				throw new IllegalArgumentException(ae.getMessage());
			}
		}

    /**
     * Sets the value of the field in the specified object to the {@code float}
     * value. This reproduces the effect of {@code object.fieldName = value}
     * <p>
     * If this field is static, the object argument is ignored.
     * Otherwise, if the object is {@code null}, a NullPointerException is
     * thrown. If the object is not an instance of the declaring class of the
     * method, an IllegalArgumentException is thrown.
     * <p>
     * If this Field object is enforcing access control (see AccessibleObject)
     * and this field is not accessible from the current context, an
     * IllegalAccessException is thrown.
     * <p>
     * If the value cannot be converted to the field type via a widening
     * conversion, an IllegalArgumentException is thrown.
     *
     * @param object
     *            the object to access
     * @param value
     *            the new value
     * @throws NullPointerException
     *             if the object is {@code null} and the field is non-static
     * @throws IllegalArgumentException
     *             if the object is not compatible with the declaring class
     * @throws IllegalAccessException
     *             if this field is not accessible
     */
		public void setFloat(Object objectJ, float value){
			try {
				Type type = objectJ.GetType ();
				System.Reflection.FieldInfo f = type.GetField (this.name);
				f.SetValue(objectJ,value);
			}
			catch (ArgumentException ae) {
				throw new IllegalArgumentException(ae.getMessage());
			}
		}

    /**
     * Set the value of the field in the specified object to the {@code int}
     * value. This reproduces the effect of {@code object.fieldName = value}
     * <p>
     * If this field is static, the object argument is ignored.
     * Otherwise, if the object is {@code null}, a NullPointerException is
     * thrown. If the object is not an instance of the declaring class of the
     * method, an IllegalArgumentException is thrown.
     * <p>
     * If this Field object is enforcing access control (see AccessibleObject)
     * and this field is not accessible from the current context, an
     * IllegalAccessException is thrown.
     * <p>
     * If the value cannot be converted to the field type via a widening
     * conversion, an IllegalArgumentException is thrown.
     *
     * @param object
     *            the object to access
     * @param value
     *            the new value
     * @throws NullPointerException
     *             if the object is {@code null} and the field is non-static
     * @throws IllegalArgumentException
     *             if the object is not compatible with the declaring class
     * @throws IllegalAccessException
     *             if this field is not accessible
     */
		public void setInt(Object objectJ, int value){
			try {
				Type type = objectJ.GetType ();
				System.Reflection.FieldInfo f = type.GetField (this.name);
				f.SetValue(objectJ,value);
			}
			catch (ArgumentException ae) {
				throw new IllegalArgumentException(ae.getMessage());
			}
		}

    /**
     * Sets the value of the field in the specified object to the {@code long}
     * value. This reproduces the effect of {@code object.fieldName = value}
     * <p>
     * If this field is static, the object argument is ignored.
     * Otherwise, if the object is {@code null}, a NullPointerException is
     * thrown. If the object is not an instance of the declaring class of the
     * method, an IllegalArgumentException is thrown.
     * <p>
     * If this Field object is enforcing access control (see AccessibleObject)
     * and this field is not accessible from the current context, an
     * IllegalAccessException is thrown.
     * <p>
     * If the value cannot be converted to the field type via a widening
     * conversion, an IllegalArgumentException is thrown.
     *
     * @param object
     *            the object to access
     * @param value
     *            the new value
     * @throws NullPointerException
     *             if the object is {@code null} and the field is non-static
     * @throws IllegalArgumentException
     *             if the object is not compatible with the declaring class
     * @throws IllegalAccessException
     *             if this field is not accessible
     */
		public void setLong(Object objectJ, long value){
			try {
				Type type = objectJ.GetType ();
				System.Reflection.FieldInfo f = type.GetField (this.name);
				f.SetValue(objectJ,value);
			}
			catch (ArgumentException ae) {
				throw new IllegalArgumentException(ae.getMessage());
			}
		}

    /**
     * Sets the value of the field in the specified object to the {@code short}
     * value. This reproduces the effect of {@code object.fieldName = value}
     * <p>
     * If this field is static, the object argument is ignored.
     * Otherwise, if the object is {@code null}, a NullPointerException is
     * thrown. If the object is not an instance of the declaring class of the
     * method, an IllegalArgumentException is thrown.
     * <p>
     * If this Field object is enforcing access control (see AccessibleObject)
     * and this field is not accessible from the current context, an
     * IllegalAccessException is thrown.
     * <p>
     * If the value cannot be converted to the field type via a widening
     * conversion, an IllegalArgumentException is thrown.
     *
     * @param object
     *            the object to access
     * @param value
     *            the new value
     * @throws NullPointerException
     *             if the object is {@code null} and the field is non-static
     * @throws IllegalArgumentException
     *             if the object is not compatible with the declaring class
     * @throws IllegalAccessException
     *             if this field is not accessible
     */
		public void setShort(Object objectJ, short value){
			try {
				Type type = objectJ.GetType ();
				System.Reflection.FieldInfo f = type.GetField (this.name);
				f.SetValue(objectJ,value);
			}
			catch (ArgumentException ae) {
				throw new IllegalArgumentException(ae.getMessage());
			}
		}

    /**
     * Returns a string containing a concise, human-readable description of this
     * field.
     * <p>
     * The format of the string is:
     * <ol>
     *   <li>modifiers (if any)
     *   <li>type
     *   <li>declaring class name
     *   <li>'.'
     *   <li>field name
     * </ol>
     * <p>
     * For example: {@code public static java.io.InputStream
     * java.lang.System.in}
     *
     * @return a printable representation for this field
     */
    
	public override String ToString() {
		return null;
	}
}
}