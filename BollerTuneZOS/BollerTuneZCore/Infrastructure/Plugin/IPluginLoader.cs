using System.Collections.Generic;
using Plugin.Infrastructure;

namespace Infrastructure.Plugin
{
    /// <summary>
    /// Loads Plugins
    /// Jonas Ahlf 23.06.2015 14:31:50
    /// </summary>
    public interface IPluginLoader
    {
        IList<IBtzPlugin> LoadBtzPlugins();

        /// <summary>
        /// Starts given plugin
        /// </summary>
        /// <param name="plugin"></param>
        void StartPlugin(IBtzPlugin plugin);

        /// <summary>
        /// Closes plugin
        /// </summary>
        /// <param name="plugin"></param>
        void ClosePlugin(IBtzPlugin plugin);
    }
}