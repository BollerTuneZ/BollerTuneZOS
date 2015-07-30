using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Settings
{
    /// <summary>
    /// Einstellungen für die Lenkung
    /// Jonas Ahlf 19.06.2015 22:36:22
    /// </summary>
    public class SteeringSettings
    {

        public string EncoderServiceIpAdress { get; set; }
        public int EncoderServicePort;
        /// <summary>
        /// Gibt an ob nach einem neustart die daten geladen werden sollen
        /// </summary>
       public bool Resume { get; set; }
       public int CurrentPositionMotor { get; set; }
       public int StoredPositionMotor { get; set; }
       public int CurrentPositionSteering { get; set; }
       public int StoredPositionSteering { get; set; }

        /*Hardware Settings*/
       public int SteeringMax { get; set; }
       public int SteeringMin { get; set; }
       public int SteeringCenter { get; set; }
       public int SteeringSpeedMin { get; set; }
       public int SteeringSpeedMax { get; set; }

        /*SoftwareSettings*/
       public int RemoteMin { get; set; }
       public int RemoteMax { get; set; }

        /// <summary>
        /// Gibt die Toleranz an zwischen der Remote und wirklichen Lenkposition
        /// </summary>
       public float SteeringPositionDiffTolerance { get; set; }

        //Gibt an wie hoch die abweichung der Positionen sein darf
       public float SteeringSensitiv { get; set; }
       public float DriveSensitiv { get; set; }

    }
}
