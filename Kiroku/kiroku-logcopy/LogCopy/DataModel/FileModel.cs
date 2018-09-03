using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLOGCopy
{
    public class FileModel
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
