using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Data.Plugin;
using DataAccess.Util;
using Infrastructure.Data;
using log4net;
using Newtonsoft.Json;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Jonas Ahlf 23.06.2015 14:43:09
    /// </summary>
    public class PluginRepository : IPluginRepository
    {
        private static readonly ILog SlLog = LogManager.GetLogger(typeof (PluginRepository));
        private static PluginList _pluginList;
        private readonly FileHelper _fileHelper = new FileHelper();
        public PluginRepository()
        {
            var pluginList = _fileHelper.ReadPluginList();
            if (pluginList == null)
            {
                _pluginList = new PluginList {PluginInfos = new List<PluginInfo>()};
                _fileHelper.WritePluginList(JsonConvert.SerializeObject(_pluginList));
            }
            else
            {
                _pluginList = JsonConvert.DeserializeObject<PluginList>(pluginList);
            }
        }

        public PluginList GetPluginList()
        {
            return _pluginList;
        }

        public bool InstallPlugin(PluginInfo info, string folder)
        {
            SlLog.InfoFormat("Installing Plugin {0}",info.Name);
            if (!File.Exists(info.ExecutiveLibrary))
            {
                SlLog.ErrorFormat("ExecutiveLibrary {0} does not exists",info.ExecutiveLibrary);
                return false;
            }

            if (!(new DirectoryInfo(folder)).Exists)
            {
                SlLog.ErrorFormat("Source directory of Plugin {0}({1}) is missing",info.Name,folder);
                return false;
            }

            var destinationDirectory = _fileHelper.CopyPluginDirectory(folder,info.Name);

            if (String.IsNullOrWhiteSpace(destinationDirectory))
            {
                SlLog.ErrorFormat("Plugin {0} could not be installed",info.Name);
                return false;
            }
            var executiveLibrary = String.Format("{0}/{1}",destinationDirectory, (new FileInfo(info.ExecutiveLibrary)).Name);
            if (!File.Exists(executiveLibrary))
            {
                SlLog.ErrorFormat("Plugin {0} ExecutiveLibrary could not be found",info.Name);
                return false;
            }
            var pluginInfo = new PluginInfo
            {
                ExecutiveLibrary = executiveLibrary,
                InstallDateTime = DateTime.Now,
                Name = info.Name,
                PluginLocation = destinationDirectory
            };
            _pluginList.PluginInfos.Add(pluginInfo);
            _pluginList.LastTimeInstalled = DateTime.Now;
            Save();
            SlLog.InfoFormat("Plugin {0} successfully installed",info.Name);
            return true;
        }

        public bool DeletePlugin(PluginInfo info)
        {
            var pluginInfo = _pluginList.PluginInfos.FirstOrDefault(n => n.Name.Equals(info.Name));
            if (pluginInfo == null)
            {
                SlLog.WarnFormat("Plugin {0} could not be found",info.Name);
                return false;
            }

            if (!_fileHelper.DeleteDirectory(pluginInfo.PluginLocation))
            {
                SlLog.ErrorFormat("Could not delete Plugin {0}",info.Name);
            }

            _pluginList.PluginInfos.Remove(pluginInfo);
            Save();
            return true;
        }

        #region Member

        void Save()
        {
            var content = JsonConvert.SerializeObject(_pluginList);
            _fileHelper.WritePluginList(content);
        }
        void RefreshPluginList()
        {
            SlLog.Debug("Refresh PluginList");

        }
        #endregion

        
    }
}
