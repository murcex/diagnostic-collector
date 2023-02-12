namespace PlyQor.Engine.Components.Maintenance
{
    using System;
    using System.Collections.Generic;
    using PlyQor.Engine.Components.Query;
    using PlyQor.Engine.Core;
    using PlyQor.Engine.Resources;
    using PlyQor.Models;
    using PlyQor.Resources;

    public class TraceRetention
    {
        private static List<string> metric_names = new List<string>()
        {
            "Records",
            "Cycles",
            "Duration"
        };

        public static void Execute(string container)
        {
            // get trace retention policy
            if (Configuration.TraceRetentionPolicy.TryGetValue(container, out var retention_value))
            {
                using (PlyQorTrace trace = new PlyQorTrace(Configuration.DatabaseConnection, Guid.NewGuid().ToString()))
                {
                    trace.AddContainer(container);
                    trace.AddOperation(PlyQorManagerValues.TraceRetentionOperation);

                    try
                    {
                        // compute datetime, rounded up to the day -- send as aux
                        var retention_threshold = DateTime.UtcNow.Subtract(TimeSpan.FromDays(retention_value)).ToString("yyyy-MM-dd HH:mm:ss.fff");

                        Console.WriteLine($"[TRACE] Current: {DateTime.UtcNow} Threshold: {retention_threshold}");

                        // build request dictionary
                        Dictionary<string, string> request = new Dictionary<string, string>
                        {
                            { RequestKeys.Container, container },
                            { RequestKeys.Aux, retention_threshold }
                        };

                        // build request
                        RequestManager requestManager = new RequestManager(request);

                        // execute
                        var retention_result = QueryProvider.TraceRetention(requestManager);

                        // TODO: write back into metrics table
                        foreach (var metric_name in metric_names)
                        {
                            var metric_value = retention_result.GetRequestStringValue(metric_name);

                            Dictionary<string, string> metric_request = new Dictionary<string, string>()
                            {
                                { RequestKeys.Container, container },
                                { "MetricType", "Trace Retention" },
                                { "MetricKey", metric_name },
                                { "MetricData", metric_value }
                            };

                            RequestManager metricRequestManager = new RequestManager(metric_request);

                            QueryProvider.InsertMetric(metricRequestManager);
                        }
                    }
                    catch (PlyQorException javelinException)
                    {
                        trace.AddCode(javelinException.Message);
                    }
                }
            }
            else
            {
                //
            }
        }
    }
}
