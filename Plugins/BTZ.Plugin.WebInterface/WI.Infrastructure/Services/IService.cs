namespace WI.Infrastructure.Services
{
    /// <summary>
    /// Simple Interface for Services
    /// Jonas Ahlf 24.06.2015 23:56:57
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Starts the Service
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the Service
        /// </summary>
        void Stop();
    }
}