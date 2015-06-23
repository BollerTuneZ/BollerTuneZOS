using Data.Plugin;

namespace Infrastructure.Data
{
    /// <summary>
    /// Datenzuggriff und verwaltung der Plugins
    /// Dataaccess and managment of the Plugins
    /// Jonas Ahlf 23.06.2015 14:35:32
    /// </summary>
    public interface IPluginRepository
    {
        /// <summary>
        /// Returns the List of all Plugins
        /// </summary>
        /// <returns></returns>
        PluginList GetPluginList();

        /// <summary>
        /// Installs a plugin 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        bool InstallPlugin(PluginInfo info,string folder);

        /// <summary>
        /// Deletes plugin
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool DeletePlugin(PluginInfo info);
    }
}