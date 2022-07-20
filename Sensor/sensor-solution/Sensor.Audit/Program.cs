namespace Sensor.Audit
{
    using System;
    using Sensor;
    using Implements;
    using System.IO;
    using System.Collections.Generic;

    class Program
    {
        private static List<KeyValuePair<string, string>> sensorConfigs;
        private static List<KeyValuePair<string, string>> kirokuConfigs;

        static void Main(string[] args)
        {
            using (Deserializer deserilaizer = new Deserializer())
            {
                var _file = Directory.GetCurrentDirectory() + @"\Config.ini";

                deserilaizer.Execute(_file);

                sensorConfigs = deserilaizer.GetTag("sensor");
            }

            if (SensorManager.Initialize(sensorConfigs))
            {
                Console.WriteLine($"Configs loaded.");
                //if (SensorManager.Execute())
                //{
                //    Console.WriteLine($"Sensor executed.");
                //}
                //else
                //{
                //    Console.WriteLine($"Sensor failed.");
                //}
            }
            else
            {
                Console.WriteLine($"Configs failed.");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\tDEBUG DETECTED, PRESS ANY KEY");
            Console.ReadKey();
        }
    }
}
