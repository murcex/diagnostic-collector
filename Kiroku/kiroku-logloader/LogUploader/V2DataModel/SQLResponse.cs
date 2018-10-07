using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLOGLoader
{
    public class SQLResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public void Successful()
        {
            this.Success = true;
        }

        public void Failure(string msg)
        {
            this.Message = msg;
        }
    }
}
