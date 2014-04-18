using System.Web.Mvc;

namespace EasyTeach.Web.Controllers
{
    public sealed class HomeController : Controller
    {
        public ActionResult Index()
        {
            return File("~/index.html", "text/html");
        }
    }
}