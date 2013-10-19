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

namespace org.apache.harmony.luni.util
{

/**
 * Helper class to avoid multiple anonymous inner class for
 * <code>{@link java.security.AccessController#doPrivileged(PrivilegedAction)}</code>
 * calls.
 */
public class PriviAction<T> : java.security.PrivilegedAction<T> {

    private Object arg1;

    private Object arg2;

    private int action;

    private const int GET_SYSTEM_PROPERTY = 1;

    private const int GET_SECURITY_POLICY = 2;

    private const int SET_ACCESSIBLE = 3;

    private const int GET_SECURITY_PROPERTY = 4;

    /**
     * Creates a PrivilegedAction to get the security property with the given
     * name.
     * 
     * @param property
     *            the name of the property
     * 
     * @see Security#getProperty
     */
    public static java.security.PrivilegedAction<String> getSecurityProperty(String property) {
        return new PriviAction<String>(GET_SECURITY_PROPERTY, property);
    }

    private PriviAction(int action, Object arg) {
        this.action = action;
        this.arg1 = arg;
    }

    /**
     * Creates a PrivilegedAction to get the current security policy object.
     * 
     * @see Policy#getPolicy
     */
    public PriviAction() {
        action = GET_SECURITY_POLICY;
    }

    /**
     * Creates a PrivilegedAction to disable the access checks to the given
     * object.
     * 
     * @param object
     *            the object whose accessible flag will be set to
     *            <code>true</code>
     * 
     * @see AccessibleObject#setAccessible(boolean)
     */
    public PriviAction(java.lang.reflect.AccessibleObject obj) {
        action = SET_ACCESSIBLE;
        arg1 = obj;
    }

    /**
     * Creates a PrivilegedAction to return the value of the system property
     * with the given key.
     * 
     * @param property
     *            the key of the system property
     * 
     * @see System#getProperty(String)
     */
    public PriviAction(String property) {
        action = GET_SYSTEM_PROPERTY;
        arg1 = property;
    }

    /**
     * Creates a PrivilegedAction to return the value of the system property
     * with the given key.
     * 
     * @param property
     *            the key of the system property
     * @param defaultAnswer
     *            the return value if the system property does not exist
     * 
     * @see System#getProperty(String, String)
     */
    public PriviAction(String property, String defaultAnswer) {
        action = GET_SYSTEM_PROPERTY;
        arg1 = property;
        arg2 = defaultAnswer;
    }

    /**
     * Performs the actual privileged computation as defined by the constructor.
     * 
     * @see java.security.PrivilegedAction#run()
     */
    public T run() {
        switch (action) {
        case GET_SYSTEM_PROPERTY:
            return (T) (Object) java.lang.SystemJ.getProperty((String) arg1, (String) arg2);
        case GET_SECURITY_PROPERTY:
            return (T) (Object)java.security.Security.getProperty((String) arg1);
        case GET_SECURITY_POLICY:
            return (T) (Object)java.security.Policy.getPolicy();
        case SET_ACCESSIBLE:
            ((java.lang.reflect.AccessibleObject) arg1).setAccessible(true);
            break;
        }
        return default(T);
    }
}
}
