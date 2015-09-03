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