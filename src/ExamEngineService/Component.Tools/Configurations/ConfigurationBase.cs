using System.Collections.Generic;
using System.Linq;

namespace Component.Tools.Configurations
{
    public abstract class ConfigurationBase<TConfig> : IConfiguration<TConfig> where TConfig : ConfigBase
    {
        protected const string BasePath = "Configurations";

        protected ConfigurationBase()
        {
            Init();
        }

        /// <summary>
        ///     获取配置文件所在目录
        /// </summary>
        protected abstract string ConfigPath { get; }

        /// <summary>
        ///     获取和设置承载配置数据的实体
        /// </summary>
        protected abstract List<TConfig> Configurations { get; set; }

        protected abstract void Init();

        public TConfig GetByKey(string key)
        {
            return Configurations.FirstOrDefault(m => m.Name == key); 
        }

        public List<TConfig> GetAll()
        {
            return Configurations;
        }
    }
}