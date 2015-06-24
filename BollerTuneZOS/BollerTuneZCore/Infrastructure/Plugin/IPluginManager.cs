namespace Infrastructure.Plugin
{
    /// <summary>
    /// Manages all plugin operations like
    /// install
    /// run
    /// deinstall
    /// and so on
    /// </summary>
    public interface IPluginManager
    {
        void EnterPluginManager();
    }
}