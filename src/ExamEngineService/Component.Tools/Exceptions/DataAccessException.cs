// 源文件头信息：
// 文 件 名：DataAccessException.cs
// 类    名：DataAccessException
// 所属工程：Component.Tools
// 最后修改：游凯
// 最后修改：2013-09-24 11:26:13

using System;
using System.Runtime.Serialization;

namespace Component.Tools.Exceptions
{
    public class DataAccessException : Exception
    {
        public DataAccessException()
        {
        }

        public DataAccessException(string message)
            : base(message)
        {
        }

        public DataAccessException(string message, Exception exception)
            : base(message, exception)
        {
        }

        protected DataAccessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}