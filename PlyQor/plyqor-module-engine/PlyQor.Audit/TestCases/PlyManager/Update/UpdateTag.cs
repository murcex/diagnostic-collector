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
    internal class UpdateTag
    {
        public static void Execute()
        {
            Console.WriteLine(" \n\r// UpdateTag");
            Dictionary<string, string> request_11 = new Dictionary<string, string>();

            request_11.Add("Token", Configuration.Token);
            request_11.Add("Collection", Configuration.Collection);
            request_11.Add("Operation", "UpdateTag");
            request_11.Add("Tag", "DeleteTagsByKeyTest3");
            request_11.Add("Aux", "DeleteTagsByKeyTest3V2");

            var requestString_11 = JsonConvert.SerializeObject(request_11);

            var result_11 = PlyQorManager.Query(requestString_11);

            Console.WriteLine(result_11);
        }
    }
}
