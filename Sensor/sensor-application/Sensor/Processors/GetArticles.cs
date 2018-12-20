using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensor
{
    public static class GetArticles
    {
        public static void Execute()
        {
            //Capsule.DNSRecords = DataDownload.GetArticle();

            Capsule.DNSRecords = GetDNSRecords.GetArticle();
        }
    }
}
