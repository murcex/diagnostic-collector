namespace KCopy.Audit
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Implements;

    class Program
    {
        private static List<KeyValuePair<string, string>> kcopyConfigs;
        private static List<KeyValuePair<string, string>> kirokuConfigs;

        static void Main(string[] args)
        {
            using (Deserializer deserilaizer = new Deserializer())
            {
                var _file = Directory.GetCurrentDirectory() + @"\Config.ini";

                deserilaizer.Execute(_file);

                kcopyConfigs = deserilaizer.GetTag("kcopy");

                kirokuConfigs = deserilaizer.GetTag("kiroku");
            }

            if (KCopyManager.Initialize(kcopyConfigs, kirokuConfigs))
            {
                Console.WriteLine($"Configs loaded.");
                if (KCopyManager.Execute())
                {
                    Console.WriteLine($"KCopy executed.");
                }
                else
                {
                    Console.WriteLine($"KCopy failed.");
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
