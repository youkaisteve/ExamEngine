using System;
using System.Web.Http;
using Exam.Api.Filters;
using Exam.Api.Framework;
using Exam.Api.Models;

namespace Exam.Api.Controllers
{
    public class HomeController : BaseApiController
    {
        [HttpGet]
        public ApiResponse Welcome()
        {
            return ApiOk("Welcome to exam");
        }

        [HttpPost]
        //[BaseAuthoriize]
        public ApiResponse Handler(ApiRequestData data)
        {
            return ApiOk();
        }
    }
}