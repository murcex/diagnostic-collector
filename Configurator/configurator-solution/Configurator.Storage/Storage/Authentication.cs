using System;
using System.Collections.Generic;
using System.Linq;

namespace Configurator.Storage.Internal
{
    public class Authentication
    {
        private Dictionary<string, List<string>> _keydex;

        public Authentication(Dictionary<string, Dictionary<string, string>> accessCfg)
        {
            _keydex = new Dictionary<string, List<string>>();

            foreach (var key in accessCfg.Keys)
            {
                var kvp = accessCfg[key];

                var tokens = kvp["tokens"].Split(',').ToList();

                _keydex[key] = tokens;
            }
        }

        public bool CheckKey(string file, string key)
        {
            if (string.IsNullOrEmpty(file))
            {
                throw new ArgumentNullException("file");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            if (_keydex.TryGetValue(file, out var tokens))
            {
                return tokens.Contains(key);
            }

            return false;
        }
    }
}
