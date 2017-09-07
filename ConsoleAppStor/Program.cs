using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConsoleAppStor
{
    class Program
    {
        static void Main(string[] args)
        {
            var appSettings = ConfigurationManager.AppSettings;
            
            foreach (var key in appSettings.AllKeys)
            {
                string value = appSettings[key];

                Console.WriteLine($"Setting [{key}] = '{value}'");
            }
        }
    }
}
