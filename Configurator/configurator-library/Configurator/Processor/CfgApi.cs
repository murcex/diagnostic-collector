namespace Configurator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;

    class CfgApi
    {
        /// <summary>
        /// Create full Uri to call the Cfg API.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="dict"></param>
        public static string CreateCfgUri(string app, Dictionary<string, string> dict)
        {
            string cfgUrl = string.Empty;

            bool checkKey = dict.TryGetValue(Constants.CfgKey, out string key);

            bool checkUri = dict.TryGetValue(Constants.CfgUri, out string uri);

            if (checkKey && checkUri)
            {
                // TODO: format string, add to constants
                cfgUrl = string.Format(Constants.BaseUri, uri, key, app);
            }
            else
            {
                cfgUrl = Errors.CREATEURI_FAILURE_EMPTYNULL;
            }

            return cfgUrl;
        }

        /// <summary>
        /// Call the Cfg API for the config.
        /// </summary>
        /// <param name="url"></param>
        public static List<string> CallCfgUrl(string url)
        {
            List<string> cfgOutput = new List<string>();
            HttpClient httpClient = new HttpClient();

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

                    cfgOutput.AddRange(lineList);
                }
            }
            catch
            {
                cfgOutput.Add(Errors.CALLCFG_EXCEPTION);
            }

            if (cfgOutput == null || cfgOutput.Count < 1)
            {
                cfgOutput.Add(Errors.CALLCFG_EMPTY);
            }

            return cfgOutput;
        }
    }
}
