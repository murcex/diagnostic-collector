using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Implements
{
    class Metadata
    {
        private Dictionary<string, string> MetadataDictionary
        {
            get
            {
                if (_dictionary == null)
                {
                    var lines = FileManager.ReadCollection("metadata");

                    _dictionary = DataManager.Deserialize(lines);
                }

                return _dictionary;
            }
        }

        Dictionary<string, string> _dictionary;
        string _job;
        int _executionTime;
        DateTime currentTime = DateTime.UtcNow;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        /// <param name="executionTime"></param>
        public Metadata(string job, string executionTime)
        {
            _job = job;
            _executionTime = Int32.Parse(executionTime);
        }

        /// <summary>
        /// Evaluate the current time agaist the next execution threshold.
        /// </summary>
        /// <param name="currTime"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        private bool Evaluate(DateTime currTime, DateTime next)
        {
            if (currTime > next)
            {
                return OpenSession(_job);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Mart metadata token as complete.
        /// </summary>
        /// <returns></returns>
        public bool Complete()
        {
            var next = GenerateNext(_executionTime);
            return CloseSession(_job, next);
        }

        /// <summary>
        /// Get the next execution threshold from the metadata token.
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        private string GetNext(string job)
        {
            // convert string to timespace, return timespan
            var getResult = MetadataDictionary.TryGetValue(job, out string next);

            if (getResult)
            {
                return next;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Mark job as open in the metadata token.
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        private bool OpenSession(string job)
        {
            MetadataDictionary[job] = "OPEN";

            return true;
        }

        /// <summary>
        /// Mark job as closed by adding the next execution threshold.
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        private bool CloseSession(string job, string next)
        {
            MetadataDictionary[job] = next;

            return true;
        }

        /// <summary>
        /// Create the next execution threshold.
        /// </summary>
        /// <param name="executionTarget"></param>
        /// <returns></returns>
        private string GenerateNext(int executionTarget)
        {
            var tomorrow = DateTime.UtcNow.AddDays(1);

            var next = new DateTime(
                tomorrow.Year, 
                tomorrow.Month, 
                tomorrow.Day, 
                executionTarget, 
                0, 
                0);

            return next.ToString();
        }
    }

    class FileManager
    {
        private const string fileExtension = ".txt";

        private static string Root
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\";
            }
        }

        // check if file exist
        public static bool CheckIfCollectionExists(string fileName)
        {
            var target = Root + fileName + FileManager.fileExtension;

            return (File.Exists(target) ? true : false);
        }

        // read file
        public static List<string> ReadCollection(string fileName)
        {
            List<string> lines = new List<string>();

            var target = Root + fileName + FileManager.fileExtension;

            try
            {
                using (StreamReader reader = new StreamReader(target))
                {
                    while (!reader.EndOfStream)
                    {
                        lines.Add(reader.ReadLine());
                    }
                }

                return lines;
            }
            catch (Exception e)
            {
                throw new Exception($"Collection Reader Exception [StorageManager].[ReadCollection()]: StreamReaders Error: {e.ToString()}");
            }
        }

        // replace all lines to file
        public static bool ReadAllLinesToCollection(string fileName, List<string> lines)
        {
            var target = Root + fileName + FileManager.fileExtension;

            File.WriteAllLines(target, lines);

            return true;
        }
    }

    class DataManager
    {
        public static Dictionary<string, string> Deserialize(List<string> lines)
        {
            Dictionary<string, string> collectionDictionary = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                string[] record = line.Split(",");
                collectionDictionary.Add(record[0], record[1]);
            }

            return collectionDictionary;
        }

        public static List<string> Serialize(Dictionary<string, string> collectionDictionary)
        {
            List<string> lines = new List<string>();

            foreach (var record in collectionDictionary)
            {
                string line = record.Key + $" <^> " + record.Value;
                lines.Add(line);
            }

            return lines;
        }
    }
}
