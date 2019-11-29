namespace Implements
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class DataManager : IDisposable
    {
        private string _delimiter;
        private bool dispose;

        public DataManager(string delimiter = null)
        {
            _delimiter = delimiter != null ? delimiter : ",";
        }

        public Dictionary<string, string> Deserialize(List<string> lines)
        {
            Dictionary<string, string> collectionDictionary = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                string[] record = line.Split(_delimiter);
                collectionDictionary.Add(record[0], record[1]);
            }

            return collectionDictionary;
        }

        public List<string> Serialize(Dictionary<string, string> collectionDictionary)
        {
            List<string> lines = new List<string>();

            foreach (var record in collectionDictionary)
            {
                string line = record.Key + _delimiter + record.Value;
                lines.Add(line);
            }

            return lines;
        }

        /// <summary>
        /// Disposable Logic
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!dispose)
            {
                if (disposing)
                {
                    //dispose managed resources
                }
            }

            //dispose unmanaged resources
            dispose = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
    }
}
