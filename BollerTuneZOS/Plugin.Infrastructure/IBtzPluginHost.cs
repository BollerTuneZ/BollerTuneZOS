using Plugin.Infrastructure.API.DataAccess;

namespace Plugin.Infrastructure
{
    /// <summary>
    /// Verbindung zum host
    /// Jonas Ahlf 23.06.2015 13:06:06
    /// </summary>
    public interface IBtzPluginHost
    {
        /// <summary>
        /// Gibt das Settings repository zurück
        /// Returns the settings repository
        /// </summary>
        /// <returns></returns>
        IPluginSettingsRepository GetSettingsRepository();
    }
}