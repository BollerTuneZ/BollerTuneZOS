using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Communication
{
    /// <summary>
    /// Alle Konstanten, die für die Kommunikation gebraucht werden
    /// Jonas Ahlf 19.06.2015 18:06:59
    /// </summary>
    public static class SerialConstants
    {

        public static readonly int BOUD_RATE = 9600;

        #region Steering
        public static readonly byte START_BYTE = 0xDE;
        public static readonly byte COMMAND_SET_PORT = 0x53;
        public static readonly byte COMMAND_PRINT_IDENTITY = 0x49;
        public static readonly byte COMMAND_SET_ENCODER = 0x45;
        public static readonly byte COMMAND_DIRECTION_STEERING = 0x44;
        public static readonly byte COMMAND_POWER = 0x50;
        public static readonly byte ENCODER_MOTOR = 0x4D;
        public static readonly byte ENCODER_STEERING = 0x53;
        public static readonly byte DIRECTION_LEFT = 0x4C;
        public static readonly byte DIRECTION_RIGHT = 0x52;
        #endregion
        public static readonly string IDENTITY_STEERING = "STEERING";

        public static readonly byte COMMAND_DIRECTION_DRIVE = 0x44; //D
        public static readonly byte COMMAND_DRIVE_POWER = 0x50;//P
        public static readonly byte COMMAND_DRIVE_READ = 0x52; //R

    }
}
