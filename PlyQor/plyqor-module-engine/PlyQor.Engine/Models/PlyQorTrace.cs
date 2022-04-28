namespace PlyQor.Models
{
    using System;
    using System.Diagnostics;
    using Microsoft.Data.SqlClient;
    using PlyQor.Engine.Resources;

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

        public PlyQorTrace(string databaseConnectionString, string traceId = null)
        {
            this.Session = DateTime.UtcNow;
            this.Container = TraceValues.TraceNoContainer;
            this.TraceId = traceId ?? Guid.NewGuid().ToString();
            this.Operation = TraceValues.TraceNoOperation;
            this.Code = TraceValues.OK;
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
            code = code.Split(TraceValues.TraceKeySplit)[0];

            this.Code = code;
            this.Status = false;
        }

        private void AddLog()
        {
            try
            {
                using (var connection = new SqlConnection(DatabaseConnection))
                {
                    var cmd = new SqlCommand(TraceValues.InsertTrace, connection);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(TraceValues.Timestamp, Session);
                    cmd.Parameters.AddWithValue(TraceValues.Container, Container.ToUpper());
                    cmd.Parameters.AddWithValue(TraceValues.Id, TraceId);
                    cmd.Parameters.AddWithValue(TraceValues.Operation, Operation);
                    cmd.Parameters.AddWithValue(TraceValues.Code, Code);
                    cmd.Parameters.AddWithValue(TraceValues.Status, Status.ToString());
                    cmd.Parameters.AddWithValue(TraceValues.Duration, Duration);

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
