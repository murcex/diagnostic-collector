namespace PlyQor.Engine.Components.Maintenance
{
    using PlyQor.Engine.Components.Query;
    using PlyQor.Models;
    using System.Collections.Generic;

    public class PostMaintenanceCollection
    {
        public static void Execute()
        {
            Dictionary<string, string> requests = new Dictionary<string, string>();
            requests.Add("PlaceHolder", "X");

            RequestManager requestManager = new RequestManager(requests);

            QueryProvider.PostMetricCollection(requestManager);
        }
    }
}
