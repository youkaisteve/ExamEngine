using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using Component.Tools.Exceptions;
using Exam.Api.Framework;
using Newtonsoft.Json;

namespace Exam.Api.Filters
{
    public class BaseExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            ApiResponse response = new ApiResponse();
            if (actionExecutedContext.Exception is BusinessException)
            {
                response.Code = 1;
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