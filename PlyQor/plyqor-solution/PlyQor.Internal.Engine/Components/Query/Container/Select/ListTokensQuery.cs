using PlyQor.Engine.Resources;
using PlyQor.Internal.Engine.Components.Storage;
using PlyQor.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace PlyQor.Internal.Engine.Components.Query.Container.Select
{
    public class ListTokensQuery
    {
        public static Dictionary<string, string> Execute(RequestManager requestManager)
        {
            ResultManager resultManager = new();

            try
            {
                // -- get values from request --
                var container = requestManager.GetRequestStringValue("Container");

                // -- execute internal query --
                // get container config
                var json = StorageProvider.SelectKey(InitializerValues.SystemContainer, InitializerValues.ContainersValue);

                var containerConfigurations = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json);

                if (containerConfigurations.TryGetValue(container, out var test))
                {
                    if (test.TryGetValue("Tokens", out var jsonTokens))
                    {
                        var tokens = JsonSerializer.Deserialize<List<string>>(jsonTokens);

                        var data = string.Join(",", tokens);

                        resultManager.AddResultData(data);
                        resultManager.AddResultSuccess();
                    }
                    else
                    {
                        resultManager.AddResultData("NOHIT_Tokens");
                        resultManager.AddResultSuccess();
                    }
                }
                else
                {
                    resultManager.AddResultData($"NOHIT_{container}");
                    resultManager.AddResultSuccess();
                }
            }
            catch (Exception ex)
            {
                resultManager.AddResultData(ex.Message);
                resultManager.AddResultSuccess();
            }

            return resultManager.ExportDataSet();
        }
    }
}
