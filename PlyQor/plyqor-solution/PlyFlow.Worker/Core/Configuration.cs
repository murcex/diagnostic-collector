using PlyQor.Client;
using System;
using System.Collections.Generic;

namespace Javelin.Worker
{
    class Configuration
    {
        private static string _type;

        private static string _url;

        private static string _container;

        private static string _token;

        public static string Type => _type;

        public static string Url => _url;

        public static string Container => _container;

        public static string Token => _token;


        public static PlyClient PlyClient;

        public static void Load(Dictionary<string, Dictionary<string, string>> cfg)
        {
            if (cfg.TryGetValue("Worker", out Dictionary<string, string> worker_cfg))
            {
                foreach (var kv in worker_cfg)
                {
                    _ = kv.Key switch
                    {
                        "type" => _type = kv.Value,
                        "url" => _url = kv.Value,
                        "container" => _container = kv.Value,
                        "token" => _token = kv.Value,
                        _ => throw new Exception("Switch Failure"),
                    };
                }
            }

            PlyClient = new PlyClient(Url, Container, Token);
        }
    }
}
