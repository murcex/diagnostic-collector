using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Implements.Configuration;

namespace Crane.Application.Utility
{
    public class CraneFileManager
    {
        public Dictionary<string,Dictionary<string,string>> LoadCraneConfig()
        {
            // file path
            var configFilePath = "asdf";

            // file exist
            if (!File.Exists(configFilePath))
            {
                throw new Exception();
            }

            // read all lines
            var lines = File.ReadAllLines(configFilePath).ToList();

            // de-serial lines to cfg and return
            using ConfigurationUtility configurationUtility = new();
            return configurationUtility.Deserialize(lines);
        }

        public Dictionary<string,Dictionary<string,string>> LoadCraneScript(Dictionary<string,Dictionary<string,string>> craneCfg)
        {
            // file path

            // file exist

            // read all lines

            // de-serial lines to cfg

            // check type name

            return null;
        }
    }
}
