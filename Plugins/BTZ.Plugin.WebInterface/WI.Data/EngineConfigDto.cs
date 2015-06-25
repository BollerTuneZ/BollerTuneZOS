using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WI.Data
{
    /// <summary>
    /// Jonas Ahlf 25.06.2015 22:22:30
    /// </summary>
    public class EngineConfigDto
    {
        // EngineSpeedStartMin: data.EngineSpeedStartMin, EngineSpeedMax: data.EngineSpeedMax, EngineRampTime: data.EngineRampTime
        public int EngineSpeedStartMin { get; set; }
        public int EngineSpeedMax { get; set; }
        public int EngineRampTime { get; set; }
    }
}
