namespace Configurator.Core
{
    public class Configuration
    {
        // ------ Constants ------

        /// <summary>
        /// Azure Function environment variable source
        /// </summary>
        public static string WEBSITE_SITE_NAME { get; } = "WEBSITE_SITE_NAME";

        /// <summary>
        /// Configurator environment variable
        /// </summary>
        public static string PYLON_CFG { get; } = "PYLON_CFG";

        /// <summary>
        /// Configurator base URI
        /// </summary>
        public static string BASECFGURI { get; } = "BASECFGURI";

        /// <summary>
        /// Configurator access key
        /// </summary>
        public static string CFGKEY { get; } = "CFGKEY";

        /// <summary>
        /// Configurator mode: embedded
        /// </summary>
        public static string Embedded { get; } = "embedded";

        /// <summary>
        /// Configurator mode: memory-only
        /// </summary>
        public static string MemoryOnly { get; } = "memoryonly";

        /// <summary>
        /// Azure Function Config directory
        /// </summary>
        public static string DefaultAzureRoot { get; } = @"D:\home\data\app\cfg";

        /// <summary>
        /// Azure Function Config.ini filepath
        /// </summary>
        public static string DefaultAzureRootConfig { get; } = @"D:\home\data\app\cfg\Config.ini";

        /// <summary>
        /// Formattable Configurator URI 
        /// </summary>
        public static string ConfiguratorUri { get; } = "{0}/api/Configurator?key={1}&app={2}";

        // ------ Errors ------

        /// <summary>
        /// Exception while trying to read the Config.ini
        /// </summary>
        public static string READCFG_EX { get; } = "ERROR:READCFG_EX";

        /// <summary>
        /// Exception while trying to write the Config.ini
        /// </summary>
        public static string WRITECFG_EX { get; } = "ERROR:WRITECFG_EX";

        /// <summary>
        /// Formattable IsNullOrEmpty error template
        /// </summary>
        public static string NULLEMPTY { get; } = "ERROR:{0}_NULLEMPTY";

        /// <summary>
        /// Exception while calling the configurator storage service
        /// </summary>
        public static string CALLCFG_EXCEPTION { get; } = "ERROR:CALLCFG_EXCEPTION";

        /// <summary>
        /// Configurator environment variable does not contain the minimum amount of values
        /// </summary>
        public static string PYLON_CFG_MINIMUM_LENGTH { get; } = "ERROR:PYLON_CFG_MINIMUM_LENGTH";

        /// <summary>
        /// Base Error template
        /// </summary>
        public static string CALLCFG_EMPTY { get; } = "ERROR:";
    }
}
