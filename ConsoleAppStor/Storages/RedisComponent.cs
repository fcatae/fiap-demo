using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace ConsoleAppStor
{
    class RedisComponent : ISource, ITarget
    {
        private string _connectionString;

        public RedisComponent(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public void Add(string key, string value)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_connectionString);
            IDatabase db = redis.GetDatabase();

            db.StringSet(key, value);
        }

        public string Get(string key)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_connectionString);
            IDatabase db = redis.GetDatabase();

            return db.StringGet(key);
        }
    }
}
