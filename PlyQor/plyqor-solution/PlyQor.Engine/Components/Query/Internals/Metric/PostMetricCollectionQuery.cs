namespace PlyQor.Engine.Components.Query.Internals.Metric
{
    using PlyQor.Engine.Components.Storage;
    using PlyQor.Models;
    using System.Collections.Generic;

    class PostMetricCollectionQuery
    {
        private static List<string> procs = new List<string>()
        {
            "usp_PlyQor_Metric_SelectDataRecords",
            "usp_PlyQor_Metric_SelectTagRecords",
            "usp_PlyQor_Metric_SelectTags"
        };

        public static Dictionary<string, string> Execute(RequestManager requestManager)
        {
            ResultManager resultManager = new ResultManager();

            foreach (var proc in procs)
            {
                // execute internal query
                StorageProvider.MetricCollection(proc);
            }

            // build result
            resultManager.AddResultSuccess();

            return resultManager.ExportDataSet();
        }
    }
}
