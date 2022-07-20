namespace KQuery
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Kiroku;

    public class KQueryManger
    {
        private static bool _configOnline = false;

        private static bool _appConfigStatus = false;

        private static bool _logConfigStatus = false;

        /// <summary>
        /// Initialize KQuery Application Configs.
        /// </summary>
        /// <param name="kqueryConfig"></param>
        /// <param name="kirokuConfig"></param>
        /// <returns></returns>
        public static bool Initialize(List<KeyValuePair<string, string>> kqueryConfig, List<KeyValuePair<string, string>> kirokuConfig)
        {
            // Null checks
            if (kqueryConfig != null)
            {
                _appConfigStatus = true;
            }
            if (kirokuConfig != null)
            {
                _logConfigStatus = true;
            }

            // Push config packages to Configuration logic
            try
            {
                return _configOnline = Configuration.SetConfigs(kqueryConfig, kirokuConfig);
            }
            catch
            {
                return _configOnline = false;
            }
        }

        /// <summary>
        /// Query Kiroku Storage for a single KLOG.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static OkObjectResult Execute(HttpRequest req)
        {
            using (KLog klog = new KLog($"KQueryManager,Execute"))
            {
                if (!_configOnline)
                {
                    klog.Error("Config failed to load.");
                    return new OkObjectResult("");
                }

                if (req == null)
                {
                    klog.Warning("Request was empty.");
                    return new OkObjectResult("");
                }

                string id = req?.Query["id"];

                try
                {
                    Guid checkId = new Guid(id);

                    byte[] doc = Storage.GetLog(id);

                    var payload = Encoding.UTF8.GetString(doc, 0, doc.Length);

                    if (!string.IsNullOrEmpty(payload))
                    {
                        payload = payload.Replace("#KLOG_INSTANCE_STATUS#", "");

                        payload = payload.Replace("}", "},");

                        payload = "[" + payload + "]";

                        payload = payload.Replace("},\r\n$", "}]");

                        payload = JValue.Parse(payload).ToString(Formatting.Indented);
                    }

                    if (doc == null)
                    {
                        klog.Error($"Document is null. Id: {id}");
                    }

                    return doc == null
                        ? new OkObjectResult($"404")
                        : new OkObjectResult(payload);
                }
                catch (Exception ex)
                {
                    klog.Error($"EXCEPTION : {ex.ToString()}");
                    return new OkObjectResult("");
                }
            }
        }
    }
}
