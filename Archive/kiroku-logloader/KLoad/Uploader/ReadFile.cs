namespace KLoad
{
    using Microsoft.WindowsAzure.Storage.Blob;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Read Blob File to list of string.
    /// </summary>
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
