using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirokuG2.Internal.Loader.Test
{
    public class ExampleKLogs
    {
        public static string BasicLog()
        {
            return "2024-04-14T03:34:59.9913558Z,I,hylix-kiroku-injektr-wus3$Kiroku-Audit\r\n" +
                "2024-04-14T03:34:59.9913584Z,T,testing info\r\n" +
                "2024-04-14T03:34:59.9913675Z,M,2$test metric$99.99\r\n" +
                "2024-04-14T03:34:59.9913729Z,B,A19C62FE$TestBlock\r\n" +
                "2024-04-14T03:34:59.9913738Z,T,doing stuff inside the block A19C62FE\r\n" +
                "2024-04-14T03:34:59.9913745Z,SB,A19C62FE\r\n" +
                "2024-04-14T03:34:59.9913755Z,T,Test Method Info\r\n" +
                "2024-04-14T03:34:59.9913758Z,E,testing error\r\n" +
                "2024-04-14T03:34:59.9913762Z,SI,0";
        }
    }
}
