using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS.DataAccess.FileSystem
{
    /// <summary>
    /// Register of all Files and Directories in the OS
    /// Jonas Ahlf 30.07.2015 16:09:06
    /// </summary>
    internal class FileSystemRegister
    {
        private readonly IList<DirectoryEntry> _coreDirectoryEntries;

        public FileSystemRegister(IList<DirectoryEntry> coreDirectoryEntries)
        {
            _coreDirectoryEntries = coreDirectoryEntries;
        }

        /// <summary>
        /// All Directories
        /// </summary>
        public IList<DirectoryEntry> DirectoryEntries { get; set; }
        /// <summary>
        /// All Files
        /// </summary>
        public IList<FileEntry> FileEntries { get; set; } 
    }

}
