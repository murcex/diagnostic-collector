namespace PlyQor.Engine.Components.Query.Internals
{
	using PlyQor.Internal.Engine.Components.Storage;
	using PlyQor.Models;
	using PlyQor.Resources;
	using System.Collections.Generic;

	class DeleteKeyTagQuery
	{
		public static Dictionary<string, string> Execute(RequestManager requestManager)
		{
			ResultManager resultManager = new ResultManager();

			// get values from request
			var container = requestManager.GetRequestStringValue(RequestKeys.Container);
			var key = requestManager.GetRequestStringValue(RequestKeys.Key);
			var tag = requestManager.GetRequestStringValue(RequestKeys.Tag);

			// execute internal query
			var count = StorageProvider.DeleteKeyTag(container, key, tag);

			// build result
			resultManager.AddResultData(count);
			resultManager.AddResultSuccess();

			return resultManager.ExportDataSet();
		}
	}
}
