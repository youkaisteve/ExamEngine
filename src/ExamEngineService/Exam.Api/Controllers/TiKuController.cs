using System.ComponentModel.Composition;
using Exam.Api.Framework;

namespace Exam.Api.Controllers
{
    [Export(typeof(TiKuController))]
    public class TiKuController : BaseApiController
    {
    }
}