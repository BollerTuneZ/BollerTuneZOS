using Data.Settings;

namespace Infrastructure.Data.Settings
{
    /// <summary>
    /// Repository welches die Settings verwaltet
    /// Jonas Ahlf 19.06.2015 22:56:46
    /// </summary>
    public interface ISettingsRepository
    {
        //SteeringSettings
        SteeringSettings RetriveSteeringSettings();
        void SaveSteeringSettings(SteeringSettings settings);
    }

}