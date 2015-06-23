using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCommunicationTestCSharp
{
    class Program
    {
        private static ArduinoControllerMain controller;
        static void Main(string[] args)
        {
            controller = new ArduinoControllerMain();
            controller.SetComPort();
            Console.ReadKey();
        }
    }
}
