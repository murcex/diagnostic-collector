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
    internal class SelectTags
    {
        public static void Execute()
        {
            Console.WriteLine(" \n\r// SelectTags");
            Dictionary<string, string> request_4 = new Dictionary<string, string>();

            request_4.Add("Token", Configuration.Token);
            request_4.Add("Collection", Configuration.Collection);
            request_4.Add("Operation", "SelectTags");

            var requestString_4 = JsonConvert.SerializeObject(request_4);

            var result_4 = PlyQorManager.Query(requestString_4);

            Console.WriteLine(result_4);
        }
    }
}
