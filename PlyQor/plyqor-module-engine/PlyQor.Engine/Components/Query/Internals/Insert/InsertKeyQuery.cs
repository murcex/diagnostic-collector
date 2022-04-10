﻿namespace PlyQor.Engine.Components.Query.Internals
{
    using System.Collections.Generic;
    using PlyQor.Models;
    using PlyQor.Resources;
    using PlyQor.Engine.Components.Storage;

    class InsertKeyQuery
    {
        public static Dictionary<string, string> Execute(RequestManager requestManager)
        {
            ResultManager resultManager = new ResultManager();

            // get values from request
            var container = requestManager.GetRequestStringValue(RequestKeys.Container);
            var key = requestManager.GetRequestStringValue(RequestKeys.Key);
            var data = requestManager.GetRequestStringValue(RequestKeys.Data);
            var tags = requestManager.GetRequestTags();

            // execute internal query
            var count = StorageProvider.InsertKey(container, key, data, tags);

            // build result
            resultManager.AddResultData(count);
            resultManager.AddResultSuccess();

            return resultManager.ExportDataSet();
        }
    }
}
