namespace Sensor
{
    using KirokuG2;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    static class GetTCPLatency
    {
        /// <summary>
        /// TCP Ping IPV4 IP for latency.
        /// </summary>
        /// <param name="capsule"></param>
        public static void Execute(IKLog klog, ref Capsule capsule)
        {
            // TODO: Hard code loop size, in future allow for use config. Add logic to check loop count, and check if it's above 3, otherwise enforce 3.
            // 3 because, remove 1 min, remove 1 max, atleast one remains as "base" value.
            foreach (var article in capsule.DNSRecords)
            {
                klog.Trace($"Starting DNS: {article.DNSName}");
                var ips = article.IPRecords;

                try
                {
                    foreach (var ip in ips)
                    {
                        TCPRecord tcpRecord = new TCPRecord();

                        var ipString = ip.IP.ToString();

                        if (ip.IPStatus == "OFFLINE")
                        {
                            tcpRecord.SetOffline();

                            ip.TCPRecord = tcpRecord;

                            break;
                        }

                        uint ipUint = BitConverter.ToUInt32(System.Net.IPAddress.Parse(ipString).GetAddressBytes(), 0);
                        IPEndPoint ipEndpoint = new IPEndPoint(ipUint, 443);

                        var latencyList = new List<double>(5);
                        var exceptionList = new List<string>(5);
                        for (int i = 0; i < 4; i++)
                        {
                            try
                            {
                                latencyList.Add(PingLatency(ipEndpoint));
                            }
                            catch (Exception ex)
                            {
                                exceptionList.Add($"Count: {i} Exception: {ex}");
                            }

                            Thread.Sleep(1000);
                        }

                        if (latencyList.Count > 0)
                        {
                            // logic to remove min and max, avg the rest
                            var minLatency = latencyList.Min();
                            var maxLatency = latencyList.Max();
                            var avgLatency = latencyList.Average();

                            klog.Trace($"IP: {ipString} Min: {minLatency}, Max:{maxLatency}, Avg: {avgLatency}");

                            latencyList.Remove(minLatency);
                            latencyList.Remove(maxLatency);

                            // Add Avg Latency to the IPRecord
                            tcpRecord.Latency = avgLatency;
                            tcpRecord.Port = "443";
                        }
                        else
                        {
                            tcpRecord.SetOffline();

                            foreach (var exMsg in exceptionList)
                            {
                                klog.Trace(exMsg);
                            }

                            klog.Error($"latencyList < 0 records for IP {ip.IP} -- review logs for details");
                        }

                        ip.TCPRecord = tcpRecord;
                    }
                }
                catch (Exception e)
                {
                    klog.Error($"GetTCPLatency - Exception: {e}");
                }
            }
        }

        private static double PingLatency(IPEndPoint ipEndpoint)
        {
            using (var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                sock.Blocking = true;

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                sock.Connect(ipEndpoint);
                sock.Close();

                stopwatch.Stop();

                return stopwatch.Elapsed.TotalMilliseconds;
            }
        }
    }
}
