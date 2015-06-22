using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Steering.Enums;

namespace Data.Steering
{
    /// <summary>
    /// Enthält die daten für eine Bewegungsoperation
    /// Jonas Ahlf 20.06.2015 00:12:08
    /// </summary>
    public class SteeringMovement
    {
        public SteeringMovement(SteeringDirection direction, int speed)
        {
            Direction = direction;
            Speed = speed;
        }

        public SteeringDirection Direction { get; private set; }

        public int Speed { get; private set; }

    }
}
