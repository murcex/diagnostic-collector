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
            // Start instance level logging
            KManager.Online((NameValueCollection)ConfigurationManager.GetSection("Kiroku"));

            BlobClient.Set();

            BlobFileCollector.Execute();

            BlobFileCheck.Execute();

            BlobFileUploader.Execute();

            BlobFileRetention.Execute();

            // End instance level logging
            KManager.Offline();

            Global.CheckDebug();
        }
    }
}
