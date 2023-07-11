namespace PlyQor.Audit.TestCases.PlyManager
{
    using PlyQor.Audit.Core;
    using PlyQor.Engine;
    using PlyQor.Resources;
    using System;
    using System.Collections.Generic;
    using System.Text.Json;

    internal class UpdateTag
    {
        public static void Execute()
        {
            Console.WriteLine(" \n\r// UpdateTag");
            Dictionary<string, string> request = new Dictionary<string, string>();

            request.Add(RequestKeys.Token, Configuration.Token);
            request.Add(RequestKeys.Container, Configuration.Container);
            request.Add(RequestKeys.Operation, QueryOperation.UpdateTag);
            request.Add(RequestKeys.Tag, "DeleteTagsByKeyTest3");
            request.Add(RequestKeys.Aux, "DeleteTagsByKeyTest3V2");

            var requestString = JsonSerializer.Serialize(request);

            var result = PlyQorManager.Query(requestString);

            Console.WriteLine(result);
        }
    }
}
