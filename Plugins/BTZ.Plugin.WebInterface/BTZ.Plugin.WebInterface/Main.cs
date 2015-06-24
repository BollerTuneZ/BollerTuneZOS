using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Data.PluginOnly;
using Plugin.Infrastructure;
using WI.Core;
using WI.Infrastructure.Services;

namespace BTZ.Plugin.WebInterface
{
    /// <summary>
    /// BollerTuneZ WebInterface powered by Juanah
    /// Jonas Ahlf 24.06.2015 23:53:54
    /// </summary>
    public class Main : IBtzPlugin
    {
        private static PluginIdentity Identity;
        private static IService _settingsService;
        public Main()
        {
            Identity = new PluginIdentity
            {
                Author = "Jonas Ahlf 25.06.2015 00:29:10",
                Name = "BollerTuneZ WebInterface",
                Version = 1,
                VersionName = "1.0.0"
            };
        }

        public PluginIdentity GetIdentity()
        {
            return Identity;
        }

        public bool Initialize()
        {
            _settingsService = new SettingsService();
            return true;
        }

        public void OnStart()
        {
            _settingsService.Start();
        }

        public void OnStop()
        {
            _settingsService.Stop();
        }

        public void Dispose()
        {
        }

        public IBtzPluginHost Host { get; set; }
    }
}
