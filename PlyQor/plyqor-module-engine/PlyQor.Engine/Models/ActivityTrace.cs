namespace PlyQor.Models
{
    using System;
    using System.Diagnostics;
    using Microsoft.Data.SqlClient;

    public class ActivityTrace : IDisposable
    {
        private bool dispose = false;

        public DateTime Session { get; }
        public string Version { get; set; }
        public string ActivityId { get; }
        public string Operation { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public double Duration { get; set; }
        private string DatabaseConnection { get; set; }
        private Stopwatch Tracer { get; set; }

        public ActivityTrace(string databaseConnectionString, string activityId = null)
        {
            this.Session = DateTime.UtcNow;
            this.Version = "NoContainer";
            this.ActivityId = activityId ?? Guid.NewGuid().ToString();
            this.Operation = "NoOperation";
            this.Code = "OK";
            this.Status = true;
            this.Tracer = new Stopwatch();
            this.Tracer.Start();
            this.DatabaseConnection = databaseConnectionString;
        }

        public void AddContainer(string container)
        {
            this.Version = container;
        }

        public void AddOperation(string operation)
        {
            this.Operation = operation;
        }

        public void AddCode(string code)
        {
            code = code.Split(",KEY")[0];

            this.Code = code;
            this.Status = false;
        }

        private void AddLog()
        {
            try
            {
                using (var connection = new SqlConnection(DatabaseConnection))
                {
                    var cmd = new SqlCommand("usp_PlyQor_Trace_Insert", connection);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("dt_timestamp", Session);
                    cmd.Parameters.AddWithValue("nvc_version", Version);
                    cmd.Parameters.AddWithValue("nvc_id", ActivityId);
                    cmd.Parameters.AddWithValue("nvc_operation", Operation);
                    cmd.Parameters.AddWithValue("nvc_code", Code);
                    cmd.Parameters.AddWithValue("nvc_status", Status.ToString());
                    cmd.Parameters.AddWithValue("i_duration", Duration);

                    connection.Open();

                    var reader = cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!dispose)
            {
                if (disposing)
                {
                    //dispose managed resources

                    Tracer.Stop();
                    this.Duration = Tracer.Elapsed.TotalMilliseconds;

                    if (this.Status)
                    {
                        Console.WriteLine($"{this.Operation} @ {this.Duration} ms");
                    }
                    else
                    {
                        Console.WriteLine($"<!> {this.Code} <!> {this.Operation} @ {this.Duration} ms");
                    }

                    AddLog();
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
    }
}
