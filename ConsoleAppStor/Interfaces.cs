using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppStor
{
    interface ISource
    {
        string Get(string key);
    }

    interface ITarget
    {
        void Add(string key, string value);
    }
}
