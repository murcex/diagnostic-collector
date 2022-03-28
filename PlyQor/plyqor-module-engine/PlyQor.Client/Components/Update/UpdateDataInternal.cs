namespace PlyQor.Client
{
    class UpdateDataInternal
    {
        public static Dictionary<string, string> Execute(
            HttpClient httpClient, 
            string uri, 
            string container, 
            string token, 
            string key, 
            string data)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { "Token", token },
                { "Collection", container },
                { "Operation", "UpdateData" },
                { "Key", key },
                { "Aux", data }
            };

            return Transmitter.Execute(httpClient, uri, request);
        }
    }
}
