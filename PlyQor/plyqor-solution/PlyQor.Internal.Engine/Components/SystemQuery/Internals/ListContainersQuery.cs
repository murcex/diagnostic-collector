using PlyQor.Engine.Components.Storage;
using PlyQor.Engine.Resources;
using PlyQor.Models;
using System.Collections.Generic;
using System.Text.Json;

namespace PlyQor.Internal.Engine.Components.SystemQuery.Internals
{
	public class ListContainersQuery
	{
		public static Dictionary<string, string> Execute(RequestManager requestManager)
		{
			ResultManager resultManager = new ResultManager();

			// get values from request
			var container = requestManager.GetRequestStringValue("");
			var token = requestManager.GetRequestStringValue("");
			var retention = requestManager.GetRequestStringValue("");

			container = container.ToUpper();

			// execute internal query

			// get container config
			var json = StorageProvider.SelectKey(InitializerValues.SystemContainer, InitializerValues.ContainersValue);

			var containers = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json);

			//var data = container.

			// return data

			return resultManager.ExportDataSet();
		}
	}
}
