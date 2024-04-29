namespace PlyQor.Audit.TestCases.StorageProvider
{
	using PlyQor.Audit.Core;
	using PlyQor.Engine.Components.Storage;
	using System;

	class SelectTags
	{
		public static void Execute()
		{
			Console.WriteLine($"// List Indexes");

			var listIndexes =
				StorageProvider.SelectTags(
					Configuration.Container);

			Console.WriteLine($"Indexes Count: {listIndexes.Count}");

			foreach (var listIndex in listIndexes)
			{
				Console.WriteLine($"Index: {listIndex}");
			}

			Console.WriteLine("");
		}
	}
}
