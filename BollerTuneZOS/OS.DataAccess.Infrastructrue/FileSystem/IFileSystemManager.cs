using System.Collections.Generic;
using System.IO;
using OS.Data.Secruity;

namespace OS.DataAccess.Infrastructrue.FileSystem
{
    /// <summary>
    /// 
    /// Jonas Ahlf 30.07.2015 13:52:15
    /// </summary>
    public interface IFileSystemManager
    {
        /// <summary>
        /// Returns all Directories from given path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        IList<DirectoryInfo> GetDirs(string path,AuthToken token);

        /// <summary>
        /// Returns list of all files in this dir
        /// </summary>
        /// <param name="path"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        IList<FileInfo> GetFiles(string path, AuthToken token);

        /// <summary>
        /// Creates a new Directory
        /// </summary>
        /// <param name="path">path to new directory</param>
        /// <param name="token"></param>
        /// <param name="force">if force then the already existing directory will be overriden</param>
        /// <returns></returns>
        DirectoryInfo CreateDir(string path, AuthToken token,bool force=false);

        /// <summary>
        /// Writes file with byte content
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        bool WriteFileAllBytes(string path, byte[] content, AuthToken token);

        /// <summary>
        /// Writes file with text content
        /// </summary>
        /// <param name="path"></param>
        /// <param name="text"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        bool WriteFileAllText(string path, string text, AuthToken token);

        /// <summary>
        /// Returns filestream to certain path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        FileStream WriteFileStream(string path, AuthToken token);

        /// <summary>
        /// Reads file and returns its byte array value
        /// </summary>
        /// <param name="path"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        byte[] ReadAllBytes(string path, AuthToken token);

        /// <summary>
        /// Reads file as string
        /// </summary>
        /// <param name="path"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        string ReadAllText(string path, AuthToken token);

        /// <summary>
        /// Reads file as stream
        /// </summary>
        /// <param name="path"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        FileStream ReadFileStream(string path, AuthToken token);
    }
}