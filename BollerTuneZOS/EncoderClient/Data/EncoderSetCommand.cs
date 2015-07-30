using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoderClient.Data
{
    /// <summary>
    /// Beinhaltet das Commando um einen Encoder wert zu überschreiben
    /// Jonas Ahlf 28.07.2015 17:37:46
    /// </summary>
    internal class EncoderSetCommand
    {
        public string Command = "SET_ENCODER";

        public string ECMODE { get; set; }

        public int Value { get; set; }
    }
}
