using System;
using System.Collections.Generic;
using System.Text;

namespace Kiroku
{
    interface ILog
    {
        Guid InstanceID { get; }

        string FilePath { get; }
    }
}
