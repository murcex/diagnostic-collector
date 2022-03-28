namespace PlyQor.Audit.TestCases.StorageProvider
{
    using System;
    using PlyQor.Engine.Components.Storage;
    using PlyQor.Audit.Ultilties;
    using PlyQor.Audit.Core;

    class UpdateKey
    { 
        public static void Execute()
        {
            Console.WriteLine($"// Update Id");

            var testUpdateId = Ultilty.GetTestIndex();

            // select get before data
            var testSelect = StorageProvider.SelectKey(Configuration.Collection, testUpdateId);

            var newId = $"TestUpdate-{Guid.NewGuid()}";

            // update id
            StorageProvider.UpdateKey(Configuration.Collection, testUpdateId, newId);

            // test select id
            var testSelect2 = StorageProvider.SelectKey(Configuration.Collection, testUpdateId);

            var testSelect3 = StorageProvider.SelectKey(Configuration.Collection, newId);

            Console.WriteLine($"Should be Null (True): {string.IsNullOrEmpty(testSelect2)}");
            Console.WriteLine($"Should Match (True): {Equals(testSelect, testSelect3)}");
            Console.WriteLine("");
        }
    }
}
