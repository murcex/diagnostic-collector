namespace PlyQor.Audit.TestCases.PlyManager
{
    using PlyQor.Audit.Core;
    using PlyQor.Engine;
    using PlyQor.Resources;
    using System;
    using System.Collections.Generic;
    using System.Text.Json;

    internal class InsertKey
    {
        public static void Execute()
        {
            Console.WriteLine("-----");
            Console.WriteLine(" \n\r// InsertKey");
            Dictionary<string, string> request = new Dictionary<string, string>();

            request.Add(RequestKeys.Token, Configuration.Token);
            request.Add(RequestKeys.Container, Configuration.Container);
            request.Add(RequestKeys.Operation, QueryOperation.InsertKey);
            request.Add(RequestKeys.Key, Configuration.Key_1);
            request.Add(RequestKeys.Data, Configuration.Data_1);

            List<string> tags = new List<string>();
            tags.Add(PlyManConst.Update);
            tags.Add(PlyManConst.V2Operation);

            var tagsString = JsonSerializer.Serialize(tags);

            request.Add(RequestKeys.Tags, tagsString);

            var requestString = JsonSerializer.Serialize(request);

            var result = PlyQorManager.Query(requestString);

            Console.WriteLine(result);
        }
    }
}
