namespace PlyQor.Client
{
    using PlyQor.Client.Resources;

    class SelectTagCountInternal
    {
        public static Dictionary<string, string> Execute(
            PlyClientConfiguration configuration,
            string tag)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { RequestKeys.Token, configuration.Token },
                { RequestKeys.Container, configuration.Container },
                { RequestKeys.Operation, QueryOperation.SelectTagCount },
                { RequestKeys.Tag, tag }
            };

            return Transmitter.Execute(configuration, request);
        }
    }
}
