using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Web;

namespace Exam.Api.Utility
{
    public enum MyCachePriority
    {
        Default,
        NotRemovable
    }

    public abstract class CacheWrapper
    {
        protected static ObjectCache cache = MemoryCache.Default;
        protected CacheItemPolicy pocily;
        protected abstract ChangeMonitor Monitor { get; }
        protected abstract DateTimeOffset Expiration { get; }

        public void AddToCache(string key, Object cacheItem, MyCachePriority myCachePriority, DateTimeOffset expiration)
        {
            if (!cache.Contains(key))
            {
                pocily = new CacheItemPolicy();
                pocily.Priority = (myCachePriority == MyCachePriority.Default) ?
                                                CacheItemPriority.Default : CacheItemPriority.NotRemovable;
                pocily.AbsoluteExpiration = expiration;
                pocily.ChangeMonitors.Add(Monitor);
                cache.Set(key, cacheItem, pocily);
            }
        }

        public void AddToCache(string key, Object cacheItem, MyCachePriority myCachePriority)
        {
            if (!cache.Contains(key))
            {
                pocily = new CacheItemPolicy();
                pocily.Priority = (myCachePriority == MyCachePriority.Default) ?
                                                CacheItemPriority.Default : CacheItemPriority.NotRemovable;
                pocily.AbsoluteExpiration = Expiration;
                pocily.ChangeMonitors.Add(Monitor);
                cache.Set(key, cacheItem, pocily);
            }
        }

        public Object GetMyCachedItem(String key)
        {
            return cache[key];
        }

        public void RemoveMyCachedItem(String key)
        {
            if (cache.Contains(key))
            {
                cache.Remove(key);
            }
        }
    }

    /// <summary>
    /// 用监控文件变化来更新缓存的方式
    /// </summary>
    public class FileMonitorCacheWrapper : CacheWrapper
    {
        private string filePath = HttpContext.Current.Server.MapPath("~") + "\\cache.txt";
        protected override ChangeMonitor Monitor
        {
            get
            {
                return new HostFileChangeMonitor(new List<string> { filePath });
            }
        }

        /// <summary>
        /// 缓存时间，默认1天
        /// </summary>
        protected override DateTimeOffset Expiration
        {
            get { return DateTimeOffset.Now.AddDays(1); }
        }
    }
}