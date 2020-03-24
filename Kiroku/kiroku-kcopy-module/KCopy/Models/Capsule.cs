namespace KCopy
{
    using System.Linq;
    using System.Collections.Generic;

    static class Capsule
    {
        private static List<FileModel> _logFiles { get; set; }

        public static IEnumerable<FileModel> SendFiles { get { return _logFiles.Where(x => x.TagCode == 1); } }

        public static IEnumerable<FileModel> CleanUpFiles { get { return _logFiles.Where(x => x.TagCode == 2); } }

        public static IEnumerable<FileModel> DeleteFiles { get { return _logFiles.Where(x => x.TagCode == 3 || (x.TagCode == 4)); } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logFiles"></param>
        public static void AddLogFiles(List<FileModel> logFiles)
        {
            _logFiles = logFiles;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int SendFileCount()
        {
            int fileCount = 0;
            fileCount = SendFiles.Count();

            return fileCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int CleanUpFileCount()
        {
            int fileCount = 0;
            fileCount = CleanUpFiles.Count();

            return fileCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int DeleteFileCount()
        {
            int fileCount = 0;
            fileCount = DeleteFiles.Count();

            return fileCount;
        }
    }
}
