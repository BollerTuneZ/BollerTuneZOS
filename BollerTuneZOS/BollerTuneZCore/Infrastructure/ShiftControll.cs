using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.JoystickApi.JoyStickEventArgs;

namespace Infrastructure
{
    public static class ShiftControll
    {
        public static float ShiftInPercent(Shift shift)
        {
            switch (shift)
            {
                case Shift.Backwards2:
                    return 0;
                case Shift.Backwards1:
                    return 0;
                case Shift.Neutral:
                    return 0;
                case Shift.First:
                    return 0.25f;
                case Shift.Second:
                    return 0.5f;
                case Shift.Third:
                    return 1.0f;
                default:
                    return 0;
            }
        }

        public static Shift DoShift(EventUpDown jEvent, Shift current)
        {
            if (jEvent == EventUpDown.Down)
            {
                return Down(current);
            }
            if (jEvent == EventUpDown.Up)
            {
                return Up(current);
            }
            return Shift.Neutral;
        }
        public static Shift Up(Shift current)
        {
            switch (current)
            {
                case Shift.Backwards2:
                    return Shift.Backwards1;
                case Shift.Backwards1:
                    return Shift.Neutral;
                case Shift.Neutral:
                    return Shift.First;
                case Shift.First:
                    return Shift.Second;
                case Shift.Second:
                    return Shift.Third;
                case Shift.Third:
                    return Shift.Third;
                default:
                    return Shift.Neutral;
            }
        }

        public static Shift Down(Shift current)
        {
            switch (current)
            {
                case Shift.Backwards2:
                    return Shift.Backwards2;
                case Shift.Backwards1:
                    return Shift.Backwards2;
                case Shift.Neutral:
                    return Shift.Backwards1;
                case Shift.First:
                    return Shift.Neutral;
                case Shift.Second:
                    return Shift.First;
                case Shift.Third:
                    return Shift.Second;
                default:
                    return Shift.Neutral;
            }
        }
    }
}
