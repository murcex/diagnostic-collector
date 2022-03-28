namespace PlyQor.Audit.TestCases.StorageProvider
{
    using System;
    using System.Collections.Generic;
    using PlyQor.Engine.Components.Storage;
    using PlyQor.Audit.Core;

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
                Configuration.Collection,
                Configuration.DocumentName,
                DataGenerator.CreateStringDocument(),
                indexes);

            // bulk insert
            for (int i = 1; i < 10; i++)
            {
                StorageProvider.InsertKey(
                    Configuration.Collection,
                    Guid.NewGuid().ToString(),
                    DataGenerator.CreateRandomDocument(),
                    DataGenerator.CreateSampleIndex());
            }
        }
    }
}
