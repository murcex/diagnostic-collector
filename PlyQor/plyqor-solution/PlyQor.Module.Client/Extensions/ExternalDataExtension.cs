namespace PlyQor.Client
{
    using PlyQor.Client.Resources;
    using System.Text.Json;

    public static class ExternalDataExtension
    {
        public static string GetPlyData(this Dictionary<string, string> result)
        {
            result.TryGetValue(ResultKeys.Data, out string output);

            return output;
        }

        public static bool GetPlyStatus(this Dictionary<string, string> result)
        {
            string output = string.Empty;

            if (result.TryGetValue(ResultKeys.Status, out output))
            {
                if (!string.IsNullOrEmpty(output))
                {
                    if (bool.TryParse(output, out bool status))
                    {
                        return status;
                    }
                }
            }

            return false;
        }

        public static string GetPlyCode(this Dictionary<string, string> result)
        {
            result.TryGetValue(ResultKeys.Code, out string output);

            return output;
        }

        public static string GetPlyTrace(this Dictionary<string, string> result)
        {
            result.TryGetValue(ResultKeys.Trace, out string output);

            return output;
        }

        public static List<string> GetPlyList(this string data)
        {
            return JsonSerializer.Deserialize<List<string>>(data);
        }

        public static List<string> GetPlyList(this Dictionary<string, string> result)
        {
            List<string> list = new List<string>();

            try
            {
                if (result.TryGetValue(ResultKeys.Data, out string output))
                {
                    list = JsonSerializer.Deserialize<List<string>>(output);
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
            return JsonSerializer.Serialize(result);
        }
    }
}
