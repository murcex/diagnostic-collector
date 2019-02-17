namespace KLOGLoader
{
    using Kiroku;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            Global.StartLogging();

            BlobClient.Set(); // move into global?

            BlobFileCollector.Execute();

            BlobFileCheck.Execute();

            BlobFileUploader.Execute();

            BlobFileRetention.Execute();

            Global.StopLogging(); // move into "shutdown" method

            Global.CheckDebug(); // move into "shutdown" method
        }
    }
}
