namespace PlyQor.Engine.Components.Query.Internals
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using PlyQor.Models;
    using PlyQor.Resources;
    using PlyQor.Engine.Core;
    using PlyQor.Engine.Components.Storage;
    using System.Diagnostics;

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
            int record_total = 0;
            int cycle_total = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (active)
            {
                cycle_total++;

                // execute internal query
                var count = StorageProvider.TraceRetention(container, capacity, threshold);

                if (count == 0)
                {
                    active = false;
                }
                else
                {
                    record_total += count;

                    Task.Delay(cycle).GetAwaiter().GetResult();
                }
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
