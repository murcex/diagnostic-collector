namespace Kiroku
{
    using System;

    /// <summary>
    /// CLASS: Contains all console verbose operations.
    /// </summary>
    class LogVerboseWriter
    {
        private static object _lock = new object();

        #region Write-LogRecord

        /// <summary>
        /// General log record formatting.
        /// </summary>
        /// <param name="logRecordPayload"></param>
        public static void WriteToConsole(LogRecord logRecordPayload)
        {
            lock (_lock)
            {
                Console.Write(" BLOCK: ");
                Console.Write(logRecordPayload.BlockName);
                Console.ResetColor();

                Console.Write(" | TYPE: ");

                ColorTypeMatch(logRecordPayload.LogType);
                Console.Write(logRecordPayload.LogType);
                Console.ResetColor();

                Console.Write(" | DATA: ");

                ColorTypeMatch(logRecordPayload.LogType);
                Console.Write("{0} \n\r", logRecordPayload.LogData);
                Console.ResetColor();
            }
        }

        #endregion

        #region Write-LogInstance

        /// <summary>
        /// Instance console formatting.
        /// </summary>
        /// <param name="logInstance"></param>
        public static void WriteToConsole(LogInstance logInstance)
        {
            lock (_lock)
            {
                Console.Write(" INSTANCE: ");

                Console.Write(logInstance.InstanceID);

                Console.Write(" | DATA: ");

                ColorTypeMatch(logInstance.InstanceStatus);
                Console.Write("{0} \n\r", logInstance.InstanceStatus);
                Console.ResetColor();
            }
        }

        #endregion

        #region Color Type Switch Sort

        /// <summary>
        /// Set color for log event based on type.
        /// </summary>
        /// <param name="logRecordPayloadType"></param>
        static void ColorTypeMatch(string logRecordPayloadType)
        {
            switch (logRecordPayloadType)
            {
                case "Trace":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;

                case "Info":
                    Console.ResetColor();
                    break;

                case "Warning":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case "Error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                default:
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    break;
            }
        }

        #endregion
    }
}