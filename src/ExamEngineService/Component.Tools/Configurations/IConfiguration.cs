// 源文件头信息：
// 文 件 名：IConfiguration.cs
// 接 口 名：IConfiguration
// 所属工程：Component.Tools
// 最后修改：游凯
// 最后修改：2013-09-29 01:59:44

using System.Collections.Generic;

namespace Component.Tools.Configurations
{
    public interface IConfiguration<TConfig> where TConfig : ConfigBase
    {
        TConfig GetByKey(string key);

        List<TConfig> GetAll();
    }
}