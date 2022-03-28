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
    internal class DeleteTagByKey
    {
        public static void Execute()
        {
            Console.WriteLine(" \n\r// DeleteTagByKey");
            Dictionary<string, string> request_12 = new Dictionary<string, string>();

            request_12.Add("Token", Configuration.Token);
            request_12.Add("Collection", Configuration.Collection);
            request_12.Add("Operation", "DeleteTagByKey");
            request_12.Add("Key", Configuration.Key_2);
            request_12.Add("Tag", "TestTagV2");

            var requestString_12 = JsonConvert.SerializeObject(request_12);

            var result_12 = PlyQorManager.Query(requestString_12);

            Console.WriteLine(result_12);
        }
    }
}
