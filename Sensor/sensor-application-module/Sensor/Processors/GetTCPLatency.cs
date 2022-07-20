namespace Sensor
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;
    using KirokuG2;

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
                klog.Trace($"DNS: {article.DNSName}");
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

                        var latencyList = new List<double>();
                        for (int i = 0; i < 4; i++)
                        {
                            var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            sock.Blocking = true;

                            var stopwatch = new Stopwatch();

                            // Measure the Connect call only
                            stopwatch.Start();
                            sock.Connect(ipEndpoint);
                            stopwatch.Stop();

                            double t = stopwatch.Elapsed.TotalMilliseconds;
                            latencyList.Add(t);

                            sock.Close();

                            Thread.Sleep(1000);
                        }

                        // logic to remove min and max, avg the rest
                        klog.Trace($"IP: {ipString} Min: {latencyList.Min()}");
                        latencyList.Remove(latencyList.Min());

                        klog.Trace($"IP: {ipString} Max: {latencyList.Max()}");
                        latencyList.Remove(latencyList.Max());

                        klog.Trace($"IP: {ipString} Avg: {latencyList.Average()}");
                        var avgLatency = latencyList.Average();

                        // Add Avg Latency to the IPRecord
                        tcpRecord.Latency = avgLatency;
                        tcpRecord.Port = "443";

                        ip.TCPRecord = tcpRecord;
                    }
                }
                catch (Exception e)
                {
                    klog.Error($"GetTCPLatency - Exception: {e.ToString()}");
                }
            }
        }
    }
}
