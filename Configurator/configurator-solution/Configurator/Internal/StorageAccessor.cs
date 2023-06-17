namespace Configurator.Processor
{
    using Configurator.Core;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    class StorageAccessor
    {
        /// <summary>
        /// Read Config.ini from Azure Function storage
        /// </summary>
        public static (bool result, List<string> cfgLines) ReadConfig(bool ex = false)
        {
            List<string> lines = new List<string>();

            try
            {
                byte[] document = File.ReadAllBytes(Configuration.DefaultAzureRootConfig);

                var singleString = Encoding.UTF8.GetString(document, 0, document.Length);

                string[] lineArray = singleString.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                var lineList = lineArray.ToList();

                lines.AddRange(lineList);
            }
            catch
            {
                if (ex)
                {
                    throw new Exception(Configuration.READCFG_EX);
                }

                return (false, lines);
            }

            return (true, lines);
        }

        /// <summary>
        /// Write Config.ini to Azure Function storage
        /// </summary>
        public static bool WriteConfig(List<string> lines)
        {
            try
            {
                if (!Directory.Exists(Configuration.DefaultAzureRoot))
                {
                    Directory.CreateDirectory(Configuration.DefaultAzureRoot);
                }

                using (StreamWriter writer = new(Configuration.DefaultAzureRootConfig, true))
                {
                    foreach (var line in lines)
                    {
                        writer.WriteLine(line);
                    }
                }

                return true;
            }
            catch
            {
                throw new Exception(Configuration.WRITECFG_EX);
            }
        }
    }
}
