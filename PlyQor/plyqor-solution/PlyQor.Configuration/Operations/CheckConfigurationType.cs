namespace PlyQor.Configurator.Operations
{
    using PlyQor.AdminTool;

    public class CheckConfigurationType
    {
        /// <summary>
        /// Check input argument for configuration operation type
        /// </summary>
        public static (ConfigurationType, string) Execute(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine($"<!> Configuration Type: {ConfigurationType.ReadOnly}");

                return (ConfigurationType.ReadOnly, string.Empty);
            }

            var action = args[0];

            if (string.IsNullOrEmpty(action))
            {
                Console.WriteLine($"<!> Configuration Type: {ConfigurationType.ReadOnly}");

                return (ConfigurationType.ReadOnly, string.Empty);
            }

            if (action.ToUpper() == "TEMPLATE")
            {
                Console.WriteLine($"<!> Configuration Type: {ConfigurationType.Template}");

                return (ConfigurationType.Template, string.Empty);
            }

            Console.WriteLine($"<!> Configuration Type: {ConfigurationType.Modify}");

            return (ConfigurationType.Modify, action);
        }
    }
}
