using Javelin.Worker;
using KirokuG2.Internal;
using PlyQor.Injektr.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlyQor.Injektr.TestCases
{
    public class Injektr : IExecutor
    {
        public bool Execute(KLog klog)
        {
            try
            {
                var id = Guid.NewGuid().ToString();
                var data_1 = Guid.NewGuid().ToString();
                var data_2 = Guid.NewGuid().ToString();

                using (var insert_block = klog.NewBlock("PlyQorInjektrInsert"))
                {
                    Configuration.PlyClient.Insert(id, data_1, "default");
                }

                using (var update_block = klog.NewBlock("PlyQorInjektrUpdate"))
                {
                    Configuration.PlyClient.UpdateData(id, data_2);
                }

                using (var select_block = klog.NewBlock("PlyQorInjektrSelect"))
                {
                    Configuration.PlyClient.Select(id);
                }

                using (var delete_block = klog.NewBlock("PlyQorInjektrDelete"))
                {
                    Configuration.PlyClient.Delete(id);
                }
            }
            catch (Exception ex)
            {
                klog.Error(ex.ToString());

                return false;
            }

            return true;
        }
    }
}
