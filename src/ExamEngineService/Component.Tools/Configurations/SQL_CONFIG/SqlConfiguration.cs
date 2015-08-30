// 源文件头信息：
// 文 件 名：SqlConfiguration.cs
// 类    名：SqlConfiguration
// 所属工程：Component.Tools
// 最后修改：游凯
// 最后修改：2013-09-29 01:52:22

using System.Xml.Serialization;

namespace Component.Tools.Configurations
{
    [XmlRoot("SqlConfig")]
    public class SqlConfiguration
    {
        [XmlArray("Commands")]
        [XmlArrayItem("Command")]
        public Command[] Commands { get; set; }
    }

    public class Command : ConfigBase
    {
        [XmlElement("CommandText")]
        public string CommandText { get; set; }
    }
}