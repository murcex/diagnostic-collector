using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirokuG2.Internal.Loader.Models
{
    public class KLogSet
    {
        private Dictionary<string, (string id, int multilogs, Dictionary<string, List<string>>)> _logSet;

        public KLogSet()
        {
            _logSet = new();
        }
    }
}
