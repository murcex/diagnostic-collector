using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlyQor.Client
{
    class Transmitter
    {
        public static Dictionary<string, string> Execute(HttpClient httpClient, string url, Dictionary<string, string> plyRequest)
        {
            var plyMessage = JsonConvert.SerializeObject(plyRequest);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            HttpContent httpContent = new StringContent(plyMessage);
            request.Content = httpContent;

            var plyResult = httpClient.Send(request).Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(plyResult);
        }
    }
}
