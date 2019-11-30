namespace Configurator.WebService
{
    class CfgIO
    {
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
                //document = Encoding.ASCII.GetBytes("GetCfg Exception.");

                return document;
            }
        }
    }
}
