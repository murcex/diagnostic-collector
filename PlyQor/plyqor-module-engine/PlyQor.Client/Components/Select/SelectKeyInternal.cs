namespace PlyQor.Client
{
    class SelectKeyInternal
    {
        public static Dictionary<string, string> Execute(
            HttpClient httpClient,
            string uri,
            string container,
            string token,
            string key)
        {
            Dictionary<string, string> request = new Dictionary<string, string>();
            request.Add("Token", token);
            request.Add("Collection", container);
            request.Add("Operation", "SelectKey");
            request.Add("Key", key);

            return Transmitter.Execute(httpClient, uri, request);
        }
    }
}
