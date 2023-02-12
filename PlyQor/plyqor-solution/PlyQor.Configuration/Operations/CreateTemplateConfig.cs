namespace PlyQor.Configurator.Operations
{
    using System.Text;

    public class CreateTemplateConfig
    {
        /// <summary>
        /// Create a default container template
        /// </summary>
        public static void Execute()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("[system]");
            stringBuilder.AppendLine("trace=1");
            stringBuilder.AppendLine("capacity=0");
            stringBuilder.AppendLine("cycle=0");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("[test-container]");
            stringBuilder.AppendLine("name=test-container");
            stringBuilder.AppendLine("retention=0");
            stringBuilder.AppendLine("trace=1");
            stringBuilder.AppendLine("tokens=\"access-token-1,access-token-2\"");

            var file_name = $"{Guid.NewGuid()}-TEMPLATE-{DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss")}.ini";

            var file_path = Path.Combine(Directory.GetCurrentDirectory(), file_name);

            File.AppendAllText(file_path, stringBuilder.ToString());
        }
    }
}
