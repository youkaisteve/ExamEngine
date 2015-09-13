using System;
using System.Runtime.Caching;

namespace Component.Tools.Cache
{
    public enum MyCachePriority
    {
        Default,
        NotRemovable
    }

    public abstract class CacheWrapper
    {
        protected static ObjectCache Cache = MemoryCache.Default;
        protected CacheItemPolicy Pocily;
        protected abstract ChangeMonitor Monitor { get; }
        protected abstract DateTimeOffset Expiration { get; }

        public void AddToCache(string key, Object cacheItem, MyCachePriority myCachePriority, DateTimeOffset expiration)
        {
            if (!Cache.Contains(key))
            {
                Pocily = new CacheItemPolicy
                {
                    Priority = (myCachePriority == MyCachePriority.Default)
                        ? CacheItemPriority.Default
                        : CacheItemPriority.NotRemovable,
                    AbsoluteExpiration = expiration
                };
                Pocily.ChangeMonitors.Add(Monitor);
                Cache.Set(key, cacheItem, Pocily);
            }
        }

        public void AddToCache(string key, Object cacheItem, MyCachePriority myCachePriority)
        {
            if (!Cache.Contains(key))
            {
                Pocily = new CacheItemPolicy();
                Pocily.Priority = (myCachePriority == MyCachePriority.Default)
                    ? CacheItemPriority.Default
                    : CacheItemPriority.NotRemovable;
                Pocily.AbsoluteExpiration = Expiration;
                Pocily.ChangeMonitors.Add(Monitor);
                Cache.Set(key, cacheItem, Pocily);
            }
        }

        public Object GetMyCachedItem(String key)
        {
            return Cache[key];
        }

        public void RemoveMyCachedItem(String key)
        {
            if (Cache.Contains(key))
            {
                Cache.Remove(key);
            }
        }
    }
}