using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.JoystickApi.JoyStickEventArgs
{
    /// <summary>
    /// Jonas Ahlf 21.06.2015 00:45:28
    /// </summary>
    public class JoyStickValueEventArgs : EventArgs
    {
        public int Value { get; set; }
    }
}
