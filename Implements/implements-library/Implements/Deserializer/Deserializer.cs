namespace Implements
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Deserializer : IDisposable
    {
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
        public Dictionary<string, List<KVPModel>> Execute(string fileName, bool logOperation = false, bool logValidation = false)
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

            Dictionary<string, List<KVPModel>> tagCollection = new Dictionary<string, List<KVPModel>>();

            List<KVPModel> tagList = new List<KVPModel>();

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

                            tagList = new List<KVPModel>();

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

                            KVPModel kvpModel = new KVPModel();

                            kvpModel.A = firstValue;
                            kvpModel.B = secondValue;

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
                            Log.Info($"A: {pair.A} = B: {pair.B}");
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception($"Deserializer Exception [Deserializer].[Execute()]: Validation Error: {e.ToString()}");
                }
            }

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
