namespace PlyQor.Models
{
    using System;
    using System.Diagnostics;
    using Microsoft.Data.SqlClient;

    public class PlyQorTrace : IDisposable
    {
        private bool dispose = false;

        public DateTime Session { get; }
        public string Container { get; set; }
        public string TraceId { get; }
        public string Operation { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public double Duration { get; set; }
        private string DatabaseConnection { get; set; }
        private Stopwatch Tracer { get; set; }

        public PlyQorTrace(string databaseConnectionString, string activityId = null)
        {
            // TODO: move literal string to const
            this.Session = DateTime.UtcNow;
            this.Container = "NoContainer";
            this.TraceId = activityId ?? Guid.NewGuid().ToString();
            this.Operation = "NoOperation";
            this.Code = "OK";
            this.Status = true;
            this.Tracer = new Stopwatch();
            this.Tracer.Start();
            this.DatabaseConnection = databaseConnectionString;
        }

        public void AddContainer(string container)
        {
            this.Container = container;
        }

        public void AddOperation(string operation)
        {
            this.Operation = operation;
        }

        public void AddCode(string code)
        {
            // TODO: move literal string to const
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
                    // TODO: move literal string to const
                    var cmd = new SqlCommand("usp_PlyQor_Trace_InsertTrace", connection);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // TODO: move literal string to const
                    cmd.Parameters.AddWithValue("dt_timestamp", Session);
                    cmd.Parameters.AddWithValue("nvc_container", Container);
                    cmd.Parameters.AddWithValue("nvc_id", TraceId);
                    cmd.Parameters.AddWithValue("nvc_operation", Operation);
                    cmd.Parameters.AddWithValue("nvc_code", Code);
                    cmd.Parameters.AddWithValue("nvc_status", Status.ToString());
                    cmd.Parameters.AddWithValue("i_duration", Duration);

                    cmd.CommandTimeout = 0;

                    connection.Open();

                    var reader = cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            { }
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
