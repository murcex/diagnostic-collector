namespace PlyQor.Audit.TestCases.StorageProvider
{
	using PlyQor.Audit.Core;
	using PlyQor.Engine.Components.Storage;
	using System;

	class SelectTagCount
	{
		public static void Execute()
		{
			Console.WriteLine($"// Count Ids by Index");

			var count =
				StorageProvider.SelectTagCount(
					Configuration.Container,
					Configuration.Tag_Upload);

			Console.WriteLine($"UPLOAD Count: {count}");
			Console.WriteLine("");
		}
	}
}
