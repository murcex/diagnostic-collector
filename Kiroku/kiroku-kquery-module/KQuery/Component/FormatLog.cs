namespace KQuery.Component
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    class FormatLog
    {
        /// <summary>
        /// Convert a raw KLOG document for into a properly formatted JSON document.
        /// </summary>
        public static string Execute(string logInput)
        {
            logInput = logInput.Replace("#KLOG_INSTANCE_STATUS#", "");

            logInput = logInput.Replace("}", "},");

            logInput = "[" + logInput + "]";

            logInput = logInput.Replace("},\r\n$", "}]");

            logInput = JValue.Parse(logInput).ToString(Formatting.Indented);

            return logInput;
        }
    }
}
