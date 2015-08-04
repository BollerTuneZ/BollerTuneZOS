using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OS.Data.Secruity;
using OS.DataAccess.Infrastructrue.FileSystem;

namespace OS.DataAccess.FileSystem
{
    public class FileSystemManager : IFileSystemManager
    {
        #region Implementation IFileSystemManager
        public IList<DirectoryInfo> GetDirs(string path, AuthToken token)
        {
            throw new NotImplementedException();
        }

        public IList<FileInfo> GetFiles(string path, AuthToken token)
        {
            throw new NotImplementedException();
        }

        public DirectoryInfo CreateDir(string path, AuthToken token, bool force = false)
        {
            throw new NotImplementedException();
        }

        public bool WriteFileAllBytes(string path, byte[] content, AuthToken token)
        {
            throw new NotImplementedException();
        }

        public bool WriteFileAllText(string path, string text, AuthToken token)
        {
            throw new NotImplementedException();
        }

        public FileStream WriteFileStream(string path, AuthToken token)
        {
            throw new NotImplementedException();
        }

        public byte[] ReadAllBytes(string path, AuthToken token)
        {
            throw new NotImplementedException();
        }

        public string ReadAllText(string path, AuthToken token)
        {
            throw new NotImplementedException();
        }

        public FileStream ReadFileStream(string path, AuthToken token)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
