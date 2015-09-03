using System.Collections.Generic;
using System.Linq;

namespace Exam.Api.Framework
{
    public enum DictSystemErrorType
    {
        /// <summary>
        ///     系统错误
        /// </summary>
        SystemError = 1,

        /// <summary>
        ///     系统异常
        /// </summary>
        SystemException = 2,

        /// <summary>
        ///     文件未找到
        /// </summary>
        FileNotFound = 3,

        /// <summary>
        ///     404错误
        /// </summary>
        Http404Error = 404,

        /// <summary>
        ///     500错误
        /// </summary>
        Http500Error = 500,

        FileExportError = 1001
    }

    public static class SystemErrorCollection
    {
        private static readonly IDictionary<int, string> SystemMsg = new Dictionary<int, string>
        {
            {1, "系统错误，请联系管理员！"},
            {2, "系统出现异常，请联系管理员！"},
            {3, "文件不存在，请联系管理员！"},
            {404, "404错误，页面未找到!"},
            {500, "500错误，内部服务器错误!"},
            {1001, "导出的数据不存在，请联系管理员！"},
        };

        /// <summary>
        ///     获取错误提示
        /// </summary>
        /// <param name="errCode"></param>
        /// <returns></returns>
        public static string GetSystemErrorMsg(int errCode)
        {
            return SystemMsg.SingleOrDefault(p => p.Key == errCode).Value;
        }
    }
}