// 源文件头信息：
// 文 件 名：ConfigurationException.cs
// 类    名：ConfigurationException
// 所属工程：Component.Tools
// 最后修改：游凯
// 最后修改：2013-09-23 01:56:25

using System;
using System.Runtime.Serialization;

namespace Component.Tools.Exceptions
{
    public class ConfigurationException : ApplicationException
    {
        public ConfigurationException()
        {
        }

        public ConfigurationException(string message)
            : base(message)
        {
        }

        public ConfigurationException(string message, Exception exception)
            : base(message, exception)
        {
        }

        protected ConfigurationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}