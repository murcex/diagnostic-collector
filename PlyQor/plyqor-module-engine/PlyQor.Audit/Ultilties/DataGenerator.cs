namespace PlyQor.Audit.TestCases
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using PlyQor.Audit.Core;

    class DataGenerator
    {
        private static byte[] _blobTwo;

        public static string CreateStringDocument()
        {
            string document = string.Empty;

            if (_blobTwo == null)
            {
                document = File.ReadAllText(Configuration.Document);
            }

            Configuration.DocumentLength = document.Length;

            return document;
        }

        public static string CreateRandomDocument()
        {
            var random = new Random();
            var stop = 100;
            var executionCounter = 0;
            var sb = new StringBuilder();

            do
            {
                sb.AppendLine(RandomStringGenerator(random, 100));

                executionCounter++;
            }
            while (stop > executionCounter);

            return sb.ToString();
        }

        private static string RandomStringGenerator(Random random, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(
              Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)])
              .ToArray()
              );
        }

        public static List<string> CreateSampleIndex()
        {
            Random random = new Random();

            List<string> indexes = new List<string>();
            var index1 = $"Stage{random.Next(0, 5)}";

            indexes.Add(index1);

            return indexes;
        }
    }
}
