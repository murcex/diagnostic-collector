using Newtonsoft.Json;
using PlyQor.Audit.Core;
using PlyQor.Engine;
using PlyQor.Client.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlyQor.Audit.TestCases.PlyManager
{
    internal class InsertKey
    {
        public static void Execute()
        {
            Console.WriteLine("-----");
            Console.WriteLine(" \n\r// InsertKey");
            Dictionary<string, string> request_1 = new Dictionary<string, string>();

            request_1.Add(RequestKeys.Token, Configuration.Token);
            request_1.Add("Collection", Configuration.Collection);
            request_1.Add("Operation", "InsertKey");
            request_1.Add("Key", Configuration.Key_1);
            request_1.Add("Data", Configuration.Data_1);

            List<string> tags = new List<string>();
            tags.Add("Update");
            tags.Add("V2Operation");

            var tagsString = JsonConvert.SerializeObject(tags);

            request_1.Add("Tags", tagsString);

            var requestString_1 = JsonConvert.SerializeObject(request_1);

            var result = PlyQorManager.Query(requestString_1);

            Console.WriteLine(result);
        }
    }
}
