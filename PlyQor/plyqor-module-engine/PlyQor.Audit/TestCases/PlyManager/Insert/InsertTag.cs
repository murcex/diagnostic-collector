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
    internal class InsertTag
    {
        public static void Execute()
        {
            Console.WriteLine(" \n\r// InsertTag");
            Dictionary<string, string> request_2 = new Dictionary<string, string>();

            request_2.Add("Token", Configuration.Token);
            request_2.Add("Collection", Configuration.Collection);
            request_2.Add("Operation", "InsertTag");
            request_2.Add("Key", Configuration.Key_1);
            request_2.Add("Tag", $"TestTag");

            var requestString_2 = JsonConvert.SerializeObject(request_2);

            var result_2 = PlyQorManager.Query(requestString_2);

            Console.WriteLine(result_2);
        }
    }
}
