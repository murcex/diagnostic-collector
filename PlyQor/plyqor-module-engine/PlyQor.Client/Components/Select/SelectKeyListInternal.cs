namespace PlyQor.Client
{
    class SelectKeyListInternal
    {
        public static Dictionary<string, string> Execute(
            HttpClient httpClient, 
            string uri, 
            string container, 
            string token, 
            string tag, 
            int count)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { "Token", token },
                { "Collection", container },
                { "Operation", "SelectKeyList" },
                { "Tag", tag },
                { "Aux", count.ToString() }
            };

            return Transmitter.Execute(httpClient, uri, request);
        }
    }
}
