using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Component.Tools.Exceptions;
using Exam.Api.Framework;
using Component.Tools;

namespace Exam.Api.Filters
{
    public class BaseExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var response = new ApiResponse();
            if (actionExecutedContext.Exception is BusinessException)
            {
                response.Code = (actionExecutedContext.Exception as BusinessException).ExceptionCode;
                response.ErrorMessage = actionExecutedContext.Exception.Message;
            }
            else
            {
                response.Code = 2;
                response.ErrorMessage = "系统错误，请联系管理员";
                var message = actionExecutedContext.Exception.Message + System.Environment.NewLine + actionExecutedContext.Exception.StackTrace;
                LogHelper.Instanse.WriteError(message);
            }
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}