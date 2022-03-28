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
    internal class DeleteTagsByKey
    {
        public static void Execute()
        {
            Console.WriteLine(" \n\r// DeleteTagsByKey");
            Dictionary<string, string> request_15 = new Dictionary<string, string>();

            var deleteTagKey = Configuration.DeleteTestKeys.FirstOrDefault();

            request_15.Add("Token", Configuration.Token);
            request_15.Add("Collection", Configuration.Collection);
            request_15.Add("Operation", "DeleteTagsByKey");
            request_15.Add("Key", deleteTagKey);

            var requestString_15 = JsonConvert.SerializeObject(request_15);

            var result_15 = PlyQorManager.Query(requestString_15);

            Console.WriteLine(result_15);
        }
    }
}
