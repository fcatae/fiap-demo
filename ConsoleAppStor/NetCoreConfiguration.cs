using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ConsoleAppStor
{
    class NetCoreConfiguration
    {
        private IConfigurationRoot _config;

        public NetCoreConfiguration()
        {
            var builder = new ConfigurationBuilder()
                                .AddJsonFile("config.json", true)
                                .AddJsonFile("config.secrets.json", true);

            this._config = builder.Build();
        }

        public string Get(string key)
        {
            var value = _config[key];

            if (value == null || value == "")
                throw new ConfigNotFoundException(key);

            if (value.StartsWith("[[") && value.EndsWith("]]"))
                throw new ConfigNotFoundException(key);

            return value;
        }
    }
}
