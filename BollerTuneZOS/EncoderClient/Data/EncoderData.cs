using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoderClient.Data
{
    /// <summary>
    /// Enthält die Encoder Daten von dem Arduino
    /// Jonas Ahlf 28.07.2015 13:04:23
    /// </summary>
    public class EncoderData
    {
        /// <summary>
        /// Position des Menchanisches Lekrades
        /// </summary>
        public int EncoderSteering { get; set; }
        /// <summary>
        /// Position des Lenkmotors an der Antriebswelle
        /// </summary>
        public int EncoderMotor { get; set; }

    }
}
