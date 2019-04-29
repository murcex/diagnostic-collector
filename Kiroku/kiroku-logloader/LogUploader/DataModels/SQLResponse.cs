namespace KLOGLoader
{
    using System;

    public class SQLResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public Guid Id { get; set; }

        public void Successful()
        {
            this.Success = true;
        }

        public void Failure(string msg)
        {
            this.Message = msg;
        }
    }
}
