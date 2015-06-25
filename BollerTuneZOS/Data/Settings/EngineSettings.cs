using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Settings
{
    /// <summary>
    /// Jonas Ahlf 25.06.2015 23:44:57
    /// </summary>
    public class EngineSettings
    {
        // EngineSpeedStartMin: data.EngineSpeedStartMin, EngineSpeedMax: data.EngineSpeedMax, EngineRampTime: data.EngineRampTime }
        public int EngineSpeedStartMin { get; set; }
        public int EngineSpeedMax { get; set; }
        public int EngineRampTime { get; set; }
    }
}
