namespace Configurator
{
    using Configurator.Core;
    using Configurator.Processor;
    using Implements.Configuration;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class ConfiguratorManager
    {
        /// <summary>
        /// Get the Config of an Azure Function operation by Azure Function name.
        /// (1) Checking if the config is already locally available on the Azure Function. 
        /// (2) If not, generates required metadata from Azure Function Environment Variables.
        /// (3) Then calling the Configurator Service API for the config which will be written locally.
        /// 
        /// Supports: default, in-memory and embedded modes.
        /// Default Mode = as perviously described.
        /// In-memory Mode = as perviously decribed, but the config is never written locally.
        /// Embedded Mode = config is read from the Configurator Environment Variable.
        /// </summary>
        /// <returns>Config.ini in List of strings.</returns>
        public static Dictionary<string, Dictionary<string, string>> Execute()
        {
            List<string> configuratorLines = new();

            // get environment var parameters
            var configuratorApp = Environment.GetEnvironmentVariable(Configuration.WEBSITE_SITE_NAME);

            if (string.IsNullOrEmpty(configuratorApp))
            {
                throw new Exception(string.Format(Configuration.NULLEMPTY, Configuration.WEBSITE_SITE_NAME));
            }

            var output = Environment.GetEnvironmentVariable(Configuration.PYLON_CFG);

            if (string.IsNullOrEmpty(output))
            {
                throw new Exception(string.Format(Configuration.NULLEMPTY, Configuration.PYLON_CFG));
            }

            // embedded execution
            if (output.Contains(Configuration.Embedded))
            {
                var embeddedCfg = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(output);

                return embeddedCfg;
            }

            // get configurator parameters
            var pylonCfg = output.Split(',');

            if (pylonCfg.Length < 2)
            {
                throw new ArgumentException(Configuration.PYLON_CFG_MINIMUM_LENGTH);
            }

            var baseConfiguratorUri = pylonCfg[0].ToString();

            if (string.IsNullOrEmpty(baseConfiguratorUri))
            {
                throw new Exception(string.Format(Configuration.NULLEMPTY, Configuration.BASECFGURI));
            }

            var configuratorKey = pylonCfg[1].ToString();

            if (string.IsNullOrEmpty(configuratorKey))
            {
                throw new Exception(string.Format(Configuration.NULLEMPTY, Configuration.CFGKEY));
            }

            var pylonSettings = string.Empty;
            if (pylonCfg.Length > 2)
            {
                pylonSettings = pylonCfg[2].ToString();
            }

            // build uri
            var configuratorUri = string.Format(Configuration.ConfiguratorUri, baseConfiguratorUri, configuratorKey, configuratorApp);

            // in-memory execution
            if (!string.IsNullOrEmpty(pylonSettings))
            {
                if (string.Equals(pylonSettings, Configuration.MemoryOnly, StringComparison.OrdinalIgnoreCase))
                {
                    configuratorLines = Transmitter.Execute(configuratorUri);

                    return ConvertConfigToDictionary(configuratorLines);
                }
            }

            // default execution
            var readOperation = StorageAccessor.ReadConfig();

            if (readOperation.result)
            {
                return ConvertConfigToDictionary(readOperation.cfgLines);
            }

            configuratorLines = Transmitter.Execute(configuratorUri);

            StorageAccessor.WriteConfig(configuratorLines);

            readOperation = StorageAccessor.ReadConfig(ex: true);

            return ConvertConfigToDictionary(readOperation.cfgLines);
        }

        private static Dictionary<string, Dictionary<string, string>> ConvertConfigToDictionary(List<string> input)
        {
            using ConfigurationUtility configurationUtility = new();
            return configurationUtility.Deserialize(input);
        }
    }
}
