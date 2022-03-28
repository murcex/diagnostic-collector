namespace PlyQor.Client
{
    class SelectTagCountInternal
    {
        public static Dictionary<string, string> Execute(
            HttpClient httpClient, 
            string uri, 
            string container, 
            string token, 
            string tag)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { "Token", token },
                { "Collection", container },
                { "Operation", "SelectTagCount" },
                { "Tag", tag }
            };

            return Transmitter.Execute(httpClient, uri, request);
        }
    }
}
