using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Settings;
using DataAccess.Util;
using Infrastructure.Data.Settings;
using log4net;
using Newtonsoft.Json;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Repository für den Datenzugriff auf die Settings
    /// Jonas Ahlf 19.06.2015 22:59:48
    /// </summary>
    public class SettingsRepository : ISettingsRepository
    {
        private SteeringSettings _settings;
        private static FileHelper _fileHelper = new FileHelper();
        private static readonly ILog SLog = LogManager.GetLogger(typeof (SettingsRepository));

        public SteeringSettings RetriveSteeringSettings()
        {
            if (_settings != null)
            {
                return _settings;
            }
            RefreshSteeringSettings();
            return _settings;
        }

        public void SaveSteeringSettings(SteeringSettings settings)
        {
            SLog.Debug("Writing Steering Settings");
            var serialized = JsonConvert.SerializeObject(settings);
            _fileHelper.WriteSettingsFile(serialized);
            RefreshSteeringSettings();
        }

        private void RefreshSteeringSettings()
        {
            SLog.Debug("Refresh Settings...");
            string steeringSettingsContent = _fileHelper.ReadSettingsFile();
            if (String.IsNullOrWhiteSpace(steeringSettingsContent))
            {
                SLog.Warn("JSON Settings Object is null, create new default settings and save them");
                _settings = new SteeringSettings();
                SaveSteeringSettings(_settings);
                return;
            }
            try
            {
                _settings = JsonConvert.DeserializeObject<SteeringSettings>(steeringSettingsContent);
            }
            catch (Exception e)
            {
                SLog.ErrorFormat("Error could not parse json object {0} : {1}",steeringSettingsContent,e);
            }
        }
    }
}
