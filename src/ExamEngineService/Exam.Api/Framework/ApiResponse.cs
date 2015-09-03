using System.Web.Http;

namespace Exam.Api.Framework
{
    public class ApiResponse
    {
        /// <summary>
        ///     接口响应状态代码
        ///     0-成功
        ///     1-业务异常
        ///     2-系统异常
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        ///     接口响应信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     接口错误信息
        /// </summary>
        public object ErrorMessage { get; set; }

        /// <summary>
        ///     接口响应的返回数据
        /// </summary>
        public object Data { get; set; }
    }
}