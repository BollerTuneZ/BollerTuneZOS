using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WI.Data
{
    /// <summary>
    /// SteeringConfig which will be received by socket io event SteeringConfig
    /// Jonas Ahlf 25.06.2015 00:17:28
    /// </summary>
    public class SteeringConfigDto
    {
        public int SteeringRangeMax { get; set; }
        public int SteeringRangeMin { get; set; }
        public int SteeringCenter { get; set; }
        public int SteeringToleranz { get; set; }
    }
}
