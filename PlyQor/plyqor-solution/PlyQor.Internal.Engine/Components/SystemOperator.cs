using PlyQor.Engine.Components.Query;
using PlyQor.Models;
using System.Collections.Generic;

namespace PlyQor.Internal.Engine.Components
{
	public class SystemOperator
	{
		public static Dictionary<string, string> ExecuteSystemQuery(
			string operation,
			RequestManager request)
		{
			return operation switch
			{

				// CreateContainer
				// ListContainers
				// DeleteContainer

				// ListTokens
				// DeleteToken
				// AddToken

				// UpdateRetention

				// TODO: (switch) move literal string to const
				"InsertKey" => QueryProvider.InsertKey(request),
				"InsertTag" => QueryProvider.InsertTag(request),

				"SelectKey" => QueryProvider.SelectKey(request),
				"SelectTags" => QueryProvider.SelectTags(request),
				"SelectTagCount" => QueryProvider.SelectTagCount(request),
				"SelectKeyList" => QueryProvider.SelectKeyList(request),
				"SelectKeyTags" => QueryProvider.SelectKeyTags(request),

				"UpdateKey" => QueryProvider.UpdateKey(request),
				"UpdateData" => QueryProvider.UpdateData(request),
				"UpdateTag" => QueryProvider.UpdateTag(request),
				"UpdateKeyTag" => QueryProvider.UpdateKeyTag(request),

				"DeleteKey" => QueryProvider.DeleteKey(request),
				"DeleteTag" => QueryProvider.DeleteTag(request),
				"DeleteKeyTags" => QueryProvider.DeleteKeyTags(request),
				"DeleteKeyTag" => QueryProvider.DeleteKeyTag(request),

				_ => new Dictionary<string, string>(),
			};
		}
	}
}
