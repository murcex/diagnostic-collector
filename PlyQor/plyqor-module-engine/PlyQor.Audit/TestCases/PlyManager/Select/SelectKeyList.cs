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
    internal class SelectKeyList
    {
        public static void Execute()
        {
            Console.WriteLine(" \n\r// SelectKeyList");
            Dictionary<string, string> request_6 = new Dictionary<string, string>();

            request_6.Add("Token", Configuration.Token);
            request_6.Add("Collection", Configuration.Collection);
            request_6.Add("Operation", "SelectKeyList");
            request_6.Add("Tag", "Upload");
            request_6.Add("Aux", "5");

            var requestString_6 = JsonConvert.SerializeObject(request_6);

            var result_6 = PlyQorManager.Query(requestString_6);

            Console.WriteLine(result_6);
        }
    }
}
