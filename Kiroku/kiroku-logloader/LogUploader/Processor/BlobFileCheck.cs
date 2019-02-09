namespace KLOGLoader
{
    using System;
    using System.Linq;

    // Kiroku
    using Kiroku;

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
                            var result = DataAccessor.CheckInstanceId(file.FileGuid);

                            if (result != Guid.Empty)
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
                    // TODO: Tracker
                }
            }
        }
    }
}
