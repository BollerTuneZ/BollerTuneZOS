using Plugin.Data;

namespace Plugin.Infrastructure.API.DataAccess
{
    /// <summary>
    /// Repository um Einstellungen zu laden und zu speichern
    /// Repository to load and save settings
    /// Jonas Ahlf 23.06.2015 12:29:47
    /// </summary>
    public interface IPluginSettingsRepository
    {
        PluginSteeringSettings LoadSteeringSettings();

        bool SaveSteeringSettings(PluginSteeringSettings settings);
    }
}