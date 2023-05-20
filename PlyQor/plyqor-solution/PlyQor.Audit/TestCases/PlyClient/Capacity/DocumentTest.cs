namespace PlyQor.Audit.TestCases.PlyClient
{
    using PlyQor.Audit.Core;
    using PlyQor.Client;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    class DocumentTest
    {
        public static void Execute()
        {
            PlyClient plyClient = new PlyClient(
                Configuration.ClientUrl,
                Configuration.ClientContainer,
                Configuration.ClientToken);

            // nano
            var totalNanoRequests = Configuration.NanoCount;
            List<Task> plyClientTasks = new List<Task>();
            for (int request = 0; request < totalNanoRequests; request++)
            {
                plyClientTasks.Add(new Task(() => NanoInsert(plyClient)));
            }

            Parallel.ForEach(plyClientTasks, (t) => { t.Start(); });
            Task.WaitAll(plyClientTasks.ToArray());

            // micro
            plyClientTasks.Clear();
            var totalMicroRequests = Configuration.MicroCount;
            for (int request = 0; request < totalMicroRequests; request++)
            {
                plyClientTasks.Add(new Task(() => MicroInsert(plyClient)));
            }

            Parallel.ForEach(plyClientTasks, (t) => { t.Start(); });
            Task.WaitAll(plyClientTasks.ToArray());

            // small
            plyClientTasks.Clear();
            var totalSmallRequests = Configuration.SmallCount;
            for (int request = 0; request < totalSmallRequests; request++)
            {
                plyClientTasks.Add(new Task(() => SmallInsert(plyClient)));
            }

            Parallel.ForEach(plyClientTasks, (t) => { t.Start(); });
            Task.WaitAll(plyClientTasks.ToArray());

            // medium
            plyClientTasks.Clear();
            var totalMediumRequests = Configuration.MediumCount;
            for (int request = 0; request < totalMediumRequests; request++)
            {
                plyClientTasks.Add(new Task(() => MediumInsert(plyClient)));
            }

            Parallel.ForEach(plyClientTasks, (t) => { t.Start(); });
            Task.WaitAll(plyClientTasks.ToArray());

            // large
            plyClientTasks.Clear();
            var totalLargeRequests = Configuration.LargeCount;
            for (int request = 0; request < totalLargeRequests; request++)
            {
                plyClientTasks.Add(new Task(() => LargeInsert(plyClient)));
            }

            Parallel.ForEach(plyClientTasks, (t) => { t.Start(); });
            Task.WaitAll(plyClientTasks.ToArray());
        }

        private static void NanoInsert(PlyClient plyClient)
        {
            var output = plyClient.Insert(Guid.NewGuid().ToString(), DataGenerator.CreateNanoDocument(), "Nano").GetPlyCode();
            Console.WriteLine($"Nano: {output}");
        }

        private static void MicroInsert(PlyClient plyClient)
        {
            var output = plyClient.Insert(Guid.NewGuid().ToString(), DataGenerator.CreateMicroDocument(), "Micro").GetPlyCode();
            Console.WriteLine($"Micro: {output}");
        }

        private static void SmallInsert(PlyClient plyClient)
        {
            var output = plyClient.Insert(Guid.NewGuid().ToString(), DataGenerator.CreateSmallDocument(), "Small").GetPlyCode();
            Console.WriteLine($"Small: {output}");
        }

        private static void MediumInsert(PlyClient plyClient)
        {
            var output = plyClient.Insert(Guid.NewGuid().ToString(), DataGenerator.CreateMediumDocument(), "Medium").GetPlyCode();
            Console.WriteLine($"Medium: {output}");
        }

        private static void LargeInsert(PlyClient plyClient)
        {
            var output = plyClient.Insert(Guid.NewGuid().ToString(), DataGenerator.CreateLargeDocument(), "Large").GetPlyCode();
            Console.WriteLine($"Large: {output}");
        }
    }
}
