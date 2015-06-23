using Plugin.Data.PluginOnly;

namespace Plugin.Infrastructure
{
    /// <summary>
    /// Stellt ein BTZOS Plugin dar
    /// Mask of BTZOS Plugin
    /// Jonas Ahlf 23.06.2015 12:02:17
    /// </summary>
    public interface IBtzPlugin
    {
        /// <summary>
        /// Gibt die Identität des plugins zurück
        /// returns the identity of the plugin
        /// </summary>
        /// <returns></returns>
        PluginIdentity GetIdentity();

        /// <summary>
        /// Initialisiert das Plugin
        /// Initilize the plugin
        /// </summary>
        /// <returns></returns>
        bool Initialize();

        /// <summary>
        /// Beendet das Plugin
        /// Closes the plugin
        /// </summary>
        void Dispose();
    }
}