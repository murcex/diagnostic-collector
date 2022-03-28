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
    internal class UpdateTagByKey
    {
        public static void Execute()
        {
            Console.WriteLine(" \n\r// UpdateTagByKey");
            Dictionary<string, string> request_10 = new Dictionary<string, string>();

            request_10.Add("Token", Configuration.Token);
            request_10.Add("Collection", Configuration.Collection);
            request_10.Add("Operation", "UpdateTagByKey");
            request_10.Add("Key", Configuration.Key_2);
            request_10.Add("Tag", "TestTag");
            request_10.Add("Aux", "TestTagV2");

            var requestString_10 = JsonConvert.SerializeObject(request_10);

            var result_10 = PlyQorManager.Query(requestString_10);

            Console.WriteLine(result_10);
        }
    }
}
