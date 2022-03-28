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
    internal class UpdateKey
    {
        public static void Execute()
        {
            Console.WriteLine(" \n\r// UpdateKey");
            Dictionary<string, string> request_8 = new Dictionary<string, string>();

            request_8.Add("Token", Configuration.Token);
            request_8.Add("Collection", Configuration.Collection);
            request_8.Add("Operation", "UpdateKey");
            request_8.Add("Key", Configuration.Key_1);
            request_8.Add("Aux", Configuration.Key_2);

            var requestString_8 = JsonConvert.SerializeObject(request_8);

            var result_8 = PlyQorManager.Query(requestString_8);

            Console.WriteLine(result_8);
        }
    }
}
