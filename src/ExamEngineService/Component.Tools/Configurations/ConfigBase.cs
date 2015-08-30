// 源文件头信息：
// 文 件 名：ConfigBase.cs
// 类    名：ConfigBase
// 所属工程：Component.Tools
// 最后修改：游凯
// 最后修改：2013-10-17 04:06:47

using System.Xml.Serialization;

namespace Component.Tools.Configurations
{
    public class ConfigBase
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
    }
}