using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Component.Tools.Exceptions;
using Exam.Api.Framework;

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
            }
            else
            {
                response.Code = 2;
            }
            response.ErrorMessage = actionExecutedContext.Exception.Message;
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}