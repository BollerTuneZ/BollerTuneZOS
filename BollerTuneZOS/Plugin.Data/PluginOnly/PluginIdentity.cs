using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Data.PluginOnly
{
    /// <summary>
    /// Alle informationen über das Plugin wie name, version usw
    /// Informations about the plugin like name,version etc.
    /// Jonas Ahlf 23.06.2015 12:12:21
    /// </summary>
    public class PluginIdentity
    {
        /// <summary>
        /// Name des Plugins
        /// Name of the Plugin
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Git die Code-Version an
        /// Code Version of the plugin
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Versionsname des plugins
        /// Versionname of the plguin
        /// </summary>
        public string VersionName { get; set; }

        /// <summary>
        /// Autor des Plugins
        /// Athor of the plugin
        /// </summary>
        public string Author { get; set; }
    }
}
