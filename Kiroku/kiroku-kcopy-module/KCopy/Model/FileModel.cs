namespace KCopy.Model
{
    using System;
    using System.IO;

    class FileModel
    {
        /// <summary>
        /// Full path of the fileInfo.
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// Directory's full path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Directory name. 
        /// </summary>
        public string DirName { get; set; }

        /// <summary>
        /// Log file name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Log file GUID.
        /// </summary>
        public Guid FileGuid { get; set; }
        
        /// <summary>
        /// The KLOG file Tag. [W]rite, [S]end, [A]rchive.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// The KLOG file Tag Code.
        /// </summary>
        public int TagCode { get; set; }

        /// <summary>
        /// Last write time UTC.
        /// </summary>
        public DateTime FileDate { get; set; }

        public FileModel(DirectoryInfo directoryInfo, FileInfo fileInfo)
        {
            // Load metadata
            this.FullPath = fileInfo.FullName;
            this.Path = fileInfo.DirectoryName;
            this.FileName = fileInfo.Name;
            this.TagCode = -1;
            this.FileDate = fileInfo.LastWriteTimeUtc;
            this.DirName = directoryInfo.Name;

            // Parse file name down to GUID and Tag
            this.FileGuid = Guid.Parse(fileInfo.Name.Substring(7, 36));
            this.Tag = fileInfo.Name.Substring(1, 6);

            // Assign Tag Code from Tag
            if (fileInfo.Name.Contains("KLOG_S")) { this.TagCode = 1; }
            if (fileInfo.Name.Contains("KLOG_W")) { this.TagCode = 2; }
            if (fileInfo.Name.Contains("KLOG_A")) { this.TagCode = 3; }
        }

        public string TracetoString()
        {
            return
                $"{this.FullPath}," +
                $"{this.Tag}," +
                $"{this.TagCode}," +
                $"{this.FileDate},";
        }
    }
}
