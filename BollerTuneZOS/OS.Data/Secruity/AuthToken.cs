using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS.Data.Secruity
{
    /// <summary>
    /// Authenfication Key which will be generated at login time
    /// Jonas Ahlf 30.07.2015 14:16:13
    /// </summary>
    public class AuthToken
    {
        public AuthToken(string loginName, string token)
        {
            LoginName = loginName;
            Token = token;
        }

        public string LoginName { get; private set; }

        public string Token { get; private set; }
    }
}
