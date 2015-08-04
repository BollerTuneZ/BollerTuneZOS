using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS.DataAccess.FileSystem
{
    /// <summary>
    /// Contants which ar important for the OS FileSystem
    /// Jonas Ahlf 30.07.2015 16:27:44
    /// </summary>
    internal static class FileSystemConstants
    {
        #region Core
        public static string BaseDir { get; private set; }
        public static string DataDir { get; private set; }
        public static string UserDir { get; private set; }
        public static string PluginDir { get; private set; }
        public static string DeviceDir { get; set; }
        #endregion

        static FileSystemConstants()
        {
            GetCoreDirectories();
        }

        static void GetCoreDirectories()
        {
            BaseDir = Environment.CurrentDirectory;
            DataDir = Path.Combine(BaseDir, "Data");
            UserDir = Path.Combine(DataDir, "Users");
            PluginDir = Path.Combine(BaseDir, "plugins");
            DeviceDir = Path.Combine(BaseDir, "devices");
        }
    }
}
