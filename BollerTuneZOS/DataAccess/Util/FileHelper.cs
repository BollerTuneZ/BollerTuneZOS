using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace DataAccess.Util
{
    /// <summary>
    /// Klasse um daten zu schreiben und zu lesen
    /// Jonas Ahlf 19.06.2015 23:01:35
    /// </summary>
    internal class FileHelper
    {
        private static readonly ILog SLog = LogManager.GetLogger(typeof (FileHelper));
        private const string SteeringSettingsFilePath = "Settings/settings_steering.btz";
        private const string EngineSettingsFilePath = "Settings/settings_engine.btz";

        private const string PluginListFilePath = "/Plugin/pluginList.btz";
        private const string SettingsDirectory = "/Settings";
        private const string PluginDirectory = "/Plugin";
        public FileHelper()
        {
            
            CreateDirectory(SettingsDirectory);
            CreateDirectory(PluginDirectory);
        }

        #region Plugin

        public void WritePluginList(string content)
        {
            var filePath = String.Format("{0}{1}", Environment.CurrentDirectory, PluginListFilePath);
            WriteFile(filePath,content);
        }

        public string ReadPluginList()
        {
            var path = String.Format("{0}{1}",Environment.CurrentDirectory, PluginListFilePath);
            return ReadFile(path);
        }

        public string CopyPluginDirectory(string source,string name)
        {
            var directoryInfo = new DirectoryInfo(source);
            var destinationDirectory = String.Format("{0}{1}\\{2}", Environment.CurrentDirectory, PluginDirectory, name);
            if ((new DirectoryInfo(destinationDirectory)).Exists)
            {
                return null;
            }
            directoryInfo.MoveTo(destinationDirectory);
            if (!(new DirectoryInfo(destinationDirectory)).Exists)
            {
                return null;
            }
            return destinationDirectory;
        }
        #endregion

        #region SteeringSettingsFile
        public void WriteSteeringSettingsFile(string content)
        {
            var currentDirectory = Environment.CurrentDirectory;
            var filePath = String.Format("{0}{1}", currentDirectory, SteeringSettingsFilePath);
            WriteFile(filePath,content);
        }

        public string ReadSteeringSettingsFile()
        {
            var currentDirectory = Environment.CurrentDirectory;
            var filePath = String.Format("{0}{1}", currentDirectory, SteeringSettingsFilePath);
            return ReadFile(filePath);
        }
        #endregion

        #region Engine Settings
        public void WriteEngineSettingsFile(string content)
        {
            var currentDirectory = Environment.CurrentDirectory;
            var filePath = String.Format("{0}{1}", currentDirectory, EngineSettingsFilePath);
            WriteFile(filePath, content);
        }

        public string ReadEngineSettingsFile()
        {
            var currentDirectory = Environment.CurrentDirectory;
            var filePath = String.Format("{0}{1}", currentDirectory, EngineSettingsFilePath);
            return ReadFile(filePath);
        }
        #endregion

        #region Generall

        public bool DeleteFile(string file)
        {
            try
            {
                File.Delete(file);
                return File.Exists(file);
            }
            catch (IOException e)
            {
                SLog.ErrorFormat("Could not delete file {0}, {1}",file,e);
                return false;
            }
        }

        public bool DeleteDirectory(string directory)
        {
            try
            {
                var dirInfo = new DirectoryInfo(directory);
                if (!dirInfo.Exists)
                {
                    SLog.WarnFormat("Directory {0} is missing",directory);
                    return true;
                }
                dirInfo.Delete(true);
                if ((new DirectoryInfo(directory)).Exists)
                {
                    SLog.ErrorFormat("Could not delete directory {0}",directory);
                    return false;
                }
                return true;
            }
            catch (IOException e)
            {
                SLog.ErrorFormat("Could not delete directory {0}, {1}", directory, e);
                return false;
            }
        }
        #endregion


        #region Member
        void WriteFile(string path, string content)
        {
            try
            {
                File.WriteAllText(path,content);
            }
            catch (IOException e)
            {
                SLog.DebugFormat("Could not write file {0}, {1}",e);
            }
        }

        string ReadFile(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (IOException e)
            {
                SLog.DebugFormat("Could not Read file {0}, {1}",path,e);
                return null;
            }
        }

        void CreateDirectory(string directory)
        {
            var path = directory;
            var directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists)
            {
                SLog.DebugFormat("Directory {0} does not exists, try to create", directoryInfo.Name);
                try
                {
                    directoryInfo.Create();
                }
                catch (Exception e)
                {
                    SLog.ErrorFormat("Could not create directory {0}, {1}", directoryInfo.FullName, e);
                }
                if (new DirectoryInfo(path).Exists)
                {
                    SLog.DebugFormat("Directory {0} successfully created", directoryInfo.Name);
                }
                else
                {
                    SLog.ErrorFormat("Directory {0} NOT created", directoryInfo.FullName);
                }
            }
            else
            {
                SLog.DebugFormat("Directory {0} already exists",directory);
            }
        }
        #endregion
    }
}
