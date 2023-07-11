namespace KirokuG2.Loader
{
    public class LogInstance
    {
        public string Id { get; }

        public DateTime Start { get; private set; }

        public DateTime Stop { get; private set; }

        public string Source { get; private set; }

        public string Function { get; private set; }

        public int Errors => _errors;

        private int _errors;

        public int Duration => _duration;

        private int _duration;

        public bool Result => _result;

        private bool _result;

        public LogInstance(string id)
        {
            Id = id;
            Source = String.Empty;
            Function = String.Empty;
        }

        public void Update(DateTime start, string source, string function)
        {
            Start = start;
            Source = source;
            Function = function;
        }

        public void StopInstance(DateTime stop)
        {
            Stop = stop;

            if (Start != DateTime.MinValue && Stop != DateTime.MinValue)
            {
                _duration = (int)(Stop - Start).TotalMilliseconds;

                _result = true;
            }
            else
            {
                _duration = -1;

                _result = false;
            }
        }

        public void AddErrorCount(int errors)
        {
            _errors = errors;
        }
    }
}
