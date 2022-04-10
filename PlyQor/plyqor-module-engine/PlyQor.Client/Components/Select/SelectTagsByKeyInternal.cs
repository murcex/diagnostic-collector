namespace PlyQor.Client
{
    using PlyQor.Client.Resources;

    class SelectTagsByKeyInternal
    {
        public static Dictionary<string, string> Execute(
            string uri, 
            string container, 
            string token, 
            string key)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { RequestKeys.Token, token },
                { RequestKeys.Container, container },
                { RequestKeys.Operation, QueryOperation.SelectTagsByKey },
                { RequestKeys.Key, key }
            };

            return Transmitter.Execute(uri, request);
        }
    }
}
