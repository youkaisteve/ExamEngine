using System.Web.Http;
using Exam.Api.Filters;

namespace Exam.Api.Controllers
{
    [BaseAuthoriize]
    public class HomeController : BaseApiController
    {
        [HttpGet]
        public IHttpActionResult Index()
        {
            return Ok("It's OK Now!");
        }
    }
}
