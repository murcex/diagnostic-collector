﻿namespace PlyQor.Engine.Components.Query.Internals
{
    using PlyQor.Engine.Components.Storage;
    using PlyQor.Models;
    using PlyQor.Resources;
    using System.Collections.Generic;

    class UpdateKeyQuery
    {
        public static Dictionary<string, string> Execute(RequestManager requestManager)
        {
            ResultManager resultManager = new ResultManager();

            // get values from request
            var container = requestManager.GetRequestStringValue(RequestKeys.Container);
            var oldkey = requestManager.GetRequestStringValue(RequestKeys.Key);
            var newkey = requestManager.GetRequestStringValue(RequestKeys.Aux);

            // execute internal query
            var count = StorageProvider.UpdateKey(container, oldkey, newkey);

            // build result
            resultManager.AddResultData(count);
            resultManager.AddResultSuccess();

            return resultManager.ExportDataSet();
        }
    }
}
