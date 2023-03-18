namespace PlyQor.Engine.Components.Query.Internals
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using PlyQor.Models;
    using PlyQor.Resources;
    using PlyQor.Engine.Components.Storage;
    using PlyQor.Engine.Core;
    using System.Diagnostics;

    class DataRetentionQuery
    {
        public static Dictionary<string, string> Execute(RequestManager requestManager)
        {
            ResultManager resultManager = new ResultManager();

            // get values from request
            var container = requestManager.GetRequestStringValue(RequestKeys.Container);
            var s_threshold = requestManager.GetRequestStringValue(RequestKeys.Aux);

            // TODO: create GetRequestDateTimeValue() -- skip the conversion step
            var threshold = DateTime.Parse(s_threshold);

            var capacity_value = 250; //Configuration.RetentionCapacity;
            var cycle_value = 1; //Configuration.RetentionCycle;

            bool active = true;
            int record_total = 0;
            int cycle_total = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine($"Starting Data Retention Cycle: {container}");

            while (active)
            {
                cycle_total++;

                // execute internal query
                var retentionKeys = StorageProvider.SelectRetentionKeys(container, capacity_value, threshold);

                if (retentionKeys != null && retentionKeys.Count > 0)
                {
                    foreach (var retentionKey in retentionKeys)
                    {
                        StorageProvider.DeleteKey(container, retentionKey);

                        StorageProvider.DeleteKeyTags(container, retentionKey);

                        record_total++;
                    }

                    Task.Delay(cycle_value).GetAwaiter().GetResult();
                }
                else
                {
                    active = false;
                }

                Console.WriteLine($"Data Retention Cycle Complete, {record_total} Records @ Cycle {cycle_total}");
            }

            stopwatch.Stop();
            var duration = stopwatch.ElapsedMilliseconds.ToString();

            // build result
            resultManager.AddCustomResultData("Records", record_total.ToString());
            resultManager.AddCustomResultData("Cycles", cycle_total.ToString());
            resultManager.AddCustomResultData("Duration", duration);
            resultManager.AddResultSuccess();

            return resultManager.ExportDataSet();
        }
    }
}
