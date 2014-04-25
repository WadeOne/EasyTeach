using System.Web.Http;
using System.Web.Http.Results;
using EasyTeach.Web.Results;

namespace EasyTeach.Web.Controllers
{
    public abstract class ApiControllerBase : ApiController
    {
        protected override OkResult Ok()
        {
            return new EmptyObjectOkResult(this);
        }
    }
}