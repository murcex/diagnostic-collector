namespace PlyQor.Engine.Components.Query.Internals
{
	using PlyQor.Engine.Components.Storage;
	using PlyQor.Models;
	using PlyQor.Resources;
	using System.Collections.Generic;

	class TraceRetentionQuery
	{
		public static Dictionary<string, string> Execute(RequestManager requestManager)
		{
			ResultManager resultManager = new ResultManager();

			// get values from request
			var days = requestManager.GetRequestIntValue(RequestKeys.Aux, positive: false);

			// execute internal query
			var count = StorageProvider.TraceRetention(days);

			// build result
			resultManager.AddResultData(count);
			resultManager.AddResultSuccess();

			return resultManager.ExportDataSet();
		}
	}
}
