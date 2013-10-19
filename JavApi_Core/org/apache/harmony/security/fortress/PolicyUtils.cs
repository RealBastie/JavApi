using System;
using java = biz.ritter.javapi;

namespace org.apache.harmony.security.fortress
{
    public class PolicyUtils
    {
        // Empty set of arguments to default constructor of a Permission.
        private static readonly java.lang.Class[] NO_ARGS = { };

        // One-arg set of arguments to default constructor of a Permission.
        private static readonly java.lang.Class[] ONE_ARGS = { typeof(String).getClass() };

        // Two-args set of arguments to default constructor of a Permission.
        private static readonly java.lang.Class[] TWO_ARGS = { typeof(String).getClass(), typeof(String).getClass() };

        /** 
         * Specific exception to signal that property expansion failed 
         * due to unknown key. 
         */
        public class ExpansionFailedException : java.lang.Exception
        {

            /**
             * @serial
             */
            private static readonly long serialVersionUID = 2869748055182612000L;

            /** 
             * Constructor with user-friendly message parameter. 
             */
            public ExpansionFailedException(String message) :
                base(message)
            {
            }

            /** 
             * Constructor with user-friendly message and causing error. 
             */
            public ExpansionFailedException(String message, java.lang.Throwable cause) :
                base(message, cause)
            {
            }
        }

        private const String START_MARK = "${"; //$NON-NLS-1$
        private const String END_MARK = "}"; //$NON-NLS-1$
        private static readonly int START_OFFSET = START_MARK.length();
        private static readonly int END_OFFSET = END_MARK.length();
        /**
         * Substitutes all entries like ${some.key}, found in specified string, 
         * for specified values.
         * If some key is unknown, throws ExpansionFailedException. 
         * @param str the string to be expanded
         * @param properties available key-value mappings 
         * @return expanded string
         * @throws ExpansionFailedException
         */
        public static String expand(String str, java.util.Properties properties)
        {//throws ExpansionFailedException {

            java.lang.StringBuilder result = new java.lang.StringBuilder(str);
            int start = result.indexOf(START_MARK);
            while (start >= 0)
            {
                int end = result.indexOf(END_MARK, start);
                if (end >= 0)
                {
                    String key = result.substring(start + START_OFFSET, end);
                    String value = properties.getProperty(key);
                    if (value != null)
                    {
                        result.replace(start, end + END_OFFSET, value);
                        start += value.length();
                    }
                    else
                    {
                        throw new ExpansionFailedException("Unknown key: " + key); //$NON-NLS-1$
                    }
                }
                start = result.indexOf(START_MARK, start);
            }
            return result.toString();
        }


        /**
         * Checks whether the objects from <code>what</code> array are all
         * presented in <code>where</code> array.
         * 
         * @param what first array, may be <code>null</code> 
         * @param where  second array, may be <code>null</code>
         * @return <code>true</code> if the first array is <code>null</code>
         * or if each and every object (ignoring null values) 
         * from the first array has a twin in the second array; <code>false</code> otherwise
         */
        public static bool matchSubset(Object[] what, Object[] where)
        {
            if (what == null)
            {
                return true;
            }

            for (int i = 0; i < what.Length; i++)
            {
                if (what[i] != null)
                {
                    if (where == null)
                    {
                        return false;
                    }
                    bool found = false;
                    for (int j = 0; j < where.Length; j++)
                    {
                        if (what[i].equals(where[j]))
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /**
         * Tries to find a suitable constructor and instantiate a new Permission
         * with specified parameters.  
         *
         * @param targetType class of expected Permission instance
         * @param targetName name of expected Permission instance
         * @param targetActions actions of expected Permission instance
         * @return a new Permission instance
         * @throws IllegalArgumentException if no suitable constructor found
         * @throws Exception any exception thrown by Constructor.newInstance()
         */
        public static java.security.Permission instantiatePermission(java.lang.Class targetType,
                String targetName, String targetActions)
        {//throws Exception {

            // let's guess the best order for trying constructors
            java.lang.Class[][] argTypes = null;
            Object[][] args = null;
            if (targetActions != null)
            {
                argTypes = new java.lang.Class[][] { TWO_ARGS, ONE_ARGS, NO_ARGS };
                args = new Object[][] { 
              new Object []{ targetName, targetActions },
              new Object []{ targetName }, 
              new Object []{} 
            };
            }
            else if (targetName != null)
            {
                argTypes = new java.lang.Class[][] { ONE_ARGS, TWO_ARGS, NO_ARGS };
                args = new Object[][] { 
              new Object []{ targetName },
              new Object []{ targetName, targetActions }, 
              new Object []{} 
            };
            }
            else
            {
                argTypes = new java.lang.Class[][] { NO_ARGS, ONE_ARGS, TWO_ARGS };
                args = new Object[][] { 
              new Object []{}, 
              new Object []{ targetName },
              new Object []{ targetName, targetActions } 
            };
            }

            // finally try to instantiate actual permission
            for (int i = 0; i < argTypes.Length; i++)
            {
                try
                {
                    java.lang.reflect.Constructor ctor = targetType.getConstructor(argTypes[i]);
                    return (java.security.Permission)ctor.newInstance(args[i]);
                }
                catch (java.lang.NoSuchMethodException ignore) { }
            }
            throw new java.lang.IllegalArgumentException("No suitable constructors found in permission class : " + targetType + ". Zero, one or two-argument constructor is expected");
        }
        /** 
         * Auxiliary action for accessing specific security property. 
         */
        public class SecurityPropertyAccessor : java.security.PrivilegedAction<String>
        {

            private String keyJ;

            /** 
             * Constructor with a property key parameter. 
             */
            public SecurityPropertyAccessor(String key)
                : base()
            {
                this.keyJ = key;
            }

            public java.security.PrivilegedAction<String> key(String key)
            {
                this.keyJ = key;
                return this;
            }

            /** 
             * Returns specified security property. 
             */
            public String run()
            {
                return java.security.Security.getProperty(this.keyJ);
            }
        }


    }
}