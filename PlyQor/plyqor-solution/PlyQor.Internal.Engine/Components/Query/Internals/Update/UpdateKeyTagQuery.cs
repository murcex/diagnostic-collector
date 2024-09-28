namespace PlyQor.Engine.Components.Query.Internals
{
	using PlyQor.Internal.Engine.Components.Storage;
	using PlyQor.Models;
	using PlyQor.Resources;
	using System.Collections.Generic;

	class UpdateKeyTagQuery
	{
		public static Dictionary<string, string> Execute(RequestManager requestManager)
		{
			ResultManager resultManager = new ResultManager();

			// get values from request
			var container = requestManager.GetRequestStringValue(RequestKeys.Container);
			var key = requestManager.GetRequestStringValue(RequestKeys.Key);
			var oldtag = requestManager.GetRequestStringValue(RequestKeys.Tag);
			var newtag = requestManager.GetRequestStringValue(RequestKeys.Aux);

			// execute internal query
			var count = StorageProvider.UpdateKeyTag(container, key, oldtag, newtag);

			// build result
			resultManager.AddResultData(count);
			resultManager.AddResultSuccess();

			return resultManager.ExportDataSet();
		}
	}
}
