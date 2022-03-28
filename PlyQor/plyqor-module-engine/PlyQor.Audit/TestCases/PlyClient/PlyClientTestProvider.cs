using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlyQor.Audit.Core;


namespace PlyQor.Audit.TestCases.PlyClient
{
    using PlyQor.Client;

    class PlyClientTestProvider
    {
        public static void Execute()
        {
            Console.WriteLine($"Starting Client Audit");

            var key_1 = Guid.NewGuid().ToString();
            var key_2 = Guid.NewGuid().ToString();
            var key_3 = Guid.NewGuid().ToString();

            var data_2 = Guid.NewGuid().ToString();

            PlyClient plyClient = new PlyClient(
                Configuration.ClientUrl, 
                Configuration.ClientContainer,
                Configuration.ClientToken);

            List<string> tags = new List<string>();
            tags.Add("Update");
            tags.Add("V2Operation");

            var output = plyClient.InsertKey(key_1, Guid.NewGuid().ToString(), tags).GetPlyData();
            Console.WriteLine(output);

            output = plyClient.InsertKey(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "TagOne").GetPlyData();
            Console.WriteLine(output);

            output = plyClient.InsertKey(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "TagOne", "TagTwo").GetPlyData();
            Console.WriteLine(output);

            output = plyClient.InsertKey(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "TagOne", "TagTwo", "TagThree").GetPlyData();
            Console.WriteLine(output);

            output = plyClient.InsertTag(key_1, "TestTag").GetPlyData();
            Console.WriteLine(output);

            output = plyClient.SelectKey(key_1).GetPlyData();
            Console.WriteLine(output);

            var output_2 = plyClient.SelectTags().GetPlyData().GetPlyList();
            output_2.ForEach(i => Console.WriteLine(i));

            output = plyClient.SelectTagCount("Update").GetPlyData();
            Console.WriteLine(output);

            output_2 = plyClient.SelectKeyList("Update", 3).GetPlyData().GetPlyList();
            output_2.ForEach(i => Console.WriteLine(i));

            output_2 = plyClient.SelectTagsByKey(key_1).GetPlyData().GetPlyList();
            output_2.ForEach(i => Console.WriteLine(i));

            output = plyClient.UpdateKey(key_1, key_2).GetPlyData();
            Console.WriteLine(output);

            output = plyClient.UpdateData(key_2, data_2).GetPlyData();
            Console.WriteLine(output);

            output = plyClient.UpdateTagByKey(key_2, "TestTag", "TestTagV2").GetPlyData();
            Console.WriteLine(output);

            List<string> tags_2 = new List<string>();
            tags_2.Add("UpdateThisTag1");
            tags_2.Add("DeleteThisTag2");
            tags_2.Add("DeleteThisTag3");

            output = plyClient.InsertKey(key_3, Guid.NewGuid().ToString(), tags_2).GetPlyData();
            Console.WriteLine(output);

            output = plyClient.UpdateTag("UpdateThisTag1", "DeleteThisTag2").GetPlyData();
            Console.WriteLine(output);

            output = plyClient.DeleteTagByKey(key_2, "TestTagV2").GetPlyData();
            Console.WriteLine(output);

            output = plyClient.DeleteKey(key_2).GetPlyData();
            Console.WriteLine(output);

            output = plyClient.DeleteTag("DeleteThisTag2").GetPlyData();
            Console.WriteLine(output);

            output = plyClient.DeleteTagsByKey(key_3).GetPlyData();
            Console.WriteLine(output);
        }
    }
}
