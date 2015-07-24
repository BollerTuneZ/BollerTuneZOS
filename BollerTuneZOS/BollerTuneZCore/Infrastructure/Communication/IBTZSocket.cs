namespace Infrastructure.Communication
{
    /// <summary>
    /// Allgemeine Schnittstelle für Kommunikationssockets
    /// Jonas Ahlf 19.06.2015 18:33:29
    /// </summary>
    public interface IBtzSocket
    {
        /// <summary>
        /// Versendet Daten
        /// </summary>
        /// <param name="payload"></param>
        void SendData(byte[] payload);
        /// <summary>
        /// Empfängtdaten
        /// </summary>
        /// <returns></returns>
        byte[] ReceiveData();
    }
}