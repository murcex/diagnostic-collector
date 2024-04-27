using PlyQor.Models;
using System.Collections.Generic;

namespace PlyQor.Internal.Engine.Components.SystemQuery.Internals
{
	public class CreateContainerQuery
	{
		public static Dictionary<string, string> Execute(RequestManager requestManager)
		{
			ResultManager resultManager = new ResultManager();

			// get values from request
			var container = requestManager.GetRequestStringValue("");
			var token = requestManager.GetRequestStringValue("");
			var retention = requestManager.GetRequestStringValue("");

			// execute internal query

			// does container exist

			// lock system lease

			// get container cfg

				// if yes, return already exist

				// if no, create 

			// build result

			return resultManager.ExportDataSet();
		}
	}
}
