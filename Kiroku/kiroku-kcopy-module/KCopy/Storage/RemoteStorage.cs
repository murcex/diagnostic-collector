namespace KCopy.Storage
{
    using KCopy.Appliance;
    using KCopy.Model;

    class RemoteStorage
    {
        public static bool LogExist(FileModel fileModel)
        {
            var blobFileName = GetBlobFileName(fileModel);

            var select = StorageClient.CheckLog(blobFileName);

            return select != null && select.Length > 0;
        }

        public static bool SendLog(FileModel fileModel, byte[] document)
        {
            var blobFileName = GetBlobFileName(fileModel);

            return StorageClient.InsertLog(blobFileName, document);
        }

        private static string GetBlobFileName(FileModel fileModel)
        {
            var blobfileName = @"KLOG_R_" + fileModel.FileGuid.ToString() + ".txt";

            return blobfileName;
        }
    }
}
