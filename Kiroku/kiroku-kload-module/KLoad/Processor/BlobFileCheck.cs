namespace KLoad
{
    using System;
    using System.Linq;

    // Kiroku
    using Kiroku;

    /// <summary>
    /// Check if Blob File already exist in database.
    /// </summary>
    public class BlobFileCheck
    {
        public static void Execute()
        {
            using (KLog checkLog = new KLog("ClassBlobFileCheck-MethodExecute"))
            {
                try
                {
                    foreach (var file in BlobFileCollection.GetFiles())
                    {
                        if (file.FileGuid != Guid.Empty)
                        {
                            var resultResponse = DataAccessor.CheckInstanceId(file.FileGuid);

                            if (!resultResponse.Success)
                            {
                                checkLog.Error($"SQL Expection on [BlobFileCheck].[CheckInstance] - Message: {resultResponse.Message}");
                                break;
                            }

                            if (resultResponse.Id != Guid.Empty)
                            {
                                BlobFileCollection.GetFiles().First(d => d.FileGuid == file.FileGuid).Exist = true;
                                checkLog.Info($"Instance Check => Guid: {file.FileGuid.ToString()} Result: true");
                            }
                            else
                            {
                                BlobFileCollection.GetFiles().First(d => d.FileGuid == file.FileGuid).Exist = false;
                                checkLog.Info($"Instance Check => Guid: {file.FileGuid.ToString()} Result: false");
                            }
                        }
                        else
                        {
                            BlobFileCollection.GetFiles().First(d => d.FileGuid == file.FileGuid).Exist = true;
                            checkLog.Info($"Instance Check => Guid: {file.FileGuid.ToString()} Result: true (empty guid)");
                        }
                    }
                }
                catch (Exception ex)
                {
                    checkLog.Error($"BlobfileCheck Expection: {ex.ToString()}");
                }
            }
        }
    }
}
