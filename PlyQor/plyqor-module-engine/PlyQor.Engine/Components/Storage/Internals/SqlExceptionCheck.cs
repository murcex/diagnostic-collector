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
                throw new JavelinException("ERR888", ex);
            }

            if (ex.Message.Contains("PRIMARY KEY constraint"))
            {
                throw new JavelinException("ERR999", ex);
            }

            throw new JavelinException("ERR777", ex);
        }
    }
}
