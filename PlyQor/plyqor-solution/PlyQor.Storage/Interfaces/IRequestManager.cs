using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlyQor.Storage.Interfaces
{
    public interface IRequestManager
    {
        public Dictionary<string, string> GetDictionaryFromString(string input);

        //public Dictionary<string, string> GetDictionaryFromDictionary(Dictionary<string, string> input, string key);

        public Dictionary<string, Dictionary<string, string>> GetDictionariesFromDictionary(Dictionary<string, string> input, string key);
    }
}
