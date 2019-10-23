namespace KQuery
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System.Text;
    using Newtonsoft.Json.Linq;

    public static class KQuery
    {
        [FunctionName("KQuery")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            string id = req.Query["id"];

            string key = req.Query["key"];

            if (!Security.Check(req, key))
            {
                return new OkObjectResult($"");
            }

            try
            {
                Guid checkId = new Guid(id);

                byte[] doc = Storage.GetLog(id);

                var payload = Encoding.UTF8.GetString(doc, 0, doc.Length);

                if (!string.IsNullOrEmpty(payload))
                {
                    payload = payload.Replace("#KLOG_INSTANCE_STATUS#", "");

                    payload = payload.Replace("}", "},");

                    payload = "[" + payload + "]";

                    payload = payload.Replace("},\r\n$", "}]");

                    payload = JValue.Parse(payload).ToString(Formatting.Indented);
                }

                return doc == null
                    ? new OkObjectResult($"404")
                    : new OkObjectResult(payload);
            }
            catch
            {
                return new OkObjectResult("");
            }
        }
    }
}
