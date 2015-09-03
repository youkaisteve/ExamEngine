using System.Web.Http;
using Exam.Api.Filters;

namespace Exam.Api.Framework
{
    [BaseExceptionFilter]
    public class BaseApiController : ApiController
    {
        protected ApiResponse ApiOk(object data)
        {
            return new ApiResponse
            {
                Code = 0,
                Data = data
            };
        }

        protected ApiResponse ApiOk()
        {
            return new ApiResponse
            {
                Code = 0
            };
        }
    }
}