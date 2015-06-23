﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Data.PluginOnly;
using Plugin.Infrastructure;
using Plugin.Infrastructure.API.DataAccess;

namespace BTZ.Plugin.Tests
{
    public class Main: IBtzPlugin
    {
        private static PluginIdentity _identity;
        public IBtzPluginHost Host { get; set; }
        private IPluginSettingsRepository _settingsRepository;

        public Main()
        {
            _identity = new PluginIdentity
            {
                Name = "BTZ.Plugin.Test",
                Author = "Jonas Ahlf 23.06.2015 21:56:28",
                Version = 1,
                VersionName = "1.0"
            };
        }

        public PluginIdentity GetIdentity()
        {
            return _identity;
        }

        public bool Initialize()
        {
            if (Host != null)
            {
                Console.WriteLine("Getting SettingsRepository");
                _settingsRepository = Host.GetSettingsRepository();
                return true;
            }
            Console.WriteLine("Error could not get SettingsRepository");
            return false;
        }

        public void OnStart()
        {
            Console.WriteLine("OnStart");
            var settings = _settingsRepository.LoadSteeringSettings();
            var message = String.Format(
                "{0}", settings.SteeringMax);
            Console.WriteLine(message);
            settings.SteeringMax = 999999;
            _settingsRepository.SaveSteeringSettings(settings);
            settings = _settingsRepository.LoadSteeringSettings();
            message = String.Format(
                "{0}", settings.SteeringMax);
            Console.WriteLine(message);
        }

        public void OnStop()
        {
            _settingsRepository = null;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
