namespace PlyQor.Audit.TestCases.StorageProvider
{
	using PlyQor.Audit.Core;
	using PlyQor.Engine.Components.Storage;
	using System;
	class DeleteTag
	{
		public static void Execute()
		{
			Console.WriteLine($"// Delete Index Set");
			var indexes = StorageProvider.SelectTags(Configuration.Container);

			string targetIndex = null;
			string checkForStage = "Stage";

			foreach (var index in indexes)
			{
				if (index.Contains(checkForStage.ToUpper()))
				{
					targetIndex = index;

					break;
				}
			}

			StorageProvider.DeleteTag(Configuration.Container, targetIndex);

			var indexes2 = StorageProvider.SelectTags(Configuration.Container);

			bool NoHit = true;

			foreach (var index in indexes2)
			{
				if (index.Contains(targetIndex))
				{
					NoHit = false;
					break;
				}
			}

			Console.WriteLine($"Removing Index: {targetIndex}");
			Console.WriteLine($"Index removed (True): {Equals(true, NoHit)}");
			Console.WriteLine($"");
		}
	}
}
