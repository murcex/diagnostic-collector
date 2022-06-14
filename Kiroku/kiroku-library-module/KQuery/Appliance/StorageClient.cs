namespace KQuery.Appliance
{
    using Implements.Substrate.Blob;

    class StorageClient
    {
        private static string _profile { get; } = "KQuery";

        /// <summary>
        /// Configure Storage Blob client for KQuery.
        /// </summary>
        public static bool Configure()
        {
            return BlobClient.Initialize(
                _profile,
                Configuration.AzureStorage,
                Configuration.AzureContainer,
                verifyContainer: true).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Select Document from Storage Blob.
        /// </summary>
        public static byte[] GetLog(string fileName)
        {
            return BlobClient.SelectBlob(
                _profile, 
                fileName).GetAwaiter().GetResult();
        }
    }
}
