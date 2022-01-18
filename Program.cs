using Reflection.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var provider = new Provider();

            var configurationManager = new ConfigurationSettings(provider);
            
            var fileSettings = new FileSettings(provider);
        }
    }
}
