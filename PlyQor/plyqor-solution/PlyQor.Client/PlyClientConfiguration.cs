namespace PlyQor.Client
{
    public class PlyClientConfiguration
    {
        /// <summary>
        /// Target PlyQor Uri
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Target PlyQor Container
        /// </summary>
        public string Container { get; set; }

        /// <summary>
        /// Access Token for the Target PlyQor Container
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Throw an exception when result data is NullOrEmpty.
        /// </summary>
        public bool NullOrEmptyDataException { get; set; }

        /// <summary>
        /// Throw an exception when the result status is false.
        /// </summary>
        public bool StatusException { get; set; }

        /// <summary>
        /// Throw an exception when the result is maleform.
        /// </summary>
        public bool MaleformException { get; set; }

        /// <summary>
        /// Let exceptions pass through during Http Requests.
        /// </summary>
        public bool HttpRequestException { get; set; }

        /// <summary>
        /// The number of retries to make for a single reuest on a server side error.
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        /// The duration between each retry.
        /// </summary>
        public int RetryCooldown { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RetryBackoff { get; set; }

        /// <summary>
        /// Provide client duration in the result.
        /// </summary>
        public bool ProvideClientDuration { get; set; }

        /// <summary>
        /// Provide retry count in the result.
        /// </summary>
        public bool ProvideRetryCount { get; set; }

        public PlyClientConfiguration()
        {
            // default values
            NullOrEmptyDataException = false;
            StatusException = true;
            MaleformException = true;
            HttpRequestException = true;
            RetryCount = 3;
            RetryCooldown = 200;
            RetryBackoff = 100;
            ProvideClientDuration = false;
            ProvideRetryCount = false;
        }
    }
}
