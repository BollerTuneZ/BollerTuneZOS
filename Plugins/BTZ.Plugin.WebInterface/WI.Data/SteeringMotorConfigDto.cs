using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WI.Data
{
    /// <summary>
    /// Jonas Ahlf 25.06.2015 22:20:45
    /// </summary>
    public class SteeringMotorConfigDto
    {
        //SteeringSpeedMax: data.SteeringSpeedMax, SteeringSpeedMin: data.SteeringSpeedMin
        public int SteeringSpeedMax { get; set; }
        public int SteeringSpeedMin { get; set; }
    }
}
