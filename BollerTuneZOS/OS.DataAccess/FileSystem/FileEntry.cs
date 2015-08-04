using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OS.Data.Secruity;
using OS.Data.Secruity.Enums;

namespace OS.DataAccess.FileSystem
{
    /// <summary>
    /// Represents one File in the OS
    /// Jonas Ahlf 30.07.2015 16:15:07
    /// </summary>
    internal class FileEntry
    {
        public string Name { get; set; }

        public AuthToken Creator { get; set; }

        public FileAccessMode FileAccessMode { get; set; }
    }
}
