namespace PlyQor.Engine.Components.Query.Internals.Metric
{
    using PlyQor.Engine.Components.Storage;
    using PlyQor.Models;
    using PlyQor.Resources;
    using System.Collections.Generic;

    class InsertMetricQuery
    {
        public static Dictionary<string, string> Execute(RequestManager requestManager)
        {
            ResultManager resultManager = new ResultManager();

            // get values from request
            var container = requestManager.GetRequestStringValue(RequestKeys.Container);
            var type = requestManager.GetRequestStringValue("MetricType");
            var metric = requestManager.GetRequestStringValue("MetricKey");
            var data = requestManager.GetRequestIntValue("MetricData");

            // execute internal query
            var count = StorageProvider.InsertMetric(container, type, metric, data);

            // build result
            resultManager.AddResultData(count);
            resultManager.AddResultSuccess();

            return resultManager.ExportDataSet();
        }
    }
}
