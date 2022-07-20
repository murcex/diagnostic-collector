using System;
using KLoad;
using Implements;
using System.IO;
using System.Collections.Generic;

namespace KLoad.Audit
{
    class Program
    {
        private static List<KeyValuePair<string, string>> kloadConfigs;
        private static List<KeyValuePair<string, string>> kirokuConfigs;

        static void Main(string[] args)
        {
            using (Deserializer deserilaizer = new Deserializer())
            {
                var _file = Directory.GetCurrentDirectory() + @"\Config.ini";

                deserilaizer.Execute(_file);

                kloadConfigs = deserilaizer.GetTag("kload");

                kirokuConfigs = deserilaizer.GetTag("kiroku_kload");
            }

            if (KLoadManager.Initialize(kloadConfigs, kirokuConfigs))
            {
                Console.WriteLine($"Configs loaded.");
                if (KLoadManager.Execute())
                {
                    Console.WriteLine($"KLoad executed.");
                }
                else
                {
                    Console.WriteLine($"KLoad failed.");
                }
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
