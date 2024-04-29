using PlyQor.Audit.Core;
using System;

namespace PlyQor.Audit.TestCases.PlyManager
{
	class PlyManagerTestProvider
	{
		public static void Execute()
		{
			Console.WriteLine($"{Configuration.Key_1}, {Configuration.Key_2}");
			Configuration.DeleteTestKeys = CreateTestKeysWithTag.Execute();

			// Insert

			InsertKey.Execute();
			InsertKey.Execute(); // PK Check (should fail)

			InsertTag.Execute();

			// Select

			SelectKey.Execute();

			SelectTags.Execute();

			SelectTagCount.Execute();

			SelectKeyList.Execute();

			// Update

			UpdateKey.Execute();

			UpdateData.Execute();

			UpdateTagByKey.Execute();

			UpdateTag.Execute();

			// Delete

			DeleteKey.Execute();

			DeleteTagByKey.Execute();

			DeleteTagsByKey.Execute();

			DeleteTag.Execute();

			// Retention

			DataRetention.Execute();

			TraceRetention.Execute();
		}
	}
}
