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
    internal class SelectTagCount
    {
        public static void Execute()
        {
            Console.WriteLine(" \n\r// SelectTagCount");
            Dictionary<string, string> request_5 = new Dictionary<string, string>();

            request_5.Add("Token", Configuration.Token);
            request_5.Add("Collection", Configuration.Collection);
            request_5.Add("Operation", "SelectTagCount");
            request_5.Add("Tag", "Upload");

            var requestString_5 = JsonConvert.SerializeObject(request_5);

            var result_5 = PlyQorManager.Query(requestString_5);

            Console.WriteLine(result_5);
        }
    }
}
