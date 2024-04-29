namespace PlyQor.Audit.TestCases.StorageProvider
{
	using PlyQor.Audit.Core;
	using PlyQor.Audit.Ultilties;
	using PlyQor.Engine.Components.Storage;
	using System;

	class UpdateKey
	{
		public static void Execute()
		{
			Console.WriteLine($"// Update Id");

			var testUpdateId = GetTestIndex.Execute();

			// select get before data
			var testSelect = StorageProvider.SelectKey(Configuration.Container, testUpdateId);

			var newId = $"TestUpdate-{Guid.NewGuid()}";

			// update id
			StorageProvider.UpdateKey(Configuration.Container, testUpdateId, newId);

			// test select id
			var testSelect2 = StorageProvider.SelectKey(Configuration.Container, testUpdateId);

			var testSelect3 = StorageProvider.SelectKey(Configuration.Container, newId);

			Console.WriteLine($"Should be Null (True): {string.IsNullOrEmpty(testSelect2)}");
			Console.WriteLine($"Should Match (True): {Equals(testSelect, testSelect3)}");
			Console.WriteLine("");
		}
	}
}
