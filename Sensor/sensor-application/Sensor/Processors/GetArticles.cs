namespace Sensor
{
    using System;
    using System.Collections.Generic;

    using Kiroku;

    public static class GetArticles
    {

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
