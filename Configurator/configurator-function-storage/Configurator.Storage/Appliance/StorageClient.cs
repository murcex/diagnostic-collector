using Configurator.Storage.Core;
using Implements.Substrate.Blob;
using System;
using System.Collections.Generic;
using System.Text;

namespace Configurator.Storage.Appliance
{
    class StorageClient
    {
        private static string _profile { get; } = "ConfiguratorStorage";

        /// <summary>
        /// Configure Storage Blob client for KQuery.
        /// </summary>
        public static bool Configure()
        {
            return BlobClient.Initialize(
                _profile,
                Configuration.StorageAccount,
                Configuration.StorageContainer,
                verifyContainer: true).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 
        /// </summary>
        public static byte[] GetCfg(string fileName)
        {
            return BlobClient.SelectBlob(
                _profile,
                fileName).GetAwaiter().GetResult();
        }
    }
}