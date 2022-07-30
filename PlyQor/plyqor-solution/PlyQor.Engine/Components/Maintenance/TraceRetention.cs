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
                        var retention_threshold = DateTime.UtcNow.Subtract(TimeSpan.FromDays(retention_value)).Date.ToShortDateString();

                        // build request dictionary
                        Dictionary<string, string> request = new Dictionary<string, string>
                        {
                            { RequestKeys.Container, container },
                            { RequestKeys.Aux, retention_threshold }
                        };

                        // build request
                        RequestManager requestManager = new RequestManager(request);

                        // execute
                        QueryProvider.TraceRetention(requestManager);
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
