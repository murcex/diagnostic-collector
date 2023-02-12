namespace PlyQor.Client
{
    using Newtonsoft.Json;
    using PlyQor.Client.Resources;

    class Transmitter
    {
        private static HttpClient _httpClient = new HttpClient();

        public static Dictionary<string, string> Execute(PlyClientConfiguration configuration, Dictionary<string, string> plyRequest)
        {
            string plyMessage = string.Empty;
            try
            {
                plyMessage = JsonConvert.SerializeObject(plyRequest);
            }
            catch (Exception ex)
            {
                throw new Exception($"Maleformed client side query: {ex}");
            }

            string plyResult = string.Empty;
            var requestCount = 0;
            var retryLimit = configuration.RetryCount;
            var requestException = configuration.HttpRequestException;
            var retryCooldown = configuration.RetryCooldown;
            var retryStacker = configuration.RetryBackoff;
            var requestRetryToken = true;
            while (requestRetryToken)
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, configuration.Uri);
                    HttpContent httpContent = new StringContent(plyMessage);
                    request.Content = httpContent;

                    plyResult = _httpClient.Send(request).Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    requestRetryToken = false;
                }
                catch (Exception ex)
                {
                    if (requestCount < retryLimit)
                    {
                        requestCount++;

                        Task.Delay(retryCooldown).Wait();

                        retryCooldown =+ retryStacker;
                    }
                    else
                    {
                        if (requestException)
                        {
                            throw new Exception($"PlyQor HttpRequestException: {ex}");
                        }
                        else
                        {
                            return GenerateMalformResult(ex);
                        }
                    }
                }
            }

            return QualityCheck(configuration, plyResult);
        }

        private static Dictionary<string, string> QualityCheck(PlyClientConfiguration configuration, string plyResult)
        {
            Dictionary<string, string> finalResult = new Dictionary<string, string>();

            // move into precheck
            if (string.IsNullOrEmpty(plyResult))
            {
                throw new Exception($"Result is NullOrEmpty");
            }

            // conversion check
            try
            {
                finalResult = JsonConvert.DeserializeObject<Dictionary<string, string>>(plyResult);

            }
            catch (Exception ex)
            {
                if (string.Equals(plyResult, "401", StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception($"Access Denied");
                }

                throw new Exception($"Maleform service side result - Exception: {ex} -- Payload: {plyResult}");
            }

            // move into post check
            if (finalResult == null || finalResult.Count == 0)
            {
                throw new Exception($"Result Dictionary NullOrEmpty");
            }

            PostCheck(configuration, finalResult);

            return finalResult;
        }

        private static Dictionary<string, string> GenerateMalformResult(Exception ex)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("Status", bool.FalseString);
            result.Add("Code", "MALFORM");
            result.Add("Exception", ex.ToString());

            return result;
        }

        private static void PostCheck(PlyClientConfiguration configuration, Dictionary<string, string> result)
        {
            StatusCheck(configuration.StatusException, result);

            NullOrEmptyCheck(configuration.NullOrEmptyDataException, result);
        }

        private static void StatusCheck(bool statusException, Dictionary<string, string> result)
        {
            if (statusException)
            {
                if (result.TryGetValue(ResultKeys.Status, out string data))
                {
                    if (string.IsNullOrEmpty(data))
                    {
                        throw new Exception($"Data is NullOrEmpty");
                    }
                    else
                    {
                        if (data.ToUpper() == "FALSE")
                        {
                            throw new Exception($"Status is False");
                        }
                    }
                }
                else
                {
                    throw new Exception($"Maleformed Result: Missing Status");
                }
            }
        }

        private static void NullOrEmptyCheck(bool nullOrEmptyDataExcetion, Dictionary<string, string> result)
        {
            if (nullOrEmptyDataExcetion)
            {
                if (result.TryGetValue(ResultKeys.Data, out string data))
                {
                    if (string.IsNullOrEmpty(data))
                    {
                        throw new Exception($"Data is NullOrEmpty");
                    }
                }
                else
                {
                    throw new Exception($"Maleformed Result: Missing Data");
                }
            }
        }
    }
}
