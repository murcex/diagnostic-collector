namespace PlyQor.Audit.Ultilties
{
	using PlyQor.Audit.Core;
	using PlyQor.Engine.Components.Storage;
	using System.Linq;

	class GetTestIndex
	{
		public static string Execute()
		{
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

			return testUpdateIdList.FirstOrDefault();
		}
	}
}
