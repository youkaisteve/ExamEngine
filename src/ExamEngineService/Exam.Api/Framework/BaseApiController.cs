using System.Web.Http;

namespace Exam.Api.Framework
{
    public class BaseApiController : ApiController
    {
        protected ApiResponse ApiOK(object data)
        {
            return new ApiResponse
            {
                Code = 0,
                Data = data
            };
        }

        protected ApiResponse ApiOK()
        {
            return new ApiResponse
            {
                Code = 0
            };
        }
    }
}