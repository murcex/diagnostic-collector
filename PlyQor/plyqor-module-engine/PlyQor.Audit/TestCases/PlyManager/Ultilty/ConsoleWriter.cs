using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlyQor.Audit.TestCases.PlyManager
{
    internal class ConsoleWriter
    {
        public static void TestHeader(string topic)
        {
            Console.WriteLine($"TEST | QUERY: {topic}");
        }

        public static void TestResult(string result)
        {
            Console.WriteLine($"TEST | RESULT: {result}");
        }
    }
}
