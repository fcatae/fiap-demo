using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;

namespace ConsoleAppStor
{
    class EventHubSender : ITarget
    {
        private EventHubClient _client;
        private PartitionReceiver _receiver;

        public EventHubSender(string connectionString)
        {
            EventHubClient client = EventHubClient.CreateFromConnectionString(connectionString);

            this._client = client;
        }

        public void Add(string key, string value)
        {
            var data = new EventData(Encoding.UTF8.GetBytes(value));

            var task = _client.SendAsync(data, key);

            task.Wait();
        }
        
    }
}
