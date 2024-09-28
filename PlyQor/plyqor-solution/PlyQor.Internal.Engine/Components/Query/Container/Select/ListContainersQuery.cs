using PlyQor.Engine.Resources;
using PlyQor.Internal.Engine.Components.Storage;
using PlyQor.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace PlyQor.Internal.Engine.Components.Query.Container.Select
{
    public class ListContainersQuery
    {
        public static Dictionary<string, string> Execute(RequestManager requestManager)
        {
            ResultManager resultManager = new ResultManager();

            // -- get values from request --
            // not required

            // -- execute internal query --
            // get container config
            var json = StorageProvider.SelectKey(InitializerValues.SystemContainer, InitializerValues.ContainersValue);

            var containerConfigurations = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json);

            var containerNames = containerConfigurations.Keys.ToList();

            var data = string.Join(",", containerNames);

            // -- build result --
            resultManager.AddResultData(data);
            resultManager.AddResultSuccess();

            return resultManager.ExportDataSet();
        }
    }
}
