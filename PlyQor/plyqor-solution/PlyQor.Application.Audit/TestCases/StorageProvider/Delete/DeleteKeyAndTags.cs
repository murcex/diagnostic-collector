namespace PlyQor.Audit.TestCases.StorageProvider
{
	using PlyQor.Audit.Core;
	using PlyQor.Audit.Ultilties;
	using PlyQor.Engine.Components.Storage;
	using System;

	class DeleteKeyAndTags
	{
		public static void Execute()
		{
			Console.WriteLine($"// TEST: {nameof(DeleteKeyAndTags)}");

			var deleteId = GetTestIndex.Execute();

			StorageProvider.DeleteKey(Configuration.Container, deleteId);
			StorageProvider.DeleteKeyTags(Configuration.Container, deleteId);

			var checkDeleteKey = StorageProvider.SelectKey(Configuration.Container, deleteId);
			var checkDeleteIndex = StorageProvider.SelectKeyTags(Configuration.Container, deleteId);

			Console.WriteLine($"Check Key is null (True): {string.IsNullOrEmpty(checkDeleteKey)}");
			Console.WriteLine($"Check Tags are empty (True): {Equals(checkDeleteIndex.Count, 0)}");
			Console.WriteLine($"Deleted {deleteId} from Storage and Tag");
			Console.WriteLine($"");
		}
	}
}
