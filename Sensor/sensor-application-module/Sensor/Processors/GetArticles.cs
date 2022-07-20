namespace Sensor
{
    using System;
    using System.Collections.Generic;
    using KirokuG2;

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
