namespace Configurator.WebService
{
    class CfgIO
    {
        /// <summary>
        /// Get configuration document from storage.
        /// </summary>
        /// <param name="cfgKey">Configuration Key</param>
        /// <param name="cfgApp">Configuration Application</param>
        public static byte[] GetCfg(string cfgKey, string cfgApp)
        {
            byte[] document = null;

            var fileName = $"{cfgKey}_{cfgApp}.txt";

            try
            {
                document = StorageClient.GetDocument(fileName);

                return document;
            }
            catch
            {
                return document;
            }
        }
    }
}
