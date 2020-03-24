namespace Sensor
{
    using System;
    using System.Collections.Generic;

    using Kiroku;

    public static class GetArticles
    {
        /// <summary>
        /// Get the Endpoints and their Configs.
        /// </summary>
        /// <returns></returns>
        public static List<DNSRecord> Execute()
        {
            using (KLog klog = new KLog("GetEndpointConfigs"))
            {
                try
                {
                    return GetDNSRecords.GetArticle();
                }
                catch (Exception ex)
                {
                    klog.Error($"GetArticles::Execute | {ex.ToString()}");
                    return new List<DNSRecord>();
                }
            }
        }
    }
}
