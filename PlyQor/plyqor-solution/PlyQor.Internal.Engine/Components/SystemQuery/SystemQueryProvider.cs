using PlyQor.Internal.Engine.Components.SystemQuery.Internals;
using PlyQor.Models;
using System.Collections.Generic;

namespace PlyQor.Internal.Engine.Components.SystemQuery
{
	public class SystemQueryProvider
	{
		public static Dictionary<string ,string> CreateContainer(RequestManager requestManager)
		{
			return CreateContainerQuery.Execute(requestManager);
		}
	}
}
