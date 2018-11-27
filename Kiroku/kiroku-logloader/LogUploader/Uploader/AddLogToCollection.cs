namespace KLOGLoader
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    // Kiroku
    using Kiroku;

    public static class AddLogToCollection
    {
        public static bool Execute(string line, List<LogRecordModel> recordModelList, KLog uploaderLog)
        {
            try
            {
                var record = JsonConvert.DeserializeObject<LogRecordModel>(line);

                if (record.LogData.Length > Global.MessageLength)
                {
                    var messageCap = Global.MessageLength - 20;
                    var cleanLogData = "[ERROR-MAX-" + Global.MessageLength + "]";
                    cleanLogData += record.LogData.Substring(1, messageCap);
                    record.LogData = cleanLogData;
                    record.LogType = "Error";
                }

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

                default:
                    {
                        return true;
                    }
            }
        }
    }


}
