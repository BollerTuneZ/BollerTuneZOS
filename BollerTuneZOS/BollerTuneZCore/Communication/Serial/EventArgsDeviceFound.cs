using System;

namespace Communication.Serial
{
    /// <summary>
    /// Wird gefeuert sobald ein Device gefunden wurde
    /// Jonas Ahlf 19.06.2015 18:00:44
    /// </summary>
    public class EventArgsDeviceFound : EventArgs
    {
        public SerialDevice Device { get; set; }
    }
}
