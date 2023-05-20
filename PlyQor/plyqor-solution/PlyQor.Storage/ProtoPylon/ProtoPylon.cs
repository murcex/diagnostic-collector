using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlyQor.Storage.ProtoPylon
{
    public class ProtoPylon : IProtoPylon
    {
        private readonly Dictionary<string, Dictionary<string, string>> _configuration; 

        public ProtoPylon() 
        {
            _configuration = new();

            _configuration["test"] = new Dictionary<string, string>() { { "a", "123" } };
            _configuration["storage"] = new Dictionary<string, string>() { { "user1234", "password1234" } };
        }

        public Dictionary<string, string> GetConfiguration(string group)
        {
            return _configuration[group];
        }
    }
}
