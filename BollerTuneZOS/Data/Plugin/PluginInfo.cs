using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Plugin
{
    /// <summary>
    /// Informationen wo sich das Plugin befindet
    /// Jonas Ahlf 23.06.2015 14:37:04
    /// </summary>
    public class PluginInfo 
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Time when the plugin was installed
        /// </summary>
        public DateTime InstallDateTime { get; set; }

        /// <summary>
        /// Path to the Plugin dll which implements IBtzPlugin
        /// </summary>
        public string ExecutiveLibrary { get; set; }

        /// <summary>
        /// Location of the Plugin folder
        /// </summary>
        public string PluginLocation { get; set; }
    }
}
