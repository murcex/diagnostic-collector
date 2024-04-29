namespace PlyQor.Audit.TestCases.StorageProvider
{
	using PlyQor.Audit.Core;
	using PlyQor.Audit.Ultilties;
	using PlyQor.Engine.Components.Storage;
	using System;

	class UpdateData
	{
		public static void Execute()
		{
			Console.WriteLine("// Update Data");

			var testUpdateId = GetTestIndex.Execute();

			// test select data before
			var testSelectData = StorageProvider.SelectKey(Configuration.Container, testUpdateId);

			// create new data string
			var newData = $"NewData-{DataGenerator.CreateDocument()}";

			// update id
			var updateTarget = StorageProvider.UpdateData(Configuration.Container, testUpdateId, newData);

			// test select data after
			var testSelectData2 = StorageProvider.SelectKey(Configuration.Container, testUpdateId);

			Console.WriteLine($"Data should not match after updating (False): {Equals(testSelectData, testSelectData2)}");

			Console.WriteLine("");
		}
	}
}
