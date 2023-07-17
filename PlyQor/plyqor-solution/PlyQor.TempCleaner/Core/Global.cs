using System.Collections.Generic;

namespace PlyQor.TempCleaner.Core
{
    public class Global
    {
        public static string DatabaseConnection { get; } = "";

        public static List<string> Containers { get; } = new List<string>() { "KIROKUG2-LOGS", "JAVELIN" };
    }
}
