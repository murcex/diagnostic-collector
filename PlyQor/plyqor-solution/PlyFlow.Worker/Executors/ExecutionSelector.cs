using PlyQor.Injektr.TestCases;
using System;

namespace PlyQor.Injektr.Executors
{
    public class ExecutionSelector
    {
        public static IExecutor SelectExecutor(string type)
        {
            if (string.Equals(type, "basic", StringComparison.OrdinalIgnoreCase))
            {
                return new Basic();
            }

            if (string.Equals(type, "standard", StringComparison.OrdinalIgnoreCase))
            {
                return new Standard();
            }

            throw new Exception($"Invalid Exector {type}");
        }
    }
}
