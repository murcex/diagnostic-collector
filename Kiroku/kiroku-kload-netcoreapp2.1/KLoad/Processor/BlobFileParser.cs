namespace KLoad
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // Kiroku
    using Kiroku;

    /// <summary>
    /// Parse Blob Name, add to Blob File Collection.
    /// </summary>
    public class BlobFileParser
    {
        // this should pass in a single List<BlobFileModel>
        public static void Execute(string blobPrefixName, List<string> prefixblobFileNames)
        {
            using (KLog parserLog = new KLog("ClassBlobFileParser-MethodExecute"))
            {
                try
                {
                    foreach (var file in prefixblobFileNames)
                    {
                        var fileNameWithoutPrefix = file.Replace(blobPrefixName, "");

                        BlobFileModel blobFile = new BlobFileModel();
                        blobFile.File = fileNameWithoutPrefix;
                        blobFile.CloudFile = file;

                        // Check and parse the fill log file name
                        if (fileNameWithoutPrefix.Count() == 47 && fileNameWithoutPrefix.Contains("KLOG_"))
                        {
                            var parseGuid = Guid.Parse(fileNameWithoutPrefix.Substring(7, 36));
                            var parseTag = fileNameWithoutPrefix.Substring(1, 6);

                            blobFile.FileGuid = parseGuid;
                            blobFile.Tag = parseTag;
                            blobFile.ParseStatus = true;

                            parserLog.Info($"Parser => File Name: {fileNameWithoutPrefix} Tag: {parseTag} Guid: {parseGuid}");
                        }
                        else
                        {
                            blobFile.ParseStatus = false;
                        }

                        BlobFileCollection.AddFile(blobFile);
                    }
                }
                catch (Exception ex)
                {
                    parserLog.Error($"BlobFileParser Exception: {ex.ToString()}");
                }
            }
        }
    }
}
