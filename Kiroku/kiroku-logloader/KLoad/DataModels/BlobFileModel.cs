namespace KLoad
{
    using System;

    public class BlobFileModel
    {
        public string Container { get; set; }
        public string Directory { get; set; }
        public string File { get; set; }
        public string CloudFile { get; set; }
        public Guid FileGuid { get; set; }
        public string Tag { get; set; }
        public bool Exist { get; set; }
        public bool ParseStatus { get; set; }
        public bool HeaderStatus { get; set; }
        public bool LogStatus { get; set; }
        public bool FooterStatus { get; set; }
    }
}
