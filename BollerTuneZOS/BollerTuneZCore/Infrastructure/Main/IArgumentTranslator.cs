using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Main
{
    /// <summary>
    /// Konvertiert strings in enums 
    /// Jonas Ahlf 20.06.2015 17:00:07
    /// </summary>
    public interface IArgumentTranslator
    {
        /// <summary>
        /// Konvertiert einen string in ein Argument
        /// </summary>
        /// <param name="rawArgument"></param>
        /// <returns></returns>
        BtzArgument GetArgument(string rawArgument);
    }

    public enum BtzArgument
    {
        Non,
        Serial,
        Network
    }
}
