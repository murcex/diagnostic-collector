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
    internal class DeleteTag
    {
        public static void Execute()
        {
            Console.WriteLine(" \n\r// DeleteTag");
            Dictionary<string, string> request_14 = new Dictionary<string, string>();

            request_14.Add("Token", Configuration.Token);
            request_14.Add("Collection", Configuration.Collection);
            request_14.Add("Operation", "DeleteTag");
            request_14.Add("Tag", "DeleteTagsByKeyTest3V2");

            var requestString_14 = JsonConvert.SerializeObject(request_14);

            var result_14 = PlyQorManager.Query(requestString_14);

            Console.WriteLine(result_14);
        }
    }
}
