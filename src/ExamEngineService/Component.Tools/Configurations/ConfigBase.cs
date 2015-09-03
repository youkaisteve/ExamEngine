using System.Xml.Serialization;

namespace Component.Tools.Configurations
{
    public class ConfigBase
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
    }
}