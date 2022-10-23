namespace PlyQor.Engine
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using PlyQor.Engine.Core;
    using PlyQor.Models;
    using PlyQor.Resources;
    using PlyQor.Engine.Components.Query;
    using PlyQor.Engine.Components.Validation;
    using PlyQor.Engine.Resources;
    using PlyQor.Engine.Components.Maintenance;

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

        #region OldRetention
        // TODO: switch to maintenance
        // --- foreach container ---
        // - data retention (new rate limit)
        // - count total records; container level and count tags
        // - count operations for each container, for each operation, for the day
        // - trace retention by container

        //public static bool Retention()
        //{
        //    // replace with global container-retention list
        //    var containers = Configuration.DataRetentionPolicy;

        //    // foreach container
        //    foreach (var container in containers)
        //    {
        //        var dataRetentionActivityId = Guid.NewGuid().ToString();

        //        using (PlyQorTrace trace = new PlyQorTrace(Configuration.DatabaseConnection, dataRetentionActivityId))
        //        {
        //            trace.AddContainer(container.Key);
        //            trace.AddOperation(PlyQorManagerValues.DataRetentionOperation);

        //            try
        //            {
        //                // build request dictionary
        //                Dictionary<string, string> request = new Dictionary<string, string>();
        //                request.Add(RequestKeys.Container, container.Key);
        //                request.Add(RequestKeys.Aux, container.Value.ToString());
        //                // add: size -- from sys cfg
        //                // add: cooldown -- from sys cfg

        //                // build request
        //                RequestManager requestManager = new RequestManager(request);

        //                // execute
        //                QueryProvider.DataRetention(requestManager);
        //            }
        //            catch (PlyQorException javelinException)
        //            {
        //                trace.AddCode(javelinException.Message);
        //            }

        //            //trace.
        //        }
        //    }


        //    // new: move trace retention into each container, add container to request payload
        //    var activityId = Guid.NewGuid().ToString();

        //    using (PlyQorTrace trace = new PlyQorTrace(Configuration.DatabaseConnection, activityId))
        //    {
        //        trace.AddContainer(PlyQorManagerValues.SystemContainer);
        //        trace.AddOperation(PlyQorManagerValues.TraceRetentionOperation);

        //        try
        //        {
        //            // build request dictionary
        //            Dictionary<string, string> request = new Dictionary<string, string>();
        //            request.Add(RequestKeys.Aux, Configuration.SystemTraceRetention);
        //            // add: request.Add(RequestKeys.Container, container.Key);
        //            // add: request.Add(RequestKeys.Aux, container.Value.ToString());

        //            // build request
        //            RequestManager requestManager = new RequestManager(request);

        //            // execute
        //            QueryProvider.TraceRetention(requestManager);

        //        }
        //        catch (PlyQorException javelinException)
        //        {
        //            trace.AddCode(javelinException.Message);
        //        }
        //    }

        //    // new: maintenance method here

        //    return true;
        //}
        #endregion

        private static bool Maintenance()
        {
            var containers = Configuration.Containers;

            // ContainerRetention(containers);

            foreach (var container in containers)
            {
                DataRetention.Execute(container);

                TraceRetention.Execute(container);
            }

            DataCollection.Execute();

            GeneralPerformance.Execute();

            return true;
        }
    }
}
