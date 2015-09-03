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
        public IHttpActionResult Welcome()
        {
            return Ok(new ApiResponse {Code = 0, Message = "Test"});
        }

        [HttpPost]
        [BaseAuthoriize]
        public ApiResponse Handler(ApiRequestData data)
        {
            return ApiOK();
        }
    }
}