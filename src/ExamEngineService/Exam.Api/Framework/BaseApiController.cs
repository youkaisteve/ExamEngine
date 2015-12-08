using System.Web.Http;
using System.Linq;

using Exam.Api.Filters;
using Exam.Model;

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

        public ModelBase GetModelBase()
        {
            var headers = ActionContext.Request.Content.Headers;
            var baseModel = new ModelBase();
            baseModel.User = new UserInfo
                {
                    UserID = headers.GetValues("UserID").FirstOrDefault(),
                    UserName = headers.GetValues("UserName").FirstOrDefault(),
                    UserSysNo = int.Parse(headers.GetValues("UserSysNo").FirstOrDefault())
                };

            return baseModel;
        }
    }
}