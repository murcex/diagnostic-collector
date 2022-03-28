namespace PlyQor.Models
{
    using System;
    using System.Runtime.Serialization;

    public class JavelinException : Exception
    {
        public JavelinException()
        {
        }

        public JavelinException(string message) : base(message)
        {
        }

        public JavelinException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected JavelinException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
