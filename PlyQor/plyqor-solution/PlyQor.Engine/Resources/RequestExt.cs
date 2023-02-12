using Azure.Core;
using PlyQor.Models;
using PlyQor.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlyQor.Engine.Resources
{
    public static class RequestExt
    {
        public static void AddContainer(this Dictionary<string, string> result, string container)
        {
            result.Add(RequestKeys.Container, container);
        }

        public static void AddMeticType(this Dictionary<string, string> result, string type)
        {
            result.Add("MetricType", type);
        }

        public static void AddMetricKey(this Dictionary<string, string> result, string key)
        {
            result.Add("MetricKey", key);
        }

        public static void AddMetricData(this Dictionary<string, string> result, string data)
        {
            result.Add("MetricData", data);
        }
    }

    public static class ResultExt
    {
        public static string GetRequestStringValue(this Dictionary<string, string> dictionary, string key)
        {
            if (dictionary.TryGetValue(key.ToString(), out string result))
            {
                if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result))
                {
                    // TODO: add const interpolated string
                    throw new PlyQorException($"{StatusCode.ERR003},KEY={key}");
                }

                return result;
            }
            else
            {
                // TODO: add const interpolated string
                throw new PlyQorException($"{StatusCode.ERR004},KEY={key}");

            }
        }

        public static int GetRequestIntValue(this Dictionary<string,string> dictionary, string key, bool positive = true)
        {
            if (dictionary.TryGetValue(key.ToString(), out string result))
            {
                if (int.TryParse(result, out int testValue))
                {
                    if (positive)
                    {
                        if (testValue > 0)
                        {
                            return testValue;
                        }
                        else
                        {
                            throw new PlyQorException($"{StatusCode.ERR005},KEY={key}");
                        }
                    }

                    return testValue;
                }
                else
                {
                    throw new PlyQorException($"{StatusCode.ERR006},KEY={key}");
                }
            }
            else
            {
                throw new PlyQorException($"{StatusCode.ERR004},KEY={key}");
            }
        }
    }
}
