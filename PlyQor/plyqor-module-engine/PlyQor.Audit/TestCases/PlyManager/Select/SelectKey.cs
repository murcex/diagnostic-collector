using Newtonsoft.Json;
using PlyQor.Audit.Core;
using PlyQor.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlyQor.Audit.TestCases.PlyManager
{
    internal class SelectKey
    {
        public static void Execute()
        {
            Console.WriteLine(" \n\r// SelectKey");
            Dictionary<string, string> request_3 = new Dictionary<string, string>();

            request_3.Add("Token", Configuration.Token);
            request_3.Add("Collection", Configuration.Collection);
            request_3.Add("Operation", "SelectKey");
            request_3.Add("Key", Configuration.Key_1);

            var requestString_3 = JsonConvert.SerializeObject(request_3);

            var result_3 = PlyQorManager.Query(requestString_3);

            Console.WriteLine(result_3);
        }
    }
}
