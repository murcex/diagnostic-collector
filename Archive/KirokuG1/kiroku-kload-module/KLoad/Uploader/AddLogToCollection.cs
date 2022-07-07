namespace KLoad
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    // Kiroku
    using Kiroku;

    /// <summary>
    /// Add log line to log collection.
    /// </summary>
    public static class AddLogToCollection
    {
        public static bool Execute(string line, List<LogRecordModel> recordModelList, KLog uploaderLog)
        {
            try
            {
                var record = JsonConvert.DeserializeObject<LogRecordModel>(line);

                if (CheckWriteByType(record.LogType))
                {
                    recordModelList.Add(record);
                }
            }
            catch
            {
                uploaderLog.Error($"Uploader [AddLogToCollection] => Line Exception: {line}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if the log Type can be written to database.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool CheckWriteByType(string type)
        {
            switch (type.ToLower())
            {
                case "trace":
                    return Configuration.Trace ? true : false;

                case "info":
                    return Configuration.Info ? true : false;

                case "warning":
                    return Configuration.Warning ? true : false;

                case "error":
                    return Configuration.Error ? true : false;

                case "metric":
                    return Configuration.Metric ? true : false;

                case "result":
                    return Configuration.Result ? true : false;

                default:
                    {
                        return true;
                    }
            }
        }
    }


}
