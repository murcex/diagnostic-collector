namespace PlyQor.Engine
{
    using Newtonsoft.Json;
    using PlyQor.Engine.Components.Maintenance;
    using PlyQor.Engine.Components.Query;
    using PlyQor.Engine.Components.Validation;
    using PlyQor.Engine.Core;
    using PlyQor.Models;
    using PlyQor.Resources;
    using System;
    using System.Collections.Generic;

    public class PlyQorManager
    {
        public static bool Initialize()
        {
            return Initializer.Execute();
        }

        public static string Query(string query)
        {
            var traceId = Guid.NewGuid().ToString();
            Dictionary<string, string> resultDictionary = new Dictionary<string, string>();

            using (PlyQorTrace trace = new PlyQorTrace(Configuration.DatabaseConnection, traceId))
            {
                try
                {
                    var requestDictionary = ValidationProvider.GenerateDictionary(query);

                    RequestManager requestManager = new RequestManager(requestDictionary);

                    var token = requestManager.GetRequestStringValue(RequestKeys.Token);

                    var container = requestManager.GetRequestStringValue(RequestKeys.Container);

                    trace.AddContainer(container);

                    ValidationProvider.CheckToken(container, token);

                    var operation = requestManager.GetRequestStringValue(RequestKeys.Operation);

                    trace.AddOperation(operation);

                    ValidationProvider.CheckOperation(operation);

                    resultDictionary = ExecuteQuery(operation, requestManager);
                }
                catch (PlyQorException javelinException)
                {
                    if (javelinException.Message == StatusCode.ERRMALFORM)
                    {
                        trace.AddCode(StatusCode.ERRMALFORM);
                        return StatusCode.ERR400;
                    }

                    if (javelinException.Message == StatusCode.ERRBLOCK)
                    {
                        trace.AddCode(StatusCode.ERRBLOCK);
                        return StatusCode.ERR401;
                    }

                    resultDictionary.Add(ResultKeys.Status, ResultValues.False);
                    resultDictionary.Add(ResultKeys.Code, javelinException.Message);
                    trace.AddCode(javelinException.Message);
                }

                resultDictionary.Add(ResultKeys.Trace, traceId);
                var result = JsonConvert.SerializeObject(resultDictionary);
                return result;
            }
        }

        // TODO: move to validation class?
        /// <summary>
        /// Submit request to query engine for execution.
        /// </summary>
        private static Dictionary<string, string> ExecuteQuery(
            string operation,
            RequestManager request)
        {
            return operation switch
            {
                // TODO: (switch) move literal string to const
                "InsertKey" => QueryProvider.InsertKey(request),
                "InsertTag" => QueryProvider.InsertTag(request),

                "SelectKey" => QueryProvider.SelectKey(request),
                "SelectTags" => QueryProvider.SelectTags(request),
                "SelectTagCount" => QueryProvider.SelectTagCount(request),
                "SelectKeyList" => QueryProvider.SelectKeyList(request),
                "SelectKeyTags" => QueryProvider.SelectKeyTags(request),

                "UpdateKey" => QueryProvider.UpdateKey(request),
                "UpdateData" => QueryProvider.UpdateData(request),
                "UpdateTag" => QueryProvider.UpdateTag(request),
                "UpdateKeyTag" => QueryProvider.UpdateKeyTag(request),

                "DeleteKey" => QueryProvider.DeleteKey(request),
                "DeleteTag" => QueryProvider.DeleteTag(request),
                "DeleteKeyTags" => QueryProvider.DeleteKeyTags(request),
                "DeleteKeyTag" => QueryProvider.DeleteKeyTag(request),

                // TODO: create a failure dict for return
                _ => new Dictionary<string, string>(),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool Maintenance()
        {
            var containers = Configuration.Containers;

            Console.WriteLine($"DataRetentionPolicy");
            foreach (var policy in Configuration.DataRetentionPolicy)
            {
                Console.WriteLine($"{policy.Key} => {policy.Value}");
            }

            Console.WriteLine($"TraceRetentionPolicy");
            foreach (var policy in Configuration.TraceRetentionPolicy)
            {
                Console.WriteLine($"{policy.Key} => {policy.Value}");
            }

            Console.WriteLine($"RetentionCapacity: {Configuration.RetentionCapacity}");
            Console.WriteLine($"RetentionCycle: {Configuration.RetentionCycle}");
            Console.WriteLine($"SystemTraceRetention: {Configuration.SystemTraceRetention}");

            // ---

            //Console.WriteLine($"\r\t..PreMaintenanceCollection");
            //PreMaintenanceCollection.Execute();

            // ---

            foreach (var container in containers)
            {
                DataRetention.Execute(container);

                TraceRetention.Execute(container);
            }

            TraceRetention.Execute("SYSTEM");

            // ---

            //Console.WriteLine($"\r\t..PostMaintenanceCollection");
            //PostMaintenanceCollection.Execute();

            //GeneralPerformance.Execute();

            return true;
        }
    }
}
