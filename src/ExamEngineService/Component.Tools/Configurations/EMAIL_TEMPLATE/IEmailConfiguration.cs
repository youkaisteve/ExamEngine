// 源文件头信息：
// 文 件 名：IEmailConfiguration.cs
// 接 口 名：IEmailConfiguration
// 所属工程：Component.Tools
// 最后修改：游凯
// 最后修改：2013-11-21 09:48:07

namespace Component.Tools.Configurations.EMAIL_TEMPLATE
{
    public interface IEmailConfiguration
    {
        Email GetMail(string mailType);
    }
}