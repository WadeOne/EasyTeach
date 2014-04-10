using System.Web.Mvc;

namespace EasyTeach.Web.Controllers
{
    public class HomeController : Controller
    {
        [System.Web.Http.Authorize]
        public ActionResult Index()
        {
            return View();
        }
	}
}