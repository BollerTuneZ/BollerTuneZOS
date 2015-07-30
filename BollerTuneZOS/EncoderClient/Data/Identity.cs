using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoderClient.Data
{
    /// <summary>
    /// Identität des Arduinos
    /// </summary>
    internal class Identity
    {
        /// <summary>
        /// Name des Geräts
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Der Zeitpunkt der tcp kopplung
        /// </summary>
        public DateTime ConnectionTime { get; set; }
    }
}
