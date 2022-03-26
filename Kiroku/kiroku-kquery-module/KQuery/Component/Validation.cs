namespace KQuery.Component
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;

    class Validation
    {
        /// <summary>
        /// Check the request for null value.
        /// </summary>
        public static bool CheckRequest(
            HttpRequest req, 
            out string message, 
            out OkObjectResult result)
        {
            if (req == null)
            {
                message = "Request is empty.";
                result = new OkObjectResult("");
                return false;
            }
            else
            {
                message = null;
                result = null;
                return true;
            }
        }

        /// <summary>
        /// Check if the request (KLOG) Id is null or empty.
        /// </summary>
        public static bool CheckRequestId(
            string idInput,
            out string message,
            out OkObjectResult result)
        {
            if (string.IsNullOrEmpty(idInput))
            {
                message = $"Log Id is NullOrEmpty.";
                result = new OkObjectResult("");
                return false;
            }
            else
            {
                try
                {
                    Guid checkId = new Guid(idInput);

                    message = null;
                    result = null;
                    return true;
                }
                catch
                {
                    message = $"Log Id format is incorrect.";
                    result = new OkObjectResult("");
                    return false;
                }
            }
        }

        /// <summary>
        /// Check if the KLOG queried from storage is null or empty.
        /// </summary>
        public static bool CheckLog(
            string idInput,
            string logInput, 
            out string message, 
            out OkObjectResult result)
        {
            if (string.IsNullOrEmpty(logInput))
            {
                message = $"Log is null - Id: {idInput}";
                result = new OkObjectResult("");
                return false;
            }
            else
            {
                message = null;
                result = null;
                return true;
            }
        }
    }
}
