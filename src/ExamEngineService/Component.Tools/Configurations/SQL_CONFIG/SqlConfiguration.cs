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