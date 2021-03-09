using DictionaryService.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DictionaryService
{
    public class StorageService: IStorageService
    {
        private readonly Dictionary<string, string> _storage;
        private readonly object locker = new object();
        public StorageService()
        {
            _storage = new Dictionary<string, string>();
        }
        public void Add(string key, string value)
        {

            if (string.IsNullOrEmpty(key))
                throw new KeyNotFoundException();
            bool isLocked = false;
            try
            {
                Monitor.Enter(locker, ref isLocked);
                if (_storage.ContainsKey(key))
                {
                    _storage[key] = value;
                }
                else
                {
                    _storage.Add(key, value);
                }
            }
            finally
            {
                if (isLocked) Monitor.Exit(locker);
            }
        }

        public IEnumerable<string> GetAllKeys()
        {
            bool isLocked = false;
            try
            {
                Monitor.Enter(locker, ref isLocked);
                return _storage.Where(x => !string.IsNullOrEmpty(x.Value)).Select(x => x.Key);
            }
            finally
            {
                if (isLocked) Monitor.Exit(locker);
            }
        }

        public void Delete(string key)
        {
            bool isLocked = false;
            if (string.IsNullOrEmpty(key) || !_storage.ContainsKey(key))
                throw new KeyNotFoundException();
            try
            {
                Monitor.Enter(locker, ref isLocked);
                _storage[key] = string.Empty;
            }
            catch (KeyNotFoundException ane)
            {
                throw new KeyNotFoundException(ane.StackTrace);
            }
            finally
            {
                if (isLocked) Monitor.Exit(locker);
            }
        }

        public string Get(string key)
        {
            bool isLocked = false;
            try
            {
                Monitor.Enter(locker, ref isLocked);
                var value = _storage[key];
                return value;
            }
            catch (KeyNotFoundException ane)
            {
                throw new KeyNotFoundException(ane.StackTrace);
            }
            finally
            {
                if (isLocked) Monitor.Exit(locker);
            }
        }
    }
}
