using Implements.Configuration;
using PlyQor.Engine;
using System.IO;
using System.Linq;

namespace PlyQor.Audit.Core
{
    class Initializer
    {
        public static void Execute()
        {
            var _file = Directory.GetCurrentDirectory() + @"\Config.ini";

            var lines = File.ReadAllLines(_file).ToList();

            using (ConfigurationUtility configurationUtility = new())
            {
                var cfg = configurationUtility.Deserialize(lines);

                PlyQorManager.Initialize(cfg);

                Configuration.Load(cfg);
            }
        }
    }
}
