using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Util
{
    /// <summary>
    /// Jonas Ahlf 01.07.2015 21:48:23
    /// </summary>
    public static class MathHelper
    {
        public static decimal Map(this decimal value, decimal fromSource, decimal toSource, decimal fromTarget, decimal toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }

        public static int Map(this int value, int fromSource, int toSource, int fromTarget, int toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }

        public static double Map(this double value, double fromSource, double toSource, double fromTarget, double toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }

        /// <summary>
        /// Rechnet aus ob die differenz zweier werte in einer bestimmten tolleranz liegen
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsTolerated(int source, int target, float tolerance)
        {
            var percentage = (float)(source*100)/target;

            if ((percentage - 100) <= tolerance )
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Rechnet aus ob die differenz zweier werte in einer bestimmten tolleranz liegen
        /// und gibt die errechnete Prozent zahl zurück
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static Tuple<bool,float> IsToleratedReturnPercentage(int source, int target, float tolerance)
        {
            var percentage = (float)(source * 100) / target;
            if ((percentage - 100) <= tolerance)
            {
                return new Tuple<bool, float>(true, percentage);
            }
            return new Tuple<bool, float>(false, percentage);
        }

    }
}
