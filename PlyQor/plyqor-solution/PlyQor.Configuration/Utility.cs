namespace PlyQor.AdminTool
{
    using System.Text;
    using Newtonsoft.Json;

    public class Utility
    {
        /// <summary>
        /// Convert config tokens value to Json string.
        /// </summary>
        public static string CreateStringOfTokens(string input)
        {
            var raw_tokens = input.Split(',');

            List<string> tokens = new List<string>();

            foreach (var token in raw_tokens)
            {
                tokens.Add(token);
            }

            return JsonConvert.SerializeObject(tokens);
        }

        public static string CreateEmptyConfig()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("[admin]");
            stringBuilder.AppendLine("database=");
            stringBuilder.AppendLine("");

            return stringBuilder.ToString();
        }
    }
}
