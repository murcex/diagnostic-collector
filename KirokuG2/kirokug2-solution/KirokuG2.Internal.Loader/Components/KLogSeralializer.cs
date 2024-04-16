using KirokuG2.Internal.Loader.Interface;

namespace KirokuG2.Internal.Loader.Components
{
    public class KLogSeralializer : IKLogSeralializer
    {
        public KLogSeralializer()
        {

        }

        public Dictionary<string, (List<string> logs, string index)> DeseralizalizeLogSet(string id, string rawLog)
        {
            var rawLogAsLines = ConvertToLines(rawLog);

            Dictionary<string, (List<string> logs, string index)> logSet = new();

            var isFirst = true;
            var count = 0;
            var isFirstMultiLog = true;
            var tempIndex = string.Empty;
            var tempLog = new List<string>();
            foreach (var line in rawLogAsLines)
            {
                if (isFirst)
                {
                    if (line.StartsWith("##multilog-start=", StringComparison.OrdinalIgnoreCase))
                    {
                        _ = int.TryParse(line.Replace("##multilog-start=", string.Empty), out count);
                        isFirst = false;

                        continue;
                    }
                    else
                    {
                        logSet[id] = (rawLogAsLines, string.Empty);

                        return logSet;
                    };
                }
                else
                {
                    if (line.StartsWith("#index=") || line.StartsWith("##multilog-end"))
                    {
                        if (!isFirstMultiLog)
                        {
                            // add log to log set
                            logSet[$"{id}.{tempIndex}"] = (new List<string>(tempLog), tempIndex);

                            // clean old temp log
                            tempLog.Clear();
                        }
                        else
                        {
                            isFirstMultiLog = false;
                        }

                        // find index
                        tempIndex = GetIndex(line);
                    }
                    else
                    {
                        // add line as new document
                        tempLog.Add(line);
                    }
                }
            }

            return logSet;
        }

        /// <summary>
        /// Convert KLOG from a single string to list of strings for processing
        /// </summary>
        private static List<string> ConvertToLines(string input)
        {
            List<string> lines = new();

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

        private static string GetIndex(string line)
        {
            return line.Replace("##index=", "").Trim();
        }
    }
}
