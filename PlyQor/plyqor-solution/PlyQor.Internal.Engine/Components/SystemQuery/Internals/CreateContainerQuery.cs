﻿using PlyQor.Engine.Components.Storage;
using PlyQor.Engine.Resources;
using PlyQor.Models;
using System.Collections.Generic;
using System.Text.Json;

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

			container = container.ToUpper();

			// execute internal query

			// get container config
			var json = StorageProvider.SelectKey(InitializerValues.SystemContainer, InitializerValues.ContainersValue);

			var containers = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json);

			// does container exist
			if (containers.ContainsKey(container))
			{
				throw new System.Exception("container already exist in system");
			}

			// lock system lease
			// TODO: add locking system?

			// get container cfg
			Dictionary<string, string> newContainer = new();

			List<string> tokens = new List<string>
			{
				token
			};

			var toJson = token.ToString();

			newContainer.Add("Tokens", toJson);
			newContainer.Add("Retention", retention);

			containers.Add(container, newContainer);

			// var json = containers

			// build result

			return resultManager.ExportDataSet();
		}
	}
}
