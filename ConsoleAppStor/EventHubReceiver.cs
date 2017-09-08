using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;

namespace ConsoleAppStor
{
    class EventHubReceiver : ISource, IPartitionReceiveHandler
    {
        private EventHubClient _client;
        Queue<EventData> _pendingMessages = new Queue<EventData>();

        public EventHubReceiver(string connectionString, string partitionKey)
        {
            EventHubClient client = EventHubClient.CreateFromConnectionString(connectionString);

            InitPartitions(client);

            this._client = client;
        }

        void InitPartitions(EventHubClient client)
        {
            string[] partitionList = client.GetRuntimeInformationAsync().Result.PartitionIds;

            foreach (string partitionId in partitionList)
            {
                var receiver = client.CreateReceiver("teste", partitionId, PartitionReceiver.EndOfStream);
                receiver.SetReceiveHandler(this);
            }
        }

        public int MaxBatchSize => 100;

        public string Get(string key)
        {
            if (_pendingMessages.Count == 0)
                return null;

            var message = _pendingMessages.Dequeue();

            return Encoding.UTF8.GetString(message.Body.Array, message.Body.Offset, message.Body.Count);
        }

        public Task ProcessErrorAsync(Exception error)
        {
            return Task.FromResult(true);
        }

        public Task ProcessEventsAsync(IEnumerable<EventData> events)
        {
            if (events != null)
            {
                foreach (var data in events)
                {
                    _pendingMessages.Enqueue(data);
                }
            }
            return Task.FromResult(true);
        }
    }
}
