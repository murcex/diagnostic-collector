using PlyQor.Audit.Core;
using PlyQor.Client.Resources;
using PlyQor.Engine;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace PlyQor.Audit.TestCases.PlyManager
{
	internal class CreateTestKeysWithTag
	{
		public static List<string> Execute()
		{
			List<string> keys = new List<string>();
			List<string> tags = new List<string>();

			var inputTag = "DeletaTagsByKeyTest1,DeleteTagsByKeyTest2,DeleteTagsByKeyTest3";
			var count = 3;

			var inputTags = inputTag.Split(",");
			foreach (var item in inputTags)
			{
				tags.Add(item);
			}

			for (int i = 0; i < count; i++)
			{
				Console.WriteLine(" \n\r  -- InsertKey (CreateTestKeysWithTag)");
				Dictionary<string, string> request = new Dictionary<string, string>();
				var key_1 = Guid.NewGuid().ToString();
				var data_1 = Guid.NewGuid().ToString();

				keys.Add(key_1);

				request.Add(RequestKeys.Token, Configuration.Token);
				request.Add(RequestKeys.Container, Configuration.Container);
				request.Add(RequestKeys.Operation, QueryOperation.InsertKey);
				request.Add(RequestKeys.Key, key_1);
				request.Add(RequestKeys.Data, data_1);

				var tagsString = JsonSerializer.Serialize(tags);

				request.Add(RequestKeys.Tags, tagsString);

				var requestString = JsonSerializer.Serialize(request);

				var result = PlyQorManager.Query(requestString);

				Console.WriteLine(result);
			}

			return keys;
		}
	}
}
