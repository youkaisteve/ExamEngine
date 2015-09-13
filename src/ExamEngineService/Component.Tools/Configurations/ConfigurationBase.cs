using System.Collections.Generic;
using System.IO;
using System.Linq;
using Component.Tools.Cache;

namespace Component.Tools.Configurations
{
    public abstract class ConfigurationBase<TConfig> : IConfiguration<TConfig> where TConfig : ConfigBase
    {
        protected const string BasePath = "Configurations";
        protected CacheWrapper CacheWrapper;
        protected List<TConfig> _Configurations;
        private FileSystemWatcher watcher;
        protected string BaseFolder = Path.Combine(PublicFunc.GetCurrentDirectory(), BasePath);

        protected ConfigurationBase()
        {
            Init();
            watcher = new FileSystemWatcher(
                Path.Combine(BaseFolder, ConfigPath), "*.*");
            watcher.Changed += watcher_Changed;
            watcher.EnableRaisingEvents = true;
        }

        void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Renamed)
            {
                return;
            }
            Init();
        }

        /// <summary>
        ///     获取配置文件所在目录
        /// </summary>
        protected abstract string ConfigPath { get; }

        protected abstract void Init();

        public TConfig GetByKey(string key)
        {
            return _Configurations.FirstOrDefault(m => m.Name == key);
        }

        public List<TConfig> GetAll()
        {
            return _Configurations;
        }
    }
}