namespace Infrastructure.Services
{
    /// <summary>
    /// Represents Controlling Process Service
    /// Jonas Ahlf 26.06.2015 23:04:05
    /// </summary>
    public interface IControllService
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