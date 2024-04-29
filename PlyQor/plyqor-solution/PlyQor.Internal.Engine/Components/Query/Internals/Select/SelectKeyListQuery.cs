namespace PlyQor.Engine.Components.Query.Internals
{
	using PlyQor.Engine.Components.Storage;
	using PlyQor.Models;
	using PlyQor.Resources;
	using System.Collections.Generic;

	class SelectKeyListQuery
	{
		public static Dictionary<string, string> Execute(RequestManager requestManager)
		{
			ResultManager resultManager = new ResultManager();

			// get values from request
			var container = requestManager.GetRequestStringValue(RequestKeys.Container);
			var tag = requestManager.GetRequestStringValue(RequestKeys.Tag);
			var count = requestManager.GetRequestIntValue(RequestKeys.Aux);

			// execute internal query
			var keys = StorageProvider.SelectKeyList(container, tag, count);

			// build result
			resultManager.AddResultData(keys);
			resultManager.AddResultSuccess();

			return resultManager.ExportDataSet();
		}
	}
}
