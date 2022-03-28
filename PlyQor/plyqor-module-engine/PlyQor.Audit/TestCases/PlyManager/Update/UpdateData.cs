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
    internal class UpdateData
    {
        public static void Execute()
        {
            Console.WriteLine(" \n\r// UpdateData");
            Dictionary<string, string> request_9 = new Dictionary<string, string>();

            request_9.Add("Token", Configuration.Token);
            request_9.Add("Collection", Configuration.Collection);
            request_9.Add("Operation", "UpdateData");
            request_9.Add("Key", Configuration.Key_2);
            request_9.Add("Data", Configuration.Data_1);
            request_9.Add("Aux", Configuration.Data_2);

            var requestString_9 = JsonConvert.SerializeObject(request_9);

            var result_9 = PlyQorManager.Query(requestString_9);

            Console.WriteLine(result_9);
        }
    }
}
