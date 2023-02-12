namespace PlyQor.Audit.TestCases.PlyClient
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using PlyQor.Audit.Core;
    using PlyQor.Client;

    class BaselineTest
    {
        public static void Execute()
        {
            Console.WriteLine($"Starting Client Audit");

            // build client
            PlyClient plyClient = new PlyClient(
                Configuration.ClientUrl,
                Configuration.ClientContainer,
                Configuration.ClientToken);

            Console.WriteLine($"{Configuration.ClientUrl}, {Configuration.ClientContainer}, {Configuration.ClientToken}");

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
            var output = plyClient.Insert(key_1, Guid.NewGuid().ToString(), tags).GetPlyData();
            Console.WriteLine($"InsertKey: {output}");

            // testing duplicate insert -- should fail
            output = plyClient.Insert(key_1, Guid.NewGuid().ToString(), tags).GetPlyRecord();
            Console.WriteLine($"InsertKey: {output}");

            output = plyClient.Insert(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "TagOne").GetPlyData();
            Console.WriteLine($"InsertKey: {output}");

            output = plyClient.Insert(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "TagOne", "TagTwo").GetPlyData();
            Console.WriteLine($"InsertKey: {output}");

            output = plyClient.Insert(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "TagOne", "TagTwo", "TagThree").GetPlyData();
            Console.WriteLine($"InsertKey: {output}");

            output = plyClient.InsertTag(key_1, "TestTag").GetPlyData();
            Console.WriteLine($"InsertTag: {output}");

            output = plyClient.Insert(key_3, Guid.NewGuid().ToString(), tags_2).GetPlyTrace();
            Console.WriteLine($"InsertKey: {output}");

            // select
            output = plyClient.Select(key_1).GetPlyData();
            Console.WriteLine($"SelectKey: {output}");

            var output_2 = plyClient.SelectTags().GetPlyData().GetPlyList();
            output_2.ForEach(i => Console.WriteLine($"SelectTags: {i}"));

            output = plyClient.SelectCount("Update").GetPlyData();
            Console.WriteLine($"SelectTagCount: {output}");

            output_2 = plyClient.Select("TagOne", 3).GetPlyList();
            output_2.ForEach(i => Console.WriteLine($"SelectKeyList: {i}"));

            output_2 = plyClient.SelectTags(key_1).GetPlyData().GetPlyList();
            output_2.ForEach(i => Console.WriteLine($"SelectTagsByKey: {i}"));

            // update
            output = plyClient.Update(key_1, key_2).GetPlyData();
            Console.WriteLine($"UpdateKey: {output}");

            output = plyClient.UpdateData(key_2, data_2).GetPlyData();
            Console.WriteLine($"UpdateData: {output}");

            output = plyClient.UpdateTag(key_2, "TestTag", "TestTagV2").GetPlyData();
            Console.WriteLine($"UpdateTagByKey: {output}");

            output = plyClient.UpdateTag("UpdateThisTag1", "UpdateThisTag2").GetPlyData();
            Console.WriteLine($"UpdateTag: {output}");

            // delete
            output = plyClient.DeleteTag(key_2, "TestTagV2").GetPlyRecord();
            Console.WriteLine($"DeleteTagByKey: {output}");

            output = plyClient.Delete(key_2).GetPlyStatus().ToString();
            Console.WriteLine($"DeleteKey: {output}");

            output = plyClient.DeleteTag("DeleteThisTag2").GetPlyData();
            Console.WriteLine($"DeleteTag: {output}");

            output = plyClient.DeleteTags(key_3).GetPlyCode();
            Console.WriteLine($"DeleteTagsByKey: {output}");
        }
    }
}
