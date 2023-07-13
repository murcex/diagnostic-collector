using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crane.Internal.Engine.SQLDatabaseDeployment
{
    public enum SqlObjectType
    {
        Table = 1,
        Proc = 2,
        View = 3,
        Security = 4
    }
}
