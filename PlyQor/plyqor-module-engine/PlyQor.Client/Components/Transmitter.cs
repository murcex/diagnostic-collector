namespace PlyQor.Client
{
    using Newtonsoft.Json;

    class Transmitter
    {
        private static HttpClient _httpClient = new HttpClient();

        public static Dictionary<string, string> Execute(string url, Dictionary<string, string> plyRequest)
        {
            // TODO: add try/catch -- return clean exception

            var plyMessage = JsonConvert.SerializeObject(plyRequest);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            HttpContent httpContent = new StringContent(plyMessage);
            request.Content = httpContent;

            var plyResult = _httpClient.Send(request).Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(plyResult);
        }
    }
}
