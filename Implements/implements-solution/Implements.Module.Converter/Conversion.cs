namespace Implements.Utility
{
    using System;
    using System.Diagnostics;

    public class Conversion
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

                if (version == null)
                {
                    version = FileVersionInfo.GetVersionInfo(dllName + ".dll").ProductVersion;

                    if (version == null)
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
    }
}