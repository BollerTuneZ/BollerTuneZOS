using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTZ.Test.Infrastructure;
using BTZ.Tests.Infrastructure;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace BTZ.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            LogInitializer.Init("BTZ_Tests",Level.All);
            CommunicationTest test = new CommunicationTest();
            test.Start();
            Console.ReadKey();
        }


    }
}
