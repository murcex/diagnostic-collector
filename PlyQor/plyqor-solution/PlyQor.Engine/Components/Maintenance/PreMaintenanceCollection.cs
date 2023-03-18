namespace PlyQor.Engine.Components.Maintenance
{
    using PlyQor.Engine.Components.Query;
    using PlyQor.Models;
    using System.Collections.Generic;

    public class PreMaintenanceCollection
    {
        public static void Execute()
        {
            Dictionary<string, string> requests = new Dictionary<string, string>();
            requests.Add("PreMaintenanceCollection", "Execute");

            RequestManager requestManager = new RequestManager(requests);

            QueryProvider.PreMetricCollection(requestManager);
        }
    }
}
