namespace PlyQor.Client
{
    using PlyQor.Client.Resources;

    class SelectTagsInternal
    {
        public static Dictionary<string, string> Execute(
            PlyClientConfiguration configuration)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { RequestKeys.Token, configuration.Token },
                { RequestKeys.Container, configuration.Container },
                { RequestKeys.Operation, QueryOperation.SelectTags }
            };

            return Transmitter.Execute(configuration, request);
        }
    }
}
