namespace PlyQor.Client
{
    class DeleteTagsByKeyInternal
    {
        public static Dictionary<string, string> Execute(
            HttpClient httpClient, 
            string uri, 
            string container, 
            string token, 
            string key)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { "Token", token },
                { "Collection", container },
                { "Operation", "DeleteTagsByKey" },
                { "Key", key }
            };

            return Transmitter.Execute(httpClient, uri, request);
        }
    }
}
