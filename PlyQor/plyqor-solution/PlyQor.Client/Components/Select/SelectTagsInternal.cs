namespace PlyQor.Client
{
    using PlyQor.Client.Resources;

    class SelectTagsInternal
    {
        public static Dictionary<string, string> Execute(
            string uri,
            string container,
            string token)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { RequestKeys.Token, token },
                { RequestKeys.Container, container },
                { RequestKeys.Operation, QueryOperation.SelectTags }
            };

            return Transmitter.Execute(uri, request);
        }
    }
}
