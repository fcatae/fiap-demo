﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;

namespace ConsoleAppStor
{
    class EventHubReceiver : ISource, IPartitionReceiveHandler
    {
        private EventHubClient _client;
        ConcurrentQueue<EventData> _pendingMessages = new ConcurrentQueue<EventData>();

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
                var receiver = client.CreateReceiver("$default", partitionId, PartitionReceiver.EndOfStream);
                receiver.SetReceiveHandler(this);
            }
        }

        public int MaxBatchSize => 100;

        public string Get(string key)
        {
            string result = null;

            if(_pendingMessages.TryDequeue(out EventData message))
            {
                result = Encoding.UTF8.GetString(message.Body.Array, message.Body.Offset, message.Body.Count);
            }

            return result;
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
