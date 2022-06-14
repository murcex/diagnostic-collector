namespace KQuery.Audit
{
    using Implements.Deserializer;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.IO;

    class Program
    {
        private static List<KeyValuePair<string, string>> kloadConfigs;
        private static List<KeyValuePair<string, string>> kirokuConfigs;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (Deserializer deserilaizer = new Deserializer())
            {
                var _file = Directory.GetCurrentDirectory() + @"\Config.ini";

                deserilaizer.Execute(_file);

                kloadConfigs = deserilaizer.GetTag("kquery");

                kirokuConfigs = deserilaizer.GetTag("kiroku_kload");
            }

            //
        }
    }
}
