namespace PlyQor.Engine.Components.Query.Internals
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using PlyQor.Models;
    using PlyQor.Resources;
    using PlyQor.Engine.Components.Storage;
    using PlyQor.Engine.Core;


    class DataRetentionQuery
    {
        public static async Dictionary<string, string> Execute(RequestManager requestManager)
        {
            ResultManager resultManager = new ResultManager();

            // get values from request
            var container = requestManager.GetRequestStringValue(RequestKeys.Container);
            var s_threshold = requestManager.GetRequestStringValue(RequestKeys.Aux);

            // TODO: create GetRequestDateTimeValue() -- skip the conversion step
            var threshold = DateTime.Parse(s_threshold);

            var capacity = Configuration.RetentionCapacity;
            var cycle = Configuration.RetentionCycle;

            bool active = true;
            int total = 0;

            while (active)
            {
                // execute internal query
                var retentionKeys = StorageProvider.SelectRetentionKeys(container, capacity, threshold);

                if (retentionKeys != null && retentionKeys.Count > 0)
                {
                    foreach (var retentionKey in retentionKeys)
                    {
                        StorageProvider.DeleteKey(container, retentionKey);

                        StorageProvider.DeleteKeyTags(container, retentionKey);

                        total++;
                    }

                    Task.Delay(cycle).GetAwaiter().GetResult();
                }
                else
                {
                    active = false;
                }
            }

            // build result
            resultManager.AddResultData(total);
            resultManager.AddResultSuccess();

            return resultManager.ExportDataSet();
        }
    }
}
