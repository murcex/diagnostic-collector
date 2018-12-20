using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sensor
{
    public static class GetTCPLatency
    {
        public static void Execute()
        {

            // TODO: Hard code loop size, in future allow for use config. Add logic to check loop count, and check if it's above 3, otherwise enforce 3.
            // 3 because, remove 1 min, remove 1 max, atleast one remains as "base" value.

            // create transfer case of TCPRecord
            List<TCPLatencyRecord> tcpRecordList = new List<TCPLatencyRecord>();

            try
            {
                foreach (var ipRecord in Capsule.IPRecords)
                {
                    TCPLatencyRecord tcpRecord = new TCPLatencyRecord();
                    tcpRecord.HostName = ipRecord.HostName;
                    tcpRecord.IP = ipRecord.IP;

                    var item = ipRecord.IP.ToString();

                    uint newip = BitConverter.ToUInt32(System.Net.IPAddress.Parse(item).GetAddressBytes(), 0);

                    IPEndPoint ipe = new IPEndPoint(newip, 443);

                    Console.WriteLine($"Target {ipRecord.HostName}");

                    var times = new List<double>();

                    for (int i = 0; i < 4; i++)
                    {
                        var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        sock.Blocking = true;

                        var stopwatch = new Stopwatch();

                        // Measure the Connect call only
                        stopwatch.Start();
                        sock.Connect(ipe);
                        stopwatch.Stop();

                        double t = stopwatch.Elapsed.TotalMilliseconds;
                        Console.WriteLine("{0:0.00}ms", t);
                        times.Add(t);

                        sock.Close();

                        Thread.Sleep(1000);
                    }

                    // logic to remove min and max, avg the rest

                    times.Remove(times.Min());
                    times.Remove(times.Max());
                    var avgLatency = times.Average();

                    Console.WriteLine("{0:0.00} {1:0.00} {2:0.00} {3:0.00}", times.Min(), times.Max(), times.Average(), avgLatency);

                    // Add Avg Latency to the IPRecord
                    tcpRecord.Latency = avgLatency;

                    tcpRecordList.Add(tcpRecord);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetTCPLatency - Exception: {e.ToString()}");
            }

            // Add transer case into Cap.TCPRecords
            Capsule.TCPLatencyRecords = tcpRecordList;
        }
    }
}
