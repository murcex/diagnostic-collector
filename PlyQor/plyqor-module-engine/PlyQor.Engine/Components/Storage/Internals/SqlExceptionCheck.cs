namespace PlyQor.Engine.Components.Storage.Internals
{
    using System;
    using PlyQor.Models;

    class SqlExceptionCheck
    {
        public static void Execute(Exception ex)
        {
            if (ex.Message.Contains("request limit"))
            {
                throw new PlyQorException("ERR888", ex);
            }

            if (ex.Message.Contains("PRIMARY KEY constraint"))
            {
                throw new PlyQorException("ERR999", ex);
            }

            throw new PlyQorException("ERR777", ex);
        }
    }
}
