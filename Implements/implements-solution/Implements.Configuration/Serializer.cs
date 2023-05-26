using System.Text;

namespace Implements.Configuration.Internal
{
    public class Serializer
    {
        public string Execute(Dictionary<string, Dictionary<string, string>> input)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var key in input)
            {
                sb.AppendLine($"[{key.Key}]");

                foreach (var kvp in key.Value)
                {
                    var value = kvp.Value;
                    if (value.Contains(";"))
                    {
                        value = $"\"{value}\"";
                    }

                    sb.AppendLine($"{kvp.Key}={value}");
                }

                sb.AppendLine(string.Empty);
            }

            return sb.ToString();
        }
    }
}
