namespace KQuery
{
    using Implements;
    using System.Collections.Generic;

    class Global
    {
        static Global()
        {
            GetConfigs();
            SetConfig();
        }

        public static void GetConfigs()
        {
            using (Deserializer deserilaizer = new Deserializer())
            {
                var _file = @"D:\home\site\wwwroot\Config.ini";

                deserilaizer.Execute(_file);

                KQueryTagList = deserilaizer.GetTag("kquery");

                //_kirokuTagList = deserilaizer.GetTag("kiroku");
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
