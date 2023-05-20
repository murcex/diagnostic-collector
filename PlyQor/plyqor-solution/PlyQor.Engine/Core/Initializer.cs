namespace PlyQor.Engine.Core
{
    using Newtonsoft.Json;
    using PlyQor.Engine.Components.Storage;
    using PlyQor.Engine.Resources;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Initializer
    {
        public static bool Execute()
        {
            SetApplication();

            SetAppliances();

            return true;
        }

        private static bool SetApplication()
        {
            var plyqor_configuration = Environment.GetEnvironmentVariable("PLYQOR_CFG", EnvironmentVariableTarget.Process);

            Configuration.Load(plyqor_configuration);

            var containers = GetContainerConfigurations();

            Configuration.LoadContainers(containers);

            return true;
        }

        private static bool SetAppliances()
        {
            return true;
        }

        public static Dictionary<string, Dictionary<string, string>> GetContainerConfigurations()
        {
            var containers_json = StorageProvider.SelectKey(InitializerValues.SystemContainer, InitializerValues.ContainersValue);

            var containers = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(containers_json);

            // ensure all keys are ToUpper for TryGet
            containers = containers.ToDictionary(x => x.Key.ToUpper(), x => x.Value);

            return containers;
        }
    }
}
