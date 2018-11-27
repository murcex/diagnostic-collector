namespace KLOGLoader
{
    using Microsoft.WindowsAzure.Storage.Blob;
    using System.Collections.Generic;
    using System.IO;

    public static class ReadFile
    {
        public static List<string> Execute(CloudBlob payload)
        {
            List<string> lines = new List<string>();

            using (StreamReader reader = new StreamReader(payload.OpenRead()))
            {
                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());
                }
            }

            return lines;
        }
    }
}
