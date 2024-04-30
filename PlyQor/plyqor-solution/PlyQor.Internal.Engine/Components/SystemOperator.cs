using PlyQor.Internal.Engine.Components.SystemQuery;
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
				//"CreateContainer" => SystemQueryProvider.CreateContainer(request),
				"ListContainers" => SystemQueryProvider.ListContainers(request),
				//"DeleteContainer" => SystemQueryProvider.DeleteContainer(request),
				"ListTokens" => SystemQueryProvider.ListTokens(request),
				//"DeleteToken" => SystemQueryProvider.DeleteToken(request),
				//"AddToken" => SystemQueryProvider.AddToken(request),
				//"UpdateRetention" => SystemQueryProvider.UpdateRetention(request),

				_ => new Dictionary<string, string>()
			};
		}
	}
}
