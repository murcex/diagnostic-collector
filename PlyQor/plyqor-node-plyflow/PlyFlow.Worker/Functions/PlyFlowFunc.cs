namespace Javelin.Worker
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using PlyQor.Client;

    public class PlyFlowFunc
    {
        [FunctionName("PlyFlow")]
        public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            Initializer.Execute();

            // build client
            PlyClient plyClient = new PlyClient(
                Configuration.Url,
                Configuration.Container,
                Configuration.Token);

            // setup
            var key_1 = Guid.NewGuid().ToString();
            var key_2 = Guid.NewGuid().ToString();
            var key_3 = Guid.NewGuid().ToString();

            var data_2 = Guid.NewGuid().ToString();

            List<string> tags = new List<string>();
            tags.Add("Update");
            tags.Add("V2Operation");

            List<string> tags_2 = new List<string>();
            tags_2.Add("UpdateThisTag1");
            tags_2.Add("DeleteThisTag2");
            tags_2.Add("DeleteThisTag3");

            // insert
            plyClient.Insert(key_1, Guid.NewGuid().ToString(), tags);

            plyClient.InsertTag(key_1, "TestTag");

            plyClient.Insert(key_3, Guid.NewGuid().ToString(), tags_2);

            // select
            plyClient.Select(key_1);

            plyClient.SelectTags();

            plyClient.SelectCount("Upload");

            plyClient.Select("Upload", 3);

            plyClient.SelectTags(key_1);

            // update
            plyClient.Update(key_1, key_2);

            plyClient.UpdateData(key_2, data_2);

            plyClient.UpdateTag(key_2, "TestTag", "TestTagV2");

            plyClient.UpdateTag("UpdateThisTag1", "DeleteThisTag2");

            // delete
            plyClient.DeleteTag(key_2, "TestTagV2");

            plyClient.Delete(key_2);

            plyClient.DeleteTag("DeleteThisTag2");

            plyClient.DeleteTags(key_3);
        }
    }
}
