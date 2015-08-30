// 源文件头信息：
// 文 件 名：ISqlConfiguration.cs
// 接 口 名：ISqlConfiguration
// 所属工程：Component.Tools
// 最后修改：游凯
// 最后修改：2013-10-17 04:05:23

namespace Component.Tools.Configurations
{
    public interface ISqlConfiguration : IConfiguration<Command>
    {
        string GetCommandTextByKey(string key);
    }
}