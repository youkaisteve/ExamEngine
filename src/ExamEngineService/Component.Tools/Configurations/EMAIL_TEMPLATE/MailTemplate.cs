// 源文件头信息：
// 文 件 名：MailTemplate.cs
// 类    名：MailTemplate
// 所属工程：Component.Tools
// 最后修改：游凯
// 最后修改：2013-11-21 09:48:57

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

    public enum EMailTemplateType
    {
        [Description("为他人报障")]
        RequestForOther,
        [Description("派单通知处理人")]
        Assign,
        [Description("换人通知新处理人")]
        Change_NewOne,
        [Description("换人通知原处理人")]
        Change_OldOne,
        [Description("评论为未解决")]
        Comment_NotDone,
        [Description("评论为不满意")]
        Comment_NotSatisfy,
        [Description("完成通知报障人")]
        Finish
    }
}