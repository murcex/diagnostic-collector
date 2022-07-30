namespace PlyQor.Engine.Components.Query.Internals
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using PlyQor.Models;
    using PlyQor.Resources;
    using PlyQor.Engine.Core;
    using PlyQor.Engine.Components.Storage;

    class TraceRetentionQuery
    {
        public static Dictionary<string, string> Execute(RequestManager requestManager)
        {
            ResultManager resultManager = new ResultManager();

            // get values from request
            var container = requestManager.GetRequestStringValue(RequestKeys.Container);
            var s_threshold = requestManager.GetRequestStringValue(RequestKeys.Aux);

            var threshold = DateTime.Parse(s_threshold);

            var capacity = Configuration.RetentionCapacity;
            var cycle = Configuration.RetentionCycle;

            var active = true;
            var total = 0;

            while (active)
            {
                // execute internal query
                var count = StorageProvider.TraceRetention(container, capacity, threshold);

                if (count == 0)
                {
                    active = false;
                }
                else
                {
                    total += count;

                    Task.Delay(cycle).GetAwaiter().GetResult();
                }
            }

            // build result
            resultManager.AddResultData(total);
            resultManager.AddResultSuccess();

            return resultManager.ExportDataSet();
        }
    }
}
