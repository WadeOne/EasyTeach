using System.Web.Mvc;

namespace EasyTeach.Web.Controllers
{
    public sealed class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return File("~/index.html", "text/html");
        }
    }
}