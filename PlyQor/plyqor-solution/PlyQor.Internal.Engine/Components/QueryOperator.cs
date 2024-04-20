using PlyQor.Engine.Components.Query;
using PlyQor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlyQor.Internal.Engine.Components
{
	public class QueryOperator
	{
		/// <summary>
		/// Submit request to query engine for execution.
		/// </summary>
		public static Dictionary<string, string> Execute(
			string operation,
			RequestManager request)
		{
			return operation switch
			{
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
