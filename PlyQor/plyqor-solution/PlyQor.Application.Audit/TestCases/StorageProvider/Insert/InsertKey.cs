namespace PlyQor.Audit.TestCases.StorageProvider
{
	using PlyQor.Audit.Core;
	using PlyQor.Engine.Components.Storage;
	using System;
	using System.Collections.Generic;

	class InsertKey
	{
		public static void Execute()
		{
			Random random = new Random();

			List<string> indexes = new List<string>();
			var index1 = $"BUILD{random.Next(0, 5)}";
			var index2 = Configuration.Tag_Upload;

			indexes.Add(index1);
			indexes.Add(index2);

			// single insert
			StorageProvider.InsertKey(
				Configuration.Container,
				Configuration.DocumentName,
				DataGenerator.CreateStringDocument(),
				indexes);

			// bulk insert
			for (int i = 1; i < 10; i++)
			{
				StorageProvider.InsertKey(
					Configuration.Container,
					Guid.NewGuid().ToString(),
					DataGenerator.CreateDocument(),
					CreateSampleIndex.Execute());
			}
		}
	}
}
