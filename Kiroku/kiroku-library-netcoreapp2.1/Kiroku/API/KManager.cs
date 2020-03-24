namespace Kiroku
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// CLASS: Starts and Stops Kiroku instance opertaions.
    /// </summary>
    public class KManager
    {
        #region Configure

        /// <summary>
        /// Set Global KLOG cofig and evaluate KLOG dynamic mode.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="dynamic"></param>
        public static void Configure(List<KeyValuePair<string, string>> config, bool dynamic = false)
        {
            try
            {
                string appName = Assembly.GetCallingAssembly().GetName().Name.ToUpper();

                AppConfiguration appConfig = new AppConfiguration(config, GetKirokuVersion());

                if (dynamic)
                {
                    appConfig.Dynamic = true;
                }

                KConfiguration.AddOrUpdateConfig(appConfig, appName);
            }
            catch (Exception ex)
            {
                //TODO: Replace => Log.Error($"[KManager].[Configure] - Exception: {ex.ToString()}");
            }
        }

        #endregion

        #region Open Static Instance

        /// <summary>
        /// Start a new Kiroku logging instance, log file.
        /// </summary>
        public static void Open()
        {
            string appName = Assembly.GetCallingAssembly().GetName().Name.ToUpper();

            Guid instanceId = Guid.NewGuid();

            AppConfiguration appConfig = KConfiguration.GetConfig(appName);

            if (appConfig.Dynamic)
            {
                throw new Exception($"Dynamic logging is set to true. This is a static only method.");
            }

            KConfiguration.AddStaticInstaneId(instanceId, appName);

            using (LogInstance logInstance = new LogInstance(KConstants.s_InstanceStart, instanceId, appConfig))
            {
                if (appConfig.WriteLog)
                {
                    LogFileWriter.AddInstanceEvent(logInstance);
                }

                if (appConfig.WriteVerbose)
                {
                    //LogVerboseWriter.Write();
                }
            }
        }

        #endregion

        #region Close Static Instance

        /// <summary>
        /// Close the current Kiroku logging instance, marking the file for agent transmission.
        /// </summary>
        public static void Close()
        {
            string appName = Assembly.GetCallingAssembly().GetName().Name.ToUpper();

            AppConfiguration appConfig = KConfiguration.GetConfig(appName);

            if (appConfig.Dynamic)
            {
                throw new Exception($"Dynamic logging is set to true. This is a static only method.");
            }

            Guid instanceId = KConfiguration.GetStaticInstanceId(appName);

            using (LogInstance logInstance = new LogInstance(KConstants.s_InstanceStop, instanceId, appConfig))
            {
                if (appConfig.WriteLog)
                {
                    LogFileWriter.AddInstanceEvent(logInstance);

                    try
                    {
                        string newFilePath = appConfig.FullFilePath.Replace(KConstants.s_WritingToLog, KConstants.s_ReadyToSend);

                        // Rename from KLOG_W_$(guid) to KLOG_S_$(guid) -- this will maket the log available for transmission
                        File.Move(appConfig.FullFilePath + instanceId.ToString() + KConstants.s_FileExt,
                            (newFilePath + instanceId.ToString() + KConstants.s_FileExt));
                    }
                    catch (Exception ex)
                    {
                        // TODO: Replace => Log.Error($"[KManager].[EndInstance] - Exception: {ex.ToString()}");
                    }
                }

                if (appConfig.WriteVerbose)
                {
                    //LogVerboseWriter.Write();
                }
            }
        }

        #endregion

        #region Open Dynamic Instance

        /// <summary>
        /// Create KLog Instance.
        /// </summary>
        /// <param name="appConfig"></param>
        internal static Guid CreateDynamicInstance(AppConfiguration appConfig)
        {
            try
            {
                Guid instanceId = Guid.NewGuid();

                using (LogInstance logInstance = new LogInstance(KConstants.s_InstanceStart, instanceId, appConfig))
                {
                    if (appConfig.WriteLog)
                    {
                        LogFileWriter.AddInstanceEvent(logInstance);
                    }

                    if (appConfig.WriteVerbose)
                    {
                        //LogVerboseWriter.Write();
                    }
                }

                return instanceId;
            }
            catch (Exception ex)
            {
                // TODO: Replace => Log.Error($"[KManager].[CreateInstance] - Exception: {ex.ToString()}");

                return Guid.Empty;
            }
        }

        #endregion

        #region Close Dynamic Instance

        /// <summary>
        /// Signals the end of KLog operations for an instance, closing KLog instance log, renaming the KLOG file for sending operation.
        /// </summary>
        internal static void CloseDynamicInstance(Guid instanceId, AppConfiguration appConfig)
        {
            using (LogInstance logInstance = new LogInstance(KConstants.s_InstanceStop, instanceId, appConfig))
            {
                if (appConfig.WriteLog)
                {
                    LogFileWriter.AddInstanceEvent(logInstance);

                    try
                    {
                        string newFilePath = appConfig.FullFilePath.Replace(KConstants.s_WritingToLog, KConstants.s_ReadyToSend);

                        // Rename from KLOG_W_$(guid) to KLOG_S_$(guid) -- this will maket the log available for transmission
                        File.Move(appConfig.FullFilePath + instanceId.ToString() + KConstants.s_FileExt,
                            (newFilePath + instanceId.ToString() + KConstants.s_FileExt));
                    }
                    catch (Exception ex)
                    {
                        // TODO: Replace => Log.Error($"[KManager].[EndInstance] - Exception: {ex.ToString()}");
                    }
                }

                if (appConfig.WriteVerbose)
                {
                    //LogVerboseWriter.Write();
                }
            }
        }

        #endregion

        #region Utility

        private static string GetKirokuVersion()
        {
            string version;

            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();

                version = FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;

                if (string.IsNullOrEmpty(version))
                {
                    version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;

                    if (string.IsNullOrEmpty(version))
                    {
                        version = "0.0.0.0";
                    }
                }
            }
            catch
            {
                version = null;
            }

            return version;
        }

        #endregion
    }
}
