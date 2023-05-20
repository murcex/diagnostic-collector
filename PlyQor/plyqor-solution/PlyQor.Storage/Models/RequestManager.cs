using Newtonsoft.Json;
using PlyQor.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlyQor.Storage.Models
{
    public class RequestManager : IRequestManager
    {
        public Dictionary<string, string> GetDictionaryFromString(string input)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(input);

            throw new NotImplementedException();
        }

        //public Dictionary<string, string> GetDictionaryFromDictionary(Dictionary<string, string> input, string key)
        //{
        //    if (input.TryGetValue(key, out string value))
        //    {
        //        if (!string.IsNullOrEmpty(value))
        //        {
        //            var output = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);

        //            if (output != null)
        //            {
        //                return output;
        //            }
        //            else
        //            {
        //                throw new Exception($"");
        //            }
        //        }
        //        else
        //        {
        //            throw new Exception($"");
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception();
        //    }

        //    throw new Exception($"key {key} not found in dictionary");
        //}

        public Dictionary<string, Dictionary<string, string>> GetDictionariesFromDictionary(Dictionary<string, string> input, string key)
        {
            input.TryGetValue(key, out var value);
            
            return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(value);
        }
    }
}
