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
            var appSettings = new NetCoreConfiguration();

            string storageUrl = appSettings.Get("storage-url");
            string storageSecrets = appSettings.Get("storage-secrets");

            Console.WriteLine($"Setting [storageUrl] = '{storageUrl}'");
            Console.WriteLine($"Setting [storageSecrets] = '{storageSecrets}'");
        }
    }
}
