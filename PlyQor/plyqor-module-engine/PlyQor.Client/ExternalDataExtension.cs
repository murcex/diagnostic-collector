namespace PlyQor.Client
{
    using Newtonsoft.Json;
    using PlyQor.Client.Resources;

    public static class ExternalDataExtension
    {
        public static string GetPlyData(this Dictionary<string, string> result)
        {
            result.TryGetValue(ResultKeys.Data, out string output);

            return output;
        }

        public static bool GetPlyStatus(this Dictionary<string, string> result)
        {
            if (result.TryGetValue(ResultKeys.Status, out string output))
            {
                if (bool.TryParse(output, out bool status))
                {
                    return status;
                }
            }

            throw new Exception("Status is not bool");
        }

        public static string GetPlyCode(this Dictionary<string, string> result)
        {
            result.TryGetValue(ResultKeys.Code, out string output);

            return output;
        }

        public static string GetPlyTrace(this Dictionary<string, string> result)
        {
            result.TryGetValue(ResultKeys.ActivityId, out string output);

            return output;
        }

        public static List<string> GetPlyList(this string data)
        {
            return JsonConvert.DeserializeObject<List<string>>(data);
        }
    }
}
