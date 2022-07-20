namespace KCopy
{
    using System;

    class FileModel
    {
        /// <summary>
        /// Full path of the file.
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
    }
}
