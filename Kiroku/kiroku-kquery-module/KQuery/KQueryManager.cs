namespace KQuery
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using Kiroku;

    using KQuery.Core;
    using KQuery.Component;
    using KQuery.Storage;


    public class KQueryManager
    {
        private static bool _configOnline;

        private static bool _appConfigStatus;

        private static bool _logConfigStatus;

        /// <summary>
        /// Initialize KQuery Application Configs.
        /// </summary>
        /// <param name="kqueryConfig"></param>
        /// <param name="kirokuConfig"></param>
        /// <returns></returns>
        public static bool Initialize(
            List<KeyValuePair<string, string>> kqueryConfig,
            List<KeyValuePair<string, string>> kirokuConfig)
        {
            // Config null checks
            _appConfigStatus = kqueryConfig != null;
            _logConfigStatus = kirokuConfig != null;

            // Send config packages to Configuration logic
            try
            {
                return _configOnline = Initializer.Execute(kqueryConfig, kirokuConfig);
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
            using (KLog klog = new KLog($"KQueryManager-Execute"))
            {
                if (!_configOnline)
                {
                    klog.Error("Config failed to load.");
                    return new OkObjectResult("");
                }

                if (!Validation.CheckRequest(
                    req,
                    out string checkRequestMessage,
                    out OkObjectResult checkRequestResult))
                {
                    klog.Warning(checkRequestMessage);
                    return checkRequestResult;
                }

                string logId = req?.Query["id"];

                if (!Validation.CheckRequestId(
                    logId,
                    out string checkRequestIdMessage,
                    out OkObjectResult checkRequestIdResult))
                {
                    klog.Warning(checkRequestIdMessage);
                    return checkRequestIdResult;
                }

                try
                {
                    var log = RemoteStorage.GetLog(logId);

                    if (!Validation.CheckLog(
                        logId,
                        log,
                        out string checklogMessage,
                        out OkObjectResult checkLogResult))
                    {
                        klog.Error(checklogMessage);
                        return checkLogResult;
                    }

                    log = FormatLog.Execute(log);

                    return new OkObjectResult(log);
                }
                catch (Exception ex)
                {
                    klog.Error($"KQueryManager::Execute - EXCEPTION: {ex}");
                    return new OkObjectResult("");
                }
            }
        }
    }
}
