using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTZ.Test.Infrastructure;
using Infrastructure.Data.Settings;
using log4net;

namespace BTZ.Settings.Test
{
    /// <summary>
    /// Jonas Ahlf 22.06.2015 22:49:56
    /// </summary>
    internal class SettingsReadWriteTest : ITest
    {
        private static readonly ILog SLog = LogManager.GetLogger(typeof (SettingsReadWriteTest));
        public event EventHandler OnTestFinish;
        private readonly ISettingsRepository _settingsRepository;

        public SettingsReadWriteTest(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public void Start()
        {
            SLog.Info("Retrive Settings...");

            var settings = _settingsRepository.RetriveSteeringSettings();
            if (settings == null)
            {
                SLog.Error("Settings are null");
                return;
            }
            SLog.Info("Manipulate settings...");
            settings.CurrentPositionMotor = 100;
            settings.CurrentPositionSteering = 200;
            settings.RemoteMax = 5000;
            settings.RemoteMin = -5000;
            settings.Resume = true;
            settings.SteeringCenter = 150;
            settings.SteeringMax = 6000;
            settings.SteeringMin = 0;
            settings.SteeringPositionDiffTolerance = 10.0f;
            settings.SteeringSpeedMax = 255;
            settings.SteeringSpeedMin = 30;
            settings.StoredPositionMotor = 5660;
            settings.StoredPositionSteering = 12345;
            SLog.Info("Save settings...");
            _settingsRepository.SaveSteeringSettings(settings);
            var reReadSettings = _settingsRepository.RetriveSteeringSettings();
        }

        public string GetName()
        {
            return "SettingsReadWriteTest";
        }
    }
}
