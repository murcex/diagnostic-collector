namespace PlyQor.Engine.Components.Query.Internals
{
    using System.Collections.Generic;
    using PlyQor.Models;
    using PlyQor.Resources;
    using PlyQor.Engine.Components.Storage;

    class SelectTagCountQuery
    {
        public static Dictionary<string, string> Execute(RequestManager requestManager)
        {
            ResultManager resultManager = new ResultManager();

            // get values from request
            var collection = requestManager.GetRequestStringValue(RequestKeys.Collection);
            var tag = requestManager.GetRequestStringValue(RequestKeys.Tag);

            // execute internal query
            var tagCount = StorageProvider.SelectTagCount(collection, tag);

            // build result
            resultManager.AddResultData(tagCount);
            resultManager.AddResultSuccess();

            return resultManager.ExportDataSet();
        }
    }
}
