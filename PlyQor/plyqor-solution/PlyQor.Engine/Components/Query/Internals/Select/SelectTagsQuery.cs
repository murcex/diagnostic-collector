namespace PlyQor.Engine.Components.Query.Internals
{
    using System.Collections.Generic;
    using PlyQor.Models;
    using PlyQor.Resources;
    using PlyQor.Engine.Components.Storage;

    class SelectTagsQuery
    {
        public static Dictionary<string, string> Execute(RequestManager requestManager)
        {
            ResultManager resultManager = new ResultManager();

            // get values from request
            var container = requestManager.GetRequestStringValue(RequestKeys.Container);

            // execute internal query
            var tags = StorageProvider.SelectTags(container);

            // build result
            resultManager.AddResultData(tags);
            resultManager.AddResultSuccess();

            return resultManager.ExportDataSet();
        }
    }
}
