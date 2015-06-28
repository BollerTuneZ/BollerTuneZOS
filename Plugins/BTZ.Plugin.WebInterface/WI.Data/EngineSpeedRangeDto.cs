using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WI.Data
{
    /// <summary>
    /// Gibt an in welchem bereich sich die Geschwindikeit vom Motor abspielen darf
    /// Jonas Ahlf 28.06.2015 01:09:37
    /// </summary>
    public class EngineSpeedRangeDto
    {
        public int SteeringSpeedMax_MaxDOM { get; set; }
        public int SteeringSpeedMax_MinDOM { get; set; }
        public int SteeringSpeedMin_MaxDOM { get; set; }
        public int SteeringSpeedMin_MinDOM { get; set; }
    }
}
