namespace PlyQor.Audit.TestCases.PlyManager
{
    using Newtonsoft.Json;
    using PlyQor.Audit.Core;
    using PlyQor.Engine;
    using PlyQor.Resources;
    using System;
    using System.Collections.Generic;

    internal class InsertTag
    {
        public static void Execute()
        {
            Console.WriteLine(" \n\r// InsertTag");
            Dictionary<string, string> request = new Dictionary<string, string>();

            request.Add(RequestKeys.Token, Configuration.Token);
            request.Add(RequestKeys.Container, Configuration.Container);
            request.Add(RequestKeys.Operation, QueryOperation.InsertTag);
            request.Add(RequestKeys.Key, Configuration.Key_1);
            request.Add(RequestKeys.Tag, PlyManConst.TestTag);

            var requestString = JsonConvert.SerializeObject(request);

            var result = PlyQorManager.Query(requestString);

            Console.WriteLine(result);
        }
    }
}
