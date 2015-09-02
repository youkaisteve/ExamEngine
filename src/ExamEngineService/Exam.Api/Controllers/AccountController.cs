using System.Web.Http;
namespace Exam.Api.Controllers
{
    public class AccountController : BaseApiController
    {
        [HttpPost]
        public IHttpActionResult Login(string user, string pw)
        {
            return Ok();
        }
    }
}