namespace PlyQor.Configurator.Operations
{
    using Implements.Deserializer;
    using PlyQor.AdminTool;

    public class LoadLocalConfig
    {
        /// <summary>
        /// Load the local config.
        /// </summary>
        public static bool Execute()
        {
            string config = Path.Combine(Directory.GetCurrentDirectory(), $"Config.ini");

            if (File.Exists(config))
            {
                Console.WriteLine($"<!> Config.ini found, trying to deserialize");

                using (Deserializer deserializer = new Deserializer())
                {
                    deserializer.Execute(config);

                    var database_connection = deserializer.GetValue(Configuration.Admin, Configuration.Database);

                    if (string.IsNullOrEmpty(database_connection))
                    {
                        throw new Exception($"database_connection IsNullOrEmpty");
                    }

                    Configuration.DatabaseConnection = database_connection;
                }

                return true;
            }
            else
            {
                Console.WriteLine($"<!> Config.ini doesn't exist, creating empty file");

                var contents = Utility.CreateEmptyConfig();

                File.AppendAllText(config, contents);

                return false;
            }
        }
    }
}
