using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTZ.Test.Infrastructure;
using DataAccess.Repositories;
using Infrastructure.Data.Settings;

namespace BTZ.Settings.Test
{
    /// <summary>
    /// Jonas Ahlf 22.06.2015 22:51:06
    /// </summary>
    internal static class BootStrapper
    {
        internal static void Register()
        {
            var container = TinyIoC.TinyIoCContainer.Current;
            container.Register<ISettingsRepository, SettingsRepository>().AsSingleton();
            container.Register<ITest, SettingsReadWriteTest>();

        }
    }
}
