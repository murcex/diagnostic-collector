using Javelin.Worker;
using KirokuG2.Internal;
using System;
using System.Collections.Generic;

namespace PlyQor.Injektr.Executors
{
    public class Standard : IExecutor
    {
        public bool Execute(KLog klog)
        {
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
            Configuration.PlyClient.Insert(key_1, Guid.NewGuid().ToString(), tags);

            Configuration.PlyClient.Insert(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "TagOne");

            Configuration.PlyClient.Insert(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "TagOne", "TagTwo");

            Configuration.PlyClient.Insert(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "TagOne", "TagTwo", "TagThree");

            Configuration.PlyClient.InsertTag(key_1, "TestTag");

            Configuration.PlyClient.Insert(key_3, Guid.NewGuid().ToString(), tags_2);

            // select
            Configuration.PlyClient.Select(key_1);

            Configuration.PlyClient.SelectTags();

            Configuration.PlyClient.SelectCount("TagTwo");

            Configuration.PlyClient.Select("TagOne", 3);

            Configuration.PlyClient.SelectTags(key_1);

            // update
            Configuration.PlyClient.Update(key_1, key_2);

            Configuration.PlyClient.UpdateData(key_2, data_2);

            Configuration.PlyClient.UpdateTag(key_2, "TestTag", "TestTagV2");

            Configuration.PlyClient.UpdateTag("UpdateThisTag1", "UpdateThisTag2");

            // delete
            Configuration.PlyClient.DeleteTag(key_2, "TestTagV2");

            Configuration.PlyClient.Delete(key_2);

            Configuration.PlyClient.DeleteTag("DeleteThisTag2");

            Configuration.PlyClient.DeleteTags(key_3);

            return true;
        }
    }
}
