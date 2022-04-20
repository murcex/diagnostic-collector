namespace PlyQor.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using PlyQor.Resources;

    public class ResultManager
    {
        private Dictionary<string, string> _result;

        public ResultManager()
        {
            _result = new Dictionary<string, string>();
        }

        public void AddResultData(string input)
        {
            _result.Add(ResultKeys.Data, input);
        }

        public void AddResultData(List<string> input)
        {
            var data = JsonConvert.SerializeObject(input);

            _result.Add(ResultKeys.Data, data);
        }

        public void AddResultData(bool input)
        {
            _result.Add(ResultKeys.Data, input.ToString());
        }

        public void AddResultData(int input)
        {
            _result.Add(ResultKeys.Data, input.ToString());
        }

        public void AddResultSuccess()
        {
            // TODO: move literal string to const
            _result.Add(ResultKeys.Status, "True");
            _result.Add(ResultKeys.Code, "OK");
        }

        public Dictionary<string, string> ExportDataSet()
        {
            return _result;
        }

        public void MergeResults(ResultManager result)
        {
            var records = result.ExportDataSet();

            foreach (var record in records)
            {
                if (!_result.TryAdd(record.Key, record.Value))
                {
                    // TODO: move literal string to const
                    throw new PlyQorException($"{StatusCode.ERR011},KEY={record.Key}");
                }
            }
        }
    }
}
