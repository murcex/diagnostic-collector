namespace KLOGLoader
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
                    return Global.Trace ? true : false;

                case "info":
                    return Global.Info ? true : false;

                case "warning":
                    return Global.Warning ? true : false;

                case "error":
                    return Global.Error ? true : false;

                case "metric":
                    return Global.Metric ? true : false;

                case "result":
                    return Global.Result ? true : false;

                default:
                    {
                        return true;
                    }
            }
        }
    }


}
