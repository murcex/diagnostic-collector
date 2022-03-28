namespace PlyQor.Engine.Components.Query.Internals
{
    using System.Collections.Generic;
    using PlyQor.Models;
    using PlyQor.Resources;
    using PlyQor.Engine.Components.Storage;

    class UpdateTagByKeyQuery
    {
        public static Dictionary<string, string> Execute(RequestManager requestManager)
        {
            ResultManager resultManager = new ResultManager();

            // get values from request
            var collection = requestManager.GetRequestStringValue(RequestKeys.Collection);
            var key = requestManager.GetRequestStringValue(RequestKeys.Key);
            var oldtag = requestManager.GetRequestStringValue(RequestKeys.Tag);
            var newtag = requestManager.GetRequestStringValue(RequestKeys.Aux);

            // execute internal query
            var count = StorageProvider.UpdateTagByKey(collection, key, oldtag, newtag);

            // build result
            resultManager.AddResultData(count);
            resultManager.AddResultSuccess();

            return resultManager.ExportDataSet();
        }
    }
}
