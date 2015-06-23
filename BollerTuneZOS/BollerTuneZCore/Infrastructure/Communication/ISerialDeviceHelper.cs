using System;
using System.Collections.Generic;

namespace Infrastructure.Communication
{
    /// <summary>
    /// Helferklasse um Serielle Geräte zu finden
    /// Jonas Ahlf 19.06.2015 17:21:06
    /// </summary>
    public interface ISerialDeviceHelper
    {
        event EventHandler OnDeviceFound;

        void StartDiscover();

        void StopDiscover();
    }
}