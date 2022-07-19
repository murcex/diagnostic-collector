using System.Net.Security;

namespace KirokuG2.Internal
{
    public class Transmitter
    {
        private static HttpClient _httpClient = new HttpClient(GetHttpClientHandler());

        public static bool Execute(string url, string data)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                HttpContent httpContent = new StringContent(data);
                request.Content = httpContent;

                var output = _httpClient.Send(request).Content.ReadAsStringAsync().GetAwaiter().GetResult();

                Console.WriteLine(output);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return false;
                // failsafe local write -or- retry logic based on ex
            }
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
                        Console.WriteLine("Approved: None");

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
                                Console.WriteLine("Approved: RemoteCertificateNameMismatch");

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
