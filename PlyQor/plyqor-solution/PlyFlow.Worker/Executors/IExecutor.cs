using KirokuG2.Internal;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlyQor.Injektr.Executors
{
    public interface IExecutor
    {
        public bool Execute(KLog klog);
    }
}
