using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiroku
{
    /// <summary>
    /// CLASS: Single KLOG event. Convertable to a single line of logging data contained within a KLOG Block.
    /// </summary>
    class LogRecord : IDisposable
    {
        #region Vars

        /// <summary>
        /// 
        /// </summary>
        bool dispose = false;
        public Nullable<System.DateTime> EventTime { get; set; }
        public Guid BlockID { get; set; }
        public string BlockName { get; set; }
        public string LogType { get; set; }
        public string LogData { get; set; }

        #endregion

        #region ConStru

        /// <summary>
        /// 
        /// </summary>
        public LogRecord()
        {
           EventTime = DateTime.Now.ToUniversalTime();
        }

        #endregion

        #region Disposal

        /// <summary>
        /// 
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

        #endregion
    }
}
