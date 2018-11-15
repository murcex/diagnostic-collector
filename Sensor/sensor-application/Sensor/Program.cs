namespace Sensor
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            #region agent mode

            // StartLogging();

            // SetGlobalValues();

            if (Global.Agent == "1")
            {
                GetArticles.Execute();

                GetIPAddress.Execute();

                TagIPAddress.Execute();

                UploadCollection.Execute();
            }
            
            //{
            //    // TODO: find total articles and build task array
            //    var tasks = new Task[articleList.Count()];
            //    var taskCounter = 0;
            //
            //    // Loop target list, collect dns data and load sensor object
            //    foreach (var article in articleList)
            //    {
            //        List<DNSSensor> sensorSubCollection = new List<DNSSensor>();
            //
            //        // Collect DNS
            //        tasks[taskCounter] = Task.Run(() => sensorDnsCollection.AddRange(sensorSubCollection = Collector.Execute(article)));
            //
            //        taskCounter++;
            //    }
            //
            //    Task.WaitAll(tasks);
            //
            //}

            #endregion

            #region principal mode

            if (Global.Principal == "1")
            {
                //TODO: Detect Activity Method

                //TODO: Detect Failover Method

                //TODO: Retention Method
                DataDownload.DataRetention();
            }

            #endregion

            #region console.debug

            if (Global.DebugMode == "1")
            {
                Console.WriteLine("-- Operation Complete --");
                Console.Read();
            }

            #endregion

            // Close application
            Environment.Exit(0);
        }
    }
}
