namespace PlyQor.Client
{
    using Newtonsoft.Json;
    using PlyQor.Client.Resources;

    class Transmitter
    {
        // for build-out only...
        private static PlyClientConfiguration configuration;

        private static HttpClient _httpClient = new HttpClient();

        // Add: Retry check on server error -- Count, Backoff
        // Add: Retry count

        public static Dictionary<string, string> Execute(string url, Dictionary<string, string> plyRequest)
        {
            Dictionary<string, string> finalResult = new Dictionary<string, string>();

            var plyMessage = JsonConvert.SerializeObject(plyRequest);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            HttpContent httpContent = new StringContent(plyMessage);
            request.Content = httpContent;

            var plyResult = _httpClient.Send(request).Content.ReadAsStringAsync().GetAwaiter().GetResult();

            // Add: Add Maleform check here

            bool active = true;
            int executeCount = 1;

            // https://alastaircrabtree.com/implementing-a-simple-retry-pattern-in-c/

            while (active)
            {
                try
                {
                    if (string.IsNullOrEmpty(plyResult))
                    {
                        throw new Exception($"Result is NullOrEmpty");
                    }

                    finalResult = JsonConvert.DeserializeObject<Dictionary<string, string>>(plyResult);

                    if (finalResult == null || finalResult.Count == 0)
                    {
                        throw new Exception($"Result Dictionary NullOrEmpty");
                    }
                    else
                    {
                        CheckForMalformResult(finalResult);
                    }
                }
                catch (Exception ex)
                {
                    if (executeCount < configuration.RetryCount)
                    {
                        Task.Delay(configuration.RetryCooldown).Wait();
                    }
                    else
                    {
                        if (configuration.MaleformException)
                        {
                            throw new Exception(ex.Message);
                        }
                        else
                        {
                            finalResult = GenerateMalformResult(ex);
                        }

                        break;
                    }
                }
            }

            PostCheck(finalResult);

            return finalResult;
        }

        private static void CheckForMalformResult(Dictionary<string, string> result)
        {
            //..
        }

        private static Dictionary<string, string> GenerateMalformResult(Exception ex)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("Status", bool.FalseString);
            result.Add("Code", "MALFORM");
            result.Add("Exception", ex.ToString());

            return result;
        }

        private static void PostCheck(Dictionary<string, string> result)
        {
            // Add: Status check here
            StatusCheck(result);

            // Add: NullOrEmpty check here
            NullOrEmptyCheck(result);
        }

        private static void NullOrEmptyCheck(Dictionary<string, string> result)
        {
            if (configuration.NullOrEmptyDataException)
            {
                if (result.TryGetValue(ResultKeys.Data, out string data))
                {
                    if (string.IsNullOrEmpty(data))
                    {
                        throw new Exception($"Data is NullOrEmpty");
                    }
                }
            }
        }

        private static void StatusCheck(Dictionary<string, string> result)
        {
            if (configuration.StatusException)
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
            }
        }
    }
}
