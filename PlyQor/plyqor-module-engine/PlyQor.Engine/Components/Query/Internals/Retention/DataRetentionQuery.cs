namespace PlyQor.Engine.Components.Query.Internals
{
    using System.Collections.Generic;
    using PlyQor.Models;
    using PlyQor.Resources;
    using PlyQor.Engine.Components.Storage;

    class DataRetentionQuery
    {
        public static Dictionary<string, string> Execute(RequestManager requestManager)
        {
            ResultManager resultManager = new ResultManager();

            // get values from request
            var collection = requestManager.GetRequestStringValue(RequestKeys.Collection);
            var days = requestManager.GetRequestIntValue(RequestKeys.Aux, positive: false);

            // execute internal query
            var retentionKeys = StorageProvider.SelectKeyListRetention(collection, days);

            int count = 0;

            foreach (var retentionKey in retentionKeys)
            {
                StorageProvider.DeleteKey(collection, retentionKey);

                StorageProvider.DeleteTagsByKey(collection, retentionKey);

                count++;
            }

            // build result
            resultManager.AddResultData(count);
            resultManager.AddResultSuccess();

            return resultManager.ExportDataSet();
        }
    }
}
