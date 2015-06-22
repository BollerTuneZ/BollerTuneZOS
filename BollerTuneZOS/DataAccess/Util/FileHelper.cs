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

        public void WriteSettingsFile(string content)
        {
            var currentDirectory = Environment.CurrentDirectory;
            var filePath = Path.Combine(currentDirectory, SettingsFilePath);
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
            var filePath = Path.Combine(currentDirectory, SettingsFilePath);
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
                SLog.ErrorFormat("Could not write SettingsFile {0}: {1}", filePath, e);
                return null;
            }
        }
    }
}
