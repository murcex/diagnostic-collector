namespace KirokuG2.Loader
{
    using KirokuG2.Loader.Components;
    using PlyQor.Client;

    public class KLoaderManager
    {
        private static PlyClient _plyClient;

        public static bool Configuration(PlyClient plyClient, string sqlConnection)
        {
            if (plyClient == null)
            {
                throw new ArgumentNullException($"PlyClient is Null");
            }

            if (string.IsNullOrEmpty(sqlConnection))
            {
                throw new ArgumentNullException($"SQL Connection value is NullOrEmpty");
            }

            _plyClient = plyClient;

            var sql_activation = SQLProvider.Initialized(sqlConnection);

            return sql_activation;
        }

        /// <summary>
        /// Process KLOG's marked for uploading into Kiroku database
        /// </summary>
        public static bool ProcessLogs()
        {
            try
            {
                // get all id's for tag=upload
                var log_ids = _plyClient.Select("upload-1004", 100).GetPlyList();

                foreach (var log_id in log_ids)
                {
                    try
                    {
                        // get log instance
                        var log = _plyClient.Select(log_id).GetPlyData();

                        // convert line to lines
                        var log_lines = ConvertToLines(log);

                        var instance = log_id.ToUpper();
                        var source = "NotSet";
                        var function = "NotSet";
                        var normal_log_type = true;

                        // setup
                        LogInstance logInstance = new LogInstance(instance);
                        Dictionary<string, LogBlock> logBlocks = new Dictionary<string, LogBlock>();
                        List<LogError> logErrors = new List<LogError>();
                        List<LogMetric> logMetrics = new List<LogMetric>();

                        // foreach line
                        foreach (var log_line in log_lines)
                        {
                            var log_components = log_line.Split(',');

                            // timestamp
                            var datetime = DateTime.Parse(log_components[0]);

                            // log event type
                            var type = log_components[1].ToUpper();

                            // clear preffix data
                            var position = log_components[0].Count() + 1 + log_components[1].Count() + 1;

                            // log data contents
                            var data = log_line.Remove(0, position);

                            // start instance
                            if (type == "I")
                            {
                                var start_instance_components = data.Split('$');
                                source = start_instance_components[0];
                                function = start_instance_components[1];

                                logInstance.Update(
                                    datetime,
                                    source,
                                    function);
                            }

                            // stop instance
                            if (type == "SI")
                            {
                                logInstance.StopInstance(datetime);
                            }

                            // start block
                            if (type == "B")
                            {
                                var block_components = data.Split('$');
                                var block_tag = block_components[0].ToUpper();
                                var block_name = block_components[1];

                                LogBlock logBlock = new LogBlock(
                                    datetime,
                                    instance,
                                    block_tag,
                                    block_name);

                                logBlocks[block_tag] = logBlock;
                            }

                            // stop block
                            if (type == "SB")
                            {
                                var block_tag = data.ToUpper();

                                if (logBlocks.TryGetValue(block_tag, out LogBlock logBlockStop))
                                {
                                    logBlockStop.StopBlock(datetime);

                                    logBlocks[block_tag] = logBlockStop;
                                }
                                else
                                {
                                    throw new Exception($"Block not found during Stop Block dictionary lookup");
                                }
                            }

                            // error
                            if (type == "E")
                            {
                                LogError logError = new LogError
                                {
                                    Timestamp = datetime,
                                    Source = source,
                                    Function = function,
                                    Id = instance,
                                    Message = data
                                };

                                logErrors.Add(logError);
                            }

                            // metrics
                            if (type == "M")
                            {
                                var metric_components = data.Split('$');
                                var metric_type = metric_components[0];
                                var key = metric_components[1];
                                var value = metric_components[2];

                                LogMetric logMetric = new LogMetric
                                {
                                    Timestamp = datetime,
                                    Source = source,
                                    Function = function,
                                    Id = instance,
                                    Type = metric_type,
                                    Key = key,
                                    Value = value
                                };

                                logMetrics.Add(logMetric);
                            }

                            // activation
                            if (type == "A")
                            {
                                normal_log_type = false;

                                SQLProvider.InsertActivation(datetime, instance, data);
                            }

                            // critical
                            if (type == "C")
                            {
                                normal_log_type = false;

                                LogError logError = new LogError
                                {
                                    Timestamp = datetime,
                                    Id = instance
                                };

                                var activation_components = data.Split('$');
                                logError.Source = activation_components[0];

                                // clear preffix data
                                var activation_position = activation_components[0].Count() + 1;

                                // log data contents
                                logError.Message = data.Remove(0, activation_position);

                                SQLProvider.InsertCritical(logError);
                            }
                        }



                        // ------



                        // if the log isn't activation or critical
                        // act as a normal logging event that contains instance, blocks, errors, metrics, etc.
                        if (normal_log_type)
                        {
                            // insert instance with duration into sql, add source and function
                            if (!logInstance.Result)
                            {
                                var error_message = $"INSTANCE FAILURE";

                                var error_log = CreateErrorLog(
                                    logInstance.Start,
                                    source,
                                    function,
                                    instance,
                                    error_message);

                                logErrors.Add(error_log);
                            }

                            // insert blocks with duration
                            foreach (var logBlock in logBlocks)
                            {
                                if (!logBlock.Value.Result)
                                {
                                    var error_message = $"BLOCK FAILURE";

                                    var error_log = CreateErrorLog(
                                        logBlock.Value.Start,
                                        source,
                                        function,
                                        instance,
                                        error_message);

                                    logErrors.Add(error_log);
                                }

                                SQLProvider.InsertBlock(logBlock.Value);
                            }

                            logInstance.AddErrorCount(logErrors.Count);

                            SQLProvider.InsertInstance(logInstance);

                            // insert errors to sql
                            foreach (var logError in logErrors)
                            {
                                SQLProvider.InsertError(logError);
                            }

                            // insert metrics to sql
                            foreach (var logMetric in logMetrics)
                            {
                                SQLProvider.InsertMetric(logMetric);
                            }
                        }

                        _plyClient.UpdateTag(log_id, "upload-1004", "archive");
                    }
                    catch (Exception ex)
                    {
                        SQLProvider.InsertQuarantine(DateTime.UtcNow, log_id);

                        _plyClient.UpdateTag(log_id, "upload-1004", "quarantine");

                        Console.WriteLine($"{log_id} EXCEPTION: {ex}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SESSION EXCEPTION: {ex}");

                return false;
            }

            return true;
        }

        /// <summary>
        /// Convert KLOG from a single string to list of strings for processing
        /// </summary>
        private static List<string> ConvertToLines(string input)
        {
            List<string> lines = new List<string>();

            var line = string.Empty;
            using (var string_reader = new StringReader(input))
            {
                while ((line = string_reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines;
        }

        /// <summary>
        /// Create Error event inline with KLOG processing
        /// </summary>
        private static LogError CreateErrorLog(
            DateTime datetime,
            string source,
            string function,
            string id,
            string data)
        {
            LogError logError = new LogError
            {
                Timestamp = datetime,
                Source = source,
                Function = function,
                Id = id,
                Message = data
            };

            return logError;
        }
    }
}
