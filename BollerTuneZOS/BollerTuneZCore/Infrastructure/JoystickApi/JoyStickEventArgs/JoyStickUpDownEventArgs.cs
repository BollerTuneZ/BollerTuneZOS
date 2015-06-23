using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.JoystickApi.JoyStickEventArgs
{
    /// <summary>
    /// Jonas Ahlf 21.06.2015 00:43:35
    /// </summary>
    public class JoyStickUpDownEventArgs : EventArgs
    {
        public EventUpDown JoyEvent { get; set; }
    }

    public enum EventUpDown
    {
        Up,
        Down
    }
}
