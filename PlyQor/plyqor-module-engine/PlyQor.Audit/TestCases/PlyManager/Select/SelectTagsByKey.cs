using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PlyQor.Audit.Core;
using PlyQor.Engine;

namespace PlyQor.Audit.TestCases.PlyManager
{
    internal class SelectTagsByKey
    {
        public static void Execute()
        {
            Console.WriteLine(" \n\r// SelectTagsByKey");
            Dictionary<string, string> request_7 = new Dictionary<string, string>();

            request_7.Add("Token", Configuration.Token);
            request_7.Add("Collection", Configuration.Collection);
            request_7.Add("Operation", "SelectTagsByKey");
            request_7.Add("Key", Configuration.Key_1);

            var requestString_7 = JsonConvert.SerializeObject(request_7);

            var result_7 = PlyQorManager.Query(requestString_7);

            Console.WriteLine(result_7);
        }
    }
}
