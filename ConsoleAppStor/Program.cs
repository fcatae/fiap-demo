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

            string redisUrl = appSettings.Get("redis-url");
            string storageUrl = appSettings.Get("storage-url");
            string eventhubUrl = appSettings.Get("eventhub-url");

            Console.WriteLine($"Setting [storageUrl] = '{storageUrl}'");

            //var redis = new RedisComponent();

            //var queue = new AzureQueue(storageUrl, "exemplo");

            //queue.Add(null, "contando...");
            //queue.Add(null, "1");
            //queue.Add(null, "2");
            //queue.Add(null, "3");

            //var msg = queue.Get(null);

            //while(msg != null)
            //{
            //    msg = queue.Get(null);
            //}

            //var table = new AzureTable(storageUrl);

            //table.Add("1", "Titulo 1");
            //var registro = table.Get("1");

            //var blob = new AzureBlob(storageUrl);

            //string localFile = "exemplo.txt";

            //blob.Add("arq.txt", localFile);
            //var text = blob.Get("arq.txt");

        }
    }
}
