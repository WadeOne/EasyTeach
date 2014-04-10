using System.Web.Mvc;

namespace EasyTeach.Web.Controllers
{
    public class UserManagementController : Controller
    {
        //
        // GET: /UserManagement/
        public ActionResult Registration()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
	}
}