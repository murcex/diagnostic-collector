namespace KLOGLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            Global.StartLogging();

            BlobClient.Set();

            BlobFileCollector.Execute();

            BlobFileCheck.Execute();

            BlobFileUploader.Execute();

            BlobFileRetention.Execute();

            Global.StopLogging();

            Global.CheckDebug();
        }
    }
}
