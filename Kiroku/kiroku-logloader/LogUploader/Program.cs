namespace KLOGLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            Global.SetLoadValues(); // new config

            Global.StartLogging();

            BlobClient.Set(); // move into global

            BlobFileCollector.Execute();

            BlobFileCheck.Execute();

            BlobFileUploader.Execute();

            BlobFileRetention.Execute();

            Global.StopLogging();

            Global.CheckDebug();
        }
    }
}
