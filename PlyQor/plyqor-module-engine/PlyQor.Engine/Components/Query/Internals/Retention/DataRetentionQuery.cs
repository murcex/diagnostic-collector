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
            var container = requestManager.GetRequestStringValue(RequestKeys.Container);
            var days = requestManager.GetRequestIntValue(RequestKeys.Aux, positive: false);

            // execute internal query
            var retentionKeys = StorageProvider.SelectRetentionKeys(container, days);

            int count = 0;

            foreach (var retentionKey in retentionKeys)
            {
                StorageProvider.DeleteKey(container, retentionKey);

                StorageProvider.DeleteKeyTags(container, retentionKey);

                count++;
            }

            // build result
            resultManager.AddResultData(count);
            resultManager.AddResultSuccess();

            return resultManager.ExportDataSet();
        }
    }
}
