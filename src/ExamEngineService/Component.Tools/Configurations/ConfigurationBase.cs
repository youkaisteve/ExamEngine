// 源文件头信息：
// 文 件 名：ConfigurationBase.cs
// 类    名：ConfigurationBase
// 所属工程：Component.Tools
// 最后修改：游凯
// 最后修改：2013-10-03 04:03:09

using System.Collections.Generic;

namespace Component.Tools.Configurations
{
    public abstract class ConfigurationBase<TConfig> where TConfig : ConfigBase
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
    }
}