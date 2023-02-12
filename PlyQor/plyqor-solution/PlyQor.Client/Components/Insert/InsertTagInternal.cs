namespace PlyQor.Client
{
    using PlyQor.Client.Resources;

    class InsertTagInternal
    {
        public static Dictionary<string, string> Execute(
            PlyClientConfiguration configuration,
            string key,
            string tag)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { RequestKeys.Token, configuration.Token },
                { RequestKeys.Container, configuration.Container },
                { RequestKeys.Operation, QueryOperation.InsertTag },
                { RequestKeys.Key, key },
                { RequestKeys.Tag, tag }
            };

            return Transmitter.Execute(configuration, request);
        }
    }
}
