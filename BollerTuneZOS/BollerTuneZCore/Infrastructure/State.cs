using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.JoystickApi.JoyStickEventArgs;

namespace Infrastructure
{
    /// <summary>
    /// Alle wichtigen Daten werden hier temporär gespeichert
    /// Jonas Ahlf 21.06.2015 01:08:36
    /// </summary>
    public static class State
    {
        static State() //Start Parameter werden gesetzt
        {
            Enabled = false;
            DriveSpeed = 0;
        }

        public static int DriveSpeed { get; set; }
        public static int RemotePosition { get; set; }
        public static Shift Shift { get; set; }
        public static float SteeringSensitive { get; set; }
        public static bool Enabled { get; set; }
    }

    public enum Shift : int
    {
        Backwards2 = -2,
        Backwards1 = -1,
        Neutral = 0,
        First = 1,
        Second = 2,
        Third = 3
    }

}
