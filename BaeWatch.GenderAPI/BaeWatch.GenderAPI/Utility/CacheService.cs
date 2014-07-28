using System;
using System.Runtime.Caching;

namespace BaeWatch.GenderAPI.Utility
{
    public class CacheService
    {
        private static volatile CacheService _instance;
        private static object _syncLock = new object();

        private MemoryCache _memCache;

        private CacheService()
        {
            this._memCache = new MemoryCache("BaeWatch");
        }

        public static CacheService Instance
        {
            get
            {
                if (_instance == null)
                    lock (_syncLock)
                        if (_instance == null)
                            _instance = new CacheService();

                return _instance;
            }
        }

        public long Count { get { return this._memCache.GetCount(); } }

        public Boolean AddObject(string inKey_, object inObj_, long inTTL_)
        {
            try
            {
                this._memCache.Add(inKey_, inObj_, DateTime.Now.AddMilliseconds(inTTL_));
                return true;
            }
            catch (Exception ex)
            {
                Logger.Instance.LogIt(string.Format("Unable to cache object with key {0}", inKey_), LOG_STATE.Critical, ex);
                return false;
            }
        }

        public T RetrieveObject<T>(string inKey_)
        {
            try
            {
                return (T)this._memCache.Get(inKey_);
            }
            catch (Exception ex)
            {
                Logger.Instance.LogIt(string.Format("Unable to return cached object with key {0}", inKey_), LOG_STATE.Critical, ex);
                return default(T);
            }
        }
    }
}