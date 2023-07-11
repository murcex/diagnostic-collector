namespace PlyQor.Engine
{
    using PlyQor.Engine.Components.Query;
    using PlyQor.Engine.Components.Validation;
    using PlyQor.Engine.Core;
    using PlyQor.Engine.Resources;
    using PlyQor.Models;
    using PlyQor.Resources;
    using System;
    using System.Collections.Generic;
    using System.Text.Json;

    public class PlyQorManager
    {
        public static bool Initialize(Dictionary<string, Dictionary<string, string>> configuration)
        {
            if (configuration == null || configuration.Count == 0)
            {
                // TODO: hold for pylon replacement
                throw new Exception("PlyQorManager Initialize Failure");
            }

            return Initializer.Execute(configuration);
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
                var result = JsonSerializer.Serialize(resultDictionary);
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

                _ => new Dictionary<string, string>(),
            };
        }

        public static bool Retention()
        {
            // replace with global container-retention list
            var containers = Configuration.RetentionPolicy;

            // foreach container
            foreach (var container in containers)
            {
                var dataRetentionActivityId = Guid.NewGuid().ToString();

                using (PlyQorTrace trace = new PlyQorTrace(Configuration.DatabaseConnection, dataRetentionActivityId))
                {
                    trace.AddContainer(container.Key);
                    trace.AddOperation(PlyQorManagerValues.DataRetentionOperation);

                    try
                    {
                        // build request dictionary
                        Dictionary<string, string> request = new Dictionary<string, string>();
                        request.Add(RequestKeys.Container, container.Key);
                        request.Add(RequestKeys.Aux, container.Value.ToString());

                        // build request
                        RequestManager requestManager = new RequestManager(request);

                        // execute
                        QueryProvider.DataRetention(requestManager);
                    }
                    catch (PlyQorException javelinException)
                    {
                        trace.AddCode(javelinException.Message);
                    }
                }
            }

            var activityId = Guid.NewGuid().ToString();

            using (PlyQorTrace trace = new PlyQorTrace(Configuration.DatabaseConnection, activityId))
            {
                trace.AddContainer(PlyQorManagerValues.SystemContainer);
                trace.AddOperation(PlyQorManagerValues.TraceRetentionOperation);

                try
                {
                    // build request dictionary
                    Dictionary<string, string> request = new Dictionary<string, string>();
                    request.Add(RequestKeys.Aux, Configuration.TraceRetention);

                    // build request
                    RequestManager requestManager = new RequestManager(request);

                    // execute
                    QueryProvider.TraceRetention(requestManager);

                }
                catch (PlyQorException javelinException)
                {
                    trace.AddCode(javelinException.Message);
                }
            }

            return true;
        }
    }
}
