using Infrastructure.Communication;

namespace Communication.Infrastructure.MessageProcessor
{
	/// <summary>
	/// Übergibt befehle an den Arduino
	/// </summary>
	public interface ISteeringProcessor
	{
        /// <summary>
        /// Initialisiert den Lenkprocessor mit dem angebenen verbindungssocket
        /// </summary>
        /// <param name="socket"></param>
        void Initialize(IBtzSocket socket);
        /// <summary>
        /// Startet den Lenkungsservice
        /// </summary>
	    void Start();
        /// <summary>
        /// Stopt den Lenkungsservice
        /// ACHTUNG ist nicht das selbe wie Enabled!!
        /// Here is complete shutdown becareful
        /// </summary>
	    void Stop();
        /// <summary>
        /// Hiermit wird die Position vom Remote gesetzt
        /// </summary>
        /// <param name="position"></param>
	    void SetPosition(int position);
        /// <summary>
        /// Aktiviert oder Deaktiviert die aktive Lenkung
        /// </summary>
        /// <param name="enabled"></param>
		void SetEnabled (bool enabled);
        /// <summary>
        /// Setzt die Position eines Encoders
        /// </summary>
        /// <param name="encoderType"></param>
        /// <param name="position"></param>
	    void SetEncoderPosition(EncoderType encoderType, int position);
	}

    public enum EncoderType
    {
        Steering,
        Motor
    }
}

