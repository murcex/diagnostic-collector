using Javelin.Worker;
using KirokuG2.Internal;
using PlyQor.Injektr.Executors;
using System;

namespace PlyQor.Injektr.TestCases
{
    public class Basic : IExecutor
    {
        public bool Execute(KLog klog)
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

            return true;
        }
    }
}
