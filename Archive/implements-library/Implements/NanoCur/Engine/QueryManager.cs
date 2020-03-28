namespace Implements
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class QueryManager : IDisposable
    {
        private static Dictionary<string, string> _collectionDictionary;
        private static string _collectionName;
        private bool _collectionState;
        private FileManager _fileManager;
        private DataManager _dataManager;
        private bool dispose;

        public QueryManager(string collectionName, bool collectionState)
        {
            _collectionName = collectionName;
            _collectionState = collectionState;

            _fileManager = new FileManager();
            _dataManager = new DataManager();

        }

        private Dictionary<string, string> CollectionDictionary
        {
            get
            {
                if (_collectionDictionary == null)
                {
                    var lines = _fileManager.ReadCollection(_collectionName);

                    _collectionDictionary = _dataManager.Deserialize(lines);
                }

                return _collectionDictionary;
            }
        }

        private bool CreateDictionary()
        {
            _collectionDictionary = new Dictionary<string, string>();

            return true;
        }

        private bool CommitDictionary()
        {
            var lines = _dataManager.Serialize(_collectionDictionary);

            return _fileManager.ReadAllLinesToCollection(_collectionName, lines);
        }

        //
        //
        // public query methods
        //
        //

        public string Select(string key)
        {
            string result = SelectFromDictionary(key);

            return result;
        }

        public bool Insert(string key, string value)
        {
            if (!_collectionState)
            {
                if (!CheckSelectFromDictionary(key))
                {
                    if (AddRecordToDictionary(key, value))
                    {
                        if (CommitDictionary())
                        {
                            return true;
                        }
                        else
                        {
                            // failed to commit dictionary -- marking false
                            return false;
                        }
                    }
                    else
                    {
                        // failed to insert record -- marking false
                        return false;
                    }
                }
                else
                {
                    // record already exist -- marking false
                    return false;
                }
            }
            else
            {
                if (CreateDictionary())
                {
                    if (AddRecordToDictionary(key, value))
                    {
                        if (CommitDictionary())
                        {
                            return true;
                        }
                        else
                        {
                            // failed to commit dictionary -- marking false
                            return false;
                        }
                    }
                    else
                    {
                        // failed to add record -- marking false
                        return false;
                    }
                }
                else
                {
                    // failed to create dictionary -- marking false
                    return false;
                }
            }
        }

        public bool Update(string key, string value)
        {
            if (CheckSelectFromDictionary(key))
            {
                if (UpdateRecordInDictionary(key, value))
                {
                    if (value == SelectFromDictionary(key))
                    {
                        if (CommitDictionary())
                        {
                            return true;
                        }
                        else
                        {
                            // failed to commit dictionary -- marking false
                            return false;
                        }
                    }
                    else
                    {
                        // cross-check failed -- marking false
                        return false;
                    }
                }
                else
                {
                    // failed to update dictionary -- marking false
                    return false;
                }
            }
            else
            {
                // key doesn't exist in dictionary -- marking false
                return true;
            }
        }

        public bool Delete(string key)
        {
            if (CheckSelectFromDictionary(key))
            {
                if (DeleteRecordFromDictionary(key))
                {
                    if (!CheckSelectFromDictionary(key))
                    {
                        if (CommitDictionary())
                        {
                            return true;
                        }
                        else
                        {
                            // failed to commit dictionary -- marking false
                            return false;
                        }
                    }
                    else
                    {
                        // failed to confirm record was delete from dictionary -- marking false
                        return false;
                    }
                }
                else
                {
                    // failed to delete from dictionary -- marking false
                    return false;
                }
            }
            else
            {
                // key doesn't exist in dictionary -- marking false
                return true;
            }
        }

        //
        //
        // private supporting query methods
        //
        //

        private bool CheckSelectFromDictionary(string key)
        {
            var tryGetStatus = CollectionDictionary.TryGetValue(key, out string value);

            return tryGetStatus;
        }

        private string SelectFromDictionary(string key)
        {
            var tryGetStatus = CollectionDictionary.TryGetValue(key, out string value);

            return value;
        }

        private bool AddRecordToDictionary(string key, string value)
        {
            CollectionDictionary.Add(key, value);

            return true;
        }

        private bool DeleteRecordFromDictionary(string key)
        {
            CollectionDictionary.Remove(key);

            return true;
        }

        private bool UpdateRecordInDictionary(string key, string value)
        {
            CollectionDictionary[key] = value;

            return true;
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
