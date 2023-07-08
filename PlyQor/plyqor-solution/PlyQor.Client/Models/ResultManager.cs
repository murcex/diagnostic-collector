namespace PlyQor.Models
{
    using Newtonsoft.Json;
    using PlyQor.Client.Resources;
    using System.Collections.Generic;

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
            _result.Add(ResultKeys.Status, bool.TrueString);
            _result.Add(ResultKeys.Code, StatusCode.OK);
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
                    // TODO: add const interpolated string
                    throw new PlyQorException($"{StatusCode.ERR011},KEY={record.Key}");
                }
            }
        }
    }
}
