namespace Configurator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    class CfgIO
    {
        /// <summary>
        /// Read the local Config. Supports Local and Azure Function execution.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="azure"></param>
        public static List<string> ReadCfg(string app = null, bool azure = false)
        {
            List<string> lines = new List<string>();

            string fileName = Directory.GetCurrentDirectory() + Constants.ConfigFile;
            if (azure && !string.IsNullOrEmpty(app))
            {
                fileName = Constants.AzureRoot + app + Constants.ConfigFile;
            }
            else if (azure && string.IsNullOrEmpty(app))
            {
                fileName = Constants.DefaultAzureRoot;
            }

            try
            {
                byte[] document = File.ReadAllBytes(fileName);

                var singleString = Encoding.UTF8.GetString(document, 0, document.Length);

                string[] lineArray = singleString.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                var lineList = lineArray.ToList();

                lines.AddRange(lineList);
            }
            catch
            {
                string error = string.Empty;

                error = azure ? Errors.READCFG_AZURE_EX : Errors.READCFG_LOCAL_EX;

                lines.Add(error);
            }

            return lines;
        }

        /// <summary>
        /// Write the config locally. Supports Local and Azure Function execution.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="lines"></param>
        public static string WriteCfg(string app, List<string> lines)
        {
            string fileName = string.Empty;

            string result = string.Empty;

            if (!string.IsNullOrEmpty(app))
            {
                fileName = Constants.AzureRoot + app + Constants.ConfigFile;
            }
            else
            {
                fileName = Constants.DefaultAzureRoot;
            }

            try
            {
                using (StreamWriter file = new StreamWriter(fileName, true))
                {
                    foreach (var line in lines)
                    {
                        file.WriteLine(line);
                    }
                }

                result = Constants.PASS;
            }
            catch
            {
                result = Errors.WRITECFG_EX;
            }

            return result;
        }
    }
}
