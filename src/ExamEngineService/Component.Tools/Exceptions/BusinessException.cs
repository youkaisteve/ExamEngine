// 源文件头信息：
// 文 件 名：BusinessException.cs
// 类    名：BusinessException
// 所属工程：Component.Tools
// 最后修改：游凯
// 最后修改：2013-09-23 01:55:54

using System;
using System.Runtime.Serialization;

namespace Component.Tools.Exceptions
{
    public class BusinessException : ApplicationException
    {
        public BusinessException()
        {
        }

        public BusinessException(string message)
            : base(message)
        {
        }

        public BusinessException(string message, Exception exception)
            : base(message, exception)
        {
        }

        protected BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}