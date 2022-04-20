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

            // TODO: move literal string to const
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

        public static List<string> GetPlyList(this Dictionary<string, string> result)
        {
            List<string> list = new List<string>();

            try
            {
                if (result.TryGetValue(ResultKeys.Data, out string output))
                {
                    list = JsonConvert.DeserializeObject<List<string>>(output);
                }

                return list;
            }
            catch (Exception e)
            {
                return list;
            }
        }

        public static string GetPlyRecord(this Dictionary<string, string> result)
        {
            return JsonConvert.SerializeObject(result);
        }
    }
}
