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
    internal class DeleteKey
    {
        public static void Execute()
        {
            Console.WriteLine(" \n\r// DeleteKey");
            Dictionary<string, string> request_13 = new Dictionary<string, string>();

            request_13.Add("Token", Configuration.Token);
            request_13.Add("Collection", Configuration.Collection);
            request_13.Add("Operation", "DeleteKey");
            request_13.Add("Key", Configuration.Key_2);

            var requestString_13 = JsonConvert.SerializeObject(request_13);

            var result_13 = PlyQorManager.Query(requestString_13);

            Console.WriteLine(result_13);
        }
    }
}
