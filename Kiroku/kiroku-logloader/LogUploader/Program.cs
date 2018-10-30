using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Specialized;

// Kiroku
using Kiroku;
using System.Configuration;

namespace KLOGLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            Global.SetLoadValues();

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
