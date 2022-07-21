namespace PlyQor.Audit.TestCases.PlyManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using PlyQor.Audit.Core;
    using PlyQor.Engine;
    using PlyQor.Resources;

    internal class DeleteTagsByKey
    {
        public static void Execute()
        {
            Console.WriteLine(" \n\r// DeleteTagsByKey");
            Dictionary<string, string> request = new Dictionary<string, string>();

            var deleteTagKey = Configuration.DeleteTestKeys.FirstOrDefault();

            request.Add(RequestKeys.Token, Configuration.Token);
            request.Add(RequestKeys.Container, Configuration.Container);
            request.Add(RequestKeys.Operation, QueryOperation.DeleteKeyTags);
            request.Add(RequestKeys.Key, deleteTagKey);

            var requestString = JsonConvert.SerializeObject(request);

            var result = PlyQorManager.Query(requestString);

            Console.WriteLine(result);
        }
    }
}
