namespace KQuery
{
    using System.Collections.Generic;
    using Implements;
    using Configurator;

    class Global
    {
        static Global()
        {
            GetConfigs();
            SetConfig();
        }

        public static void GetConfigs()
        {
            var cfg = CfgManager.GetCfg("kquery");

            if (CfgManager.CheckCfg(cfg, out string errorMsg))
            {
                using (Deserializer deserilaizer = new Deserializer())
                {
                    var _file = @"D:\home\data\kquery\Config.ini";

                    deserilaizer.Execute(_file);

                    KQueryTagList = deserilaizer.GetTag("kquery");

                    //_kirokuTagList = deserilaizer.GetTag("kiroku");
                }
            }
            else
            {
                //errorMsg;
            }
        }

        private static void SetConfig()
        {
            foreach (var kvp in KQueryTagList)
            {
                switch (kvp.Key.ToString())
                {
                    case "storage":
                        StorageConnectionString = kvp.Value;
                        break;

                    default:
                        {
                            //Log.Error($"Not Hit: {kvp.Key}");
                        }
                        break;
                }
            }
        }

        private static List<KeyValuePair<string, string>> KQueryTagList { get; set; }
        public static string StorageConnectionString { get; private set; }
    }
}
