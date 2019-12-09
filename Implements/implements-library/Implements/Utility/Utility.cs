namespace Implements
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public class Utility
    {
        /// <summary>
        /// Utility to convert string to int.
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static int ConvertValueToInt(string inputValue)
        {
            int outputValue;

            try
            {
                outputValue = Int32.Parse(inputValue);
            }
            catch
            {
                outputValue = 0;
            }

            return outputValue;
        }

        /// <summary>
        /// Utility to convert string to bool.
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static bool ConvertValueToBool(string inputValue)
        {
            bool outputValue;

            if (inputValue == "1")
            {
                outputValue = true;
            }
            else
            {
                outputValue = false;
            }

            return outputValue;
        }

        /// <summary>
        /// Utility to find Assembly Version.
        /// </summary>
        /// <param name="dllName"></param>
        /// <returns></returns>
        public static string GetAssemblyVersion(string dllName)
        {
            string version;

            try
            {
                version = FileVersionInfo.GetVersionInfo(dllName + ".dll").FileVersion;
            }
            catch
            {
                version = null;
            }

            return version;
        }


        public static string GetBuildVersion()
        {
            return "200001010101";
        }


        public static string GetDeploymentVersion()
        {
            return "200001010101";
        }

        public static string GetConfigSignature()
        {
            return "cfg-aehtv-9992";
        }

        public static bool CreateAppDirectory(string root, string app)
        {
            try
            {
                string path = root + app;

                if (Directory.Exists(path))
                {
                    return true;
                }

                DirectoryInfo dir = Directory.CreateDirectory(path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}