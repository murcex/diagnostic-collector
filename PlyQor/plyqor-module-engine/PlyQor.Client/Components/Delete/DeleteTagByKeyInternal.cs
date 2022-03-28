namespace PlyQor.Client
{
    class DeleteTagByKeyInternal
    {
        public static Dictionary<string, string> Execute(
            HttpClient httpClient, 
            string uri, 
            string container, 
            string token, 
            string key, 
            string tag)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { "Token", token },
                { "Collection", container },
                { "Operation", "DeleteTagByKey" },
                { "Key", key },
                { "Tag", tag }
            };

            return Transmitter.Execute(httpClient, uri, request);
        }
    }
}
