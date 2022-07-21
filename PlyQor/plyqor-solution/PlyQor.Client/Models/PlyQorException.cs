namespace PlyQor.Models
{
    using System;
    using System.Runtime.Serialization;

    public class PlyQorException : Exception
    {
        public PlyQorException()
        {
        }

        public PlyQorException(string message) : base(message)
        {
        }

        public PlyQorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PlyQorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
