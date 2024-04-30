using PlyQor.Engine.Components.Storage;
using PlyQor.Engine.Resources;
using PlyQor.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace PlyQor.Internal.Engine.Components.SystemQuery.Internals
{
	public class ListTokensQuery
	{
		public static Dictionary<string, string> Execute(RequestManager requestManager)
		{
			ResultManager resultManager = new ResultManager();

			try
			{
				// -- get values from request --
				var container = requestManager.GetRequestStringValue("Container");

				// -- execute internal query --
				// get container config
				var json = StorageProvider.SelectKey(InitializerValues.SystemContainer, InitializerValues.ContainersValue);

				var containerConfigurations = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json);

				if (containerConfigurations.TryGetValue(container.ToUpper(), out var containerConfiguration))
				{
					if (containerConfiguration == null)
					{

					}
					else
					{
						if (containerConfiguration.TryGetValue("Token", out var tokens))
						{
							//resultManager.AddResultData(tokens);
							resultManager.AddResultData("Tokens");
							resultManager.AddResultSuccess();
						}
						else
						{
							resultManager.AddResultData("NoTokensForContainer");
							resultManager.AddResultSuccess();
						}
					}
				}
				else
				{
					resultManager.AddResultData("NoContainer");
					resultManager.AddResultSuccess();
				}

				// -- build result --
				resultManager.AddResultData("Test");
				resultManager.AddResultSuccess();
			}
			catch (Exception ex)
			{
				resultManager.AddResultData(ex.Message);
				resultManager.AddResultSuccess();
			}

			return resultManager.ExportDataSet();
		}
	}
}
