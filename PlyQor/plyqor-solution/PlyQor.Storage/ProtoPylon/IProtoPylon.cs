using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlyQor.Storage.ProtoPylon
{
    public interface IProtoPylon
    {
        public Dictionary<string, string> GetConfiguration(string group);
    }
}
