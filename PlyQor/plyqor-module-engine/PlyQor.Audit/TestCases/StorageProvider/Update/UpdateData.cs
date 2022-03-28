namespace PlyQor.Audit.TestCases.StorageProvider
{
    using System;
    using PlyQor.Engine.Components.Storage;
    using PlyQor.Audit.Ultilties;
    using PlyQor.Audit.Core;

    class UpdateData
    {
        public static void Execute()
        {
            Console.WriteLine("// Update Data");

            var testUpdateId = Ultilty.GetTestIndex();

            // test select data before
            var testSelectData = StorageProvider.SelectKey(Configuration.Collection, testUpdateId);

            // create new data string
            var newData = $"NewData-{DataGenerator.CreateRandomDocument()}";

            // update id
            var updateTarget = StorageProvider.UpdateData(Configuration.Collection, testUpdateId, newData);

            // test select data after
            var testSelectData2 = StorageProvider.SelectKey(Configuration.Collection, testUpdateId);

            Console.WriteLine($"Data should not match after updating (False): {Equals(testSelectData, testSelectData2)}");

            Console.WriteLine("");
        }
    }
}
