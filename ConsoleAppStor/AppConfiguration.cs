using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppStor
{
    class ConfigNotFoundException : Exception
    {
        public ConfigNotFoundException(string setting) : base($"Config [{setting}] = NULL")
        {
        }
    }

    class AppConfiguration
    {
        public string Get(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;
            var value = appSettings[key];

            if (value == null || value == "")
                throw new ConfigNotFoundException(key);

            if (value.StartsWith("[[") && value.EndsWith("]]"))
                throw new ConfigNotFoundException(key);

            return value;
        }
    }
}
