namespace Implements
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Deserializer : IDisposable
    {
        /// <summary>
        /// Internal collection of deserialized information, in a dictionary of Tags to their lists of KeyValuePair sets.
        /// </summary>
        Dictionary<string, List<KeyValuePair<string, string>>> _tagCollection { get; set; }

        /// <summary>
        /// Flag used by the rule engine to determine if a Tag has been identified.
        /// </summary>
        static bool TagFilterSwitch = false;

        /// <summary>
        /// The current Tag name.
        /// </summary>
        static string CurrentTagName;

        /// <summary>
        /// Disposable flag.
        /// </summary>
        bool dispose = false;

        /// <summary>
        /// Core Deserializer process.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="logOperation"></param>
        /// <param name="logValidation"></param>
        /// <returns></returns>
        public Dictionary<string, List<KeyValuePair<string, string>>> Execute(string fileName, bool logOperation = false, bool logValidation = false)
        {
            if (logOperation || logValidation)
            {
                if (!Log.Status)
                {
                    throw new Exception($"Log Exception [Log].[Status]: Logger has not been Initialized!");
                }
            }
            if (logOperation)
            {
                Log.Info($"File Name: {Path.GetFileName(fileName)}");
            }

            List<string> lines = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    while (!reader.EndOfStream)
                    {
                        lines.Add(reader.ReadLine());
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Deserializer Exception [Deserializer].[Execute()]: StreamReaders Error: {e.ToString()}");
            }

            List<string> includesFiles = new List<string>();

            ///
            /// --- DESERIALIZER RULE ENGINE ---
            ///

            Dictionary<string, List<KeyValuePair<string, string>>> tagCollection = new Dictionary<string, List<KeyValuePair<string, string>>>();

            List<KeyValuePair<string, string>> tagList = new List<KeyValuePair<string, string>>();

            try
            {
                foreach (var line in lines)
                {
                    if (logOperation)
                    {
                        Log.Info("");
                        Log.Info($"Line Check: {line}");
                    }

                    if (TagFilterSwitch && line != string.Empty && !line.Contains(";"))
                    {
                        if (line.Contains("[") && line.Contains("]"))
                        {
                            TagFilterSwitch = true;

                            var tagName = CleanTag(line);

                            tagCollection.Add(CurrentTagName, tagList);

                            tagList = new List<KeyValuePair<string, string>>();

                            CurrentTagName = tagName;

                            if (logOperation)
                            {
                                Log.Info($"New Tag Dectected: {tagName}");
                                Log.Info($"Added tagList to tagCollection.");
                                Log.Info($"Cleared tagList.");
                                Log.Info($"Update CurrentTag.");
                            }
                        }
                        else
                        {
                            var SecondValueSwitch = false;

                            string firstValue = string.Empty;
                            string secondValue = string.Empty;

                            char checkthis = '=';

                            foreach (var chr in line)
                            {
                                if (!SecondValueSwitch)
                                {
                                    if (chr == checkthis)
                                    {
                                        SecondValueSwitch = true;
                                    }
                                    else
                                    {
                                        if (firstValue == string.Empty)
                                        {
                                            firstValue = chr.ToString();
                                        }
                                        else
                                        {
                                            firstValue = string.Concat(firstValue, chr.ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    if (secondValue == string.Empty)
                                    {
                                        secondValue = chr.ToString();
                                    }
                                    else
                                    {
                                        secondValue = string.Concat(secondValue, chr.ToString());
                                    }
                                }
                            }

                            KeyValuePair<string, string> kvpModel = new KeyValuePair<string, string>(firstValue, secondValue);
                            
                            tagList.Add(kvpModel);

                            if (logOperation)
                            {
                                Log.Info($"Tag Member Detected: {line} -- Adding to {CurrentTagName} list.");
                                Log.Info($"Part A: {firstValue} Part B: {secondValue}");
                            }
                        }
                    }
                    else if (!line.Contains(";"))
                    {
                        if (line.Contains("[") && line.Contains("]"))
                        {
                            TagFilterSwitch = true;

                            var tagName = CleanTag(line);

                            CurrentTagName = tagName;

                            if (logOperation)
                            {
                                Log.Info($"Tag Detected: {tagName}");
                            }
                        }
                        else
                        {
                            if (logOperation)
                            {
                                Log.Info($"No-Hit: {line}");
                            }
                        }
                    }
                    else
                    {
                        if (line.Contains(";"))
                        {
                            if (logOperation)
                            {
                                Log.Info($"Comment Detected: {line}");
                            }
                        }
                        else
                        {
                            if (logOperation)
                            {
                                Log.Info($"Unknown Hit: {line}");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Deserializer Exception [Deserializer].[Execute()]: Rule Engine Error: {e.ToString()}");
            }

            if (logValidation)
            {
                try
                {
                    Log.Info($"");
                    Log.Info($"--- Validation ---");

                    foreach (var tag in tagCollection)
                    {
                        Log.Info("");
                        Log.Info($"Tag: {tag.Key}");

                        foreach (var pair in tag.Value)
                        {
                            Log.Info($"Key: {pair.Key} = Value: {pair.Value}");
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception($"Deserializer Exception [Deserializer].[Execute()]: Validation Error: {e.ToString()}");
                }
            }

            _tagCollection = tagCollection;
            return tagCollection;
        }

        /// <summary>
        /// Clean the Tag of brackets.
        /// </summary>
        /// <param name="rawTag"></param>
        /// <returns></returns>
        private string CleanTag(string rawTag)
        {
            var tagName = rawTag.Replace("[", "");
            tagName = tagName.Replace("]", "");

            return tagName;
        }

        /// <summary>
        /// Get the complete collection of Tags and their KeyValuePair lists.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<KeyValuePair<string, string>>> GetCollection()
        {
            return _tagCollection;
        }

        /// <summary>
        /// Get a single Tag and it's KeyValuePair list.
        /// </summary>
        /// <param name="_tag"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> GetTag(string _tag)
        {
            List<KeyValuePair<string, string>> _tagList = new List<KeyValuePair<string, string>>();

            _tagList = _tagCollection.Where(x => x.Key == _tag).Select(x => x.Value).FirstOrDefault();

            return _tagList;
        }

        /// <summary>
        /// Get a single Value from within the current output collection by providing the group Tag and Key.
        /// </summary>
        /// <param name="_tag"></param>
        /// <param name="_key"></param>
        /// <returns></returns>
        public string GetValue(string _tag, string _key)
        {
            List<KeyValuePair<string, string>> _tagList = new List<KeyValuePair<string, string>>();
            string _value = null;

            _tagList = _tagCollection.Where(x => x.Key == _tag).Select(x => x.Value).FirstOrDefault();

            _value = _tagList.Where(x => x.Key == _key).Select(x => x.Value).FirstOrDefault();

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
            List<KeyValuePair<string, string>> _tagList = new List<KeyValuePair<string, string>>();
            List<string> _value = new List<string>();

            _tagList = _tagCollection.Where(x => x.Key == _tag).Select(x => x.Value).FirstOrDefault();

            _value = _tagList.Where(x => x.Key == _key).Select(x => x.Value).ToList();

            return _value;
        }

        /// <summary>
        /// Disposable Logic
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!dispose)
            {
                if (disposing)
                {
                    //dispose managed resources
                }
            }

            //dispose unmanaged resources
            dispose = true;
        }

        /// <summary>
        /// Dispose process.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
