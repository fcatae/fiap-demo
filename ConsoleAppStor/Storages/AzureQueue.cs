using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace ConsoleAppStor
{
    class AzureQueue : ITarget, ISource
    {
        private CloudQueue _queue;

        public AzureQueue(string connectionString, string queuename)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            var client = storageAccount.CreateCloudQueueClient();

            var queue = client.GetQueueReference(queuename);

            queue.CreateIfNotExists();

            this._queue = queue;
        }

        public void Add(string key, string value)
        {
            var message = new CloudQueueMessage(value);
            _queue.AddMessage(message);
        }

        public string Get(string key)
        {
            var message = _queue.GetMessage();

            return message?.AsString;
        }
    }
}
