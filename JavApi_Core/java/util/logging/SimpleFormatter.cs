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

namespace biz.ritter.javapi.util.logging
{
/**
 * {@code SimpleFormatter} can be used to print a summary of the information
 * contained in a {@code LogRecord} object in a human readable format.
 */
public class SimpleFormatter : Formatter {
    /**
     * Constructs a new {@code SimpleFormatter}.
     */
    public SimpleFormatter() :base(){
    }

    /**
     * Converts a {@link LogRecord} object into a human readable string
     * representation.
     *
     * @param r
     *            the log record to be formatted into a string.
     * @return the formatted string.
     */
    
    public override String format(LogRecord r) {
        java.lang.StringBuilder sb = new java.lang.StringBuilder();
        sb.append(java.text.MessageFormat.format("{0, date} {0, time} ", //$NON-NLS-1$
                new Object[] { new java.util.Date(r.getMillis()) }));
        sb.append(r.getSourceClassName()).append(" "); //$NON-NLS-1$
        sb.append(r.getSourceMethodName()).append(java.lang.SystemJ.getProperty("line.separator"));
        sb.append(r.getLevel().getName()).append(": "); //$NON-NLS-1$
        sb.append(formatMessage(r)).append(java.lang.SystemJ.getProperty("line.separator"));
        if (null != r.getThrown()) {
            sb.append("Throwable occurred: "); //$NON-NLS-1$
            java.lang.Throwable t = r.getThrown();
            java.io.PrintWriter pw = null;
            try {
                java.io.StringWriter sw = new java.io.StringWriter();
                pw = new java.io.PrintWriter(sw);
                t.printStackTrace(pw);
                sb.append(sw.toString());
            } finally {
                if (pw != null) {
                    try {
                        pw.close();
                    } catch (Exception e) {
                        // ignore
                    }
                }
            }
        }
        return sb.toString();
    }
}
}