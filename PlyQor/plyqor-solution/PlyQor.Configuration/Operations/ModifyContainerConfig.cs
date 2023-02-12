namespace PlyQor.Configurator.Operations
{
    using Implements.Deserializer;
    using Newtonsoft.Json;
    using PlyQor.AdminTool;

    public class ModifyContainerConfig
    {
        /// <summary>
        /// Load, convernt and upsert local container config to PlyQor System container. 
        /// Otherwise query PlyQor System container for current container config.
        /// </summary>
        public static void Execute(string file_name)
        {
            ReadContainerConfig.Execute(true);

            string config_filepath = Path.Combine(Directory.GetCurrentDirectory(), file_name);

            if (File.Exists(config_filepath))
            {
                Dictionary<string, Dictionary<string, string>> container_configs = new Dictionary<string, Dictionary<string, string>>();

                using (Deserializer deserializer = new Deserializer())
                {
                    deserializer.Execute(config_filepath);

                    // get container list
                    var containers = deserializer.GetCollection();

                    // process config for each container
                    foreach (var container in containers)
                    {
                        Dictionary<string, string> container_config = new Dictionary<string, string>();

                        string name = string.Empty;

                        foreach (var kvp in container.Value)
                        {
                            // name
                            if (kvp.Key.ToUpper() == "NAME")
                            {
                                name = kvp.Value;
                            }

                            // retention
                            if (kvp.Key.ToUpper() == "RETENTION")
                            {
                                container_config.Add("Retention", kvp.Value);
                            }

                            // trace
                            if (kvp.Key.ToUpper() == "TRACE")
                            {
                                container_config.Add("Trace", kvp.Value);
                            }

                            // tokens
                            if (kvp.Key.ToUpper() == "TOKENS")
                            {
                                container_config.Add("Tokens", Utility.CreateStringOfTokens(kvp.Value));
                            }

                            // capacity
                            if (kvp.Key.ToUpper() == "CAPACITY")
                            {
                                container_config.Add("Capacity", kvp.Value);
                            }

                            // cycle
                            if (kvp.Key.ToUpper() == "CYCLE")
                            {
                                container_config.Add("Cycle", kvp.Value);
                            }
                        }

                        container_configs.Add(name, container_config);
                    }
                }

                var s_container_configs = JsonConvert.SerializeObject(container_configs);

                // validate config?

                if (Storage.UpsertContainerConfig(s_container_configs))
                {
                    Console.WriteLine($"");
                }
                else
                {
                    Console.WriteLine($"");
                }
            }
            else
            {
                Console.WriteLine($"<!> Provide Container config doesn't exist");
            }
        }
    }
}
