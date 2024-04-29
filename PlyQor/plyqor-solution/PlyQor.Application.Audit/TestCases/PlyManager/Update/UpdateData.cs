namespace PlyQor.Audit.TestCases.PlyManager
{
	using PlyQor.Audit.Core;
	using PlyQor.Engine;
	using PlyQor.Resources;
	using System;
	using System.Collections.Generic;
	using System.Text.Json;

	internal class UpdateData
	{
		public static void Execute()
		{
			Console.WriteLine(" \n\r// UpdateData");
			Dictionary<string, string> request = new Dictionary<string, string>();

			request.Add(RequestKeys.Token, Configuration.Token);
			request.Add(RequestKeys.Container, Configuration.Container);
			request.Add(RequestKeys.Operation, QueryOperation.UpdateData);
			request.Add(RequestKeys.Key, Configuration.Key_2);
			request.Add(RequestKeys.Data, Configuration.Data_1);
			request.Add(RequestKeys.Aux, Configuration.Data_2);

			var requestString = JsonSerializer.Serialize(request);

			var result = PlyQorManager.Query(requestString);

			Console.WriteLine(result);
		}
	}
}
