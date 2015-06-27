using System;
using System.Linq;
using System.Text;
using Btz.Debugging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Btz.DebugginTest
{
    [TestClass]
    public class ArduinoLogTest
    {
        [TestMethod]
        public void TestParsingTrue()
        {
            const string identitySteering = "STEERING";
            const string identityEngine = "ENGINE";
            Random rnd = new Random();
            int logId;
            var logLevels = new[] {"DEBUG", "WARN", "ERROR", "INFO"};
            var fakeClasses = new[] {"RasenMähen", "Durchrennen", "GOA", "Abgehen", "rausgehen", "VorWegLaufen"};
            //Pattern Log LOG_%IDENTITY% Class[id]{LEVEL} Message
            for (int i = 0; i < 1000000; i++)
            {
                logId = rnd.Next(0, 234);
                string tempIdentity;
                if (rnd.Next(0,1) == 1)
                {
                    tempIdentity = identitySteering;
                }
                else
                {
                    tempIdentity = identityEngine;
                }
                var tempLogLevel = logLevels[rnd.Next(0, 3)];
                var tempFakeClass = fakeClasses[rnd.Next(0, 5)];

                var tempMessage = String.Format("LOG_{0} {1}[{2}]",
                    new object[] { tempIdentity,tempFakeClass,logId });
                tempMessage += "{" + tempLogLevel.ToString() + "}";
                tempMessage += " " + GetRandomString(14);
                Tuple<bool, string> log = LogBtzArduino.Log(Encoding.Default.GetBytes(tempMessage));
                Assert.AreEqual(true,log.Item1);
            }
        }


        string GetRandomString(int size)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789" + "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower();
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, size)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
    }
}
