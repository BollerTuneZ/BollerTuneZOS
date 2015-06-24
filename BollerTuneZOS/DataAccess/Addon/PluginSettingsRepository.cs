﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Settings;
using Infrastructure.Data.Settings;
using log4net;
using Plugin.Data;
using Plugin.Infrastructure.API.DataAccess;
using AutoMapper;
namespace DataAccess.Addon
{
    /// <summary>
    /// Settings Repository for plugins
    /// Jonas Ahlf 23.06.2015 13:31:51
    /// </summary>
    public class PluginSettingsRepository : IPluginSettingsRepository
    {
        private readonly ISettingsRepository _setiingsRepository;
        private static readonly ILog SLog = LogManager.GetLogger(typeof (PluginSettingsRepository));

        public PluginSettingsRepository(ISettingsRepository setiingsRepository)
        {
            _setiingsRepository = setiingsRepository;
            Mapper.CreateMap<SteeringSettings, PluginSteeringSettings>();
            Mapper.CreateMap<PluginSteeringSettings, SteeringSettings>();
        }


        public PluginSteeringSettings LoadSteeringSettings()
        {
            SLog.Debug("Plugin is loading SteeringSettings");
            return Mapper.Map<SteeringSettings,PluginSteeringSettings>(_setiingsRepository.RetriveSteeringSettings());
        }

        public bool SaveSteeringSettings(PluginSteeringSettings settings)
        {
            var mappedSettings = Mapper.Map<PluginSteeringSettings, SteeringSettings>(settings);
            SLog.Debug("Plugin is saving settings");
            _setiingsRepository.SaveSteeringSettings(mappedSettings);
            return true; //TODO Hier mit rechten und so weiter arbeiten
        }
    }
}