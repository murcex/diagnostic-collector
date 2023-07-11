using Implements.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace Implements.Audit
{
    public class ConfigurationAudit
    {
        public static void Execute(bool execute)
        {
            if (!execute)
            {
                Console.WriteLine($"Audit: {nameof(ConfigurationAudit)} has not been marked for execution, skipping audit.");
                Console.WriteLine($"*** PRESS ANY KEY TO CONTINUE *** \r\n");
                Console.ReadKey();

                return;
            }

            var file = Path.Combine(Directory.GetCurrentDirectory(), "MyConfigFileV2.ini");

            var input = File.ReadAllText(file);

            Dictionary<string, Dictionary<string, string>> output = new();

            using (ConfigurationUtility configManage = new ConfigurationUtility())
            {
                output = configManage.Deserialize(input);
            }

            foreach (var element in output)
            {
                foreach (var component in element.Value)
                {
                    Console.WriteLine($"Element:{element.Key}, Component Key:{component.Key}, Componment Value:{component.Value}");
                }

                Console.WriteLine("");
            }

            Console.WriteLine("");

            string config = string.Empty;

            using (ConfigurationUtility configManage = new ConfigurationUtility())
            {
                config = configManage.Serialize(output);
            }

            Console.WriteLine(config.ToString());
        }
    }
}
