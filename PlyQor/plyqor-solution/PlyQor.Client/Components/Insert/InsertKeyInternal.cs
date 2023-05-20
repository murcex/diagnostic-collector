namespace PlyQor.Client
{
    using PlyQor.Client.DataExtension.Internal;
    using PlyQor.Client.Resources;

    class InsertKeyInternal
    {
        public static Dictionary<string, string> Execute(
            PlyClientConfiguration configuration,
            string key,
            string data,
            List<string> tags)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { RequestKeys.Token, configuration.Token },
                { RequestKeys.Container, configuration.Container },
                { RequestKeys.Operation, QueryOperation.InsertKey },
                { RequestKeys.Key, key },
                { RequestKeys.Data, data },
                { RequestKeys.Tags, tags.UnwrapTags() }
            };

            return Transmitter.Execute(configuration, request);
        }
    }
}
