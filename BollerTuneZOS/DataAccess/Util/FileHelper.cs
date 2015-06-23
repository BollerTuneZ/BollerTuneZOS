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
        private const string SettingsFilePath = @"\Settings\settings.btz";

        public FileHelper()
        {
            var directoryPath = String.Format("{0}{1}",Environment.CurrentDirectory, @"\Settings");
            var settingsDirectory = new DirectoryInfo(directoryPath);
            if (!settingsDirectory.Exists)
            {
                SLog.DebugFormat("Directory {0} does not exists, try to create",settingsDirectory.Name);
                try
                {
                    settingsDirectory.Create();
                }
                catch (Exception e)
                {
                    SLog.ErrorFormat("Could not create directory {0}, {1}",settingsDirectory.FullName,e);
                }
                if (new DirectoryInfo(directoryPath).Exists)
                {
                    SLog.DebugFormat("Directory {0} successfully created",settingsDirectory.Name);
                }
                else
                {
                    SLog.ErrorFormat("Directory {0} NOT created", settingsDirectory.FullName);
                }
            }
        }

        public void WriteSettingsFile(string content)
        {
            var currentDirectory = Environment.CurrentDirectory;
            var filePath = String.Format("{0}{1}", currentDirectory, SettingsFilePath);
            try
            {
                SLog.DebugFormat("Write SettingsFile {0}", filePath);
                File.WriteAllText(filePath, content);
            }
            catch (IOException e)
            {
                SLog.ErrorFormat("Could not write SettingsFile {0}: {1}",filePath,e);
            }
        }

        public string ReadSettingsFile()
        {
            var currentDirectory = Environment.CurrentDirectory;
            var filePath = String.Format("{0}{1}",currentDirectory, SettingsFilePath);
            try
            {
                SLog.DebugFormat("Read SettingsFile {0}", filePath);
                var content = File.ReadAllText(filePath);
                if (String.IsNullOrWhiteSpace(content))
                {
                    SLog.ErrorFormat("SettingsFile {0} is empty/null", filePath);
                    return null;
                }
                return content;
            }
            catch (IOException e)
            {
                SLog.ErrorFormat("Could not read SettingsFile {0}: {1}", filePath, e);
                return null;
            }
        }
    }
}
