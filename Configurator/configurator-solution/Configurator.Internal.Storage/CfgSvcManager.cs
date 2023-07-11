namespace Configurator.Storage
{
    using Configurator.Storage.Core;
    using Configurator.Storage.Internal;
    using Implements.Configuration;
    using KirokuG2;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CfgSvcManager
    {
        private const string _accessFileName = "access";
        private static Dictionary<string, List<string>> _access = new();

        /// <summary>
        /// Initialize the Configurator API.
        /// </summary>
        public static bool Initialize(Dictionary<string, Dictionary<string, string>> config)
        {
            Configuration.SetConfigs(config);

            _access = SetupAccess();

            return true;
        }

        /// <summary>
        /// Get and serve configuration document response.
        /// </summary>
        public static object Execute(string cfkKey, string cfgApp, IKLog klog)
        {
            try
            {
                if (string.IsNullOrEmpty(cfkKey))
                {
                    klog.Info("NULL CFG KEY");
                    return string.Empty;
                }

                if (string.IsNullOrEmpty(cfgApp))
                {
                    klog.Info("NULL APP KEY");
                    return string.Empty;
                }

                klog.Info($"CfgKey: {cfkKey}, CfkApp: {cfgApp}");

                if (_access.TryGetValue(cfgApp, out var tokens))
                {
                    if (tokens.Contains(cfkKey))
                    {
                        var document = GetCfg(cfgApp);

                        if (document != null)
                        {
                            klog.Info($"PASS");
                            return document;
                        }
                        else
                        {
                            klog.Error($"FAIL: NULL DOC");
                            return string.Empty;
                        }
                    }
                    else
                    {
                        klog.Info($"FAIL: CFG KEY NOT FOUND");
                        return string.Empty;
                    }
                }
                else
                {
                    klog.Info($"FAIL: APP KEY NOT FOUND");
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                klog.Error($"Exception: {ex}");
                return string.Empty;
            }

        }

        /// <summary>
        /// Get a single configure document from the storage blob.
        /// </summary>
        private static byte[] GetCfg(string cfgApp)
        {
            byte[] document = null;

            var fileName = $"{cfgApp}.ini";

            try
            {
                document = StorageClient.GetDocument(fileName);

                return document;
            }
            catch
            {
                return document;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, List<string>> SetupAccess()
        {
            Dictionary<string, List<string>> accessSet = new Dictionary<string, List<string>>();

            var accessCfgByte = GetCfg(_accessFileName);

            var singleString = Encoding.UTF8.GetString(accessCfgByte);

            string[] lineArray = singleString.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            var lineList = lineArray.ToList();

            ConfigurationUtility configurationUtility = new ConfigurationUtility();

            var accessCfg = configurationUtility.Deserialize(lineList);

            foreach (var fileName in accessCfg.Keys)
            {
                var record = accessCfg[fileName];

                var tokens = record["tokens"].Split(",");

                List<string> x = new List<string>();
                foreach (var token in tokens)
                {
                    x.Add(token);
                }

                accessSet.Add(fileName, x);
            }

            return accessSet;
        }
    }
}
