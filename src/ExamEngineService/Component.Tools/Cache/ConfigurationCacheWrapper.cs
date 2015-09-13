using System;
using System.IO;
using System.Runtime.Caching;

namespace Component.Tools.Cache
{
    public class ConfigurationCacheWrapper : CacheWrapper
    {
        private readonly string _configPath =
            Path.Combine(PublicFunc.GetCurrentDirectory(), PublicFunc.GetConfigByKey_AppSettings("Config_Path"));

        protected override ChangeMonitor Monitor
        {
            get
            {
                string[] files = Directory.GetFiles(_configPath, "*.*", SearchOption.AllDirectories);
                return new HostFileChangeMonitor(files);
            }
        }

        /// <summary>
        ///     缓存时间，默认1天
        /// </summary>
        protected override DateTimeOffset Expiration
        {
            get { return DateTimeOffset.Now.AddMonths(1); }
        }
    }
}