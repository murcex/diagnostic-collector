namespace PlyQor.Client
{
    using Newtonsoft.Json;
    using System.Net.Security;

    class Transmitter
    {
        private static HttpClient _httpClient = new HttpClient(GetHttpClientHandler());

        public static Dictionary<string, string> Execute(string url, Dictionary<string, string> plyRequest)
        {
            var plyMessage = JsonConvert.SerializeObject(plyRequest);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            HttpContent httpContent = new StringContent(plyMessage);
            request.Content = httpContent;

            var plyResult = _httpClient.Send(request).Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(plyResult);
        }

        private static SocketsHttpHandler GetHttpClientHandler()
        {
            var sslOptions = new SslClientAuthenticationOptions
            {
                RemoteCertificateValidationCallback = (message, certificate, chain, sslErrors) =>
                {
                    // direct to url with matching server certificate
                    if (sslErrors.ToString() == "None")
                    {
                        return true;
                    }

                    // support Azure Function redirect
                    if (sslErrors.ToString() == "RemoteCertificateNameMismatch")
                    {
                        if (certificate == null)
                        {
                            throw new Exception($"Server Certificate Validation Failure: Certificate is Null");
                        }
                        else
                        {
                            // Issed by Microsoft
                            if (certificate.Issuer.Contains("O=Microsoft Corporation")

                            // Azure Function Host URL
                            && certificate.Subject.Contains("CN=*.azurewebsites.net"))
                            {
                                return true;
                            }
                            else
                            {
                                throw new Exception($"Server Certificate Validation Failure: Not Trusted Certificate - {certificate.Issuer}, {certificate.Subject}");
                            }
                        }
                    }

                    if (certificate == null)
                    {
                        throw new Exception($"Server Certificate Validation Failure: {sslErrors}");
                    }
                    else
                    {
                        throw new Exception($"Server Certificate Validation Failure: {sslErrors}, {certificate.Issuer}, {certificate.Subject}");
                    }
                }
            };

            var socketHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(1),

                SslOptions = sslOptions
            };

            return socketHandler;
        }
    }
}
