namespace PlyQor.Client
{
    using PlyQor.Client.Resources;

    class DeleteKeyInternal
    {
        public static Dictionary<string, string> Execute(
            PlyClientConfiguration configuration,
            string key)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { RequestKeys.Token, configuration.Token },
                { RequestKeys.Container, configuration.Container },
                { RequestKeys.Operation, QueryOperation.DeleteKey },
                { RequestKeys.Key, key }
            };

            return Transmitter.Execute(configuration, request);
        }
    }
}
