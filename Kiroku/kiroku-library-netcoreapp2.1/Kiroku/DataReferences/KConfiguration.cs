namespace Kiroku
{
    using System;
    using System.Collections.Generic;

    class KConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, AppConfiguration> Configs
        {
            get
            {
                return _configs;
            }
        }
        private static Dictionary<string, AppConfiguration> _configs = new Dictionary<string, AppConfiguration>();

        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, Guid> InstanceIds 
        { 
            get 
            {
                if (_instanceIds == null)
                {
                    _instanceIds = new Dictionary<string, Guid>();
                }

                return _instanceIds;
            } 
        }
        private static Dictionary<string, Guid> _instanceIds;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appConfig"></param>
        public static void AddOrUpdateConfig(AppConfiguration appConfig, string appName)
        {
            if (appConfig == null)
            {
                // TODO: return empty config msg
            }
            else
            {
                // convert raw config into "log package"
            }

            if (!string.IsNullOrEmpty(appName))
            {
                if (!Configs.ContainsKey(appName))
                {
                    Configs.Add(appName, appConfig);
                }
                else if (Configs.ContainsKey(appName))
                {
                    Configs[appName] = appConfig;
                }
            }
            else
            {
                // return empty app name msg
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static AppConfiguration GetConfig(string appName)
        {
            AppConfiguration appConfig;

            if (!string.IsNullOrEmpty(appName))
            {
                if (Configs.ContainsKey(appName))
                {
                    Configs.TryGetValue(appName, out appConfig);

                    return appConfig;
                }
                else
                {
                    // TODO: return default config

                    return null;
                }
            }
            else
            {
                // TODO: return default config

                return null;
            }
        }

        /// <summary>
        /// Get the instance id for a static application.
        /// </summary>
        /// <returns></returns>
        public static Guid GetStaticInstanceId(string appName)
        {
            if (!string.IsNullOrEmpty(appName))
            {
                if (InstanceIds.ContainsKey(appName))
                {
                    InstanceIds.TryGetValue(appName, out Guid instanceId);

                    return instanceId;
                }
                else
                {
                    return new Guid("appname0-not0-0000-0000-found000000");
                }
            }
            else
            {
                // return empty assembly msg
                return new Guid("appname0-null-0000-0000-found000000");
            }

        }

        /// <summary>
        /// Set an instance id for a static application.
        /// </summary>
        /// <returns></returns>
        public static bool AddStaticInstaneId(Guid instanceId, string appName)
        {
            if (!string.IsNullOrEmpty(appName))
            {
                if (!InstanceIds.ContainsKey(appName))
                {
                    InstanceIds.Add(appName, instanceId);
                }
                else
                {
                    InstanceIds[appName] = instanceId;
                }
            }
            else
            {
                // TODO: return empty assembly msg

                return false;
            }

            return true;
        }
    }
}
