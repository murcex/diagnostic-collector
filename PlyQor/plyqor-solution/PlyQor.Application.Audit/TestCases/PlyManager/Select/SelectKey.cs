namespace PlyQor.Audit.TestCases.PlyManager
{
	using PlyQor.Audit.Core;
	using PlyQor.Engine;
	using PlyQor.Resources;
	using System;
	using System.Collections.Generic;
	using System.Text.Json;

	internal class SelectKey
	{
		public static void Execute()
		{
			Console.WriteLine(" \n\r// SelectKey");
			Dictionary<string, string> request = new Dictionary<string, string>();

			request.Add(RequestKeys.Token, Configuration.Token);
			request.Add(RequestKeys.Container, Configuration.Container);
			request.Add(RequestKeys.Operation, QueryOperation.SelectKey);
			request.Add(RequestKeys.Key, Configuration.Key_1);

			var requestString = JsonSerializer.Serialize(request);

			var result = PlyQorManager.Query(requestString);

			Console.WriteLine(result);
		}
	}
}
