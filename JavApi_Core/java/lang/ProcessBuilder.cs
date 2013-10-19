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

namespace biz.ritter.javapi.lang{

/**
 * Creates operating system processes.
 *
 * @since 1.5
 */
public sealed class ProcessBuilder {

    private java.util.List<String> commandJ;

    private java.io.File directoryJ;

    private java.util.Map<String, String> environmentJ;

    private bool redirectErrorStreamJ;

    /**
     * Constructs a new {@code ProcessBuilder} instance with the specified
     * operating system program and its arguments.
     * 
     * @param command
     *            the requested operating system program and its arguments.
     */
    public ProcessBuilder(params String []commandJ): this(toList(commandJ)) {
        
    }

    /**
     * Constructs a new {@code ProcessBuilder} instance with the specified
     * operating system program and its arguments. Note that the list passed to
     * this constructor is not copied, so any subsequent updates to it are
     * reflected in this instance's state.
     * 
     * @param command
     *            the requested operating system program and its arguments.
     * @throws NullPointerException
     *             if {@code command} is {@code null}.
     */
    public ProcessBuilder(java.util.List<String> commandJ) :base(){
        
        if (commandJ == null) {
            throw new NullPointerException();
        }
        this.commandJ = commandJ;
		
			java.lang.SystemJ.getProperties();
        this.environmentJ = // new org.apache.harmony.luni.platform.Environment.EnvironmentMap(
                java.lang.SystemJ.getenv();
    }

    /**
     * Returns this process builder's current program and arguments. Note that
     * the returned list is not a copy and modifications to it will change the
     * state of this instance.
     *
     * @return this process builder's program and arguments.
     */
    public java.util.List<String> command() {
        return commandJ;
    }

    /**
     * Changes the program and arguments of this process builder.
     * 
     * @param command
     *            the new operating system program and its arguments.
     * @return this process builder instance.
     */
    public ProcessBuilder command(params String []commandJ) {
        return command(toList(commandJ));
    }

    /**
     * Changes the program and arguments of this process builder. Note that the
     * list passed to this method is not copied, so any subsequent updates to it
     * are reflected in this instance's state.
     *
     * @param command
     *            the new operating system program and its arguments.
     * @return this process builder instance.
     * @throws NullPointerException
     *             if {@code command} is {@code null}.
     */
    public ProcessBuilder command(java.util.List<String> commandJ) {
        if (commandJ == null) {
            throw new NullPointerException();
        }
        this.commandJ = commandJ;
        return this;
    }

    /**
     * Returns the working directory of this process builder. If {@code null} is
     * returned, then the working directory of the Java process is used when a
     * process is started.
     * 
     * @return the current working directory, may be {@code null}.
     */
    public java.io.File directory() {
        return directoryJ;
    }

    /**
     * Changes the working directory of this process builder. If the specified
     * directory is {@code null}, then the working directory of the Java
     * process is used when a process is started.
     *
     * @param directory
     *            the new working directory for this process builder.
     * @return this process builder instance.
     */
    public ProcessBuilder directory(java.io.File directoryJ) {
        this.directoryJ = directoryJ;
        return this;
    }

    /**
     * Returns this process builder's current environment. When a process
     * builder instance is created, the environment is populated with a copy of
     * the environment, as returned by {@link System#getenv()}. Note that the
     * map returned by this method is not a copy and any changes made to it are
     * reflected in this instance's state.
     *
     * @return the map containing this process builder's environment variables.
     */
    public java.util.Map<String, String> environment() {
        return environmentJ;
    }

    /**
     * Indicates whether the standard error should be redirected to standard
     * output. If redirected, the {@link Process#getErrorStream()} will always
     * return end of stream and standard error is written to
     * {@link Process#getInputStream()}.
     *
     * @return {@code true} if the standard error is redirected; {@code false}
     *         otherwise.
     */
    public bool redirectErrorStream() {
        return redirectErrorStreamJ;
    }

    /**
     * Changes the state of whether or not standard error is redirected to
     * standard output.
     * 
     * @param redirectErrorStream
     *            {@code true} to redirect standard error, {@code false}
     *            otherwise.
     * @return this process builder instance.
     */
    public ProcessBuilder redirectErrorStream(bool redirectErrorStreamJ) {
        this.redirectErrorStreamJ = redirectErrorStreamJ;
        return this;
    }

    /**
     * Starts a new process based on the current state of this process builder.
     *
     * @return the new {@code Process} instance.
     * @throws NullPointerException
     *             if any of the elements of {@link #command()} is {@code null}.
     * @throws IndexOutOfBoundsException
     *             if {@link #command()} is empty.
     * @throws SecurityException
     *             if {@link SecurityManager#checkExec(String)} doesn't allow
     *             process creation.
     * @throws IOException
     *             if an I/O error happens.
     */
    public Process start() {//throws IOException {
        if (commandJ.isEmpty()) {
            throw new IndexOutOfBoundsException();
        }
        String[] cmdArray = new String[commandJ.size()];
        for (int i = 0; i < cmdArray.Length; i++) {
            if ((cmdArray[i] = commandJ.get(i)) == null) {
                throw new NullPointerException();
            }
        }
        String[] envArray = new String[environmentJ.size()];
        int i2 = 0;

			/*
	        foreach (Map.Entry<String, String> entry in environmentJ.entrySet()) {
	            envArray[i2++] = entry.getKey() + "=" + entry.getValue(); //$NON-NLS-1$
	        }
			 */
			java.util.Iterator<String> it = environmentJ.keySet().iterator();
			while (it.hasNext()) {
				String key = it.next();
				String value = environmentJ.get(key);
				envArray[i2++] = key + "=" + value;
			}

        java.lang.Process process = Runtime.getRuntime().exec(cmdArray, envArray,
                directoryJ);

        // TODO implement support for redirectErrorStream
        return process;
    }

    private static java.util.List<String> toList(String[] strings) {
        java.util.ArrayList<String> arrayList = new java.util.ArrayList<String>(strings.Length);
        foreach (String str in strings) {
            arrayList.add(str);
        }
        return arrayList;
    }
}
	}
