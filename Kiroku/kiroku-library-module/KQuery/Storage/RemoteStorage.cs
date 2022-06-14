namespace KQuery.Storage
{
    using System.Text;
    using KQuery.Appliance;

    class RemoteStorage
    {
        /// <summary>
        /// Get KLOG from Storage Blob.
        /// </summary>
        public static string GetLog(string fileKey)
        {
            var blobfileName = @"KLOG_R_" + fileKey + ".txt";

            var byteLog = StorageClient.GetLog(blobfileName);

            var stringLog = Encoding.UTF8.GetString(byteLog, 0, byteLog.Length);

            return stringLog;
        }
    }
}
