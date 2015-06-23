using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Main
{
    /// <summary>
    /// Jonas Ahlf 20.06.2015 17:02:46
    /// </summary>
    public class ArgumentTranslator : IArgumentTranslator
    {
        private static IDictionary<string,BtzArgument> _stringKey = new Dictionary<string, BtzArgument>();
        private static IDictionary<BtzArgument,string> _enumKey = new Dictionary<BtzArgument, string>();

        public ArgumentTranslator()
        {
            CreateDictionaries();
        }

        public BtzArgument GetArgument(string rawArgument)
        {
            if (!_stringKey.ContainsKey(rawArgument))
            {
                return BtzArgument.Non;
            }
            return _stringKey[rawArgument];
        }

        void CreateDictionaries()
        {
            _stringKey.Add("-serial",BtzArgument.Serial);
            _stringKey.Add("-network",BtzArgument.Network);
            _enumKey.Add(BtzArgument.Serial,"-serial");
            _enumKey.Add(BtzArgument.Network,"-network");
        }
    }
}
