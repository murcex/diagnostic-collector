namespace PlyQor.Engine.Components.Query.Internals.Metric
{
    using System.Collections.Generic;
    using PlyQor.Models;
    using PlyQor.Engine.Components.Storage;
    using System;

    class PreMetricCollectionQuery
    {
        private static List<string> procs = new List<string>()
        {
            "usp_PlyQor_Metric_SelectOperations",
            "usp_PlyQor_Metric_SelectLatency",
            "usp_PlyQor_Metric_SelectTransactions",
            "usp_PlyQor_Metric_SelectAvailability",
        };

        public static Dictionary<string, string> Execute(RequestManager requestManager)
        {
            ResultManager resultManager = new ResultManager();

            foreach (var proc in procs)
            {
                try
                {
                    // execute internal query
                    StorageProvider.MetricCollection(proc);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"PreMetricCollectionQuery {proc} - Exception: {ex}");
                }
            }

            // build result
            resultManager.AddResultSuccess();

            return resultManager.ExportDataSet();
        }
    }
}
