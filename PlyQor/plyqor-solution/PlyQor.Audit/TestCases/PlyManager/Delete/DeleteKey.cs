namespace PlyQor.Audit.TestCases.PlyManager
{
    using Newtonsoft.Json;
    using PlyQor.Audit.Core;
    using PlyQor.Engine;
    using PlyQor.Resources;
    using System;
    using System.Collections.Generic;

    internal class DeleteKey
    {
        public static void Execute()
        {
            Console.WriteLine(" \n\r// DeleteKey");
            Dictionary<string, string> requestDictionary = new Dictionary<string, string>
            {
                { RequestKeys.Token, Configuration.Token },
                { RequestKeys.Container, Configuration.Container },
                { RequestKeys.Operation, QueryOperation.DeleteKey },
                { RequestKeys.Key, Configuration.Key_2 }
            };

            var requestString = JsonConvert.SerializeObject(requestDictionary);

            var result = PlyQorManager.Query(requestString);

            Console.WriteLine(result);
        }
    }
}
