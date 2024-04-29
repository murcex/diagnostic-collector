namespace PlyQor.Audit.TestCases.StorageProvider
{
	using System;
	using System.Collections.Generic;

	class CreateSampleIndex
	{
		public static List<string> Execute()
		{
			Random random = new Random();

			List<string> indexes = new List<string>();
			var index1 = $"Stage{random.Next(0, 5)}";

			indexes.Add(index1);

			return indexes;
		}
	}
}
