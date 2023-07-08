namespace Sensor
{
    using KirokuG2;
    using System;
    using System.Collections.Generic;

    static class GetArticles
    {
        /// <summary>
        /// Get the Endpoints and their Configs.
        /// </summary>
        /// <returns></returns>
        public static List<DNSRecord> Execute(IKLog klog)
        {
            try
            {
                return GetDNSRecords.GetArticle();
            }
            catch (Exception ex)
            {
                klog.Error($"GetArticles::Execute | {ex}");

                return new List<DNSRecord>();
            }
        }
    }
}
