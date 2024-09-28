using PlyQor.Internal.Engine.Components.Query.Container.Delete;
using PlyQor.Internal.Engine.Components.Query.Container.Insert;
using PlyQor.Internal.Engine.Components.Query.Container.Select;
using PlyQor.Internal.Engine.Components.Query.Container.Update;
using PlyQor.Models;
using System.Collections.Generic;

namespace PlyQor.Internal.Engine.Components.Query
{
    public class SystemQueryProvider
	{
		public static Dictionary<string, string> CreateContainer(RequestManager requestManager)
		{
			return CreateContainerQuery.Execute(requestManager);
		}

		public static Dictionary<string, string> ListContainers(RequestManager requestManager)
		{
			return ListContainersQuery.Execute(requestManager);
		}

		public static Dictionary<string, string> DeleteContainer(RequestManager requestManager)
		{
			return DeleteContainerQuery.Execute(requestManager);
		}

		public static Dictionary<string, string> ListTokens(RequestManager requestManager)
		{
			return ListTokensQuery.Execute(requestManager);
		}

		public static Dictionary<string, string> DeleteToken(RequestManager requestManager)
		{
			return DeleteTokenQuery.Execute(requestManager);
		}

		public static Dictionary<string, string> AddToken(RequestManager requestManager)
		{
			return AddTokenQuery.Execute(requestManager);
		}

		public static Dictionary<string, string> UpdateRetention(RequestManager requestManager)
		{
			return UpdateRetentionQuery.Execute(requestManager);
		}
	}
}
