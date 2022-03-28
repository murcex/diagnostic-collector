using Newtonsoft.Json;
using PlyQor.Audit.Core;
using PlyQor.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlyQor.Audit.TestCases.PlyManager
{
    internal class CreateTestKeysWithTag
    {
        public static List<string> Execute(string token, int count, string inputTag)
        {
            List<string> keys = new List<string>();
            List<string> tags = new List<string>();

            var inputTags = inputTag.Split(",");
            foreach (var item in inputTags)
            {
                tags.Add(item);
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(" \n\r  -- InsertKey (CreateTestKeysWithTag)");
                Dictionary<string, string> request_1 = new Dictionary<string, string>();
                var key_1 = Guid.NewGuid().ToString();
                var data_1 = Guid.NewGuid().ToString();

                keys.Add(key_1);

                request_1.Add("Token", Configuration.Token);
                request_1.Add("Collection", Configuration.Collection);
                request_1.Add("Operation", "InsertKey");
                request_1.Add("Key", key_1);
                request_1.Add("Data", data_1);

                var tagsString = JsonConvert.SerializeObject(tags);

                request_1.Add("Tags", tagsString);

                var requestString_1 = JsonConvert.SerializeObject(request_1);

                var result = PlyQorManager.Query(requestString_1);

                Console.WriteLine(result);
            }

            return keys;
        }
    }
}
