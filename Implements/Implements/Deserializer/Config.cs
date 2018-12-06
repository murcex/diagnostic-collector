namespace Implements
{
    using System.Collections.Generic;
    using System.Linq;

    public class Config
    {
        /// <summary>
        /// dictionary output collection from the deserializer.
        /// </summary>
        public Dictionary<string, List<KVPModel>> Collection { get; set; }

        /// <summary>
        /// Get a single Value from within the current output collection by providing the group Tag and Key.
        /// </summary>
        /// <param name="_tag"></param>
        /// <param name="_key"></param>
        /// <returns></returns>
        public string GetValue(string _tag, string _key)
        {
            List<KVPModel> _tagList = new List<KVPModel>();
            string _value = null;

            _tagList = Collection.Where(x => x.Key == _tag).Select(x => x.Value).FirstOrDefault();

            _value = _tagList.Where(x => x.A == _key).Select(x => x.B).FirstOrDefault();

            return _value;
        }

        /// <summary>
        /// Get multiple Values from within the current output collection by providing the group Tag and Key.
        /// </summary>
        /// <param name="_tag"></param>
        /// <param name="_key"></param>
        /// <returns></returns>
        public List<string> GetValues(string _tag, string _key)
        {
            List<KVPModel> _tagList = new List<KVPModel>();
            List<string> _value = new List<string>();

            _tagList = Collection.Where(x => x.Key == _tag).Select(x => x.Value).FirstOrDefault();

            _value = _tagList.Where(x => x.A == _key).Select(x => x.B).ToList();

            return _value;
        }
    }
}
