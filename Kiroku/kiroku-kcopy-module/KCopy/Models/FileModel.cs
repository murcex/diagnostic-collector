namespace KCopy
{
    using System;

    class FileModel
    {
        public string FullPath { get; set; }

        public string Path { get; set; }

        public string DirName { get; set; }

        public string FileName { get; set; }

        public Guid FileGuid { get; set; }

        public string Tag { get; set; }

        public int TagCode { get; set; }

        public DateTime FileDate { get; set; }
    }
}
