namespace Implements
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    internal class FileManager : IDisposable
    {
        private string _root;
        private string _fileExtension;
        private bool dispose;

        public FileManager(string root = null, string ext = null)
        {
            _root = root != null ? root : Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            _fileExtension = ext != null ? ext : ".txt";
        }

        private string Root
        {
            get
            {
                return _root + @"\";
            }
        }

        // check if file exist
        public bool CheckIfCollectionExists(string fileName)
        {
            var target = Root + fileName + _fileExtension;

            return (File.Exists(target) ? true : false);
        }

        // read file
        public List<string> ReadCollection(string fileName)
        {
            List<string> lines = new List<string>();

            var target = Root + fileName + _fileExtension;

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

        // append line to file
        public bool AppendLineToCollection(string fileName, string line)
        {
            var target = Root + fileName + _fileExtension;

            File.AppendAllText(target, line + Environment.NewLine);

            return true;
        }

        // replace all lines to file
        public bool ReadAllLinesToCollection(string fileName, List<string> lines)
        {
            var target = Root + fileName + _fileExtension;

            File.WriteAllLines(target, lines);

            return true;
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
