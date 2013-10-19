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
 *  Copyright © 2011-2013 Sebastian Ritter
 */
using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.io
{
    [Serializable()]
    public class File : Serializable, java.lang.Comparable<File>
    {
        public static readonly String separator = java.lang.SystemJ.getProperty ("file.separator");
        public static readonly char separatorChar = java.lang.SystemJ.getProperty("file.separator")[0];
        public static readonly String pathSeparator = java.lang.SystemJ.getProperty("path.separator");
        public static readonly char pathSeparatorChar = java.lang.SystemJ.getProperty("path.separator")[0];
        protected System.IO.FileInfo info;
        private String fullQualifiedFile;

        public File(String pathname)
        {
            if (null == pathname) throw new java.lang.NullPointerException();
            this.init(pathname);
        }
        public File(File parent, String child)
        {
            if (null == child) throw new java.lang.NullPointerException();
            if (parent == null)
            {
                this.init(child);
            }
            else
            {
                this.init(parent.fullQualifiedFile + java.lang.SystemJ.getProperty("file.separator") + child);
            }
        }
        public File(String parent, String child)
        {
            if (null == child) throw new java.lang.NullPointerException();
            if (parent == null)
            {
                this.init(child);
            }
            else
            {
                this.init(parent + java.lang.SystemJ.getProperty("file.separator") + child);
            }
        }
        public File(java.net.URI uri)
        {
            if (null == uri) throw new java.lang.NullPointerException();
            throw new java.lang.UnsupportedOperationException("Not yet implemented");
        }
        private void init(String newFullQualifiedFile)
        {
            this.fullQualifiedFile = newFullQualifiedFile;
            try
            {
                this.info = new System.IO.FileInfo(this.fullQualifiedFile);
            }
            catch (System.ArgumentException)
            {
                //Empty strings are allowed in Java not in .net
            }
        }

        public virtual int compareTo(File other) {
            String otherPath = "null";
            if (null != other) otherPath = other.getAbsolutePath();
            if (java.lang.SystemJ.getProperty("os.name").indexOf("Windows") > -1)
            {
                return this.getAbsolutePath().compareTo(otherPath);
            }
            else
            {
                return this.getAbsolutePath().compareTo(otherPath);
            }
        }

        public virtual String toString()
        {
            return this.ToString();
        }
        public override string ToString()
        {
            return fullQualifiedFile;
        }

        public String getPath()
        {
            if (isDirectory())
            {
                if (this.fullQualifiedFile.endsWith(java.lang.SystemJ.getProperty("file.separator")))
                {
                    return this.fullQualifiedFile.Substring(0, fullQualifiedFile.Length - java.lang.SystemJ.getProperty("file.separator").length());
                }
            }
            return this.fullQualifiedFile;
        }
        public bool isDirectory()
        {
            return null == info ? false : info.Attributes.HasFlag(System.IO.FileAttributes.Directory);
        }

        public bool isFile()
        {
            if (null == info) return false;
            return !info.Attributes.HasFlag(System.IO.FileAttributes.Device) &&
                   !info.Attributes.HasFlag(System.IO.FileAttributes.Directory);
        }
        public long length()
        {
            if (!isFile()) return 0;
            return info.Length;
        }
        public long lastModified()
        {
            TimeSpan timeDiff = info.LastWriteTimeUtc - new DateTime(1970, 1, 1);
            return (long)timeDiff.TotalMilliseconds;
        }
        public String[] list()
        {
            if (!this.isDirectory()) return null;
            java.util.ArrayList<String> entries = new util.ArrayList<String>();
            foreach (String next in System.IO.Directory.EnumerateFileSystemEntries(this.fullQualifiedFile))
            {
                entries.add(next);
            }
            String [] content = new String [entries.size()];
            return entries.toArray<String>(content);
        }
        public File[] listFiles()
        {
            if (!this.isDirectory()) return null;
            java.util.ArrayList<File> entries = new util.ArrayList<File>();
            foreach (String next in System.IO.Directory.EnumerateFileSystemEntries(this.fullQualifiedFile))
            {
                entries.add(new File(next));
            }
            File[] content = new File[entries.size()];
            return entries.toArray<File>(content);
        }
        public String getName()
        {
            return this.info.Name;
        }

        public bool delete()
        {
            try
            {
                if (this.isDirectory()) new System.IO.DirectoryInfo(this.getAbsolutePath()).Delete();
                else this.info.Delete();
                return !this.exists();
            }
            catch (System.IO.IOException notDeleted)
            {
                return false;
            }
        }

        public String getAbsolutePath()
        {
            return info.FullName;
        }

        public bool exists () {
            if (null == this.info) return false;
            if (this.isDirectory())
            {
                return new System.IO.DirectoryInfo(this.getAbsolutePath()).Exists;
            }
            else
            {
                this.info.Refresh();
                return this.info.Exists;
            }
        }
        public bool canRead()
        {
            lock (this.info)
            {
                System.IO.Stream canReadCheckStream = null;
                try
                {
                    //Die Datei öffnen.
                    canReadCheckStream = info.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
                    canReadCheckStream.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
        }
        /// <summary>
        /// Check is java.io.File instance is writable.
        /// </summary>
        /// <returns></returns>
        public bool canWrite()
        {
            lock (this.info)
            {
                System.IO.Stream canReadCheckStream = null;
                try
                {
                    //Die Datei öffnen.
                    canReadCheckStream = info.Open(System.IO.FileMode.Open, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);
                    canReadCheckStream.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
        }
        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            if (obj is File)
            {
                File other = (File)obj;
                return 0 == this.compareTo(other);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Create a new file, with given path if not exist
        /// </summary>
        /// <returns>true, if created</returns>
        public virtual bool createNewFile()
        {
            bool result = false;
            this.info.Refresh();

            if (!this.exists())
            {
                // TODO: throw a security execption, if not writable
                try
                {
                    System.IO.FileStream fs = this.info.OpenWrite();
                    fs.Close();
                    result = true;
                }
                catch (System.UnauthorizedAccessException)
                {
                    throw new java.lang.SecurityException("Cannot write file " + this.getAbsolutePath());
                }
                catch (System.Exception)
                {
                    result = false;
                }
            }

            this.info.Refresh();
            return result;
        }

        /// <summary>
        /// Rename this file to dest.
        /// </summary>
        /// <param name="dest">new File</param>
        /// <returns>true, if renamed</returns>
        /// <exception cref="biz.ritter.javapi.lang.NullPointerException">dest is null</exception>
        /// <exception cref="biz.ritter.javapi.lang.SecurityException">no read/write access for old/new file</exception>
        public virtual bool renameTo(File dest)
        {
            // TODO: Basties note:check method make same as Java!!!
            if (null == dest) throw new java.lang.NullPointerException();
            try
            {
                this.info.MoveTo(dest.getAbsolutePath());
            }
            catch (Exception e)
            {
                throw new java.lang.SecurityException();
            }
            return true;
        }

        /// <summary>
        /// Create a new directory
        /// </summary>
        /// <returns>true if is created</returns>
        public virtual bool mkdir() {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(this.fullQualifiedFile);
            try
            {
                if (!di.Parent.Exists) return false;
                di.Create();
                return di.Exists;
            }
            catch (Exception) { }
            return false;
        }

		public virtual String getCanonicalPath() {
			try {
			//TODO implement security manager checkRead check
			String absolutePath = this.getAbsolutePath();
			absolutePath.Replace("/../","/").Replace("\\..\\","\\")
				.Replace("\\..\\","\\").Replace("\\.\\","\\");
			if (java.lang.SystemJ.getProperty("os.name").toLowerCase().Contains("windows")) {
				absolutePath = absolutePath.substring(0,1).toLowerCase() + absolutePath.substring(1);
			}
			return absolutePath;
			}
			catch (Exception e) {
				if (e is IOException) throw e;
				else throw new java.io.IOException(e.Message);
			}
		}
    }
}
