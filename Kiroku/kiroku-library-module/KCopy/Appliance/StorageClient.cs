namespace KCopy.Appliance
{
    using Implements.Substrate.Blob;
    using KCopy.Core;

    class StorageClient
    {
        private static string _profile { get; } = "KCopy";

        /// <summary>
        /// 
        /// </summary>
        public static bool Configure()
        {
            var result = BlobClient.Initialize(
            _profile,
            Configuration.AzureStorage,
            Configuration.AzureContainer,
            verifyContainer: true).GetAwaiter().GetResult();

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public static byte[] CheckLog(string fileName)
        {
            return BlobClient.SelectBlob(_profile, fileName).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool InsertLog(string fileName, byte[] document)
        {
            return BlobClient.InsertBlob(_profile, fileName, document).GetAwaiter().GetResult();
        }
    }
}
