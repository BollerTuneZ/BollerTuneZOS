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
        /// BollerTuneZ Server host connection
        /// </summary>
        IBtzPluginHost Host { get; set; }
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
        /// Will be triggered as soon as the host wants to start the plugin
        /// </summary>
        void OnStart();

        /// <summary>
        /// Will be fired when the host wants to stop the plugin
        /// (It will be stopped if you want or not ;) )
        /// </summary>
        void OnStop();

        /// <summary>
        /// Beendet das Plugin
        /// Closes the plugin
        /// </summary>
        void Dispose();
    }
}