namespace PlyQor.Audit.TestCases.StorageProvider
{
    using PlyQor.Audit.Core;
    using PlyQor.Internal.Engine.Components.StorageAdapter;
    using System;
    using System.Linq;

    class DeleteTagByKey
	{
		public static void Execute()
		{
			Console.WriteLine("// Delete Index Id");

			var indexes = StorageProvider.SelectTags(Configuration.Container);

			string targetIndex = null;
			string checkForStage = Configuration.Tag_Stage;

			foreach (var index in indexes)
			{
				if (index.Contains(checkForStage.ToUpper()))
				{
					targetIndex = index;

					break;
				}
			}

			var testUpdateIdList = StorageProvider.SelectKeyList(Configuration.Container, targetIndex, 1);

			var testDeleteId = testUpdateIdList.FirstOrDefault();

			StorageProvider.DeleteKeyTag(
				Configuration.Container,
				testDeleteId,
				targetIndex);

			var checkSelectIndexes =
				StorageProvider.SelectKeyTags(
					Configuration.Container,
					testDeleteId);

			bool NoHit = false;
			foreach (var index in checkSelectIndexes)
			{
				if (index.Contains(targetIndex))
				{
					NoHit = true;
				}

				Console.WriteLine($"Index: {index}");
			}

			Console.WriteLine($"Check Delete Id Index (True): {Equals(NoHit, false)}");
			Console.WriteLine($"Removing Index {targetIndex} on Storage Id {testDeleteId}");
			Console.WriteLine($"");
		}
	}
}
