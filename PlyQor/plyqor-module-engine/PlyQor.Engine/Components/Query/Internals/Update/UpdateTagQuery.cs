namespace PlyQor.Engine.Components.Query.Internals
{
    using System.Collections.Generic;
    using PlyQor.Models;
    using PlyQor.Resources;
    using PlyQor.Engine.Components.Storage;

    class UpdateTagQuery
    {
        public static Dictionary<string, string> Execute(RequestManager requestManager)
        {
            ResultManager resultManager = new ResultManager();

            // get values from request
            var collection = requestManager.GetRequestStringValue(RequestKeys.Collection);
            var oldtag = requestManager.GetRequestStringValue(RequestKeys.Tag);
            var newtag = requestManager.GetRequestStringValue(RequestKeys.Aux);

            // execute internal query
            var count = StorageProvider.UpdateTag(collection, oldtag, newtag);

            // build result
            resultManager.AddResultData(count);
            resultManager.AddResultSuccess();

            return resultManager.ExportDataSet();
        }
    }
}
