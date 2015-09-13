using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Caching;

namespace Component.Tools.Cache
{
    /// <summary>
    ///     用监控文件变化来更新缓存的方式
    /// </summary>
    public class GlobalCacheWrapper : CacheWrapper
    {
        private readonly string _filePath = Path.Combine(PublicFunc.GetCurrentDirectory(), "cache.txt");

        protected override ChangeMonitor Monitor
        {
            get { return new HostFileChangeMonitor(new List<string> { _filePath }); }
        }

        /// <summary>
        ///     缓存时间，默认1天
        /// </summary>
        protected override DateTimeOffset Expiration
        {
            get { return DateTimeOffset.Now.AddDays(1); }
        }
    }
}