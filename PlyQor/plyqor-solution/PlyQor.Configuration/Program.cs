namespace PlyQor.AdminTool
{
    using PlyQor.Configurator.Operations;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("--- PlyQor Container Config Manager ---");

                if (LoadLocalConfig.Execute())
                {
                    var (configuration_type, container_config_name) = CheckConfigurationType.Execute(args);

                    switch (configuration_type)
                    {
                        case ConfigurationType.ReadOnly:
                            ReadContainerConfig.Execute();
                            break;
                        case ConfigurationType.Template:
                            CreateTemplateConfig.Execute();
                            break;
                        case ConfigurationType.Modify:
                            ModifyContainerConfig.Execute(container_config_name);
                            break;

                        default:
                            ReadContainerConfig.Execute();
                            break;
                    }

                    Console.WriteLine($"\r\tSession complete");
                }
                else
                {
                    Console.WriteLine($"Failed to load Config.ini, ending session");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }
    }
}