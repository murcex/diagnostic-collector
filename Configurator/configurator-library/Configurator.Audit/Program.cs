namespace Configurator.Audit
{
    using System;
    using Configurator;

    class Program
    {
        static void Main(string[] args)
        {
            string app = "testApp";

            var lines = CfgManager.GetCfg(app);

            if (lines.Count > 0)
            {
                Console.WriteLine($"Config:");

                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                }
            }

            Console.ReadKey();
        }
    }
}