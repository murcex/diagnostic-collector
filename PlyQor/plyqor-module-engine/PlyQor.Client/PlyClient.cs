namespace PlyQor.Client
{
    public class PlyClient
    {
        private string _uri;

        private string _token;

        private string _container;

        public PlyClient(string uri, string container, string token)
        {
            // TODO: add null check

            _uri = uri;
            _container = container;
            _token = token;
        }

        public Dictionary<string,string> InsertKey(string key, string data, string tag)
        {
            List<string> tags = new List<string>();
            tags.Add(tag);

            return InsertKeyInternal.Execute(_uri, _container, _token, key, data, tags);
        }

        public Dictionary<string, string> InsertKey(string key, string data, string tag_1, string tag_2)
        {
            List<string> tags = new List<string>();
            tags.Add(tag_1);
            tags.Add(tag_2);

            return InsertKeyInternal.Execute(_uri, _container, _token, key, data, tags);
        }

        public Dictionary<string, string> InsertKey(string key, string data, string tag_1, string tag_2, string tag_3)
        {
            List<string> tags = new List<string>();
            tags.Add(tag_1);
            tags.Add(tag_2);
            tags.Add(tag_3);

            return InsertKeyInternal.Execute(_uri, _container, _token, key, data, tags);
        }

        public Dictionary<string, string> InsertKey(string key, string data, List<string> tags)
        {
            return InsertKeyInternal.Execute(_uri, _container, _token, key, data, tags);
        }

        public Dictionary<string, string> InsertTag(string key, string tag)
        {
            return InsertTagInternal.Execute(_uri, _container, _token, key, tag);
        }

        public Dictionary<string, string> SelectKey(string key)
        {
            return SelectKeyInternal.Execute(_uri, _container, _token, key);
        }

        public Dictionary<string, string> SelectKeyList(string key, int count)
        {
            return SelectKeyListInternal.Execute(_uri, _container, _token, key, count);
        }

        public Dictionary<string, string> SelectTagCount(string tag)
        {
            return SelectTagCountInternal.Execute(_uri, _container, _token, tag);
        }

        public Dictionary<string, string> SelectTagsByKey(string key)
        {
            return SelectTagsByKeyInternal.Execute(_uri, _container, _token, key);
        }

        public Dictionary<string, string> SelectTags()
        {
            return SelectTagsInternal.Execute(_uri, _container, _token);
        }

        public Dictionary<string, string> UpdateData(string key, string data)
        {
            return UpdateDataInternal.Execute(_uri, _container, _token, key, data);
        }

        public Dictionary<string, string> UpdateKey(string key, string newKey)
        {
            return UpdateKeyInternal.Execute(_uri, _container, _token, key, newKey);
        }

        public Dictionary<string, string> UpdateTagByKey(string key, string tag, string newTag)
        {
            return UpdateTagByKeyInternal.Execute(_uri, _container, _token, key, tag, newTag);
        }

        public Dictionary<string, string> UpdateTag(string tag, string newTag)
        {
            return UpdateTagInternal.Execute(_uri, _container, _token, tag, newTag);
        }

        public Dictionary<string, string> DeleteKey(string key)
        {
            return DeleteKeyInternal.Execute(_uri, _container, _token, key);
        }

        public Dictionary<string, string> DeleteTagByKey(string key, string tag)
        {
            return DeleteTagByKeyInternal.Execute(_uri, _container, _token, key, tag);
        }

        public Dictionary<string, string> DeleteTag(string tag)
        {
            return DeleteTagInternal.Execute(_uri, _container, _token, tag);
        }

        public Dictionary<string, string> DeleteTagsByKey(string key)
        {
            return DeleteTagsByKeyInternal.Execute(_uri, _container, _token, key);
        }
    }
}
