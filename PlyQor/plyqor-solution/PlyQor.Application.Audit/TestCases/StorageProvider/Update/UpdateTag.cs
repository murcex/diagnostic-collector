namespace PlyQor.Audit.TestCases.StorageProvider
{
    using PlyQor.Audit.Core;
    using PlyQor.Internal.Engine.Components.StorageAdapter;
    using System;

    class UpdateTag
	{
		public static bool Execute()
		{
			Console.WriteLine("// Update Index Set");

			var indexes = StorageProvider.SelectTags(Configuration.Container);

			string targetIndex = null;
			string checkIndex = null;
			string checkForStage = "Stage";

			foreach (var index in indexes)
			{
				if (index.Contains(checkForStage.ToUpper()))
				{
					targetIndex = index;

					break;
				}
			}

			StorageProvider.UpdateTag(Configuration.Container, targetIndex, "ARCHIVE");

			var indexes2 = StorageProvider.SelectTags(Configuration.Container);

			bool NoHit = false;

			foreach (var index in indexes2)
			{
				if (index.Contains("ARCHIVE"))
				{
					checkIndex = index;
				}

				if (Equals(index, targetIndex))
				{
					NoHit = true;
				}
			}

			Console.WriteLine($"Updated {targetIndex} to Archive");
			Console.WriteLine($"Index Update (True): {Equals("ARCHIVE", checkIndex)}");
			Console.WriteLine($"Old Index has been updated (True): {Equals(NoHit, false)}");
			Console.WriteLine($"");

			return true;
		}
	}
}
