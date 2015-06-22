using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.JoystickApi.JoyStickEventArgs;
using JoystickApi;

namespace ControllerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Teste JoyStick");
            JoyStickHandler handler = new JoyStickHandler();
            handler.OnButtonTriggered += HandlerOnOnButtonTriggered;
            handler.Initialize();
            handler.Run();
            Console.ReadKey();
        }

        private static void HandlerOnOnButtonTriggered(object sender, EventArgs eventArgs)
        {
            JoyStickEventArgs args = (JoyStickEventArgs) eventArgs;

            Console.WriteLine(String.Format("ID({0}) Type {1}, Value {2}, Triggered {3}",new object[]{args.Key,args.Button,args.Value,args.Triggered}));

        }
    }
}
