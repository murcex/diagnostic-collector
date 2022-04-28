namespace PlyQor.Audit.TestCases
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using PlyQor.Audit.Core;

    class DataGenerator
    {
        public static string CreateStringDocument()
        {
            string document = string.Empty;

            document = File.ReadAllText(Configuration.Document);

            Configuration.DocumentLength = document.Length;

            return document;
        }

        public static string CreateDocument(int lines = 0)
        {
            var random = new Random();
            int stop = 100;
            var executionCounter = 0;
            var sb = new StringBuilder();
            
            if (lines > 0)
            {
                stop = lines;
            }

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

        public static string CreateNanoDocument()
        {
            return CreateDocument(1);
        }

        public static string CreateMicroDocument()
        {
            return CreateDocument(100);
        }

        public static string CreateSmallDocument()
        {
            return CreateDocument(1000);
        }

        public static string CreateMediumDocument()
        {
            return CreateDocument(10030);
        }

        public static string CreateLargeDocument()
        {
            return CreateDocument(100390);
        }
    }
}
