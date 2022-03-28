namespace PlyQor.Client
{
    class SelectTagsInternal
    {
        public static Dictionary<string, string> Execute(
            HttpClient httpClient, 
            string uri, 
            string container, 
            string token)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { "Token", token },
                { "Collection", container },
                { "Operation", "SelectTags" }
            };

            return Transmitter.Execute(httpClient, uri, request);
        }
    }
}
