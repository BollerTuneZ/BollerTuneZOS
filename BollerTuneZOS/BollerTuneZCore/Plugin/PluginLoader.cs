using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Plugin;
using Plugin.Infrastructure;

namespace BollerTuneZCore.Plugin
{
    /// <summary>
    /// Jonas Ahlf 23.06.2015 14:34:11
    /// </summary>
    public class PluginLoader :IPluginLoader
    {
        public IList<IBtzPlugin> LoadBtzPlugins()
        {
            throw new NotImplementedException();
        }

        public void StartPlugin(IBtzPlugin plugin)
        {
            throw new NotImplementedException();
        }

        public void ClosePlugin(IBtzPlugin plugin)
        {
            throw new NotImplementedException();
        }
    }
}
