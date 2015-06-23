using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Plugin
{
    /// <summary>
    /// Liste aller Plugins
    /// List of all Plugins
    /// Jonas Ahlf 23.06.2015 14:37:44
    /// </summary>
    public class PluginList
    {

        /// <summary>
        /// LastTime a plugin was installed
        /// </summary>
        public DateTime LastTimeInstalled { get; set; }

        /// <summary>
        /// All PluginInfos
        /// </summary>
        public IList<PluginInfo> PluginInfos { get; set; } 

    }
}
