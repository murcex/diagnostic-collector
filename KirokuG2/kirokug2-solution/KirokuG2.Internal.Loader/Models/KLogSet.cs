using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirokuG2.Internal.Loader.Models
{
    public class KLogSet
    {
        private Dictionary<string, (string id, int count, Dictionary<string, List<string>> logs)> _logSet;

        public KLogSet()
        {
            _logSet = new();
        }

        public void AddLogSet()
        {

        }
    }
}
