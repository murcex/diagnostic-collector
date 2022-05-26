using System;
using System.Collections.Generic;
using System.Text;

namespace ConfiguratorApp.Core
{
    class Initializer
    {
        private static bool _initialized = false;

        private static Dictionary<string, Dictionary<string, string>> _config;

        public static void Execute()
        {
            if (!_initialized)
            {
                _config = Pylon.Execute();

                Configuration.Load(_config);

                _initialized = true;
            }
        }
    }
}
