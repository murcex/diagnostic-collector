namespace KLoad
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.WindowsAzure.Storage.Blob;

    // Kiroku
    using Kiroku;

    /// <summary>
    /// Collect all File in Azure Storage Blob.
    /// </summary>
    public static class BlobFileCollector
    {
        public static void Execute()
        {
            using (KLog collectorLog = new KLog("ClassBlobFileCollector-MethodExecute"))
            {
                try
                {
                    IEnumerable<IListBlobItem> blobCollection = BlobClient.BlobContainer.ListBlobs(null, false, BlobListingDetails.None);

                    // blobPrefixName = Directory Name of folders inside the Container
                    List<string> blobPrefixNames = blobCollection.OfType<CloudBlobDirectory>().Select(b => b.Prefix).ToList();

                    collectorLog.Info($"Collector => Prefix Count: {blobPrefixNames.Count()}");

                    // for each dir in the blob container
                    foreach (var blobPrefixName in blobPrefixNames)
                    {
                        IEnumerable<IListBlobItem> prefixblobs = BlobClient.BlobContainer.ListBlobs(blobPrefixName, false, BlobListingDetails.None);
                        List<string> prefixblobFileNames = prefixblobs.OfType<CloudBlockBlob>().Select(b => b.Name).ToList();

                        collectorLog.Info($"Collector => Parsing Prefix: {blobPrefixName}");
                        BlobFileParser.Execute(blobPrefixName, prefixblobFileNames);
                    }
                }
                catch (Exception ex)
                {
                    collectorLog.Error($"BlobFileCollector Exception: {ex.ToString()}");
                }
            }
        }
    }
}
