namespace Configurator.Processor
{
    using Configurator.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Security;
    using System.Text;

    class Transmitter
    {
        /// <summary>
        /// Call Configurator storage service and request Config.ini
        /// </summary>
        /// <param name="url"></param>
        public static List<string> Execute(string url)
        {
            List<string> outputCfg = new();

            HttpClient httpClient = new(GetHttpClientHandler());

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result.Replace("\"", string.Empty);

                    var byteOutput = Convert.FromBase64String(result);

                    var singleString = Encoding.UTF8.GetString(byteOutput);

                    string[] lineArray = singleString.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                    var lineList = lineArray.ToList();

                    outputCfg.AddRange(lineList);
                }
            }
            catch
            {
                throw new Exception(Configuration.CALLCFG_EXCEPTION);
            }

            if (outputCfg == null || outputCfg.Count < 1)
            {
                throw new Exception(Configuration.CALLCFG_EMPTY);
            }

            return outputCfg;
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
