namespace PlyQor.Engine.Components.Query.Internals
{
    using PlyQor.Engine.Components.Storage;
    using PlyQor.Models;
    using PlyQor.Resources;
    using System.Collections.Generic;

    class DeleteTagQuery
    {
        public static Dictionary<string, string> Execute(RequestManager requestManager)
        {
            ResultManager resultManager = new ResultManager();

            // get values from request
            var container = requestManager.GetRequestStringValue(RequestKeys.Container);
            var tag = requestManager.GetRequestStringValue(RequestKeys.Tag);

            // execute internal query
            var count = StorageProvider.DeleteTag(container, tag);

            // build result
            resultManager.AddResultData(count);
            resultManager.AddResultSuccess();

            return resultManager.ExportDataSet();
        }
    }
}
