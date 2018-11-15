using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Diagnostics;

namespace Sensor
{
    class DNSEvaluation
    {
        //public static IPAddress[] Execute(string dns)
        //{
        //    try
        //    {
        //        var count = 0;
        //        List<IPRecord> ipRecordList = new List<IPRecord>();

        //        while (count < 30)
        //        {
        //            var ips = Dns.GetHostAddresses(dns);

        //            foreach (var ip in ips)
        //            {
        //                IPRecord record = new IPRecord();
        //                record.IP = ip;
        //                ipRecordList.Add(record);
        //            }

        //            count++;
        //        }

        //        // Version 1
        //        var distinctIpList = ipRecordList.GroupBy(ip => ip.IP).Select(y => y.First());

        //        foreach (var x in distinctIpList)
        //        {
        //            //Console.WriteLine("Distict {0}: {1}", dns, x.IP.ToString());
        //        }

        //        IPAddress[] ipAddressArray = distinctIpList.Select(x => x.IP).ToArray();

        //        Console.WriteLine("\r");

        //        return ipAddressArray;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.ToString().Contains("No such host is known"))
        //        {
        //            Console.WriteLine("Exception: Unknow Host. ({0})", dns);
        //        }
        //        else
        //        {
        //            Console.WriteLine("Exception: {0}", ex.ToString());
        //        }

        //        IPAddress[] address = { IPAddress.Parse("0.0.0.0") };

        //        return address;
        //    }
        //}
    }
}
