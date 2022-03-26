namespace KCopy.Model
{
    using System.Linq;
    using System.Collections.Generic;

    static class Capsule
    {
        /// <summary>
        /// Collection of log files.
        /// </summary>
        private static List<FileModel> _logFiles { get; set; }

        /// <summary>
        /// Set of log files to be sent.
        /// </summary>
        public static IEnumerable<FileModel> SendFiles { get { return _logFiles.Where(x => x.TagCode == 1); } }

        /// <summary>
        /// Set of log files requiring clean up.
        /// </summary>
        public static IEnumerable<FileModel> CleanUpFiles { get { return _logFiles.Where(x => x.TagCode == 2); } }

        /// <summary>
        /// Set of log files be to deleted.
        /// </summary>
        public static IEnumerable<FileModel> DeleteFiles { get { return _logFiles.Where(x => x.TagCode == 3 || (x.TagCode == 4)); } }

        /// <summary>
        /// Add a log file to the collection.
        /// </summary>
        /// <param name="logFiles"></param>
        public static void AddLogFiles(List<FileModel> logFiles)
        {
            _logFiles = logFiles;
        }

        /// <summary>
        /// Return the count of log files that were sent.
        /// </summary>
        /// <returns></returns>
        public static int SendFileCount()
        {
            return SendFiles.Count();
        }

        /// <summary>
        /// Return the count of log files required for clean-up.
        /// </summary>
        /// <returns></returns>
        public static int CleanUpFileCount()
        {
            return CleanUpFiles.Count();
        }

        /// <summary>
        /// Return the count of log files set for deletion.
        /// </summary>
        /// <returns></returns>
        public static int DeleteFileCount()
        {
            return DeleteFiles.Count();
        }
    }
}
