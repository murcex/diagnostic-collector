namespace PlyQor.Client
{
    using PlyQor.Client.Resources;

    class DeleteKeyTagsInternal
    {
        public static Dictionary<string, string> Execute(
            PlyClientConfiguration configuration,
            string key)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { RequestKeys.Token, configuration.Token },
                { RequestKeys.Container, configuration.Container },
                { RequestKeys.Operation, QueryOperation.DeleteKeyTags },
                { RequestKeys.Key, key }
            };

            return Transmitter.Execute(configuration, request);
        }
    }
}
