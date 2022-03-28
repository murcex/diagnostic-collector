namespace PlyQor.Engine.Components.Query.Internals
{
    using System.Collections.Generic;
    using PlyQor.Models;
    using PlyQor.Resources;
    using PlyQor.Engine.Components.Storage;

    class SelectTagsByKeyQuery
    {
        public static Dictionary<string, string> Execute(RequestManager requestManager)
        {
            ResultManager resultManager = new ResultManager();

            // get values from request
            var collection = requestManager.GetRequestStringValue(RequestKeys.Collection);
            var key = requestManager.GetRequestStringValue(RequestKeys.Key);

            // execute internal query
            var tags = StorageProvider.SelectTagsByKey(collection, key);

            // build result
            resultManager.AddResultData(tags);
            resultManager.AddResultSuccess();

            return resultManager.ExportDataSet();
        }
    }
}
