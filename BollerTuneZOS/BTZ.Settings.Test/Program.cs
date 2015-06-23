using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTZ.Test.Infrastructure;
using DataAccess.Repositories;
using Infrastructure.Data.Settings;
using log4net.Core;

namespace BTZ.Settings.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            LogInitializer.Init("Settings",Level.All);
            BootStrapper.Register();
            ITest test = TinyIoC.TinyIoCContainer.Current.Resolve<ITest>();
            test.Start();
            Console.ReadKey();
        }
    }
}
