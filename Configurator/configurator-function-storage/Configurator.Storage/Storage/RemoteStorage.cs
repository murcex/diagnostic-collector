namespace Configurator.Storage.Storage
{
    using Configurator.Storage.Appliance;

    class RemoteStorage
    {
        /// <summary>
        /// Get a document from storage blob by file name.
        /// </summary>
        public static byte[] GetDocument(string fileName)
        {
            try
            {
                return StorageClient.GetCfg(fileName);
            }
            catch
            {
                return null;
            }
        }
    }
}