// NewInstance.java - create a new instance of a class by name.
// http://www.saxproject.org
// Written by Edwin Goei, edwingo@apache.org
// and by David Brownell, dbrownell@users.sourceforge.net
// NO WARRANTY!  This class is in the Public Domain.
// $Id: NewInstance.cs 19669 2013-03-26 19:16:43Z unknown $
using System;
using java = biz.ritter.javapi;

namespace org.xml.sax.helpers
{

/**
 * Create a new instance of a class by name.
 *
 * <blockquote>
 * <em>This module, both source code and documentation, is in the
 * Public Domain, and comes with <strong>NO WARRANTY</strong>.</em>
 * See <a href='http://www.saxproject.org'>http://www.saxproject.org</a>
 * for further information.
 * </blockquote>
 *
 * <p>This class contains a static method for creating an instance of a
 * class from an explicit class name.  It tries to use the thread's context
 * ClassLoader if possible and falls back to using
 * Class.forName(String).</p>
 *
 * <p>This code is designed to compile and run on JDK version 1.1 and later
 * including versions of Java 2.</p>
 *
 * @author Edwin Goei, David Brownell
 * @version 2.0.1 (sax2r2)
 */
class NewInstance {

    /**
     * Creates a new instance of the specified class name
     *
     * Package private so this code is not exposed at the API level.
     */
    internal static Object newInstance (java.lang.ClassLoader classLoader, String className)
        //throws ClassNotFoundException, IllegalAccessException,
        //    InstantiationException
    {
        java.lang.Class driverClass;
        if (classLoader == null) {
            driverClass = java.lang.Class.forName(className);
        } else {
            driverClass = classLoader.loadClass(className);
        }
        return driverClass.newInstance();
    }

    /**
     * Figure out which ClassLoader to use.  For JDK 1.2 and later use
     * the context ClassLoader.
     */           
    internal static java.lang.ClassLoader getClassLoader ()
    {
        return java.lang.ClassLoader.getSystemClassLoader();
        /*
        java.lang.reflect.Method m = null;
        try {
            m = java.lang.Thread.class.getMethod("getContextClassLoader", null);
        } catch (java.lang.NoSuchMethodException e) {
            // Assume that we are running JDK 1.1, use the current ClassLoader
            return NewInstance.class.getClassLoader();
        }

        try {
            return (java.lang.ClassLoader) m.invoke(Thread.currentThread(), null);
        } catch (IllegalAccessException e) {
            // assert(false)
            throw new java.lang.UnknownError(e.getMessage());
        } catch (InvocationTargetException e) {
            // assert(e.getTargetException() instanceof SecurityException)
            throw new java.lang.UnknownError(e.getMessage());
        }
        */
    }
}
}
