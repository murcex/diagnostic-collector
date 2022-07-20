using Configurator.Service;
using KirokuG2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfiguratorApp.Core
{
    public class Setup
    {
        private static bool _initialized = false;

        public static void Execute()
        {
            if (!_initialized)
            {
                CfgSvcManager.Initialize();

                KManager.Configure(true);

                _initialized = true;
            }
        }
    }
}
