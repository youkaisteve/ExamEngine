using System.ComponentModel;
using System.Xml.Serialization;

namespace Component.Tools.Configurations.EMAIL_TEMPLATE
{
    [XmlRoot("MailTemplate")]
    public class MailTemplate
    {
        [XmlArray("Emails")]
        [XmlArrayItem("Email")]
        public Email[] Emails { get; set; }
    }

    public class Email : ConfigBase
    {
        [XmlElement("Type")]
        public string Type { get; set; }

        [XmlElement("Subject")]
        public string Subject { get; set; }

        [XmlElement("Content")]
        public string Content { get; set; }
    }
}