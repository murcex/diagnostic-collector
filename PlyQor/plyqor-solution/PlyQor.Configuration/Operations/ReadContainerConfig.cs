namespace PlyQor.Configurator.Operations
{
    using Newtonsoft.Json;
    using PlyQor.AdminTool;
    using System.Text;

    public class ReadContainerConfig
    {
        /// <summary>
        /// Create a local copy of the PlyQor container config from the serialized Json string.
        /// </summary>
        public static void Execute(bool backup = false)
        {
            var container_config = Storage.SelectContainerConfig();

            if (string.IsNullOrEmpty(container_config))
            {
                Console.WriteLine($"<!> Container config is empty, create a template and upload the container config");
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();

                var containers = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(container_config);

                foreach (var container in containers)
                {
                    var title = $"[{container.Key}]";

                    var name = $"name={container.Key}";

                    var retention = string.Empty;

                    var tokens = string.Empty;

                    var trace = string.Empty;

                    var capacity = string.Empty;

                    var cycle = string.Empty;

                    foreach (var kvp in container.Value)
                    {
                        if (kvp.Key.ToUpper() == "RETENTION")
                        {
                            retention = $"retention={kvp.Value}";
                        }

                        if (kvp.Key.ToUpper() == "TOKENS")
                        {
                            var token_list = JsonConvert.DeserializeObject<List<string>>(kvp.Value);

                            foreach (var token in token_list)
                            {
                                tokens += $"{token},";
                            }

                            tokens = tokens.TrimEnd(',');

                            tokens = $"tokens={tokens}";
                        }

                        if (kvp.Key.ToUpper() == "TRACE")
                        {
                            trace = $"trace={kvp.Value}";
                        }

                        if (kvp.Key.ToUpper() == "CAPACITY")
                        {
                            capacity = $"capacity={kvp.Value}";
                        }

                        if (kvp.Key.ToUpper() == "CYCLE")
                        {
                            cycle = $"cycle={kvp.Value}";
                        }
                    }

                    if (container.Key.ToUpper() == "SYSTEM")
                    {
                        stringBuilder.AppendLine(title);
                        stringBuilder.AppendLine(name);
                        stringBuilder.AppendLine(trace);
                        stringBuilder.AppendLine(capacity);
                        stringBuilder.AppendLine(cycle);
                        stringBuilder.AppendLine("");
                    }
                    else
                    {
                        stringBuilder.AppendLine(title);
                        stringBuilder.AppendLine(name);
                        stringBuilder.AppendLine(retention);
                        stringBuilder.AppendLine(tokens);
                        stringBuilder.AppendLine("");
                    }
                }

                string file_name = string.Empty;
                if (backup)
                {
                    file_name = $"{Guid.NewGuid()}-BAK-{DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss")}.ini";
                }
                else
                {
                    file_name = $"{Guid.NewGuid()}-{DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss")}.ini";
                }

                var file_path = Path.Combine(Directory.GetCurrentDirectory(), file_name);

                File.AppendAllText(file_path, stringBuilder.ToString());
            }
        }
    }
}
