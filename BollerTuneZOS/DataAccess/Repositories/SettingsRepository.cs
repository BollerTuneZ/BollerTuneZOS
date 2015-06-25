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
        private SteeringSettings _steeringSettings;
        private EngineSettings _engineSettings;
        private static FileHelper _fileHelper = new FileHelper();
        private static readonly ILog SLog = LogManager.GetLogger(typeof (SettingsRepository));

        
        #region Steering
        public SteeringSettings RetriveSteeringSettings()
        {
            if (_steeringSettings != null)
            {
                return _steeringSettings;
            }
            RefreshSteeringSettings();
            return _steeringSettings;
        }
        public void SaveSteeringSettings(SteeringSettings settings)
        {
            SLog.Debug("Writing Steering Settings");
            var serialized = JsonConvert.SerializeObject(settings);
            _fileHelper.WriteSteeringSettingsFile(serialized);
            RefreshSteeringSettings();
        }

        private void RefreshSteeringSettings()
        {
            SLog.Debug("Refresh SteeringSettings...");
            string steeringSettingsContent = _fileHelper.ReadSteeringSettingsFile();
            if (String.IsNullOrWhiteSpace(steeringSettingsContent))
            {
                SLog.Warn("JSON Settings Object is null, create new default steeringsettings and save them");
                _steeringSettings = new SteeringSettings();
                SaveSteeringSettings(_steeringSettings);
                return;
            }
            try
            {
                _steeringSettings = JsonConvert.DeserializeObject<SteeringSettings>(steeringSettingsContent);
            }
            catch (Exception e)
            {
                SLog.ErrorFormat("Error could not parse json object {0} : {1}",steeringSettingsContent,e);
            }
        }
        #endregion

        #region Engine

        public EngineSettings RetriveEngineSettings()
        {
            if (_engineSettings != null)
            {
                return _engineSettings;
            }
            RefreshEngineSettings();
            return _engineSettings;
        }

        public void SaveEngineSettings(EngineSettings settings)
        {
            var serialized = JsonConvert.SerializeObject(settings);
            _fileHelper.WriteEngineSettingsFile(serialized);
            RefreshEngineSettings();
        }
        void RefreshEngineSettings()
        {
            string engineSettingsContent = _fileHelper.ReadEngineSettingsFile();
            if (String.IsNullOrWhiteSpace(engineSettingsContent))
            {
                SLog.Warn("JSON Settings Object is null, create new default enginesettings and save them");
                _engineSettings = new EngineSettings();
                SaveEngineSettings(_engineSettings);
                return;
            }
            try
            {
                _engineSettings = JsonConvert.DeserializeObject<EngineSettings>(engineSettingsContent);
            }
            catch (Exception e)
            {
                SLog.ErrorFormat("Error could not parse json object {0} : {1}", engineSettingsContent, e);
            }
        }
        #endregion
    }
}
