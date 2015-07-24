using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonOutputTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var testObject = new Dto {name = "Jonas Ahlf",counter = 1};
            var ob = JsonConvert.SerializeObject(testObject);
            Console.WriteLine(ob);
            var path =
            Path.Combine(Environment.CurrentDirectory, "jsonTest.txt");
            File.WriteAllText(path, ob);
            Process.Start(path);
            Console.ReadKey();
        }
    }


    public class Dto
    {
        public string name { get; set; }

        public int counter { get; set; }
    }
}
