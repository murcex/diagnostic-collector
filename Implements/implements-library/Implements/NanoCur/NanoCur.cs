using System.IO;
using System.Reflection;

namespace Implements
{
    class NanoCfg
    {
        public string CollectionName { get; set; }

        public string Root { get; set; }

        public string FileExtension { get; set; }

        public string Delimiter { get; set; }

        public NanoCfg()
        {
            Root = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            FileExtension = ".txt";
            Delimiter = ",";
        }
    }

    class NanoCur
    {
        // cfg
        static private string _collectionName = "empty";
        static private bool _collectionState = false;
        static private string _root = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        static private string _fileExtension = ".txt";
        // check collection state
        // check for empty/null

        public NanoCur(string collectionName)
        {

            //VerifyCollectionName()
            _collectionName = collectionName;

            //VerifyCollection()
        }

        public NanoCur(NanoCfg cfg)
        {

        }

        // insert
        public static bool Insert(string key, string value)
        {
            if (VerifyKey(key)) { return false; }

            bool result = false;
            using (QueryManager engine = new QueryManager(_collectionName, _collectionState))
            {
                result = engine.Insert(key, value);
            }

            return result;
        }

        // update

        // select

        // delete

        private static bool VerifyKey(string key)
        {
            return !string.IsNullOrEmpty(key);
        }

        private static bool VerifyValue(string value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}
